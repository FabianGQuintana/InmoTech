using System;
using System.Globalization;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class UcPagos_PagarCuota : UserControl
    {
        // --------- Tipos expuestos ----------
        public sealed class HeaderCuota
        {
            public string Contrato { get; init; } = "";
            public string Inquilino { get; init; } = "";
            public string Inmueble { get; init; } = "";
            public int NroCuota { get; init; }
            public int CantCuotas { get; init; }
            public string Periodo { get; init; } = "";
            public decimal Monto { get; init; }
        }

        public sealed class PagoInput
        {
            public string Contrato { get; init; } = "";
            public int NroCuota { get; init; }
            public DateTime FechaPago { get; init; }
            public string MedioPago { get; init; } = "";
            public string? Comprobante { get; init; }
            public string? Observaciones { get; init; }
            public bool EmitirRecibo { get; init; }
            public decimal Monto { get; init; }
            public string Periodo { get; init; } = "";
        }

        // --------- Eventos ----------
        public event EventHandler<PagoInput>? GuardarPagoClicked;
        public event EventHandler? CancelarClicked;
        public event EventHandler<PagoInput>? VistaPreviaReciboClicked;

        // --------- Estado ----------
        private HeaderCuota? _header;

        public UcPagos_PagarCuota()
        {
            InitializeComponent();

            // Defaults
            dtpFechaPago.Value = DateTime.Today;
            if (cboMedioPago.Items.Count > 0) cboMedioPago.SelectedIndex = 0;

            // Eventos UI
            btnGuardar.Click += (_, __) => OnGuardar();
            btnCancelar.Click += (_, __) => CancelarClicked?.Invoke(this, EventArgs.Empty);
            lnkVistaPrevia.LinkClicked += (_, __) =>
            {
                var inp = BuildInput(validate: false);
                if (inp != null) VistaPreviaReciboClicked?.Invoke(this, inp);
            };
        }

        // --------- API pública ----------
        public void LoadHeader(HeaderCuota h)
        {
            _header = h;

            lblTitulo.Text = $"Contrato Nº: {h.Contrato}";
            lblLinea1.Text = $"Inquilino: {h.Inquilino}";
            lblLinea2.Text = $"Inmueble: {h.Inmueble}";

            var montoFmt = string.Format(CultureInfo.GetCultureInfo("es-AR"), "$ {0:N0}", h.Monto);
            lblLinea3.Text = $"Cuota: {h.NroCuota} / {h.CantCuotas}    |    Período: {h.Periodo}    |    Monto: {montoFmt}";
        }

        // --------- Lógica ----------
        private void OnGuardar()
        {
            var input = BuildInput(validate: true);
            if (input == null) return;

            // Emitir evento con todos los datos para que MainForm guarde en BD
            GuardarPagoClicked?.Invoke(this, input);
        }

        private PagoInput? BuildInput(bool validate)
        {
            if (_header == null)
            {
                MessageBox.Show("No se cargó la cabecera de la cuota.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (validate)
            {
                if (cboMedioPago.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccioná un método de pago.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMedioPago.Focus();
                    return null;
                }
            }

            return new PagoInput
            {
                Contrato = _header.Contrato,
                NroCuota = _header.NroCuota,
                FechaPago = dtpFechaPago.Value.Date,
                MedioPago = cboMedioPago.SelectedItem?.ToString() ?? "",
                Comprobante = string.IsNullOrWhiteSpace(txtComprobante.Text) ? null : txtComprobante.Text.Trim(),
                Observaciones = string.IsNullOrWhiteSpace(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim(),
                EmitirRecibo = chkEmitirRecibo.Checked,
                Monto = _header.Monto,
                Periodo = _header.Periodo
            };
        }
    }
}
