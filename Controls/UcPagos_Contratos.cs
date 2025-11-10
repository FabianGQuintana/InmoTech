using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using InmoTech.Models;
using InmoTech.Repositories;

namespace InmoTech.Controls
{
    /// <summary>
    /// Control para listar, filtrar, paginar y seleccionar contratos.
    /// Pensado para usarse desde el flujo de Pagos, exponiendo un evento de contrato elegido.
    /// </summary>
    public partial class UcPagos_Contratos : UserControl
    {
        // ======================================================
        //  REGIÓN: Campos y Propiedades
        // ======================================================
        #region Campos y Propiedades
        private readonly ContratoRepository _repo = new ContratoRepository();
        private readonly BindingList<Contrato> _pageBinding = new BindingList<Contrato>();
        private List<Contrato> _contratos = new();
        private List<Contrato> _filtrados = new();

        private int _page = 1;
        private int _pageSize = 20;

        /// <summary>
        /// Filtro opcional por DNI del usuario (operador/propietario) que limita los contratos obtenidos del repositorio.
        /// </summary>
        public int? DniUsuarioFiltro { get; set; } = null;

        /// <summary>
        /// Contrato actualmente seleccionado en la grilla, o <c>null</c> si no hay selección.
        /// </summary>
        public Contrato? SelectedContrato => dgv.CurrentRow?.DataBoundItem as Contrato;
        #endregion

        // ======================================================
        //  REGIÓN: Eventos Públicos
        // ======================================================
        #region Eventos Públicos
        /// <summary>
        /// Se dispara al confirmar una selección (botón, doble clic o Enter) con el contrato elegido.
        /// </summary>
        public event EventHandler<ContratoSelectedEventArgs>? ContratoElegido;

        /// <summary>
        /// Se dispara al cancelar la selección de contratos (botón Cancelar).
        /// </summary>
        public event EventHandler? Cancelado;
        #endregion

        // ======================================================
        //  REGIÓN: Constructor
        // ======================================================
        #region Constructor
        /// <summary>
        /// Inicializa componentes, configura valores por defecto y carga contratos iniciales.
        /// </summary>
        public UcPagos_Contratos()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                // Asegurar “Activos” por defecto
                if (cboEstado.Items.Count > 1 && cboEstado.SelectedIndex < 0)
                    cboEstado.SelectedIndex = 1;

                WireEvents();
                CargarContratos();
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Configuración de Eventos (WireEvents)
        // ======================================================
        #region Configuración de Eventos (WireEvents)
        /// <summary>
        /// Conecta los manejadores de eventos de UI (búsqueda con debounce, filtros, paginación y selección).
        /// </summary>
        private void WireEvents()
        {
            txtBuscar.TextChanged += (s, e) => { timerBuscar.Stop(); timerBuscar.Start(); };
            timerBuscar.Tick += (s, e) =>
            {
                timerBuscar.Stop();
                _page = 1;
                AplicarFiltrosYBind();
            };

            cboEstado.SelectedIndexChanged += (s, e) =>
            {
                _page = 1;
                AplicarFiltrosYBind();
            };

            btnAnterior.Click += (s, e) =>
            {
                if (_page > 1) { _page--; BindPagina(); }
            };
            btnSiguiente.Click += (s, e) =>
            {
                if (_page * _pageSize < _filtrados.Count) { _page++; BindPagina(); }
            };

            btnElegir.Click += (s, e) => ElegirActual();
            btnCancelar.Click += (s, e) => Cancelado?.Invoke(this, EventArgs.Empty);
            dgv.CellDoubleClick += (s, e) => ElegirActual();
            dgv.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.Handled = true; ElegirActual(); }
            };

