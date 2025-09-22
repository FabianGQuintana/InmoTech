using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    partial class UcPagos_Recibo
    {
        private System.ComponentModel.IContainer components = null;

        private Panel cardHeader;
        private Label lblTitulo;
        private Label lblContrato;
        private Label lblCuota;
        private Label lblInquilino;
        private Label lblInmueble;

        private Panel cardDatos;
        private Label lblFecha;
        private Label lblMedio;
        private Label lblImporteTitulo;
        private Label lblImporte;

        private FlowLayoutPanel actionsPanel;
        private Button btnImprimir;
        private Button btnDescargarPdf;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            SuspendLayout();

            BackColor = Color.White;
            AutoScaleMode = AutoScaleMode.Dpi;
            Name = "UcPagos_Recibo";
            Size = new Size(1000, 640);

            // Card Header
            cardHeader = new Panel { Location = new Point(20, 16), Size = new Size(960, 120), Padding = new Padding(16) };
            cardHeader.Paint += (s, e) => { using var p = new Pen(Color.FromArgb(235, 235, 235)); e.Graphics.DrawRectangle(p, 0, 0, cardHeader.Width - 1, cardHeader.Height - 1); };

            lblTitulo = new Label { AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), Text = "Recibo #—" };
            lblContrato = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), Location = new Point(18, 54), Text = "Contrato: —" };
            lblCuota = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), Location = new Point(18, 76), Text = "Cuota: —" };
            lblInquilino = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), Location = new Point(480, 54), Text = "Inquilino: —" };
            lblInmueble = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), Location = new Point(480, 76), Text = "Inmueble: —" };

            cardHeader.Controls.Add(lblTitulo);
            cardHeader.Controls.Add(lblContrato);
            cardHeader.Controls.Add(lblCuota);
            cardHeader.Controls.Add(lblInquilino);
            cardHeader.Controls.Add(lblInmueble);

            // Card Datos
            cardDatos = new Panel { Location = new Point(20, 148), Size = new Size(960, 140), Padding = new Padding(16) };
            cardDatos.Paint += (s, e) => { using var p = new Pen(Color.FromArgb(235, 235, 235)); e.Graphics.DrawRectangle(p, 0, 0, cardDatos.Width - 1, cardDatos.Height - 1); };

            lblFecha = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), Location = new Point(18, 18), Text = "Fecha de pago: —" };
            lblMedio = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), Location = new Point(18, 42), Text = "Medio de pago: —" };
            lblImporteTitulo = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), Location = new Point(18, 80), Text = "Importe:" };
            lblImporte = new Label { AutoSize = true, Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(90, 72), Text = "$0" };

            cardDatos.Controls.Add(lblFecha);
            cardDatos.Controls.Add(lblMedio);
            cardDatos.Controls.Add(lblImporteTitulo);
            cardDatos.Controls.Add(lblImporte);

            // Acciones
            actionsPanel = new FlowLayoutPanel
            {
                Location = new Point(20, 300),
                Size = new Size(960, 40),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            btnImprimir = new Button { Text = "Imprimir", AutoSize = true, FlatStyle = FlatStyle.Standard };
            btnDescargarPdf = new Button { Text = "Descargar PDF", AutoSize = true, FlatStyle = FlatStyle.Standard };
            btnImprimir.Click += (_, __) => MessageBox.Show("Función de impresión a implementar.");
            btnDescargarPdf.Click += (_, __) => MessageBox.Show("Función de exportar PDF a implementar.");

            actionsPanel.Controls.Add(btnImprimir);
            actionsPanel.Controls.Add(btnDescargarPdf);

            Controls.Add(cardHeader);
            Controls.Add(cardDatos);
            Controls.Add(actionsPanel);

            ResumeLayout(false);
        }
    }
}
