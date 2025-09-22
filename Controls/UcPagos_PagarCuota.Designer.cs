namespace InmoTech
{
    partial class UcPagos_PagarCuota
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.root = new System.Windows.Forms.TableLayoutPanel();
            this.card = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblLinea1 = new System.Windows.Forms.Label();
            this.lblLinea2 = new System.Windows.Forms.Label();
            this.lblLinea3 = new System.Windows.Forms.Label();
            this.grpForm = new System.Windows.Forms.TableLayoutPanel();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
            this.lblMedio = new System.Windows.Forms.Label();
            this.cboMedioPago = new System.Windows.Forms.ComboBox();
            this.lblComp = new System.Windows.Forms.Label();
            this.txtComprobante = new System.Windows.Forms.TextBox();
            this.lblObs = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.chkEmitirRecibo = new System.Windows.Forms.CheckBox();
            this.lnkVistaPrevia = new System.Windows.Forms.LinkLabel();
            this.pnlAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.root.SuspendLayout();
            this.card.SuspendLayout();
            this.grpForm.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // root
            // 
            this.root.ColumnCount = 3;
            this.root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86F));
            this.root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.root.Controls.Add(this.card, 1, 1);
            this.root.Dock = System.Windows.Forms.DockStyle.Fill;
            this.root.RowCount = 3;
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.root.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.root.BackColor = System.Drawing.Color.WhiteSmoke;
            this.root.Location = new System.Drawing.Point(0, 0);
            this.root.Name = "root";
            this.root.Size = new System.Drawing.Size(980, 620);
            this.root.TabIndex = 0;
            // 
            // card
            // 
            this.card.BackColor = System.Drawing.Color.White;
            this.card.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card.Controls.Add(this.lblTitulo);
            this.card.Controls.Add(this.lblLinea1);
            this.card.Controls.Add(this.lblLinea2);
            this.card.Controls.Add(this.lblLinea3);
            this.card.Controls.Add(this.grpForm);
            this.card.Controls.Add(this.chkEmitirRecibo);
            this.card.Controls.Add(this.lnkVistaPrevia);
            this.card.Controls.Add(this.pnlAcciones);
            this.card.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card.Location = new System.Drawing.Point(71, 49);
            this.card.Margin = new System.Windows.Forms.Padding(8);
            this.card.Name = "card";
            this.card.Padding = new System.Windows.Forms.Padding(18);
            this.card.Size = new System.Drawing.Size(838, 522);
            this.card.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoEllipsis = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(700, 36);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Contrato Nº: —";
            // 
            // lblLinea1
            // 
            this.lblLinea1.AutoSize = true;
            this.lblLinea1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblLinea1.Location = new System.Drawing.Point(24, 58);
            this.lblLinea1.Name = "lblLinea1";
            this.lblLinea1.Size = new System.Drawing.Size(195, 19);
            this.lblLinea1.TabIndex = 1;
            this.lblLinea1.Text = "Inquilino: —";
            // 
            // lblLinea2
            // 
            this.lblLinea2.AutoSize = true;
            this.lblLinea2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblLinea2.Location = new System.Drawing.Point(24, 80);
            this.lblLinea2.Name = "lblLinea2";
            this.lblLinea2.Size = new System.Drawing.Size(187, 19);
            this.lblLinea2.TabIndex = 2;
            this.lblLinea2.Text = "Inmueble: —";
            // 
            // lblLinea3
            // 
            this.lblLinea3.AutoSize = true;
            this.lblLinea3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblLinea3.Location = new System.Drawing.Point(24, 102);
            this.lblLinea3.Name = "lblLinea3";
            this.lblLinea3.Size = new System.Drawing.Size(233, 19);
            this.lblLinea3.TabIndex = 3;
            this.lblLinea3.Text = "Cuota: — | Período: — | Monto: —";
            // 
            // grpForm
            // 
            this.grpForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpForm.ColumnCount = 4;
            this.grpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.grpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.grpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.grpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.grpForm.Controls.Add(this.lblFecha, 0, 0);
            this.grpForm.Controls.Add(this.dtpFechaPago, 1, 0);
            this.grpForm.Controls.Add(this.lblMedio, 2, 0);
            this.grpForm.Controls.Add(this.cboMedioPago, 3, 0);
            this.grpForm.Controls.Add(this.lblComp, 0, 1);
            this.grpForm.Controls.Add(this.txtComprobante, 1, 1);
            this.grpForm.Controls.Add(this.lblObs, 0, 2);
            this.grpForm.Controls.Add(this.txtObservaciones, 1, 2);
            this.grpForm.Location = new System.Drawing.Point(20, 140);
            this.grpForm.Name = "grpForm";
            this.grpForm.RowCount = 3;
            this.grpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.grpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.grpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.grpForm.Size = new System.Drawing.Size(798, 220);
            this.grpForm.TabIndex = 4;
            // 
            // lblFecha
            // 
            this.lblFecha.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblFecha.Location = new System.Drawing.Point(3, 8);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(98, 19);
            this.lblFecha.TabIndex = 0;
            this.lblFecha.Text = "Fecha de pago";
            // 
            // dtpFechaPago
            // 
            this.dtpFechaPago.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaPago.Location = new System.Drawing.Point(163, 6);
            this.dtpFechaPago.Name = "dtpFechaPago";
            this.dtpFechaPago.Size = new System.Drawing.Size(254, 23);
            this.dtpFechaPago.TabIndex = 1;
            // 
            // lblMedio
            // 
            this.lblMedio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMedio.AutoSize = true;
            this.lblMedio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblMedio.Location = new System.Drawing.Point(423, 8);
            this.lblMedio.Name = "lblMedio";
            this.lblMedio.Size = new System.Drawing.Size(111, 19);
            this.lblMedio.TabIndex = 2;
            this.lblMedio.Text = "Método de pago";
            // 
            // cboMedioPago
            // 
            this.cboMedioPago.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMedioPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMedioPago.FormattingEnabled = true;
            this.cboMedioPago.Items.AddRange(new object[] {
            "Efectivo",
            "Transferencia",
            "Tarjeta",
            "Depósito",
            "Mercado Pago"});
            this.cboMedioPago.Location = new System.Drawing.Point(583, 6);
            this.cboMedioPago.Name = "cboMedioPago";
            this.cboMedioPago.Size = new System.Drawing.Size(212, 23);
            this.cboMedioPago.TabIndex = 3;
            // 
            // lblComp
            // 
            this.lblComp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblComp.AutoSize = true;
            this.lblComp.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblComp.Location = new System.Drawing.Point(3, 44);
            this.lblComp.Name = "lblComp";
            this.lblComp.Size = new System.Drawing.Size(150, 19);
            this.lblComp.TabIndex = 4;
            this.lblComp.Text = "Número de comprobante";
            // 
            // txtComprobante
            // 
            this.txtComprobante.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.grpForm.SetColumnSpan(this.txtComprobante, 3);
            this.txtComprobante.Location = new System.Drawing.Point(163, 42);
            this.txtComprobante.Name = "txtComprobante";
            this.txtComprobante.PlaceholderText = "Opcional";
            this.txtComprobante.Size = new System.Drawing.Size(632, 23);
            this.txtComprobante.TabIndex = 5;
            // 
            // lblObs
            // 
            this.lblObs.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblObs.AutoSize = true;
            this.lblObs.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblObs.Location = new System.Drawing.Point(3, 144);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(93, 19);
            this.lblObs.TabIndex = 6;
            this.lblObs.Text = "Observaciones";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpForm.SetColumnSpan(this.txtObservaciones, 3);
            this.txtObservaciones.Location = new System.Drawing.Point(163, 75);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(632, 142);
            this.txtObservaciones.TabIndex = 7;
            // 
            // chkEmitirRecibo
            // 
            this.chkEmitirRecibo.AutoSize = true;
            this.chkEmitirRecibo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkEmitirRecibo.Location = new System.Drawing.Point(24, 374);
            this.chkEmitirRecibo.Name = "chkEmitirRecibo";
            this.chkEmitirRecibo.Size = new System.Drawing.Size(181, 23);
            this.chkEmitirRecibo.TabIndex = 5;
            this.chkEmitirRecibo.Text = "Emitir recibo automáticamente";
            this.chkEmitirRecibo.UseVisualStyleBackColor = true;
            // 
            // lnkVistaPrevia
            // 
            this.lnkVistaPrevia.AutoSize = true;
            this.lnkVistaPrevia.Location = new System.Drawing.Point(24, 403);
            this.lnkVistaPrevia.Name = "lnkVistaPrevia";
            this.lnkVistaPrevia.Size = new System.Drawing.Size(117, 15);
            this.lnkVistaPrevia.TabIndex = 6;
            this.lnkVistaPrevia.TabStop = true;
            this.lnkVistaPrevia.Text = "Vista previa del recibo";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAcciones.AutoSize = true;
            this.pnlAcciones.Controls.Add(this.btnGuardar);
            this.pnlAcciones.Controls.Add(this.btnCancelar);
            this.pnlAcciones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlAcciones.Location = new System.Drawing.Point(558, 452);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(260, 38);
            this.pnlAcciones.TabIndex = 7;
            // 
            // btnGuardar
            // 
            this.btnGuardar.AutoSize = true;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(137, 3);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnGuardar.Size = new System.Drawing.Size(117, 32);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar pago";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = true;
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btnCancelar.Location = new System.Drawing.Point(6, 3);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.btnCancelar.Size = new System.Drawing.Size(119, 32);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // UcPagos_PagarCuota
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.root);
            this.Name = "UcPagos_PagarCuota";
            this.Size = new System.Drawing.Size(980, 620);
            this.root.ResumeLayout(false);
            this.card.ResumeLayout(false);
            this.card.PerformLayout();
            this.grpForm.ResumeLayout(false);
            this.grpForm.PerformLayout();
            this.pnlAcciones.ResumeLayout(false);
            this.pnlAcciones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel root;
        private System.Windows.Forms.Panel card;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblLinea1;
        private System.Windows.Forms.Label lblLinea2;
        private System.Windows.Forms.Label lblLinea3;
        private System.Windows.Forms.TableLayoutPanel grpForm;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFechaPago;
        private System.Windows.Forms.Label lblMedio;
        private System.Windows.Forms.ComboBox cboMedioPago;
        private System.Windows.Forms.Label lblComp;
        private System.Windows.Forms.TextBox txtComprobante;
        private System.Windows.Forms.Label lblObs;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.CheckBox chkEmitirRecibo;
        private System.Windows.Forms.LinkLabel lnkVistaPrevia;
        private System.Windows.Forms.FlowLayoutPanel pnlAcciones;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