            dgv.CellFormatting += Dgv_CellFormatting;
            dgv.DataError += (s, e) => { e.ThrowException = false; };
        }
        #endregion

        // ======================================================
        //  REGIÓN: Metodos de Carga y Filtrado
        // ======================================================
        #region Métodos de Carga y Filtrado
        /// <summary>
        /// Obtiene los contratos desde el repositorio aplicando <see cref="DniUsuarioFiltro"/> si corresponde,
        /// y luego prepara la grilla con filtros y paginación.
        /// </summary>
        public void CargarContratos()
        {
            try
            {
                _contratos = _repo.ObtenerContratos(DniUsuarioFiltro);
                _page = 1;
                AplicarFiltrosYBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar contratos:\n{ex.Message}",
                    "InmoTech", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Aplica los filtros de texto y estado a la colección en memoria, ordena por fecha de inicio descendente
        /// y actualiza la página visible.
        /// </summary>
        private void AplicarFiltrosYBind()
        {
            string q = (txtBuscar.Text ?? "").Trim().ToLowerInvariant();
            bool? est = EstadoSeleccionado();

            _filtrados = _contratos
                .Where(c =>
                    (est is null || c.Estado == est.Value) &&
                    (string.IsNullOrEmpty(q) ||
                     c.IdContrato.ToString().Contains(q) ||
                     (c.NombreInquilino ?? "").ToLower().Contains(q) ||
                     (c.DireccionInmueble ?? "").ToLower().Contains(q) ||
                     (c.NombreUsuario ?? "").ToLower().Contains(q)))
                .OrderByDescending(c => c.FechaInicio)
                .ToList();

            BindPagina();
        }

        /// <summary>
        /// Calcula el segmento de elementos a mostrar según la página y el tamaño de página,
        /// refresca el <see cref="_pageBinding"/> y actualiza la grilla y los botones de navegación.
        /// </summary>
        private void BindPagina()
        {
            _page = Math.Max(1, _page);
            int total = _filtrados.Count;
            int skip = (_page - 1) * _pageSize;
            var pageItems = _filtrados.Skip(skip).Take(_pageSize).ToList();

            _pageBinding.Clear();
            foreach (var it in pageItems) _pageBinding.Add(it);

            dgv.DataSource = _pageBinding;

            int desde = total == 0 ? 0 : skip + 1;
            int hasta = Math.Min(skip + _pageSize, total);
            lblInfo.Text = $"Mostrando {desde}-{hasta} de {total}";

            btnAnterior.Enabled = _page > 1;
            btnSiguiente.Enabled = (_page * _pageSize) < total;
        }

        /// <summary>
        /// Interpreta el estado seleccionado en el combo.
        /// 0 = Todos, 1 = Activos, 2 = Inactivos.
        /// </summary>
        /// <returns>
        /// <c>null</c> si no se filtra por estado; <c>true</c> para activos; <c>false</c> para inactivos.
        /// </returns>
        private bool? EstadoSeleccionado() =>
            cboEstado.SelectedIndex switch
            {
                0 => (bool?)null,
                1 => true,
                2 => false,
                _ => null
            };

        /// <summary>
        /// Dispara el evento <see cref="ContratoElegido"/> con el contrato actualmente seleccionado, si existe.
        /// </summary>
        private void ElegirActual()
        {
            if (SelectedContrato is null) return;
            ContratoElegido?.Invoke(this, new ContratoSelectedEventArgs(SelectedContrato));
        }
        #endregion

        // ======================================================
        //  REGIÓN: Formato de Grilla
        // ======================================================
        #region Formato de Grilla
        /// <summary>
        /// Da formato a las celdas de fecha, monto y estado: fechas dd/MM/yyyy, montos con dos decimales y estado legible.
        /// </summary>
        private void Dgv_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgv.Columns[e.ColumnIndex] is not DataGridViewTextBoxColumn) return;

            var prop = dgv.Columns[e.ColumnIndex].DataPropertyName;

            if ((prop == "FechaInicio" || prop == "FechaFin") && e.Value is DateTime dt)
            {
                e.Value = dt.ToString("dd/MM/yyyy");
                e.FormattingApplied = true;
            }
            else if (prop == "Monto" && e.Value is decimal dec)
            {
                e.Value = dec.ToString("N2");
                e.FormattingApplied = true;
            }
            else if (prop == "Estado" && e.Value is bool b)
            {
                e.Value = b ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Metodos Públicos Auxiliares
        // ======================================================
        #region Métodos Públicos Auxiliares
        /// <summary>
        /// Cambia el tamaño de página respetando un mínimo de 5, reinicia a la página 1 y re-bindea.
        /// </summary>
        /// <param name="size">Cantidad de elementos por página.</param>
        public void SetPageSize(int size)
        {
            _pageSize = Math.Max(5, size);
            _page = 1;
            BindPagina();
        }

        /// <summary>
        /// Reaplica filtros vigentes y refresca el contenido mostrado.
        /// </summary>
        public void Refrescar() => AplicarFiltrosYBind();
        #endregion

        // ======================================================
        //  REGIÓN: Eventos de UI (Sin Lógica)
        // ======================================================
        #region Eventos de UI (Sin Lógica)
        /// <summary>
        /// Placeholder para dibujo personalizado del contenedor de filtros.
        /// </summary>
        private void tlpFiltros_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion
    }
    // ======================================================
    //  REGIÓN: Clases de Argumentos de Eventos
    // ======================================================
    #region Clases de Argumentos de Eventos
    /// <summary>
    /// Argumentos del evento que expone el contrato seleccionado por el usuario.
    /// </summary>
    public sealed class ContratoSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Contrato elegido por el usuario.
        /// </summary>
        public Contrato Contrato { get; }

        /// <summary>
        /// Crea una nueva instancia con el contrato elegido.
        /// </summary>
        /// <param name="c">Contrato seleccionado.</param>
        public ContratoSelectedEventArgs(Contrato c) => Contrato = c;
    }
    #endregion
}
