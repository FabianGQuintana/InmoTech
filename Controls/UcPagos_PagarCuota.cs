using System;
using System.Linq;
using System.Windows.Forms;
using InmoTech.Models;
using InmoTech.Repositories;

namespace InmoTech.Controls
{
    public partial class UcPagos_PagarCuota : UserControl
    {
        private readonly Contrato _contrato;
        private readonly Cuota _cuota;

        private readonly MetodoPagoRepository _repoMetodos = new MetodoPagoRepository();
        private readonly PagoRepository _repoPago = new PagoRepository();
        private readonly CuotaRepository _repoCuota = new CuotaRepository();

        public event EventHandler? Cancelado;
        public event EventHandler<int>? PagoGuardado; // devuelve id_pago

        public UcPagos_PagarCuota(Contrato contrato, Cuota cuota)
        {
            InitializeComponent();
            _contrato = contrato;
            _cuota = cuota;
            Load += UcPagos_PagarCuota_Load;
        }

        private void UcPagos_PagarCuota_Load(object? sender, EventArgs e)
        {
            // Cabecera
            lblTituloContrato.Text = $"Contrato N°: C-{_contrato.IdContrato}";
            lblLinea1.Text = $"Inquilino: {_contrato.NombreInquilino ?? "—"}";
            lblLinea2.Text = $"Inmueble: {_contrato.DireccionInmueble ?? "—"}";
            lblLinea3.Text = $"Cuota: {_cuota.NroCuota} / {CalcularMesesContrato()}   |   " +
                             $"Período: {_cuota.FechaVencimiento:MMMM yyyy}   |   Monto: $ {_cuota.Importe:N0}";

            // Campos
            dtpFechaPago.Value = DateTime.Now.Date;
            nudMonto.Value = _cuota.Importe;
            txtComprobante.PlaceholderText = "Opcional";
            txtObservaciones.PlaceholderText = "Opcional";

            // Métodos de pago
            var metodos = _repoMetodos.ObtenerTodos();
            cboMetodoPago.DataSource = metodos;
            cboMetodoPago.DisplayMember = nameof(Models.MetodoPago.TipoPago);
            cboMetodoPago.ValueMember = nameof(Models.MetodoPago.IdMetodoPago);

            // Seleccionar Efectivo (id 1) si existe
            var idxEfectivo = metodos.FindIndex(m => m.IdMetodoPago == 1);
            cboMetodoPago.SelectedIndex = idxEfectivo >= 0 ? idxEfectivo : 0;

            // Botones
            btnCancelar.Click += (s, ev) => Cancelado?.Invoke(this, EventArgs.Empty);
            btnGuardar.Click += BtnGuardar_Click;
            lnkVistaPrevia.Click += (s, ev) => MessageBox.Show("Vista previa del recibo (pendiente de implementación).", "Recibo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            // Validaciones mínimas
            if (cboMetodoPago.SelectedItem is null)
            {
                MessageBox.Show("Seleccione un método de pago.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudMonto.Value <= 0)
            {
                MessageBox.Show("El monto debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Datos del usuario actual
            var usr = InmoTech.Security.AuthService.CurrentUser;
            if (usr is null)
            {
                MessageBox.Show("No hay un usuario autenticado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 1) Insertar pago
                var pago = new Models.Pago
                {
                    FechaPago = dtpFechaPago.Value.Date,
                    MontoTotal = nudMonto.Value,
                    IdUsuario = usr.Dni, // asumo que Dni = PK usuario (por tu repo)
                    IdMetodoPago = (int)cboMetodoPago.SelectedValue,
                    IdContrato = _contrato.IdContrato,
                    NroCuota = _cuota.NroCuota,
                    Detalle = ConstruirDetalle(txtComprobante.Text, txtObservaciones.Text),
                    Estado = Models.PagoEstados.Pagado,
                    FechaRegistro = DateTime.Now,
                    IdPersona = _contrato.IdPersona,
                    UsuarioCreador = $"{usr.Apellido} {usr.Nombre}".Trim(),
                    Activo = true
                };

                int nuevoIdPago = _repoPago.Agregar(pago);

                // 2) Actualizar cuota -> id_pago + estado Pagada
                _repoCuota.AsignarPago(_contrato.IdContrato, _cuota.NroCuota, nuevoIdPago);
                _repoCuota.ActualizarEstado(_contrato.IdContrato, _cuota.NroCuota, "Pagada");

                // 3) Emitir recibo (opcional)
                if (chkEmitirRecibo.Checked)
                {
                    // Acá podrías abrir/emitir el recibo real
                    // Por ahora, mostramos un aviso
                    MessageBox.Show("Recibo emitido (demo).", "Recibo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                MessageBox.Show("Pago registrado correctamente.", "Pagos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PagoGuardado?.Invoke(this, nuevoIdPago);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el pago:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string? ConstruirDetalle(string nroComprobante, string obs)
        {
            var partes = new[]
            {
                string.IsNullOrWhiteSpace(nroComprobante) ? null : $"Comprobante: {nroComprobante}",
                string.IsNullOrWhiteSpace(obs) ? null : obs.Trim()
            }.Where(x => x != null);

            var texto = string.Join(" | ", partes);
            return string.IsNullOrWhiteSpace(texto) ? null : texto;
        }

        private int CalcularMesesContrato()
        {
            var m = ((_contrato.FechaFin.Year - _contrato.FechaInicio.Year) * 12)
                    + (_contrato.FechaFin.Month - _contrato.FechaInicio.Month);
            return Math.Max(1, m);
        }
    }
}
