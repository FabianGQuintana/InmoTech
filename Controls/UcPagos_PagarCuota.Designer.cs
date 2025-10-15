using System.Drawing;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    partial class UcPagos_PagarCuota
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTituloContrato;
        private Label lblLinea1;
        private Label lblLinea2;
        private Label lblLinea3;

        private Label lblFechaPago;
        private DateTimePicker dtpFechaPago;

        private Label lblMetodoPago;
        private ComboBox cboMetodoPago;

        private Label lblComprobante;
        private TextBox txtComprobante;

        private Label lblObservaciones;
        private TextBox txtObservaciones;

        private Label lblMonto;
        private NumericUpDown nudMonto;

        private CheckBox chkEmitirRecibo;
        private LinkLabel lnkVistaPrevia;

        private Button btnCancelar;
        private Button btnGuardar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblTituloContrato = new Label();
            lblLinea1 = new Label();
            lblLinea2 = new Label();
            lblLinea3 = new Label();

            lblFechaPago = new Label();
            dtpFechaPago = new DateTimePicker();

            lblMetodoPago = new Label();
            cboMetodoPago = new ComboBox();

            lblComprobante = new Label();
            txtComprobante = new TextBox();

            lblObservaciones = new Label();
            txtObservaciones = new TextBox();

            lblMonto = new Label();
            nudMonto = new NumericUpDown();

            chkEmitirRecibo = new CheckBox();
            lnkVistaPrevia = new LinkLabel();

            btnCancelar = new Button();
            btnGuardar = new Button();

            SuspendLayout();

            // Título
            lblTituloContrato.AutoSize = true;
            lblTituloContrato.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTituloContrato.Location = new Point(32, 26);
            lblTituloContrato.Text = "Contrato N°: C-XXXX";

            // Subtítulos
            lblLinea1.AutoSize = true;
            lblLinea1.Font = new Font("Segoe UI", 10F);
            lblLinea1.Location = new Point(36, 70);

            lblLinea2.AutoSize = true;
            lblLinea2.Font = new Font("Segoe UI", 10F);
            lblLinea2.Location = new Point(36, 95);

            lblLinea3.AutoSize = true;
            lblLinea3.Font = new Font("Segoe UI", 10F);
            lblLinea3.Location = new Point(36, 120);

            // Fecha pago
            lblFechaPago.AutoSize = true;
            lblFechaPago.Location = new Point(36, 170);
            lblFechaPago.Text = "Fecha de pago";

            dtpFechaPago.Format = DateTimePickerFormat.Short;
            dtpFechaPago.Location = new Point(36, 190);
            dtpFechaPago.Width = 160;

            // Método pago
            lblMetodoPago.AutoSize = true;
            lblMetodoPago.Location = new Point(240, 170);
            lblMetodoPago.Text = "Método de pago";

            cboMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMetodoPago.Location = new Point(240, 190);
            cboMetodoPago.Width = 220;

            // Comprobante
            lblComprobante.AutoSize = true;
            lblComprobante.Location = new Point(36, 235);
            lblComprobante.Text = "Número de comprobante";

            txtComprobante.Location = new Point(36, 255);
            txtComprobante.Width = 424;

            // Observaciones
            lblObservaciones.AutoSize = true;
            lblObservaciones.Location = new Point(36, 300);
            lblObservaciones.Text = "Observaciones";

            txtObservaciones.Location = new Point(36, 320);
            txtObservaciones.Multiline = true;
            txtObservaciones.ScrollBars = ScrollBars.Vertical;
            txtObservaciones.Size = new Size(640, 140);

            // Monto
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(500, 170);
            lblMonto.Text = "Monto";

            nudMonto.DecimalPlaces = 2;
            nudMonto.Maximum = 1000000000;
            nudMonto.Minimum = 0;
            nudMonto.Location = new Point(500, 190);
            nudMonto.Width = 176;
            nudMonto.ThousandsSeparator = true;

            // Recibo
            chkEmitirRecibo.AutoSize = true;
            chkEmitirRecibo.Location = new Point(36, 478);
            chkEmitirRecibo.Text = "Emitir recibo automáticamente";

            lnkVistaPrevia.AutoSize = true;
            lnkVistaPrevia.Location = new Point(36, 505);
            lnkVistaPrevia.Text = "Vista previa del recibo";

            // Botones
            btnCancelar.Text = "Cancelar";
            btnCancelar.BackColor = Color.WhiteSmoke;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Location = new Point(480, 540);
            btnCancelar.Size = new Size(120, 36);

            btnGuardar.Text = "Guardar pago";
            btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Location = new Point(610, 540);
            btnGuardar.Size = new Size(150, 36);

            // Control
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            Controls.Add(lblTituloContrato);
            Controls.Add(lblLinea1);
            Controls.Add(lblLinea2);
            Controls.Add(lblLinea3);

            Controls.Add(lblFechaPago);
            Controls.Add(dtpFechaPago);

            Controls.Add(lblMetodoPago);
            Controls.Add(cboMetodoPago);

            Controls.Add(lblComprobante);
            Controls.Add(txtComprobante);

            Controls.Add(lblObservaciones);
            Controls.Add(txtObservaciones);

            Controls.Add(lblMonto);
            Controls.Add(nudMonto);

            Controls.Add(chkEmitirRecibo);
            Controls.Add(lnkVistaPrevia);

            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);

            Name = "UcPagos_PagarCuota";
            Size = new Size(800, 600);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
