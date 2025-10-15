using System.Drawing;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    partial class UcRecibo
    {
        private System.ComponentModel.IContainer components = null;

        // Controles
        private Label lblTitulo;
        private Label lblNroComprobante;
        private Label lblFechaEmision;
        private Label lblNombreInquilino;
        private Label lblDireccionInmueble;
        private Label lblConceptoHeader;
        private Label lblMontoHeader;
        private Label lblConcepto;
        private Label lblMonto;
        private Label lblMontoTotalTexto;
        private Label lblMontoTotal;
        private Label lblObservaciones;
        private Label lblFormaPago;
        private Button btnImprimir;
        private Button btnCerrar;

        // Paneles de diseño para adaptabilidad
        private TableLayoutPanel tblLayoutPrincipal;
        private Panel pnlHeader;
        private Panel pnlDetallesCliente;
        private TableLayoutPanel tblDetails;
        private Panel pnlFooter;
        private Panel pnlBotones;
        private Panel lineaSeparadora1;
        private Panel lineaSeparadora2;
        private Panel lineaSeparadora3;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblNroComprobante = new System.Windows.Forms.Label();
            this.lblFechaEmision = new System.Windows.Forms.Label();
            this.lblNombreInquilino = new System.Windows.Forms.Label();
            this.lblDireccionInmueble = new System.Windows.Forms.Label();
            this.lblConceptoHeader = new System.Windows.Forms.Label();
            this.lblMontoHeader = new System.Windows.Forms.Label();
            this.lblConcepto = new System.Windows.Forms.Label();
            this.lblMonto = new System.Windows.Forms.Label();
            this.lblMontoTotalTexto = new System.Windows.Forms.Label();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.lblFormaPago = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.tblLayoutPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lineaSeparadora1 = new System.Windows.Forms.Panel();
            this.pnlDetallesCliente = new System.Windows.Forms.Panel();
            this.tblDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lineaSeparadora2 = new System.Windows.Forms.Panel();
            this.lineaSeparadora3 = new System.Windows.Forms.Panel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.tblLayoutPrincipal.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlDetallesCliente.SuspendLayout();
            this.tblDetails.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(221, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "RECIBO DE PAGO";
            // 
            // lblNroComprobante
            // 
            this.lblNroComprobante.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNroComprobante.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNroComprobante.Location = new System.Drawing.Point(410, 2);
            this.lblNroComprobante.Name = "lblNroComprobante";
            this.lblNroComprobante.Size = new System.Drawing.Size(300, 21);
            this.lblNroComprobante.TabIndex = 1;
            this.lblNroComprobante.Text = "RECIBO Nº: PENDIENTE";
            this.lblNroComprobante.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFechaEmision
            // 
            this.lblFechaEmision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFechaEmision.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaEmision.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(110)))), ((int)(((byte)(114)))));
            this.lblFechaEmision.Location = new System.Drawing.Point(410, 26);
            this.lblFechaEmision.Name = "lblFechaEmision";
            this.lblFechaEmision.Size = new System.Drawing.Size(300, 19);
            this.lblFechaEmision.TabIndex = 2;
            this.lblFechaEmision.Text = "Fecha de Emisión: 15/10/2025";
            this.lblFechaEmision.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblNombreInquilino
            // 
            this.lblNombreInquilino.AutoSize = true;
            this.lblNombreInquilino.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreInquilino.Location = new System.Drawing.Point(0, 0);
            this.lblNombreInquilino.Name = "lblNombreInquilino";
            this.lblNombreInquilino.Size = new System.Drawing.Size(130, 17);
            this.lblNombreInquilino.TabIndex = 3;
            this.lblNombreInquilino.Text = "Recibí de: Romero Juan";
            // 
            // lblDireccionInmueble
            // 
            this.lblDireccionInmueble.AutoSize = true;
            this.lblDireccionInmueble.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccionInmueble.Location = new System.Drawing.Point(0, 27);
            this.lblDireccionInmueble.Name = "lblDireccionInmueble";
            this.lblDireccionInmueble.Size = new System.Drawing.Size(342, 17);
            this.lblDireccionInmueble.TabIndex = 4;
            this.lblDireccionInmueble.Text = "En concepto de alquiler del inmueble sito en: Alvear 1232";
            // 
            // lblConceptoHeader
            // 
            this.lblConceptoHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConceptoHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConceptoHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(110)))), ((int)(((byte)(114)))));
            this.lblConceptoHeader.Location = new System.Drawing.Point(3, 0);
            this.lblConceptoHeader.Name = "lblConceptoHeader";
            this.lblConceptoHeader.Size = new System.Drawing.Size(524, 30);
            this.lblConceptoHeader.TabIndex = 5;
            this.lblConceptoHeader.Text = "CONCEPTO";
            this.lblConceptoHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMontoHeader
            // 
            this.lblMontoHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMontoHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(110)))), ((int)(((byte)(114)))));
            this.lblMontoHeader.Location = new System.Drawing.Point(533, 0);
            this.lblMontoHeader.Name = "lblMontoHeader";
            this.lblMontoHeader.Size = new System.Drawing.Size(174, 30);
            this.lblMontoHeader.TabIndex = 6;
            this.lblMontoHeader.Text = "MONTO";
            this.lblMontoHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblConcepto
            // 
            this.lblConcepto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConcepto.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConcepto.Location = new System.Drawing.Point(3, 31);
            this.lblConcepto.Name = "lblConcepto";
            this.lblConcepto.Size = new System.Drawing.Size(524, 30);
            this.lblConcepto.TabIndex = 7;
            this.lblConcepto.Text = "Pago de alquiler - Cuota N°2";
            this.lblConcepto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMonto
            // 
            this.lblMonto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMonto.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonto.Location = new System.Drawing.Point(533, 31);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(174, 30);
            this.lblMonto.TabIndex = 8;
            this.lblMonto.Text = "$ 600.000,00";
            this.lblMonto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMontoTotalTexto
            // 
            this.lblMontoTotalTexto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMontoTotalTexto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoTotalTexto.Location = new System.Drawing.Point(3, 62);
            this.lblMontoTotalTexto.Name = "lblMontoTotalTexto";
            this.lblMontoTotalTexto.Size = new System.Drawing.Size(524, 34);
            this.lblMontoTotalTexto.TabIndex = 14;
            this.lblMontoTotalTexto.Text = "TOTAL:";
            this.lblMontoTotalTexto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMontoTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoTotal.Location = new System.Drawing.Point(533, 62);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Size = new System.Drawing.Size(174, 34);
            this.lblMontoTotal.TabIndex = 9;
            this.lblMontoTotal.Text = "$ 600.000,00";
            this.lblMontoTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblObservaciones.Location = new System.Drawing.Point(0, 24);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(100, 15);
            this.lblObservaciones.TabIndex = 11;
            this.lblObservaciones.Text = "Sin observaciones.";
            // 
            // lblFormaPago
            // 
            this.lblFormaPago.AutoSize = true;
            this.lblFormaPago.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFormaPago.Location = new System.Drawing.Point(0, 0);
            this.lblFormaPago.Name = "lblFormaPago";
            this.lblFormaPago.Size = new System.Drawing.Size(130, 15);
            this.lblFormaPago.TabIndex = 10;
            this.lblFormaPago.Text = "Forma de Pago: Efectivo";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(590, 0);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(120, 38);
            this.btnImprimir.TabIndex = 13;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.White;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.btnCerrar.FlatAppearance.BorderSize = 2;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.btnCerrar.Location = new System.Drawing.Point(464, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 38);
            this.btnCerrar.TabIndex = 12;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            // 
            // tblLayoutPrincipal
            // 
            this.tblLayoutPrincipal.ColumnCount = 1;
            this.tblLayoutPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutPrincipal.Controls.Add(this.pnlHeader, 0, 0);
            this.tblLayoutPrincipal.Controls.Add(this.lineaSeparadora1, 0, 1);
            this.tblLayoutPrincipal.Controls.Add(this.pnlDetallesCliente, 0, 2);
            this.tblLayoutPrincipal.Controls.Add(this.tblDetails, 0, 3);
            this.tblLayoutPrincipal.Controls.Add(this.pnlFooter, 0, 4);
            this.tblLayoutPrincipal.Controls.Add(this.pnlBotones, 0, 5);
            this.tblLayoutPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tblLayoutPrincipal.Name = "tblLayoutPrincipal";
            this.tblLayoutPrincipal.Padding = new System.Windows.Forms.Padding(25);
            this.tblLayoutPrincipal.RowCount = 6;
            this.tblLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblLayoutPrincipal.Size = new System.Drawing.Size(760, 500);
            this.tblLayoutPrincipal.TabIndex = 0;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.lblNroComprobante);
            this.pnlHeader.Controls.Add(this.lblFechaEmision);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(28, 28);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(704, 44);
            this.pnlHeader.TabIndex = 0;
            // 
            // lineaSeparadora1
            // 
            this.lineaSeparadora1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.lineaSeparadora1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineaSeparadora1.Location = new System.Drawing.Point(28, 78);
            this.lineaSeparadora1.Name = "lineaSeparadora1";
            this.lineaSeparadora1.Size = new System.Drawing.Size(704, 14);
            this.lineaSeparadora1.TabIndex = 1;
            // 
            // pnlDetallesCliente
            // 
            this.pnlDetallesCliente.Controls.Add(this.lblNombreInquilino);
            this.pnlDetallesCliente.Controls.Add(this.lblDireccionInmueble);
            this.pnlDetallesCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetallesCliente.Location = new System.Drawing.Point(28, 98);
            this.pnlDetallesCliente.Name = "pnlDetallesCliente";
            this.pnlDetallesCliente.Size = new System.Drawing.Size(704, 54);
            this.pnlDetallesCliente.TabIndex = 2;
            // 
            // tblDetails
            // 
            this.tblDetails.ColumnCount = 2;
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tblDetails.Controls.Add(this.lblConceptoHeader, 0, 0);
            this.tblDetails.Controls.Add(this.lblMontoHeader, 1, 0);
            this.tblDetails.Controls.Add(this.lineaSeparadora2, 0, 1);
            this.tblDetails.Controls.Add(this.lblConcepto, 0, 2);
            this.tblDetails.Controls.Add(this.lblMonto, 1, 2);
            this.tblDetails.Controls.Add(this.lineaSeparadora3, 0, 3);
            this.tblDetails.Controls.Add(this.lblMontoTotal, 1, 4);
            this.tblDetails.Controls.Add(this.lblMontoTotalTexto, 0, 4);
            this.tblDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDetails.Location = new System.Drawing.Point(28, 158);
            this.tblDetails.Name = "tblDetails";
            this.tblDetails.RowCount = 5;
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDetails.Size = new System.Drawing.Size(704, 179);
            this.tblDetails.TabIndex = 3;
            // 
            // lineaSeparadora2
            // 
            this.lineaSeparadora2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(246)))));
            this.tblDetails.SetColumnSpan(this.lineaSeparadora2, 2);
            this.lineaSeparadora2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineaSeparadora2.Location = new System.Drawing.Point(3, 33);
            this.lineaSeparadora2.Name = "lineaSeparadora2";
            this.lineaSeparadora2.Size = new System.Drawing.Size(698, 1);
            this.lineaSeparadora2.TabIndex = 15;
            // 
            // lineaSeparadora3
            // 
            this.lineaSeparadora3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(246)))));
            this.tblDetails.SetColumnSpan(this.lineaSeparadora3, 2);
            this.lineaSeparadora3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineaSeparadora3.Location = new System.Drawing.Point(3, 64);
            this.lineaSeparadora3.Name = "lineaSeparadora3";
            this.lineaSeparadora3.Size = new System.Drawing.Size(698, 1);
            this.lineaSeparadora3.TabIndex = 16;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.lblFormaPago);
            this.pnlFooter.Controls.Add(this.lblObservaciones);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFooter.Location = new System.Drawing.Point(28, 343);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(704, 54);
            this.pnlFooter.TabIndex = 4;
            // 
            // pnlBotones
            // 
            this.pnlBotones.Controls.Add(this.btnCerrar);
            this.pnlBotones.Controls.Add(this.btnImprimir);
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBotones.Location = new System.Drawing.Point(28, 403);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(704, 44);
            this.pnlBotones.TabIndex = 5;
            // 
            // UcRecibo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblLayoutPrincipal);
            this.MinimumSize = new System.Drawing.Size(760, 500);
            this.Name = "UcRecibo";
            this.Size = new System.Drawing.Size(760, 500);
            this.tblLayoutPrincipal.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlDetallesCliente.ResumeLayout(false);
            this.pnlDetallesCliente.PerformLayout();
            this.tblDetails.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}