using System;
using System.Windows.Forms;
using InmoTech.Data.Repositories;

namespace InmoTech.Forms
{
    public partial class FrmBuscarInmueble : Form
    {
        private readonly InmuebleRepository _repo = new InmuebleRepository();
        private int _page = 1, _pageSize = 20, _total = 0;

        /// <summary> Resultado seleccionado por el usuario. </summary>
        public InmuebleLite? Seleccionado { get; private set; }

        public FrmBuscarInmueble()
        {
            InitializeComponent();
            WireEvents();
            Cargar();
        }

        private void WireEvents()
        {
            // búsqueda reactiva con timer
            txtBuscar.TextChanged += (s, e) => { timerBuscar.Stop(); timerBuscar.Start(); };
            timerBuscar.Tick += (s, e) => { timerBuscar.Stop(); _page = 1; Cargar(); };

            // cambio de filtro
            cboEstado.SelectedIndexChanged += (s, e) => { _page = 1; Cargar(); };

            // navegación por páginas
            btnAnterior.Click += (s, e) => { if (_page > 1) { _page--; Cargar(); } };
            btnSiguiente.Click += (s, e) => { if (_page * _pageSize < _total) { _page++; Cargar(); } };

            // selección
            btnElegir.Click += (s, e) => ElegirActual();
            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            dgv.CellDoubleClick += (s, e) => ElegirActual();
            dgv.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.Handled = true; ElegirActual(); }
            };

            // formateo booleanos → texto
            dgv.CellFormatting += Dgv_CellFormatting;
        }

        private bool? EstadoSeleccionado()
        {
            return cboEstado.SelectedIndex switch
            {
                0 => (bool?)null, // Todos
                1 => true,        // Activos
                2 => false,       // Inactivos
                _ => null
            };
        }

        private void Cargar()
        {
            var (items, total) = _repo.BuscarPaginado(
                txtBuscar.Text?.Trim() ?? "",
                EstadoSeleccionado(),
                _page,
                _pageSize
            );

            _total = total;
            dgv.DataSource = items;

            var desde = (_page - 1) * _pageSize + 1;
            var hasta = Math.Min(_page * _pageSize, _total);
            if (_total == 0) { desde = 0; hasta = 0; }

            lblInfo.Text = $"Mostrando {desde}-{hasta} de {_total}";
            btnAnterior.Enabled = _page > 1;
            btnSiguiente.Enabled = _page * _pageSize < _total;
        }

        private void Dgv_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var prop = dgv.Columns[e.ColumnIndex].DataPropertyName;

            if (prop == "Amueblado" && e.Value is bool am)
            {
                e.Value = am ? "Sí" : "No";
                e.FormattingApplied = true;
            }
            else if (prop == "Estado" && e.Value is bool ac)
            {
                e.Value = ac ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
        }

        private void ElegirActual()
        {
            if (dgv.CurrentRow?.DataBoundItem is InmuebleLite it)
            {
                Seleccionado = it;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
