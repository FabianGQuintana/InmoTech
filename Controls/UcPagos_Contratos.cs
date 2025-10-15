using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using InmoTech.Models;
using InmoTech.Repositories;

namespace InmoTech.Controls
{
    public partial class UcPagos_Contratos : UserControl
    {
        private readonly ContratoRepository _repo = new ContratoRepository();
        private readonly BindingList<Contrato> _pageBinding = new BindingList<Contrato>();
        private List<Contrato> _contratos = new();
        private List<Contrato> _filtrados = new();

        private int _page = 1;
        private int _pageSize = 20;

        public int? DniUsuarioFiltro { get; set; } = null;

        public Contrato? SelectedContrato => dgv.CurrentRow?.DataBoundItem as Contrato;

        public event EventHandler<ContratoSelectedEventArgs>? ContratoElegido;
        public event EventHandler? Cancelado;

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

        private bool? EstadoSeleccionado() =>
            cboEstado.SelectedIndex switch
            {
                0 => (bool?)null,
                1 => true,
                2 => false,
                _ => null
            };

        private void ElegirActual()
        {
            if (SelectedContrato is null) return;
            ContratoElegido?.Invoke(this, new ContratoSelectedEventArgs(SelectedContrato));
        }

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

        public void SetPageSize(int size)
        {
            _pageSize = Math.Max(5, size);
            _page = 1;
            BindPagina();
        }

        public void Refrescar() => AplicarFiltrosYBind();

        private void tlpFiltros_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public sealed class ContratoSelectedEventArgs : EventArgs
    {
        public Contrato Contrato { get; }
        public ContratoSelectedEventArgs(Contrato c) => Contrato = c;
    }
}
