using System;
using System.ComponentModel;
using System.Windows.Forms;
using InmoTech.Forms;
using InmoTech.Models;
using InmoTech.Repositories;
using InmoTech.Security;

namespace InmoTech.Controls
{
    public partial class UcContratos : UserControl
    {
        // ======================================================
        //  REGIÓN: Campos y Propiedades Privadas
        // ======================================================
        #region Campos y Propiedades Privadas
        private readonly BindingSource _bs = new BindingSource();
        private readonly ContratoRepository _repo = new ContratoRepository();
        private readonly InquilinoRepository _repoInquilino = new InquilinoRepository();

        private int? _selPersonaId = null;    // FK persona (Inquilino)
        private int? _selInmuebleId = null;   // FK inmueble
        #endregion

        // ======================================================
        //  REGIÓN: Constructor
        // ======================================================
        #region Constructor
        public UcContratos()
        {
            InitializeComponent();

            _bs.DataSource = new BindingList<Contrato>();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = _bs;

            // Handlers de inicialización
            Load += UcContratos_Load;

            // Handlers de la grilla
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            // Handlers de la UI (Formulario)
            btnBuscarInmueble.Click += BtnBuscarInmueble_Click;
            // btnBuscarInquilino.Click += btnBuscarInquilino_Click; // LÍNEA ELIMINADA
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += (s, e) => LimpiarFormulario();
            BEstado.Click += BEstado_Click;
        }
        #endregion

        // ======================================================
        //  REGIÓN: Metodos de Inicialización y Actualización de Grilla
        // ======================================================
        #region Métodos de Inicialización y Actualización de Grilla
        private void UcContratos_Load(object? sender, EventArgs e) => RefrescarGrilla();

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

            // Se elimina el bucle que seteaba el texto del botón en la celda

            // Forzar actualización del estado del botón después de refrescar
            DataGridView1_SelectionChanged(this, EventArgs.Empty);
        }
        #endregion

        // ======================================================
        //  REGIÓN:Manejadores de Eventos de la Grilla (DataGridView)
        // ======================================================
        #region Manejadores de Eventos de la Grilla (DataGridView)

        // MÉTODO NUEVO: Se ejecuta cada vez que el usuario selecciona una fila diferente
        private void DataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            // Si hay al menos una fila seleccionada
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Habilitamos el botón
                BEstado.Enabled = true;
                // Obtenemos el contrato de la fila seleccionada
                var contrato = (Contrato)dataGridView1.SelectedRows[0].DataBoundItem;
                // Cambiamos el texto del botón según el estado del contrato
                BEstado.Text = contrato.Estado ? "Anular" : "Restaurar";
            }
            else
            {
                // Si no hay ninguna fila seleccionada, deshabilitamos el botón
                BEstado.Enabled = false;
                BEstado.Text = "Anular"; // Texto por defecto
            }
        }

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

        // MÉTODO ELIMINADO: DataGridView1_CellContentClick ya no se usa
        #endregion

        // ======================================================
        //  REGIÓN: Manejadores de Eventos del Formulario (Botones)
        // ======================================================
        #region Manejadores de Eventos del Formulario (Botones)

        // MÉTODO NUEVO: Lógica centralizada para anular o restaurar
        private void BEstado_Click(object? sender, EventArgs e)
        {
            // Verificamos si hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count == 0) return;

            // Obtenemos el contrato seleccionado
            var contrato = (Contrato)dataGridView1.SelectedRows[0].DataBoundItem;
            var id = contrato.IdContrato;

            // Si el contrato está Activo, queremos anularlo
            if (contrato.Estado)
            {
                var confirm = MessageBox.Show($"¿Está seguro que desea anular el contrato N°{id}?",
                    "Confirmar Anulación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    _repo.ActualizarEstado(id, false); // Damos de baja
                    MessageBox.Show("El contrato ha sido anulado.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            // Si el contrato está Inactivo, queremos restaurarlo
            else
            {
                _repo.ActualizarEstado(id, true); // Damos de alta
                MessageBox.Show("El contrato ha sido restaurado.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Refrescamos la grilla para que se vean los cambios
            RefrescarGrilla();
        }

        private void BtnBuscarInmueble_Click(object? sender, EventArgs e)
        {
            using var dlg = new FrmBuscarInmueble();
            if (dlg.ShowDialog(this) == DialogResult.OK && dlg.Seleccionado != null)
            {
                _selInmuebleId = dlg.Seleccionado.IdInmueble;
                txtInmueble.Text = $"{dlg.Seleccionado.Direccion} ({dlg.Seleccionado.Tipo})";
            }
        }

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

            var nuevo = new Contrato
            {
                FechaInicio = dtpInicio.Value.Date,
                FechaFin = dtpFin.Value.Date,
                Monto = nudMonto.Value,
                Condiciones = null,
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
                    MessageBox.Show("Contrato guardado correctamente.", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefrescarGrilla();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el contrato:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Auxiliares
        // ======================================================
        #region Métodos Auxiliares
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