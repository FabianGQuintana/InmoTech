using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class UcContratos : UserControl
    {
        // ====== VM/DTO mínimo ======
        private class ContratoVm
        {
            public int Id { get; set; }
            public string Numero { get; set; } = "";
            public string Inquilino { get; set; } = "";
            public string Inmueble { get; set; } = "";
            public DateTime FechaInicio { get; set; }
            public DateTime FechaFin { get; set; }
            public decimal MontoMensual { get; set; }
            public string Estado { get; set; } = "Vigente";
            public bool Activo { get; set; } = true;
        }

        // ====== “Servicio” en memoria para probar (luego lo reemplazamos por BD) ======
        private interface IContratoService
        {
            Task<BindingList<ContratoVm>> ListarAsync(string? filtro = null);
            Task<ContratoVm?> ObtenerAsync(int id);
            Task<int> CrearAsync(ContratoVm vm);
            Task ActualizarAsync(ContratoVm vm);
            Task CambiarActivoAsync(int id, bool activo);
        }

        private sealed class ContratoServiceMem : IContratoService
        {
            private int _seq = 3;
            private readonly BindingList<ContratoVm> _data = new BindingList<ContratoVm>
            {
                new ContratoVm{ Id=1, Numero="C-0001", Inquilino="Juan Pérez", Inmueble="Rivadavia 567", FechaInicio=DateTime.Today.AddMonths(-6), FechaFin=DateTime.Today.AddMonths(6), MontoMensual=180000, Estado="Vigente", Activo=true },
                new ContratoVm{ Id=2, Numero="C-0002", Inquilino="Lucía Gómez", Inmueble="Junín 1645",    FechaInicio=DateTime.Today.AddMonths(-2), FechaFin=DateTime.Today.AddMonths(10), MontoMensual=220000, Estado="Vigente", Activo=true },
            };

            public Task<BindingList<ContratoVm>> ListarAsync(string? filtro = null)
            {
                if (string.IsNullOrWhiteSpace(filtro)) return Task.FromResult(_data);
                var f = filtro.Trim().ToLowerInvariant();
                var res = new BindingList<ContratoVm>(_data.Where(x =>
                    (x.Numero?.ToLower().Contains(f) ?? false) ||
                    (x.Inquilino?.ToLower().Contains(f) ?? false) ||
                    (x.Inmueble?.ToLower().Contains(f) ?? false)).ToList());
                return Task.FromResult(res);
            }

            public Task<ContratoVm?> ObtenerAsync(int id) =>
                Task.FromResult(_data.FirstOrDefault(x => x.Id == id));

            public Task<int> CrearAsync(ContratoVm vm)
            {
                vm.Id = ++_seq;
                _data.Add(vm);
                return Task.FromResult(vm.Id);
            }

            public Task ActualizarAsync(ContratoVm vm)
            {
                var idx = _data.ToList().FindIndex(x => x.Id == vm.Id);
                if (idx >= 0) _data[idx] = vm;
                return Task.CompletedTask;
            }

            public Task CambiarActivoAsync(int id, bool activo)
            {
                var it = _data.FirstOrDefault(x => x.Id == id);
                if (it != null) it.Activo = activo;
                return Task.CompletedTask;
            }
        }

        // ====== Campos de la vista ======
        private readonly IContratoService _service = new ContratoServiceMem();
        private readonly BindingList<ContratoVm> _data = new();
        private readonly BindingSource _bs = new();

        private int? _editandoId = null;

        public UcContratos()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.Load += UcContratos_Load;
                btnGuardar.Click += BtnGuardar_Click;
                btnCancelar.Click += BtnCancelar_Click;
                btnBuscar.Click += async (_, __) => await RefrescarAsync(txtBuscar.Text);
                dgvContratos.CellContentClick += DgvContratos_CellContentClick;
                dgvContratos.CellValueChanged += DgvContratos_CellValueChanged;
                dgvContratos.CurrentCellDirtyStateChanged += (s, e) =>
                {
                    if (dgvContratos.IsCurrentCellDirty)
                        dgvContratos.CommitEdit(DataGridViewDataErrorContexts.Commit);
                };
            }
        }

        private async void UcContratos_Load(object? sender, EventArgs e)
        {
            // grid
            dgvContratos.AutoGenerateColumns = false;
            _bs.DataSource = _data;
            dgvContratos.DataSource = _bs;

            // columnas (si no están del diseñador)
            if (dgvContratos.Columns.Count == 0)
                CrearColumnasGrid();
            else
                ConfigurarGridResponsive(); // << [RESPONSIVE] si ya venían del diseñador

            cmbEstado.Items.Clear();
            cmbEstado.Items.AddRange(new[] { "Vigente", "Vencido", "Rescindido", "Pendiente" });
            cmbEstado.SelectedIndex = 0;

            nudMonto.Maximum = 1000000000;
            nudMonto.DecimalPlaces = 2;
            nudMonto.ThousandsSeparator = true;

            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today.AddMonths(12);

            await RefrescarAsync();

            HacerResponsivo(); // << [RESPONSIVE] activar layout elástico
        }

        // =============== [RESPONSIVE] Columnas a Fill con pesos ===============
        private void CrearColumnasGrid()
        {
            dgvContratos.Columns.Clear();
            dgvContratos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var colNumero = new DataGridViewTextBoxColumn { HeaderText = "N°", DataPropertyName = "Numero", FillWeight = 80 };
            var colInquilino = new DataGridViewTextBoxColumn { HeaderText = "Inquilino", DataPropertyName = "Inquilino", FillWeight = 160 };
            var colInmueble = new DataGridViewTextBoxColumn { HeaderText = "Inmueble", DataPropertyName = "Inmueble", FillWeight = 220 };
            var colInicio = new DataGridViewTextBoxColumn { HeaderText = "Inicio", DataPropertyName = "FechaInicio", FillWeight = 95, DefaultCellStyle = { Format = "d" } };
            var colFin = new DataGridViewTextBoxColumn { HeaderText = "Fin", DataPropertyName = "FechaFin", FillWeight = 95, DefaultCellStyle = { Format = "d" } };
            var colMonto = new DataGridViewTextBoxColumn { HeaderText = "Monto", DataPropertyName = "MontoMensual", FillWeight = 110, DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } };
            var colEstado = new DataGridViewTextBoxColumn { HeaderText = "Estado", DataPropertyName = "Estado", FillWeight = 100 };

            var colEditar = new DataGridViewButtonColumn
            {
                HeaderText = "Editar",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                FillWeight = 75
            };

            var colActivo = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Activo",
                DataPropertyName = "Activo",
                FillWeight = 60
            };

            dgvContratos.Columns.AddRange(new DataGridViewColumn[]
            {
                colNumero, colInquilino, colInmueble, colInicio, colFin, colMonto, colEstado, colEditar, colActivo
            });

            ConfigurarGridResponsive(); // << asegura pesos mínimos
        }

        private void ConfigurarGridResponsive()
        {
            dgvContratos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn c in dgvContratos.Columns)
            {
                c.MinimumWidth = 60;
                c.Resizable = DataGridViewTriState.False;
            }
        }

        // =============== [RESPONSIVE] Dock + Anchors del formulario ===============
        private void HacerResponsivo()
        {
            // Paneles principales del UserControl (del Designer)
            // grpEdicion arriba, lista ocupa el resto
            grpEdicion.Dock = DockStyle.Top;
            grpEdicion.Height = 150; // alto que ya venías usando

            grpLista.Dock = DockStyle.Fill;

            // La grilla ocupa todo dentro del groupbox
            dgvContratos.Dock = DockStyle.Fill;

            // Campos que deben estirarse con el ancho
            txtInquilino.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtInmueble.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Botones y búsqueda a la derecha
            btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // El resto puede quedar fijo a la izquierda (valores por defecto)
            // txtNumero, dtpInicio, dtpFin, nudMonto, cmbEstado, chkActivo
        }

        private async Task RefrescarAsync(string? filtro = null)
        {
            var list = await _service.ListarAsync(filtro);
            _data.Clear();
            foreach (var it in list) _data.Add(it);
            _bs.ResetBindings(false);
        }

        // ====== CRUD ======
        private ContratoVm TomarFormulario()
        {
            var vm = new ContratoVm
            {
                Id = _editandoId ?? 0,
                Numero = txtNumero.Text.Trim(),
                Inquilino = txtInquilino.Text.Trim(),
                Inmueble = txtInmueble.Text.Trim(),
                FechaInicio = dtpInicio.Value.Date,
                FechaFin = dtpFin.Value.Date,
                MontoMensual = nudMonto.Value,
                Estado = (cmbEstado.SelectedItem?.ToString() ?? "Vigente"),
                Activo = chkActivo.Checked
            };
            return vm;
        }

        private string? Validar(ContratoVm vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Numero)) return "Ingrese Nº de contrato.";
            if (string.IsNullOrWhiteSpace(vm.Inquilino)) return "Ingrese Inquilino.";
            if (string.IsNullOrWhiteSpace(vm.Inmueble)) return "Ingrese Inmueble.";
            if (vm.FechaFin <= vm.FechaInicio) return "La fecha de fin debe ser posterior al inicio.";
            if (vm.MontoMensual <= 0) return "El monto mensual debe ser mayor a 0.";
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
            {
                await _service.CrearAsync(vm);
            }
            else
            {
                await _service.ActualizarAsync(vm);
            }

            await RefrescarAsync();
            LimpiarFormulario();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e) => LimpiarFormulario();

        private void LimpiarFormulario()
        {
            _editandoId = null;
            txtNumero.Clear();
            txtInquilino.Clear();
            txtInmueble.Clear();
            nudMonto.Value = 0;
            cmbEstado.SelectedIndex = 0;
            chkActivo.Checked = true;
            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today.AddMonths(12);
            txtNumero.Focus();
        }

        // ====== Grid events ======
        private void DgvContratos_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Columna "Editar" es la anteúltima (botón)
            var esBotonEditar = dgvContratos.Columns[e.ColumnIndex] is DataGridViewButtonColumn;
            if (!esBotonEditar) return;

            var item = (ContratoVm)dgvContratos.Rows[e.RowIndex].DataBoundItem;
            _editandoId = item.Id;

            // Cargar al formulario
            txtNumero.Text = item.Numero;
            txtInquilino.Text = item.Inquilino;
            txtInmueble.Text = item.Inmueble;
            dtpInicio.Value = item.FechaInicio;
            dtpFin.Value = item.FechaFin;
            nudMonto.Value = item.MontoMensual;
            cmbEstado.SelectedItem = item.Estado;
            chkActivo.Checked = item.Activo;
        }

        private async void DgvContratos_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            // Columna "Activo" es la última
            var esColActivo = dgvContratos.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn;
            if (!esColActivo) return;

            var item = (ContratoVm)dgvContratos.Rows[e.RowIndex].DataBoundItem;
            await _service.CambiarActivoAsync(item.Id, item.Activo);
        }
    }
}
