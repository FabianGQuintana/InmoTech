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
            lblTituloContrato = new Label();
            lblLinea1 = new Label();
            lblLinea2 = new Label();
            lblLinea3 = new Label();
            lblFechaPago = new Label();
            dtpFechaPago = new DateTimePicker();
            lblMetodoPago = new Label();
            cboMetodoPago = new ComboBox();
            lblObservaciones = new Label();
            txtObservaciones = new TextBox();
            lblMonto = new Label();
            nudMonto = new NumericUpDown();
            chkEmitirRecibo = new CheckBox();
            lnkVistaPrevia = new LinkLabel();
            btnCancelar = new Button();
            btnGuardar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudMonto).BeginInit();
            SuspendLayout();
            // 
            // lblTituloContrato
            // 
            lblTituloContrato.AutoSize = true;
            lblTituloContrato.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTituloContrato.Location = new Point(40, 30);
            lblTituloContrato.Name = "lblTituloContrato";
            lblTituloContrato.Size = new Size(334, 45);
            lblTituloContrato.TabIndex = 0;
            lblTituloContrato.Text = "Contrato N°: C-XXXX";
            // 
            // lblLinea1
            // 
            lblLinea1.AutoSize = true;
            lblLinea1.Font = new Font("Segoe UI", 10F);
            lblLinea1.Location = new Point(44, 74);
            lblLinea1.Name = "lblLinea1";
            lblLinea1.Size = new Size(0, 28);
            lblLinea1.TabIndex = 1;
            // 
            // lblLinea2
            // 
            lblLinea2.AutoSize = true;
            lblLinea2.Font = new Font("Segoe UI", 10F);
            lblLinea2.Location = new Point(44, 99);
            lblLinea2.Name = "lblLinea2";
            lblLinea2.Size = new Size(0, 28);
            lblLinea2.TabIndex = 2;
            // 
            // lblLinea3
            // 
            lblLinea3.AutoSize = true;
            lblLinea3.Font = new Font("Segoe UI", 10F);
            lblLinea3.Location = new Point(44, 124);
            lblLinea3.Name = "lblLinea3";
            lblLinea3.Size = new Size(0, 28);
            lblLinea3.TabIndex = 3;
            // 
            // lblFechaPago
            // 
            lblFechaPago.AutoSize = true;
            lblFechaPago.Location = new Point(44, 172);
            lblFechaPago.Name = "lblFechaPago";
            lblFechaPago.Size = new Size(129, 25);
            lblFechaPago.TabIndex = 4;
            lblFechaPago.Text = "Fecha de pago";
            // 
            // dtpFechaPago
            // 
            dtpFechaPago.Format = DateTimePickerFormat.Short;
            dtpFechaPago.Location = new Point(44, 200);
            dtpFechaPago.Name = "dtpFechaPago";
            dtpFechaPago.Size = new Size(180, 31);
            dtpFechaPago.TabIndex = 5;
            // 
            // lblMetodoPago
            // 
            lblMetodoPago.AutoSize = true;
            lblMetodoPago.Location = new Point(250, 172);
            lblMetodoPago.Name = "lblMetodoPago";
            lblMetodoPago.Size = new Size(148, 25);
            lblMetodoPago.TabIndex = 6;
            lblMetodoPago.Text = "Método de pago";
            // 
            // cboMetodoPago
            // 
            cboMetodoPago.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMetodoPago.Location = new Point(250, 200);
            cboMetodoPago.Name = "cboMetodoPago";
            cboMetodoPago.Size = new Size(220, 33);
            cboMetodoPago.TabIndex = 7;
            // 
            // lblObservaciones
            // 
            lblObservaciones.AutoSize = true;
            lblObservaciones.Location = new Point(44, 255);
            lblObservaciones.Name = "lblObservaciones";
            lblObservaciones.Size = new Size(128, 25);
            lblObservaciones.TabIndex = 8;
            lblObservaciones.Text = "Observaciones";
            // 
            // txtObservaciones
            // 
            txtObservaciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtObservaciones.Location = new Point(44, 283);
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.ScrollBars = ScrollBars.Vertical;
            txtObservaciones.Size = new Size(330, 97);
            txtObservaciones.TabIndex = 9;
            // 
            // lblMonto
            // 
            lblMonto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(500, 172);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(66, 25);
            lblMonto.TabIndex = 10;
            lblMonto.Text = "Monto";
            // 
            // nudMonto
            // 
            nudMonto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nudMonto.DecimalPlaces = 2;
            nudMonto.Location = new Point(500, 200);
            nudMonto.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudMonto.Name = "nudMonto";
            nudMonto.Size = new Size(260, 31);
            nudMonto.TabIndex = 11;
            nudMonto.ThousandsSeparator = true;
            // 
            // chkEmitirRecibo
            // 
            chkEmitirRecibo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkEmitirRecibo.AutoSize = true;
            chkEmitirRecibo.Location = new Point(44, 445);
            chkEmitirRecibo.Name = "chkEmitirRecibo";
            chkEmitirRecibo.Size = new Size(280, 29);
            chkEmitirRecibo.TabIndex = 12;
            chkEmitirRecibo.Text = "Emitir recibo automáticamente";
            // 
            // lnkVistaPrevia
            // 
            lnkVistaPrevia.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lnkVistaPrevia.AutoSize = true;
            lnkVistaPrevia.Location = new Point(44, 477);
            lnkVistaPrevia.Name = "lnkVistaPrevia";
            lnkVistaPrevia.Size = new Size(186, 25);
            lnkVistaPrevia.TabIndex = 13;
            lnkVistaPrevia.TabStop = true;
            lnkVistaPrevia.Text = "Vista previa del recibo";
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.BackColor = Color.WhiteSmoke;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Location = new Point(510, 480);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 36);
            btnCancelar.TabIndex = 14;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(640, 480);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(150, 36);
            btnGuardar.TabIndex = 15;
            btnGuardar.Text = "Guardar pago";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // UcPagos_PagarCuota
            // 
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
            Controls.Add(lblObservaciones);
            Controls.Add(txtObservaciones);
            Controls.Add(lblMonto);
            Controls.Add(nudMonto);
            Controls.Add(chkEmitirRecibo);
            Controls.Add(lnkVistaPrevia);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Name = "UcPagos_PagarCuota";
            Size = new Size(800, 550);
            ((System.ComponentModel.ISupportInitialize)nudMonto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}