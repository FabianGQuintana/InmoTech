using InmoTech.Data.Repositories;
using InmoTech.Forms;
using InmoTech.Models;
using InmoTech.Repositories;
using InmoTech.Security;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    /// <summary>
    /// Control de usuario para administrar contratos de alquiler: búsqueda, alta, y cambio de estado.
    /// Presenta una grilla con contratos y un formulario para registrar nuevos.
    /// </summary>
    public partial class UcContratos : UserControl
    {
        // ======================================================
        //  REGIÓN: Campos y Propiedades Privadas
        // ======================================================
        #region Campos y Propiedades Privadas
        private readonly BindingSource _bs = new BindingSource();
        private readonly ContratoRepository _repo = new ContratoRepository();
        private readonly InquilinoRepository _repoInquilino = new InquilinoRepository();
        private readonly InmuebleRepository _repoInmueble = new InmuebleRepository();

        private int? _selPersonaId = null;    // FK persona (Inquilino)
        private int? _selInmuebleId = null;   // FK inmueble
        #endregion

        // ======================================================
        //  REGIÓN: Constructor
        // ======================================================
        #region Constructor
        /// <summary>
        /// Inicializa el control, configura el origen de datos, columnas de la grilla y registra los manejadores de eventos.
        /// </summary>
        public UcContratos()
        {
            InitializeComponent();

            _bs.DataSource = new BindingList<Contrato>();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = _bs;

            // (Opcional) si querés reforzar desde la UI:
            // try { nudMonto.Minimum = 0.01m; } catch {}

            // Handlers de inicialización
            Load += UcContratos_Load;

            // Handlers de la grilla
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            // Handlers de la UI (Formulario)
            btnBuscarInmueble.Click += BtnBuscarInmueble_Click;
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += (s, e) => LimpiarFormulario();
            BEstado.Click += BEstado_Click;
        }
        #endregion

        // ======================================================
        //  REGIÓN: Metodos de Inicialización y Actualización de Grilla
        // ======================================================
        #region Métodos de Inicialización y Actualización de Grilla
        /// <summary>
        /// Evento Load del control. Dispara la carga inicial de la grilla.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Argumentos de evento.</param>
        private void UcContratos_Load(object? sender, EventArgs e) => RefrescarGrilla();

        /// <summary>
        /// Obtiene los contratos (filtrados por DNI de usuario autenticado si aplica), 
        /// refresca el BindingList y aplica formatos de columnas en la grilla.
        /// </summary>
        private void RefrescarGrilla()
        {
            int? dniFiltro = AuthService.IsAuthenticated ? AuthService.CurrentUser!.Dni : (int?)null;
            var contratos = _repo.ObtenerContratos(dniFiltro);

            var list = (BindingList<Contrato>)_bs.DataSource;
            list.Clear();
            foreach (var c in contratos) list.Add(c);

            // Formatos
            dataGridView1.Columns["colInicio"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns["colFin"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns["colMonto"].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns["colFechaCreacion"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            // Forzar actualización del estado del botón después de refrescar
            DataGridView1_SelectionChanged(this, EventArgs.Empty);
        }
        #endregion

        // ======================================================
        //  REGIÓN:Manejadores de Eventos de la Grilla (DataGridView)
        // ======================================================
        #region Manejadores de Eventos de la Grilla (DataGridView)

        /// <summary>
        /// Habilita/deshabilita el botón de estado según la selección y actualiza su texto a "Anular" o "Restaurar".
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Argumentos de evento.</param>
        private void DataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                BEstado.Enabled = true;
                var contrato = (Contrato)dataGridView1.SelectedRows[0].DataBoundItem;
                BEstado.Text = contrato.Estado ? "Anular" : "Restaurar";
            }
            else
            {
                BEstado.Enabled = false;
                BEstado.Text = "Anular";
            }
        }

        /// <summary>
        /// Aplica formato personalizado a celdas: en la columna "colEstado" muestra "Activo"/"Inactivo".
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Argumentos con datos de formato de celda.</param>
        private void DataGridView1_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "colEstado")
            {
                var contrato = (Contrato)_bs[e.RowIndex];
                e.Value = contrato.Estado ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Manejadores de Eventos del Formulario (Botones)
        // ======================================================
        #region Manejadores de Eventos del Formulario (Botones)

        /// <summary>
        /// Alterna el estado del contrato seleccionado: Anula (desactiva) o Restaura (activa).
        /// Si se anula y no quedan contratos activos del inmueble, lo marca como "Disponible".
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Argumentos de evento.</param>
        private void BEstado_Click(object? sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            var contrato = (Contrato)dataGridView1.SelectedRows[0].DataBoundItem;
            var id = contrato.IdContrato;

            if (contrato.Estado)
            {
                var confirm = MessageBox.Show($"¿Está seguro que desea anular el contrato N°{id}?",
                    "Confirmar Anulación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    // 1) Anular contrato
                    _repo.ActualizarEstado(id, false);

                    // 2) Si no quedan contratos activos para ese inmueble, ponerlo "Disponible"
                    var idInmueble = contrato.IdInmueble;
                    bool quedanActivos = _repo.ExisteContratoActivoPorInmueble(idInmueble); // ya lo usás en otro control
                    if (!quedanActivos)
                    {
                        _repoInmueble.ActualizarCondicion(idInmueble, "Disponible");
                    }

                    MessageBox.Show("El contrato ha sido anulado.", "Operación Exitosa",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                _repo.ActualizarEstado(id, true);
                MessageBox.Show("El contrato ha sido restaurado.", "Operación Exitosa",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // (Opcional) Si querés, al restaurar podrías marcar el inmueble como "Ocupado":
                // _repoInmueble.ActualizarCondicion(contrato.IdInmueble, "Ocupado");
            }

            RefrescarGrilla();
        }

        /// <summary>
        /// Abre el cuadro de diálogo de búsqueda de inmueble y, si hay selección, guarda el Id y muestra un texto descriptivo.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Argumentos de evento.</param>
        private void BtnBuscarInmueble_Click(object? sender, EventArgs e)
        {
            using var dlg = new FrmBuscarInmueble();
            if (dlg.ShowDialog(this) == DialogResult.OK && dlg.Seleccionado != null)
            {
                _selInmuebleId = dlg.Seleccionado.IdInmueble;
                txtInmueble.Text = $"{dlg.Seleccionado.Direccion} ({dlg.Seleccionado.Tipo})";
            }
        }

        /// <summary>
        /// Abre el cuadro de diálogo de búsqueda de inquilino, resuelve el Id de persona por DNI y lo asocia al formulario.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Argumentos de evento.</param>
        private void btnBuscarInquilino_Click(object sender, EventArgs e)
        {
            using var dlg = new FrmBuscarInquilino();
            if (dlg.ShowDialog(this) == DialogResult.OK && dlg.Seleccionado != null)
            {
                _selPersonaId = _repoInquilino.ObtenerIdPersonaPorDni(dlg.Seleccionado.Dni);
                if (_selPersonaId == null)
                {
                    MessageBox.Show("No se encontró el inquilino en la tabla persona.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                txtInquilino.Text = $"{dlg.Seleccionado.Apellido} {dlg.Seleccionado.Nombre}";
            }
        }

        /// <summary>
        /// Valida los datos del formulario y registra un nuevo contrato.
        /// Requiere inmueble, inquilino, sesión iniciada y monto &gt; 0. 
        /// Si inserta, refresca la grilla y limpia el formulario.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Argumentos de evento.</param>
        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (_selInmuebleId is null)
            {
                MessageBox.Show("Seleccioná un inmueble antes de guardar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_selPersonaId is null)
            {
                MessageBox.Show("Seleccioná un inquilino válido antes de guardar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!AuthService.IsAuthenticated)
            {
                MessageBox.Show("Debés iniciar sesión para registrar contratos.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // ✅ Validación solicitada: monto > 0
            if (nudMonto.Value <= 0)
            {
                MessageBox.Show("El monto del alquiler debe ser mayor a 0.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudMonto.Focus();
                return;
            }

            var nuevo = new Contrato
            {
                FechaInicio = dtpInicio.Value.Date,
                FechaFin = dtpFin.Value.Date,
                Monto = nudMonto.Value,
                IdInmueble = _selInmuebleId.Value,
                IdPersona = _selPersonaId.Value,
                FechaCreacion = DateTime.Now,
                DniUsuario = AuthService.CurrentUser!.Dni,
                Estado = chkActivo.Checked
            };

            try
            {
                var filas = _repo.AgregarContrato(nuevo);
                if (filas > 0)
                {
                    MessageBox.Show("Contrato guardado correctamente. El inmueble fue marcado como 'Ocupado'.", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefrescarGrilla();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo guardar el contrato:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Auxiliares
        // ======================================================
        #region Métodos Auxiliares
        /// <summary>
        /// Restablece el formulario a su estado inicial:
        /// limpia IDs seleccionados, textos, fechas, monto y estado activo.
        /// </summary>
        private void LimpiarFormulario()
        {
            _selPersonaId = null;
            txtInquilino.Text = "...";
            _selInmuebleId = null;
            txtInmueble.Text = "...";
            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;
            nudMonto.Value = 0;
            chkActivo.Checked = true;
        }
        #endregion
    }
}
