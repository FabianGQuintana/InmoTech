using System;
using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class UcPagos_Recibo : UserControl
    {
        public class ReciboVm
        {
            public string Contrato { get; set; } = "";
            public int NroCuota { get; set; }
            public string Periodo { get; set; } = "";
            public DateTime FechaPago { get; set; }
            public string MedioPago { get; set; } = "";   // Efectivo/Transferencia/…
            public decimal Importe { get; set; }
            public string Inquilino { get; set; } = "";
            public string Inmueble { get; set; } = "";
            public string NroRecibo { get; set; } = "";
        }

        public UcPagos_Recibo()
        {
            InitializeComponent();
        }

        public void LoadRecibo(ReciboVm r)
        {
            lblTitulo.Text = $"Recibo #{r.NroRecibo}";
            lblContrato.Text = $"Contrato: {r.Contrato}";
            lblCuota.Text = $"Cuota: {r.NroCuota} — {r.Periodo}";
            lblFecha.Text = $"Fecha de pago: {r.FechaPago:dd/MM/yyyy}";
            lblMedio.Text = $"Medio de pago: {r.MedioPago}";
            lblImporte.Text = r.Importe.ToString("$#,0.##");
            lblInquilino.Text = $"Inquilino: {r.Inquilino}";
            lblInmueble.Text = $"Inmueble: {r.Inmueble}";
        }
    }
}
