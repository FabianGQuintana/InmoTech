using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class UcPagos : UserControl
    {
        // =======================
        //      VMs / DTOs
        // =======================
        private class ContratoVm
        {
            public int Id { get; set; }
            public string Numero { get; set; } = "";
            public string Inquilino { get; set; } = "";
            public string Inmueble { get; set; } = "";
            public bool Activo { get; set; } = true;
            public decimal Total { get; set; }
            public decimal Atraso { get; set; }
            public DateTime Inicio { get; set; }
            public DateTime Fin { get; set; }
        }

        private class CuotaVm
        {
            public int Id { get; set; }
            public int ContratoId { get; set; }
            public string ContratoNumero { get; set; } = "";
            public int NumeroCuota { get; set; }
            public int CantidadCuotas { get; set; }
            public string Periodo { get; set; } = "";
            public decimal Monto { get; set; }
            public DateTime Vencimiento { get; set; }
            public string Estado { get; set; } = "Pendiente"; // Pendiente | Pagada | Vencida
            public int? PagoId { get; set; }
        }

        private class PagoVm
        {
            public int Id { get; set; }
            public int CuotaId { get; set; }
            public string ContratoNumero { get; set; } = "";
            public string Inquilino { get; set; } = "";
            public DateTime Fecha { get; set; } = DateTime.Today;
            public decimal Importe { get; set; }
            public string Metodo { get; set; } = "Efectivo";
            public string Estado { get; set; } = "Pagado";  // UX: lo dejamos en Pagado al registrar
            public string Observaciones { get; set; } = "";
            public string? NroComprobante { get; set; }
            public bool EmitirRecibo { get; set; }
        }

        private class ReciboVm
        {
            public string ContratoNumero { get; set; } = "";
            public string Inquilino { get; set; } = "";
            public string Inmueble { get; set; } = "";
            public int NroCuota { get; set; }
            public int CantCuotas { get; set; }
            public string Periodo { get; set; } = "";
            public DateTime FechaPago { get; set; }
            public decimal Importe { get; set; }
            public string Metodo { get; set; } = "";
            public int NroRecibo { get; set; }
        }

        // =======================
        //     Servicio (mock)
        // =======================
        private interface IPagoService
        {
            Task<BindingList<ContratoVm>> ListarContratosAsync(string? texto = null, bool? soloActivos = null);
            Task<ContratoVm?> ObtenerContratoAsync(int contratoId);

            Task<BindingList<CuotaVm>> ListarCuotasAsync(int contratoId, string? estado = null, int? anio = null);
            Task<CuotaVm?> ObtenerCuotaAsync(int cuotaId);

            Task<PagoVm?> ObtenerPagoAsync(int pagoId);
            Task<int> RegistrarPagoAsync(PagoVm pago);
            Task<ReciboVm?> GenerarReciboAsync(int pagoId);
        }

        private sealed class PagoServiceMem : IPagoService
        {
            private int _seqPago = 1;
            private readonly BindingList<ContratoVm> _contratos = new()
            {
                new ContratoVm{ Id=1, Numero="C-1024", Inquilino="Juan Pérez",  Inmueble="Calle 123 — Dpto 4B", Activo=true,  Total=600000, Atraso=58000, Inicio=new DateTime(2024,5,1), Fin=new DateTime(2025,4,30)},
                new ContratoVm{ Id=2, Numero="C-1025", Inquilino="Ana Gómez",   Inmueble="Urquiza 90 — PB A",   Activo=true,  Total=600000, Atraso=0,     Inicio=new DateTime(2024,5,1), Fin=new DateTime(2025,4,30)},
                new ContratoVm{ Id=3, Numero="C-1027", Inquilino="Inquilino 3", Inmueble="Avenida 456",         Activo=false, Total=500000, Atraso=0,     Inicio=new DateTime(2024,5,1), Fin=new DateTime(2025,4,30)},
            };

            private readonly BindingList<CuotaVm> _cuotas = new();
            private readonly BindingList<PagoVm> _pagos = new();

            public PagoServiceMem()
            {
                // Generar 12 cuotas por contrato
                foreach (var c in _contratos)
                {
                    var monto = 50000m;
                    for (int i = 1; i <= 12; i++)
                    {
                        var periodo = c.Inicio.AddMonths(i - 1);
                        var cuota = new CuotaVm
                        {
                            Id = _cuotas.Count + 1,
                            ContratoId = c.Id,
                            ContratoNumero = c.Numero,
                            NumeroCuota = i,
                            CantidadCuotas = 12,
                            Periodo = $"{Mes(periodo.Month)} {periodo.Year}",
                            Monto = monto,
                            Vencimiento = new DateTime(periodo.Year, periodo.Month, 10),
                            Estado = i == 1 ? "Pagada" : (periodo < DateTime.Today.AddMonths(-1) ? "Vencida" : "Pendiente"),
                            PagoId = i == 1 ? 1 : (int?)null
                        };
                        _cuotas.Add(cuota);
                    }
                }

                // Un pago de ejemplo (cuota 1 del contrato 1)
                _pagos.Add(new PagoVm
                {
                    Id = 1,
                    CuotaId = _cuotas.First(x => x.ContratoId == 1 && x.NumeroCuota == 1).Id,
                    ContratoNumero = "C-1024",
                    Inquilino = "Juan Pérez",
                    Fecha = DateTime.Today.AddMonths(-1),
                    Importe = 50000,
                    Metodo = "Efectivo",
                    Estado = "Pagado",
                    Observaciones = "1/12",
                    NroComprobante = "A0001-00001234",
                    EmitirRecibo = true
                });
                _seqPago = 1;
            }

            private static string Mes(int m) =>
                new[] { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" }[m];

            public Task<BindingList<ContratoVm>> ListarContratosAsync(string? texto = null, bool? soloActivos = null)
            {
                var q = _contratos.AsEnumerable();
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    var f = texto.Trim().ToLowerInvariant();
                    q = q.Where(x => x.Numero.ToLower().Contains(f)
                                  || x.Inquilino.ToLower().Contains(f)
                                  || x.Inmueble.ToLower().Contains(f));
                }
                if (soloActivos == true) q = q.Where(x => x.Activo);
                return Task.FromResult(new BindingList<ContratoVm>(q.ToList()));
            }

            public Task<ContratoVm?> ObtenerContratoAsync(int contratoId) =>
                Task.FromResult(_contratos.FirstOrDefault(x => x.Id == contratoId));

            public Task<BindingList<CuotaVm>> ListarCuotasAsync(int contratoId, string? estado = null, int? anio = null)
            {
                var q = _cuotas.AsEnumerable().Where(x => x.ContratoId == contratoId);
                if (!string.IsNullOrWhiteSpace(estado) && estado != "Todas")
                    q = q.Where(x => x.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
                if (anio.HasValue) q = q.Where(x => x.Periodo.EndsWith(anio.Value.ToString()));
                return Task.FromResult(new BindingList<CuotaVm>(q.ToList()));
            }

            public Task<CuotaVm?> ObtenerCuotaAsync(int cuotaId) =>
                Task.FromResult(_cuotas.FirstOrDefault(x => x.Id == cuotaId));

            public Task<PagoVm?> ObtenerPagoAsync(int pagoId) =>
                Task.FromResult(_pagos.FirstOrDefault(p => p.Id == pagoId));

            public Task<int> RegistrarPagoAsync(PagoVm pago)
            {
                if (pago.Id == 0)
                {
                    pago.Id = ++_seqPago;
                    _pagos.Add(pago);
                }
                else
                {
                    var idx = _pagos.ToList().FindIndex(p => p.Id == pago.Id);
                    if (idx >= 0) _pagos[idx] = pago;
                }

                var cuota = _cuotas.First(c => c.Id == pago.CuotaId);
                cuota.Estado = "Pagada";
                cuota.PagoId = pago.Id;

                return Task.FromResult(pago.Id);
            }

            public Task<ReciboVm?> GenerarReciboAsync(int pagoId)
            {
                var pago = _pagos.FirstOrDefault(p => p.Id == pagoId);
                if (pago == null) return Task.FromResult<ReciboVm?>(null);
                var cuota = _cuotas.First(c => c.Id == pago.CuotaId);
                var contrato = _contratos.First(c => c.Id == cuota.ContratoId);

                var r = new ReciboVm
                {
                    ContratoNumero = contrato.Numero,
                    Inquilino = contrato.Inquilino,
                    Inmueble = contrato.Inmueble,
                    NroCuota = cuota.NumeroCuota,
                    CantCuotas = cuota.CantidadCuotas,
                    Periodo = cuota.Periodo,
                    FechaPago = pago.Fecha,
                    Importe = pago.Importe,
                    Metodo = pago.Metodo,
                    NroRecibo = pago.Id
                };
                return Task.FromResult<ReciboVm?>(r);
            }
        }

        // =======================
        //  Vista / Estado actual
        // =======================
        private enum Vista { Contratos, Cuotas, RegistrarPago, Recibo }

        private readonly IPagoService _service = new PagoServiceMem();

        private readonly BindingList<ContratoVm> _blContratos = new();
        private readonly BindingList<CuotaVm> _blCuotas = new();

        private Vista _vista = Vista.Contratos;
        private int? _contratoSelId = null;
        private ContratoVm? _contratoSel = null;
        private CuotaVm? _cuotaSel = null;

        // =======================
        //      Constructor
        // =======================
        public UcPagos()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                Load += UcPagos_Load;

                // Header
                btnVolver.Click += async (_, __) =>
                {
                    if (_vista == Vista.Cuotas) { IrA(Vista.Contratos); await CargarContratosAsync(); }
                    else if (_vista == Vista.RegistrarPago) { IrA(Vista.Cuotas); if (_contratoSelId.HasValue) await CargarCuotasAsync(_contratoSelId.Value); }
                    else if (_vista == Vista.Recibo) { IrA(Vista.Cuotas); if (_contratoSelId.HasValue) await CargarCuotasAsync(_contratoSelId.Value); }
                };

                // Contratos
                btnBuscarContrato.Click += async (_, __) => await CargarContratosAsync();
                dgvContratos.CellContentClick += DgvContratos_CellContentClick;

                // Cuotas
                dgvCuotas.CellContentClick += DgvCuotas_CellContentClick;
                dgvCuotas.CellFormatting += dgvCuotas_CellFormatting;
                cmbEstadoCuota.SelectedIndexChanged += async (_, __) => { if (_contratoSelId.HasValue) await CargarCuotasAsync(_contratoSelId.Value); };
                cmbAnio.SelectedIndexChanged += async (_, __) => { if (_contratoSelId.HasValue) await CargarCuotasAsync(_contratoSelId.Value); };

                // Registrar pago
                btnGuardarPago.Click += BtnGuardarPago_Click;
                btnCancelarPago.Click += async (_, __) =>
                {
                    IrA(Vista.Cuotas);
                    if (_contratoSelId.HasValue) await CargarCuotasAsync(_contratoSelId.Value);
                };

                // Recibo
                btnCerrarRecibo.Click += async (_, __) =>
                {
                    IrA(Vista.Cuotas);
                    if (_contratoSelId.HasValue) await CargarCuotasAsync(_contratoSelId.Value);
                };
            }
        }

        private async void UcPagos_Load(object? sender, EventArgs e)
        {
            // Columnas
            ConfigurarColumnasContratos();
            ConfigurarColumnasCuotas();

            // Filtros
            cmbEstadoCuota.Items.Clear();
            cmbEstadoCuota.Items.AddRange(new[] { "Todas", "Pendiente", "Pagada", "Vencida" });
            cmbEstadoCuota.SelectedIndex = 0;

            cmbAnio.Items.Clear();
            cmbAnio.Items.Add("Todos");
            foreach (var a in Enumerable.Range(DateTime.Today.Year - 2, 5))
                cmbAnio.Items.Add(a.ToString());
            cmbAnio.SelectedIndex = 0;

            // Métodos de pago
            cmbMetodo.Items.Clear();
            cmbMetodo.Items.AddRange(new[] { "Efectivo", "Transferencia", "Tarjeta", "Débito", "Mercado Pago" });
            cmbMetodo.SelectedIndex = 0;

            // Vista inicial
            IrA(Vista.Contratos);
            await CargarContratosAsync();
        }

        // =======================
        //   Navegación de vistas
        // =======================
        private void IrA(Vista v)
        {
            _vista = v;

            pnlContratos.Visible = v == Vista.Contratos;
            pnlCuotas.Visible = v == Vista.Cuotas;
            pnlRegistrar.Visible = v == Vista.RegistrarPago;
            pnlRecibo.Visible = v == Vista.Recibo;

            btnVolver.Visible = v != Vista.Contratos;

            lblTitulo.Text = v switch
            {
                Vista.Contratos => "Pagos · Contratos",
                Vista.Cuotas => "Pagos · Cuotas del contrato",
                Vista.RegistrarPago => "Registrar pago",
                Vista.Recibo => "Recibo",
                _ => "Pagos"
            };
        }

        // =======================
        //   Columnas de grillas
        // =======================
        private void ConfigurarColumnasContratos()
        {
            dgvContratos.Columns.Clear();
            dgvContratos.AutoGenerateColumns = false;
            dgvContratos.ReadOnly = true;

            dgvContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNumero",
                HeaderText = "Contrato",
                DataPropertyName = "Numero",
                Width = 100
            });
            dgvContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colInquilino",
                HeaderText = "Inquilino",
                DataPropertyName = "Inquilino",
                Width = 180
            });
            dgvContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colInmueble",
                HeaderText = "Inmueble",
                DataPropertyName = "Inmueble",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvContratos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colEstado",
                HeaderText = "Estado",
                DataPropertyName = "Activo",
                Width = 90
            });
            dgvContratos.CellFormatting += (s, e) =>
            {
                if (dgvContratos.Columns[e.ColumnIndex].Name == "colEstado" && e.Value is bool b)
                {
                    e.Value = b ? "Activo" : "Inactivo";
                    e.FormattingApplied = true;
                }
            };

            var verCuotas = new DataGridViewButtonColumn
            {
                Name = "colVerCuotas",
                HeaderText = "Acciones",
                Text = "Ver cuotas",
                UseColumnTextForButtonValue = true,
                Width = 110
            };
            dgvContratos.Columns.Add(verCuotas);
        }

        private void dgvCuotas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Deshabilitar botón "Ver recibo" si la cuota no tiene PagoId
            if (dgvCuotas.Columns[e.ColumnIndex].Name == "colRecibo")
            {
                var q = dgvCuotas.Rows[e.RowIndex].DataBoundItem as CuotaVm;
                if (q == null) return;

                // DataGridViewButtonCell no tiene Enabled, pero con ReadOnly evitamos el click
                var cell = dgvCuotas.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                bool sinPago = !q.PagoId.HasValue;
                cell.ReadOnly = sinPago;

                // Opcional: dar feedback visual leve
                cell.FlatStyle = sinPago ? FlatStyle.Popup : FlatStyle.Standard;
            }
        }


        private void ConfigurarColumnasCuotas()
        {
            dgvCuotas.Columns.Clear();
            dgvCuotas.AutoGenerateColumns = false;
            dgvCuotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNro",
                HeaderText = "Cuota",
                DataPropertyName = "NumeroCuota",
                Width = 70
            });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDe",
                HeaderText = "de",
                DataPropertyName = "CantidadCuotas",
                Width = 50
            });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPeriodo",
                HeaderText = "Periodo",
                DataPropertyName = "Periodo",
                Width = 140
            });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMonto",
                HeaderText = "Monto",
                DataPropertyName = "Monto",
                Width = 110,
                DefaultCellStyle = { Format = "N2" }
            });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colVto",
                HeaderText = "Vencimiento",
                DataPropertyName = "Vencimiento",
                Width = 110,
                DefaultCellStyle = { Format = "d" }
            });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colEstado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 100
            });

            // Botón Pagar
            var pagar = new DataGridViewButtonColumn
            {
                Name = "colPagar",
                HeaderText = "Acciones",
                Text = "Pagar",
                UseColumnTextForButtonValue = true,
                Width = 90
            };
            dgvCuotas.Columns.Add(pagar);

            // Botón Ver recibo (siempre visible; lo deshabilitamos visualmente abajo)
            var verRecibo = new DataGridViewButtonColumn
            {
                Name = "colRecibo",
                HeaderText = "",
                Text = "Ver recibo",
                UseColumnTextForButtonValue = true,
                Width = 110
            };
            dgvCuotas.Columns.Add(verRecibo);
        }


        // =======================
        //    Cargas de datos
        // =======================
        private async Task CargarContratosAsync()
        {
            var filtro = txtBuscarContrato.Text;
            var list = await _service.ListarContratosAsync(filtro, null);

            _blContratos.Clear();
            foreach (var c in list) _blContratos.Add(c);

            dgvContratos.DataSource = _blContratos;
        }

        private async Task CargarCuotasAsync(int contratoId)
        {
            var estado = cmbEstadoCuota.SelectedItem?.ToString();
            var anio = (cmbAnio.SelectedItem?.ToString() ?? "Todos") == "Todos"
                ? (int?)null
                : int.Parse(cmbAnio.SelectedItem!.ToString()!);

            var list = await _service.ListarCuotasAsync(contratoId, estado, anio);

            _blCuotas.Clear();
            foreach (var q in list) _blCuotas.Add(q);

            dgvCuotas.DataSource = _blCuotas;

            _contratoSel = await _service.ObtenerContratoAsync(contratoId);
            lblContratoResumen.Text = _contratoSel is null ? "Contrato" : $"Contrato {_contratoSel.Numero}";
            lblInquilinoResumen.Text = _contratoSel is null ? "Inquilino: —" : $"Inquilino: {_contratoSel.Inquilino}";
            lblInmuebleResumen.Text = _contratoSel is null ? "Inmueble: —" : $"Inmueble: {_contratoSel.Inmueble}";
        }

        // =======================
        //     Eventos de grid
        // =======================
        private async void DgvContratos_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvContratos.Columns[e.ColumnIndex].Name != "colVerCuotas") return;

            var contrato = (ContratoVm)dgvContratos.Rows[e.RowIndex].DataBoundItem;
            _contratoSelId = contrato.Id;

            ConfigurarColumnasCuotas(); // por si venimos de otra vista
            IrA(Vista.Cuotas);
            await CargarCuotasAsync(contrato.Id);
        }

        private async void DgvCuotas_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var col = dgvCuotas.Columns[e.ColumnIndex].Name;
            var cuota = (CuotaVm)dgvCuotas.Rows[e.RowIndex].DataBoundItem;

            if (col == "colPagar")
            {
                _cuotaSel = cuota;
                MostrarFormularioPago(cuota);
                IrA(Vista.RegistrarPago);
                return;
            }

            if (col == "colRecibo")
            {
                if (cuota.PagoId.HasValue) await MostrarReciboAsync(cuota.PagoId.Value);
                else MessageBox.Show("Aún no hay recibo: la cuota no está pagada.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        // =======================
        //     Registrar pago
        // =======================
        private void MostrarFormularioPago(CuotaVm cuota)
        {
            if (_contratoSel == null) return;

            lblTituloPago.Text = $"Contrato N° {_contratoSel.Numero}";
            lblContrato.Text = $"Inquilino: {_contratoSel.Inquilino}";
            lblInquilino.Text = $"Inmueble: {_contratoSel.Inmueble}";
            lblCuotaPeriodo.Text = $"Cuota {cuota.NumeroCuota}/{cuota.CantidadCuotas}  ·  {cuota.Periodo}  ·  $ {cuota.Monto:N2}";

            dtpFecha.Value = DateTime.Today;
            cmbMetodo.SelectedIndex = 0;
            txtComprobante.Text = "";
            txtObs.Text = "";
            chkEmitirRecibo.Checked = true;
        }

        private async void BtnGuardarPago_Click(object? sender, EventArgs e)
        {
            if (_cuotaSel == null || _contratoSel == null)
            {
                MessageBox.Show("No hay cuota seleccionada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pago = new PagoVm
            {
                Id = 0,
                CuotaId = _cuotaSel.Id,
                ContratoNumero = _contratoSel.Numero,
                Inquilino = _contratoSel.Inquilino,
                Fecha = dtpFecha.Value.Date,
                Importe = _cuotaSel.Monto,
                Metodo = cmbMetodo.SelectedItem?.ToString() ?? "Efectivo",
                Estado = "Pagado",
                Observaciones = string.IsNullOrWhiteSpace(txtObs.Text) ? $"Pago de cuota {_cuotaSel.NumeroCuota}/{_cuotaSel.CantidadCuotas} ({_cuotaSel.Periodo})" : txtObs.Text.Trim(),
                NroComprobante = string.IsNullOrWhiteSpace(txtComprobante.Text) ? null : txtComprobante.Text.Trim(),
                EmitirRecibo = chkEmitirRecibo.Checked
            };

            if (pago.Importe <= 0)
            {
                MessageBox.Show("El importe debe ser mayor que 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pagoId = await _service.RegistrarPagoAsync(pago);

            // Volver a cuotas y refrescar
            IrA(Vista.Cuotas);
            if (_contratoSelId.HasValue) await CargarCuotasAsync(_contratoSelId.Value);

            if (pago.EmitirRecibo)
            {
                await MostrarReciboAsync(pagoId);
            }
            else
            {
                var r = MessageBox.Show("Pago registrado. ¿Desea ver el recibo?", "Éxito",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes) await MostrarReciboAsync(pagoId);
            }
        }

        // =======================
        //    Recibo (preview)
        // =======================
        private async Task MostrarReciboAsync(int pagoId)
        {
            var r = await _service.GenerarReciboAsync(pagoId);
            if (r == null)
            {
                MessageBox.Show("No se pudo generar el recibo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            rtbRecibo.Clear();
            rtbRecibo.Text =
$@"InmoTech — Recibo #{r.NroRecibo}

Contrato: {r.ContratoNumero}
Inquilino: {r.Inquilino}
Inmueble: {r.Inmueble}

Cuota: {r.NroCuota} de {r.CantCuotas}   |   Periodo: {r.Periodo}
Fecha de pago: {r.FechaPago:d}

Importe: {r.Importe:N2}
Método: {r.Metodo}";

            IrA(Vista.Recibo);
        }
    }
}
