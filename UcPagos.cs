using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class UcPagos : UserControl
    {
        // ====== VM/DTO mínimo ======
        private class PagoVm
        {
            public int Id { get; set; }
            public string ContratoNumero { get; set; } = "";
            public string Inquilino { get; set; } = "";
            public DateTime Fecha { get; set; } = DateTime.Today;
            public decimal Importe { get; set; }
            public string Metodo { get; set; } = "Efectivo";
            public string Estado { get; set; } = "Pendiente"; // Pendiente | Pagado | Anulado
            public string Observaciones { get; set; } = "";
            public bool Pagado { get; set; } // solo para UX (checkbox)
        }

        // ====== “Servicio” en memoria (para probar) ======
        private interface IPagoService
        {
            Task<BindingList<PagoVm>> ListarAsync(string? texto = null, string? estado = null);
            Task<PagoVm?> ObtenerAsync(int id);
            Task<int> CrearAsync(PagoVm vm);
            Task ActualizarAsync(PagoVm vm);
            Task CambiarPagadoAsync(int id, bool pagado);
        }

        private sealed class PagoServiceMem : IPagoService
        {
            private int _seq = 3;
            private readonly BindingList<PagoVm> _data = new()
            {
                new PagoVm{ Id=1, ContratoNumero="C-0001", Inquilino="Juan Pérez",  Fecha=DateTime.Today.AddDays(-5), Importe=180000, Metodo="Transferencia", Estado="Pagado",   Pagado=true,  Observaciones="Mes 08/2025" },
                new PagoVm{ Id=2, ContratoNumero="C-0002", Inquilino="Lucía Gómez", Fecha=DateTime.Today.AddDays(-2), Importe=220000, Metodo="Efectivo",      Estado="Pendiente", Pagado=false, Observaciones="Mes 08/2025" },
                new PagoVm{ Id=3, ContratoNumero="C-0001", Inquilino="Juan Pérez",  Fecha=DateTime.Today.AddMonths(-1), Importe=180000, Metodo="Débito",      Estado="Pagado",   Pagado=true,  Observaciones="Mes 07/2025" },
            };

            public Task<BindingList<PagoVm>> ListarAsync(string? texto = null, string? estado = null)
            {
                var q = _data.AsEnumerable();
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    var f = texto.Trim().ToLowerInvariant();
                    q = q.Where(x =>
                        (x.ContratoNumero?.ToLower().Contains(f) ?? false) ||
                        (x.Inquilino?.ToLower().Contains(f) ?? false) ||
                        (x.Observaciones?.ToLower().Contains(f) ?? false));
                }
                if (!string.IsNullOrWhiteSpace(estado) && estado != "Todos")
                {
                    q = q.Where(x => string.Equals(x.Estado, estado, StringComparison.OrdinalIgnoreCase));
                }
                return Task.FromResult(new BindingList<PagoVm>(q.ToList()));
            }

            public Task<PagoVm?> ObtenerAsync(int id) =>
                Task.FromResult(_data.FirstOrDefault(x => x.Id == id));

            public Task<int> CrearAsync(PagoVm vm)
            {
                vm.Id = ++_seq;
                vm.Pagado = string.Equals(vm.Estado, "Pagado", StringComparison.OrdinalIgnoreCase);
                _data.Add(vm);
                return Task.FromResult(vm.Id);
            }

            public Task ActualizarAsync(PagoVm vm)
            {
                var idx = _data.ToList().FindIndex(x => x.Id == vm.Id);
                if (idx >= 0)
                {
                    vm.Pagado = string.Equals(vm.Estado, "Pagado", StringComparison.OrdinalIgnoreCase);
                    _data[idx] = vm;
                }
                return Task.CompletedTask;
            }

            public Task CambiarPagadoAsync(int id, bool pagado)
            {
                var it = _data.FirstOrDefault(x => x.Id == id);
                if (it != null)
                {
                    it.Pagado = pagado;
                    it.Estado = pagado ? "Pagado" : "Pendiente";
                }
                return Task.CompletedTask;
            }
        }

        // ====== Campos de la vista ======
        private readonly IPagoService _service = new PagoServiceMem();
        private readonly BindingList<PagoVm> _data = new();
        private readonly BindingSource _bs = new();
        private int? _editandoId = null;

        public UcPagos()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.Load += UcPagos_Load;
                btnGuardar.Click += BtnGuardar_Click;
                btnCancelar.Click += (_, __) => LimpiarFormulario();
                btnBuscar.Click += async (_, __) => await RefrescarAsync();

                dgvPagos.CellContentClick += DgvPagos_CellContentClick;
                dgvPagos.CellValueChanged += DgvPagos_CellValueChanged;
                dgvPagos.CurrentCellDirtyStateChanged += (s, e) =>
                {
                    if (dgvPagos.IsCurrentCellDirty)
                        dgvPagos.CommitEdit(DataGridViewDataErrorContexts.Commit);
                };
            }
        }

        private async void UcPagos_Load(object? sender, EventArgs e)
        {
            // combos
            cmbMetodo.Items.Clear();
            cmbMetodo.Items.AddRange(new[] { "Efectivo", "Transferencia", "Tarjeta", "Débito", "Mercado Pago" });
            cmbMetodo.SelectedIndex = 0;

            cmbEstado.Items.Clear();
            cmbEstado.Items.AddRange(new[] { "Pendiente", "Pagado", "Anulado" });
            cmbEstado.SelectedIndex = 0;

            cmbFiltroEstado.Items.Clear();
            cmbFiltroEstado.Items.AddRange(new[] { "Todos", "Pendiente", "Pagado", "Anulado" });
            cmbFiltroEstado.SelectedIndex = 0;

            nudImporte.Maximum = 1000000000;
            nudImporte.DecimalPlaces = 2;
            nudImporte.ThousandsSeparator = true;

            dtpFecha.Value = DateTime.Today;

            // grid
            dgvPagos.AutoGenerateColumns = false;
            _bs.DataSource = _data;
            dgvPagos.DataSource = _bs;

            if (dgvPagos.Columns.Count == 0)
                CrearColumnasGrid();

            await RefrescarAsync();
        }

        private void CrearColumnasGrid()
        {
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha", DataPropertyName = "Fecha", Width = 95, DefaultCellStyle = { Format = "d" } });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Contrato", DataPropertyName = "ContratoNumero", Width = 110 });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Inquilino", DataPropertyName = "Inquilino", Width = 180 });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Importe", DataPropertyName = "Importe", Width = 110, DefaultCellStyle = { Format = "N2" } });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Método", DataPropertyName = "Metodo", Width = 110 });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estado", DataPropertyName = "Estado", Width = 100 });
            dgvPagos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Observaciones", DataPropertyName = "Observaciones", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            var colEditar = new DataGridViewButtonColumn
            {
                HeaderText = "Editar",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgvPagos.Columns.Add(colEditar);

            var colPagado = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Pagado",
                DataPropertyName = "Pagado",
                Width = 70
            };
            dgvPagos.Columns.Add(colPagado);
        }

        private async Task RefrescarAsync()
        {
            var estado = cmbFiltroEstado.SelectedItem?.ToString();
            var texto = txtBuscar.Text;
            var list = await _service.ListarAsync(texto, estado);
            _data.Clear();
            foreach (var it in list) _data.Add(it);
            _bs.ResetBindings(false);
        }

        // ====== CRUD ======
        private PagoVm TomarFormulario()
        {
            return new PagoVm
            {
                Id = _editandoId ?? 0,
                ContratoNumero = txtContrato.Text.Trim(),
                Inquilino = txtInquilino.Text.Trim(),
                Fecha = dtpFecha.Value.Date,
                Importe = nudImporte.Value,
                Metodo = (cmbMetodo.SelectedItem?.ToString() ?? "Efectivo"),
                Estado = (cmbEstado.SelectedItem?.ToString() ?? "Pendiente"),
                Observaciones = txtObs.Text.Trim(),
                Pagado = string.Equals(cmbEstado.SelectedItem?.ToString(), "Pagado", StringComparison.OrdinalIgnoreCase)
            };
        }

        private string? Validar(PagoVm vm)
        {
            if (string.IsNullOrWhiteSpace(vm.ContratoNumero)) return "Ingrese Nº de contrato.";
            if (string.IsNullOrWhiteSpace(vm.Inquilino)) return "Ingrese inquilino.";
            if (vm.Importe <= 0) return "El importe debe ser mayor a 0.";
            if (vm.Fecha.Date > DateTime.Today.AddDays(1)) return "La fecha no puede ser futura (tolerancia un día).";
            return null;
        }

        private async void BtnGuardar_Click(object? sender, EventArgs e)
        {
            var vm = TomarFormulario();
            var err = Validar(vm);
            if (err != null)
            {
                MessageBox.Show(err, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_editandoId is null)
                await _service.CrearAsync(vm);
            else
                await _service.ActualizarAsync(vm);

            await RefrescarAsync();
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            _editandoId = null;
            txtContrato.Clear();
            txtInquilino.Clear();
            dtpFecha.Value = DateTime.Today;
            nudImporte.Value = 0;
            cmbMetodo.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
            txtObs.Clear();
            txtContrato.Focus();
        }

        // ====== Grid events ======
        private void CargarAlFormulario(PagoVm it)
        {
            _editandoId = it.Id;
            txtContrato.Text = it.ContratoNumero;
            txtInquilino.Text = it.Inquilino;
            dtpFecha.Value = it.Fecha;
            nudImporte.Value = it.Importe;
            cmbMetodo.SelectedItem = it.Metodo;
            cmbEstado.SelectedItem = it.Estado;
            txtObs.Text = it.Observaciones;
        }

        private void DgvPagos_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var esEditar = dgvPagos.Columns[e.ColumnIndex] is DataGridViewButtonColumn;
            if (!esEditar) return;

            var item = (PagoVm)dgvPagos.Rows[e.RowIndex].DataBoundItem;
            CargarAlFormulario(item);
        }

        private async void DgvPagos_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var esColPagado = dgvPagos.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn;
            if (!esColPagado) return;

            var item = (PagoVm)dgvPagos.Rows[e.RowIndex].DataBoundItem;
            await _service.CambiarPagadoAsync(item.Id, item.Pagado);
            // actualizar estado visible sin recargar toda la lista
            dgvPagos.Refresh();
        }

        private void cmbMetodo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
