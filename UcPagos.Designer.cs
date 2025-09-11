using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    partial class UcPagos
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private Panel pnlHeader;
        private Label lblTitulo;
        private Button btnVolver;

        // VISTA 1: Contratos
        private Panel pnlContratos;
        private TextBox txtBuscarContrato;
        private Button btnBuscarContrato;
        private DataGridView dgvContratos;

        // VISTA 2: Cuotas
        private Panel pnlCuotas;
        private Panel pnlResumenContrato;
        private Label lblContratoResumen;
        private Label lblInquilinoResumen;
        private Label lblInmuebleResumen;
        private ComboBox cmbEstadoCuota;
        private ComboBox cmbAnio;
        private Label lblEstadoCuota;
        private Label lblAnio;
        private DataGridView dgvCuotas;

        // VISTA 3: Registrar Pago
        private Panel pnlRegistrar;
        private Label lblTituloPago;
        private Label lblContrato;
        private Label lblInquilino;
        private Label lblCuotaPeriodo;
        private DateTimePicker dtpFecha;
        private Label lblFecha;
        private ComboBox cmbMetodo;
        private Label lblMetodo;
        private TextBox txtComprobante;
        private Label lblComprobante;
        private TextBox txtObs;
        private Label lblObs;
        private CheckBox chkEmitirRecibo;
        private Button btnGuardarPago;
        private Button btnCancelarPago;

        // VISTA 4: Recibo
        private Panel pnlRecibo;
        private Label lblTituloRecibo;
        private RichTextBox rtbRecibo;
        private Button btnCerrarRecibo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitulo = new Label();
            btnVolver = new Button();
            pnlContratos = new Panel();
            txtBuscarContrato = new TextBox();
            btnBuscarContrato = new Button();
            dgvContratos = new DataGridView();
            pnlCuotas = new Panel();
            pnlResumenContrato = new Panel();
            lblContratoResumen = new Label();
            lblInquilinoResumen = new Label();
            lblInmuebleResumen = new Label();
            lblEstadoCuota = new Label();
            cmbEstadoCuota = new ComboBox();
            lblAnio = new Label();
            cmbAnio = new ComboBox();
            dgvCuotas = new DataGridView();
            pnlRegistrar = new Panel();
            lblTituloPago = new Label();
            lblContrato = new Label();
            lblInquilino = new Label();
            lblCuotaPeriodo = new Label();
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            lblMetodo = new Label();
            cmbMetodo = new ComboBox();
            lblComprobante = new Label();
            txtComprobante = new TextBox();
            lblObs = new Label();
            txtObs = new TextBox();
            chkEmitirRecibo = new CheckBox();
            btnGuardarPago = new Button();
            btnCancelarPago = new Button();
            pnlRecibo = new Panel();
            lblTituloRecibo = new Label();
            rtbRecibo = new RichTextBox();
            btnCerrarRecibo = new Button();
            pnlHeader.SuspendLayout();
            pnlContratos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvContratos).BeginInit();
            pnlCuotas.SuspendLayout();
            pnlResumenContrato.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCuotas).BeginInit();
            pnlRegistrar.SuspendLayout();
            pnlRecibo.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.Teal;
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Controls.Add(btnVolver);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 12, 20, 12);
            pnlHeader.Size = new Size(1250, 70);
            pnlHeader.TabIndex = 4;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(78, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Pagos";
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.FlatStyle = FlatStyle.System;
            btnVolver.Location = new Point(1101, 14);
            btnVolver.Margin = new Padding(4);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(125, 40);
            btnVolver.TabIndex = 1;
            btnVolver.Text = "◀ Volver";
            btnVolver.Visible = false;
            // 
            // pnlContratos
            // 
            pnlContratos.BackColor = Color.Teal;
            pnlContratos.Controls.Add(txtBuscarContrato);
            pnlContratos.Controls.Add(btnBuscarContrato);
            pnlContratos.Controls.Add(dgvContratos);
            pnlContratos.Dock = DockStyle.Fill;
            pnlContratos.Location = new Point(0, 70);
            pnlContratos.Margin = new Padding(4);
            pnlContratos.Name = "pnlContratos";
            pnlContratos.Padding = new Padding(30);
            pnlContratos.Size = new Size(1250, 742);
            pnlContratos.TabIndex = 0;
            // 
            // txtBuscarContrato
            // 
            txtBuscarContrato.Location = new Point(30, 30);
            txtBuscarContrato.Margin = new Padding(4);
            txtBuscarContrato.Name = "txtBuscarContrato";
            txtBuscarContrato.PlaceholderText = "Buscar contratos…";
            txtBuscarContrato.Size = new Size(399, 27);
            txtBuscarContrato.TabIndex = 0;
            // 
            // btnBuscarContrato
            // 
            btnBuscarContrato.Location = new Point(450, 28);
            btnBuscarContrato.Margin = new Padding(4);
            btnBuscarContrato.Name = "btnBuscarContrato";
            btnBuscarContrato.Size = new Size(112, 35);
            btnBuscarContrato.TabIndex = 1;
            btnBuscarContrato.Text = "Filtrar";
            // 
            // dgvContratos
            // 
            dgvContratos.AllowUserToAddRows = false;
            dgvContratos.AllowUserToDeleteRows = false;
            dgvContratos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvContratos.BackgroundColor = SystemColors.Control;
            dgvContratos.ColumnHeadersHeight = 29;
            dgvContratos.Location = new Point(30, 98);
            dgvContratos.Margin = new Padding(4);
            dgvContratos.Name = "dgvContratos";
            dgvContratos.ReadOnly = true;
            dgvContratos.RowHeadersVisible = false;
            dgvContratos.RowHeadersWidth = 51;
            dgvContratos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContratos.Size = new Size(1138, 580);
            dgvContratos.TabIndex = 2;
            // 
            // pnlCuotas
            // 
            pnlCuotas.Controls.Add(pnlResumenContrato);
            pnlCuotas.Controls.Add(lblEstadoCuota);
            pnlCuotas.Controls.Add(cmbEstadoCuota);
            pnlCuotas.Controls.Add(lblAnio);
            pnlCuotas.Controls.Add(cmbAnio);
            pnlCuotas.Controls.Add(dgvCuotas);
            pnlCuotas.Dock = DockStyle.Fill;
            pnlCuotas.Location = new Point(0, 70);
            pnlCuotas.Margin = new Padding(4);
            pnlCuotas.Name = "pnlCuotas";
            pnlCuotas.Padding = new Padding(30);
            pnlCuotas.Size = new Size(1250, 742);
            pnlCuotas.TabIndex = 1;
            // 
            // pnlResumenContrato
            // 
            pnlResumenContrato.BackColor = Color.White;
            pnlResumenContrato.Controls.Add(lblContratoResumen);
            pnlResumenContrato.Controls.Add(lblInquilinoResumen);
            pnlResumenContrato.Controls.Add(lblInmuebleResumen);
            pnlResumenContrato.Location = new Point(30, 30);
            pnlResumenContrato.Margin = new Padding(4);
            pnlResumenContrato.Name = "pnlResumenContrato";
            pnlResumenContrato.Padding = new Padding(20);
            pnlResumenContrato.Size = new Size(1188, 112);
            pnlResumenContrato.TabIndex = 0;
            // 
            // lblContratoResumen
            // 
            lblContratoResumen.AutoSize = true;
            lblContratoResumen.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblContratoResumen.Location = new Point(0, 0);
            lblContratoResumen.Margin = new Padding(4, 0, 4, 0);
            lblContratoResumen.Name = "lblContratoResumen";
            lblContratoResumen.Size = new Size(161, 25);
            lblContratoResumen.TabIndex = 0;
            lblContratoResumen.Text = "Contrato C-XXXX";
            // 
            // lblInquilinoResumen
            // 
            lblInquilinoResumen.AutoSize = true;
            lblInquilinoResumen.Location = new Point(0, 38);
            lblInquilinoResumen.Margin = new Padding(4, 0, 4, 0);
            lblInquilinoResumen.Name = "lblInquilinoResumen";
            lblInquilinoResumen.Size = new Size(0, 20);
            lblInquilinoResumen.TabIndex = 1;
            // 
            // lblInmuebleResumen
            // 
            lblInmuebleResumen.AutoSize = true;
            lblInmuebleResumen.Location = new Point(0, 65);
            lblInmuebleResumen.Margin = new Padding(4, 0, 4, 0);
            lblInmuebleResumen.Name = "lblInmuebleResumen";
            lblInmuebleResumen.Size = new Size(0, 20);
            lblInmuebleResumen.TabIndex = 2;
            // 
            // lblEstadoCuota
            // 
            lblEstadoCuota.AutoSize = true;
            lblEstadoCuota.Location = new Point(30, 162);
            lblEstadoCuota.Margin = new Padding(4, 0, 4, 0);
            lblEstadoCuota.Name = "lblEstadoCuota";
            lblEstadoCuota.Size = new Size(57, 20);
            lblEstadoCuota.TabIndex = 1;
            lblEstadoCuota.Text = "Estado:";
            // 
            // cmbEstadoCuota
            // 
            cmbEstadoCuota.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadoCuota.Location = new Point(90, 158);
            cmbEstadoCuota.Margin = new Padding(4);
            cmbEstadoCuota.Name = "cmbEstadoCuota";
            cmbEstadoCuota.Size = new Size(160, 28);
            cmbEstadoCuota.TabIndex = 2;
            // 
            // lblAnio
            // 
            lblAnio.AutoSize = true;
            lblAnio.Location = new Point(270, 162);
            lblAnio.Margin = new Padding(4, 0, 4, 0);
            lblAnio.Name = "lblAnio";
            lblAnio.Size = new Size(39, 20);
            lblAnio.TabIndex = 3;
            lblAnio.Text = "Año:";
            // 
            // cmbAnio
            // 
            cmbAnio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAnio.Location = new Point(310, 158);
            cmbAnio.Margin = new Padding(4);
            cmbAnio.Name = "cmbAnio";
            cmbAnio.Size = new Size(100, 28);
            cmbAnio.TabIndex = 4;
            // 
            // dgvCuotas
            // 
            dgvCuotas.AllowUserToAddRows = false;
            dgvCuotas.AllowUserToDeleteRows = false;
            dgvCuotas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCuotas.BackgroundColor = SystemColors.Control;
            dgvCuotas.ColumnHeadersHeight = 29;
            dgvCuotas.Location = new Point(30, 200);
            dgvCuotas.Margin = new Padding(4);
            dgvCuotas.Name = "dgvCuotas";
            dgvCuotas.ReadOnly = true;
            dgvCuotas.RowHeadersVisible = false;
            dgvCuotas.RowHeadersWidth = 51;
            dgvCuotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCuotas.Size = new Size(1188, 512);
            dgvCuotas.TabIndex = 5;
            // 
            // pnlRegistrar
            // 
            pnlRegistrar.Controls.Add(lblTituloPago);
            pnlRegistrar.Controls.Add(lblContrato);
            pnlRegistrar.Controls.Add(lblInquilino);
            pnlRegistrar.Controls.Add(lblCuotaPeriodo);
            pnlRegistrar.Controls.Add(lblFecha);
            pnlRegistrar.Controls.Add(dtpFecha);
            pnlRegistrar.Controls.Add(lblMetodo);
            pnlRegistrar.Controls.Add(cmbMetodo);
            pnlRegistrar.Controls.Add(lblComprobante);
            pnlRegistrar.Controls.Add(txtComprobante);
            pnlRegistrar.Controls.Add(lblObs);
            pnlRegistrar.Controls.Add(txtObs);
            pnlRegistrar.Controls.Add(chkEmitirRecibo);
            pnlRegistrar.Controls.Add(btnGuardarPago);
            pnlRegistrar.Controls.Add(btnCancelarPago);
            pnlRegistrar.Dock = DockStyle.Fill;
            pnlRegistrar.Location = new Point(0, 70);
            pnlRegistrar.Margin = new Padding(4);
            pnlRegistrar.Name = "pnlRegistrar";
            pnlRegistrar.Padding = new Padding(30);
            pnlRegistrar.Size = new Size(1250, 742);
            pnlRegistrar.TabIndex = 2;
            // 
            // lblTituloPago
            // 
            lblTituloPago.Location = new Point(50, 30);
            lblTituloPago.Margin = new Padding(4, 0, 4, 0);
            lblTituloPago.Name = "lblTituloPago";
            lblTituloPago.Size = new Size(125, 29);
            lblTituloPago.TabIndex = 0;
            // 
            // lblContrato
            // 
            lblContrato.Location = new Point(50, 70);
            lblContrato.Margin = new Padding(4, 0, 4, 0);
            lblContrato.Name = "lblContrato";
            lblContrato.Size = new Size(125, 29);
            lblContrato.TabIndex = 1;
            // 
            // lblInquilino
            // 
            lblInquilino.Location = new Point(50, 98);
            lblInquilino.Margin = new Padding(4, 0, 4, 0);
            lblInquilino.Name = "lblInquilino";
            lblInquilino.Size = new Size(125, 29);
            lblInquilino.TabIndex = 2;
            // 
            // lblCuotaPeriodo
            // 
            lblCuotaPeriodo.Location = new Point(50, 125);
            lblCuotaPeriodo.Margin = new Padding(4, 0, 4, 0);
            lblCuotaPeriodo.Name = "lblCuotaPeriodo";
            lblCuotaPeriodo.Size = new Size(125, 29);
            lblCuotaPeriodo.TabIndex = 3;
            // 
            // lblFecha
            // 
            lblFecha.Location = new Point(50, 175);
            lblFecha.Margin = new Padding(4, 0, 4, 0);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(125, 29);
            lblFecha.TabIndex = 4;
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(50, 202);
            dtpFecha.Margin = new Padding(4);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(249, 27);
            dtpFecha.TabIndex = 5;
            // 
            // lblMetodo
            // 
            lblMetodo.Location = new Point(325, 175);
            lblMetodo.Margin = new Padding(4, 0, 4, 0);
            lblMetodo.Name = "lblMetodo";
            lblMetodo.Size = new Size(125, 29);
            lblMetodo.TabIndex = 6;
            // 
            // cmbMetodo
            // 
            cmbMetodo.Location = new Point(325, 202);
            cmbMetodo.Margin = new Padding(4);
            cmbMetodo.Name = "cmbMetodo";
            cmbMetodo.Size = new Size(150, 28);
            cmbMetodo.TabIndex = 7;
            // 
            // lblComprobante
            // 
            lblComprobante.Location = new Point(50, 256);
            lblComprobante.Margin = new Padding(4, 0, 4, 0);
            lblComprobante.Name = "lblComprobante";
            lblComprobante.Size = new Size(125, 29);
            lblComprobante.TabIndex = 8;
            // 
            // txtComprobante
            // 
            txtComprobante.Location = new Point(50, 284);
            txtComprobante.Margin = new Padding(4);
            txtComprobante.Name = "txtComprobante";
            txtComprobante.Size = new Size(250, 27);
            txtComprobante.TabIndex = 9;
            // 
            // lblObs
            // 
            lblObs.Location = new Point(400, 256);
            lblObs.Margin = new Padding(4, 0, 4, 0);
            lblObs.Name = "lblObs";
            lblObs.Size = new Size(125, 29);
            lblObs.TabIndex = 10;
            // 
            // txtObs
            // 
            txtObs.Location = new Point(400, 284);
            txtObs.Margin = new Padding(4);
            txtObs.Multiline = true;
            txtObs.Name = "txtObs";
            txtObs.Size = new Size(360, 90);
            txtObs.TabIndex = 11;
            // 
            // chkEmitirRecibo
            // 
            chkEmitirRecibo.Location = new Point(50, 400);
            chkEmitirRecibo.Margin = new Padding(4);
            chkEmitirRecibo.Name = "chkEmitirRecibo";
            chkEmitirRecibo.Size = new Size(220, 30);
            chkEmitirRecibo.TabIndex = 12;
            chkEmitirRecibo.Text = "Emitir recibo automáticamente";
            // 
            // btnGuardarPago
            // 
            btnGuardarPago.Location = new Point(50, 450);
            btnGuardarPago.Margin = new Padding(4);
            btnGuardarPago.Name = "btnGuardarPago";
            btnGuardarPago.Size = new Size(140, 36);
            btnGuardarPago.TabIndex = 13;
            btnGuardarPago.Text = "Guardar pago";
            // 
            // btnCancelarPago
            // 
            btnCancelarPago.Location = new Point(210, 450);
            btnCancelarPago.Margin = new Padding(4);
            btnCancelarPago.Name = "btnCancelarPago";
            btnCancelarPago.Size = new Size(120, 36);
            btnCancelarPago.TabIndex = 14;
            btnCancelarPago.Text = "Cancelar";
            // 
            // pnlRecibo
            // 
            pnlRecibo.Controls.Add(lblTituloRecibo);
            pnlRecibo.Controls.Add(rtbRecibo);
            pnlRecibo.Controls.Add(btnCerrarRecibo);
            pnlRecibo.Dock = DockStyle.Fill;
            pnlRecibo.Location = new Point(0, 70);
            pnlRecibo.Margin = new Padding(4);
            pnlRecibo.Name = "pnlRecibo";
            pnlRecibo.Padding = new Padding(30);
            pnlRecibo.Size = new Size(1250, 742);
            pnlRecibo.TabIndex = 3;
            // 
            // lblTituloRecibo
            // 
            lblTituloRecibo.Location = new Point(30, 30);
            lblTituloRecibo.Margin = new Padding(4, 0, 4, 0);
            lblTituloRecibo.Name = "lblTituloRecibo";
            lblTituloRecibo.Size = new Size(250, 29);
            lblTituloRecibo.TabIndex = 0;
            lblTituloRecibo.Text = "Vista previa del recibo";
            // 
            // rtbRecibo
            // 
            rtbRecibo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbRecibo.Location = new Point(30, 70);
            rtbRecibo.Margin = new Padding(4);
            rtbRecibo.Name = "rtbRecibo";
            rtbRecibo.ReadOnly = true;
            rtbRecibo.Size = new Size(1188, 612);
            rtbRecibo.TabIndex = 1;
            rtbRecibo.Text = "";
            // 
            // btnCerrarRecibo
            // 
            btnCerrarRecibo.Location = new Point(30, 690);
            btnCerrarRecibo.Margin = new Padding(4);
            btnCerrarRecibo.Name = "btnCerrarRecibo";
            btnCerrarRecibo.Size = new Size(100, 32);
            btnCerrarRecibo.TabIndex = 2;
            btnCerrarRecibo.Text = "Cerrar";
            // 
            // UcPagos
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.WhiteSmoke;
            Controls.Add(pnlContratos);
            Controls.Add(pnlCuotas);
            Controls.Add(pnlRegistrar);
            Controls.Add(pnlRecibo);
            Controls.Add(pnlHeader);
            Margin = new Padding(4);
            Name = "UcPagos";
            Size = new Size(1250, 812);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlContratos.ResumeLayout(false);
            pnlContratos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvContratos).EndInit();
            pnlCuotas.ResumeLayout(false);
            pnlCuotas.PerformLayout();
            pnlResumenContrato.ResumeLayout(false);
            pnlResumenContrato.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCuotas).EndInit();
            pnlRegistrar.ResumeLayout(false);
            pnlRegistrar.PerformLayout();
            pnlRecibo.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
