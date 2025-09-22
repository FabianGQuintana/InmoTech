namespace InmoTech
{
    partial class UcReportesPropietario
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            panelTop = new Panel();
            gbFiltros = new GroupBox();
            btnLimpiar = new Button();
            btnBuscar = new Button();
            cmbInmueble = new ComboBox();
            label6 = new Label();
            cmbInquilino = new ComboBox();
            label5 = new Label();
            cmbEstado = new ComboBox();
            label4 = new Label();
            cmbTipoReporte = new ComboBox();
            label3 = new Label();
            dpHasta = new DateTimePicker();
            label2 = new Label();
            dpDesde = new DateTimePicker();
            label1 = new Label();
            flowKpis = new FlowLayoutPanel();
            cardTotal = new Panel();
            lblKpiTotal = new Label();
            label7 = new Label();
            cardCantidad = new Panel();
            lblKpiCantidad = new Label();
            label9 = new Label();
            cardPromedio = new Panel();
            lblKpiPromedio = new Label();
            label11 = new Label();
            dgv = new DataGridView();
            panelBottom = new Panel();
            btnImprimir = new Button();
            btnExportarCsv = new Button();
            panelTop.SuspendLayout();
            gbFiltros.SuspendLayout();
            flowKpis.SuspendLayout();
            cardTotal.SuspendLayout();
            cardCantidad.SuspendLayout();
            cardPromedio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.Teal;
            panelTop.Controls.Add(gbFiltros);
            panelTop.Controls.Add(flowKpis);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(4, 3, 4, 3);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(12, 12, 12, 6);
            panelTop.Size = new Size(1100, 210);
            panelTop.TabIndex = 0;
            // 
            // gbFiltros
            // 
            gbFiltros.Controls.Add(btnLimpiar);
            gbFiltros.Controls.Add(btnBuscar);
            gbFiltros.Controls.Add(cmbInmueble);
            gbFiltros.Controls.Add(label6);
            gbFiltros.Controls.Add(cmbInquilino);
            gbFiltros.Controls.Add(label5);
            gbFiltros.Controls.Add(cmbEstado);
            gbFiltros.Controls.Add(label4);
            gbFiltros.Controls.Add(cmbTipoReporte);
            gbFiltros.Controls.Add(label3);
            gbFiltros.Controls.Add(dpHasta);
            gbFiltros.Controls.Add(label2);
            gbFiltros.Controls.Add(dpDesde);
            gbFiltros.Controls.Add(label1);
            gbFiltros.Dock = DockStyle.Fill;
            gbFiltros.Location = new Point(12, 12);
            gbFiltros.Margin = new Padding(4, 3, 4, 3);
            gbFiltros.Name = "gbFiltros";
            gbFiltros.Padding = new Padding(10, 8, 10, 10);
            gbFiltros.Size = new Size(1076, 110);
            gbFiltros.TabIndex = 0;
            gbFiltros.TabStop = false;
            gbFiltros.Text = "Filtros";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Top;
            btnLimpiar.Location = new Point(972, 66);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(90, 30);
            btnLimpiar.TabIndex = 13;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Top;
            btnBuscar.Location = new Point(972, 26);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(90, 30);
            btnBuscar.TabIndex = 12;
            btnBuscar.Text = "Aplicar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // cmbInmueble
            // 
            cmbInmueble.Anchor = AnchorStyles.Top;
            cmbInmueble.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbInmueble.FormattingEnabled = true;
            cmbInmueble.Location = new Point(690, 68);
            cmbInmueble.Name = "cmbInmueble";
            cmbInmueble.Size = new Size(260, 28);
            cmbInmueble.TabIndex = 11;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.Location = new Point(610, 72);
            label6.Name = "label6";
            label6.Size = new Size(71, 20);
            label6.TabIndex = 10;
            label6.Text = "Inmueble";
            // 
            // cmbInquilino
            // 
            cmbInquilino.Anchor = AnchorStyles.Top;
            cmbInquilino.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbInquilino.FormattingEnabled = true;
            cmbInquilino.Location = new Point(690, 26);
            cmbInquilino.Name = "cmbInquilino";
            cmbInquilino.Size = new Size(260, 28);
            cmbInquilino.TabIndex = 9;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Location = new Point(610, 30);
            label5.Name = "label5";
            label5.Size = new Size(67, 20);
            label5.TabIndex = 8;
            label5.Text = "Inquilino";
            // 
            // cmbEstado
            // 
            cmbEstado.Anchor = AnchorStyles.Top;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(430, 68);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(160, 28);
            cmbEstado.TabIndex = 7;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Location = new Point(370, 72);
            label4.Name = "label4";
            label4.Size = new Size(54, 20);
            label4.TabIndex = 6;
            label4.Text = "Estado";
            // 
            // cmbTipoReporte
            // 
            cmbTipoReporte.Anchor = AnchorStyles.Top;
            cmbTipoReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoReporte.FormattingEnabled = true;
            cmbTipoReporte.Location = new Point(430, 26);
            cmbTipoReporte.Name = "cmbTipoReporte";
            cmbTipoReporte.Size = new Size(160, 28);
            cmbTipoReporte.TabIndex = 5;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Location = new Point(370, 30);
            label3.Name = "label3";
            label3.Size = new Size(39, 20);
            label3.TabIndex = 4;
            label3.Text = "Tipo";
            // 
            // dpHasta
            // 
            dpHasta.Anchor = AnchorStyles.Top;
            dpHasta.Format = DateTimePickerFormat.Short;
            dpHasta.Location = new Point(105, 68);
            dpHasta.Name = "dpHasta";
            dpHasta.Size = new Size(120, 27);
            dpHasta.TabIndex = 3;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Location = new Point(18, 72);
            label2.Name = "label2";
            label2.Size = new Size(47, 20);
            label2.TabIndex = 2;
            label2.Text = "Hasta";
            // 
            // dpDesde
            // 
            dpDesde.Anchor = AnchorStyles.Top;
            dpDesde.Format = DateTimePickerFormat.Short;
            dpDesde.Location = new Point(105, 26);
            dpDesde.Name = "dpDesde";
            dpDesde.Size = new Size(120, 27);
            dpDesde.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(18, 30);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 0;
            label1.Text = "Desde";
            // 
            // flowKpis
            // 
            flowKpis.Controls.Add(cardTotal);
            flowKpis.Controls.Add(cardCantidad);
            flowKpis.Controls.Add(cardPromedio);
            flowKpis.Dock = DockStyle.Bottom;
            flowKpis.Location = new Point(12, 122);
            flowKpis.Margin = new Padding(4, 3, 4, 3);
            flowKpis.Name = "flowKpis";
            flowKpis.Padding = new Padding(0, 6, 0, 0);
            flowKpis.Size = new Size(1076, 82);
            flowKpis.TabIndex = 1;
            // 
            // cardTotal
            // 
            cardTotal.BackColor = Color.White;
            cardTotal.BorderStyle = BorderStyle.FixedSingle;
            cardTotal.Controls.Add(lblKpiTotal);
            cardTotal.Controls.Add(label7);
            cardTotal.Location = new Point(3, 9);
            cardTotal.Margin = new Padding(3, 3, 12, 3);
            cardTotal.Name = "cardTotal";
            cardTotal.Padding = new Padding(10, 8, 10, 8);
            cardTotal.Size = new Size(220, 64);
            cardTotal.TabIndex = 0;
            // 
            // lblKpiTotal
            // 
            lblKpiTotal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblKpiTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblKpiTotal.Location = new Point(10, 30);
            lblKpiTotal.Name = "lblKpiTotal";
            lblKpiTotal.Size = new Size(198, 23);
            lblKpiTotal.TabIndex = 1;
            lblKpiTotal.Text = "$0,00";
            lblKpiTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 8);
            label7.Name = "label7";
            label7.Size = new Size(115, 20);
            label7.TabIndex = 0;
            label7.Text = "Ingresos Totales";
            // 
            // cardCantidad
            // 
            cardCantidad.BackColor = Color.White;
            cardCantidad.BorderStyle = BorderStyle.FixedSingle;
            cardCantidad.Controls.Add(lblKpiCantidad);
            cardCantidad.Controls.Add(label9);
            cardCantidad.Location = new Point(238, 9);
            cardCantidad.Margin = new Padding(3, 3, 12, 3);
            cardCantidad.Name = "cardCantidad";
            cardCantidad.Padding = new Padding(10, 8, 10, 8);
            cardCantidad.Size = new Size(200, 64);
            cardCantidad.TabIndex = 1;
            // 
            // lblKpiCantidad
            // 
            lblKpiCantidad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblKpiCantidad.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblKpiCantidad.Location = new Point(10, 30);
            lblKpiCantidad.Name = "lblKpiCantidad";
            lblKpiCantidad.Size = new Size(178, 23);
            lblKpiCantidad.TabIndex = 1;
            lblKpiCantidad.Text = "0";
            lblKpiCantidad.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(10, 8);
            label9.Name = "label9";
            label9.Size = new Size(109, 20);
            label9.TabIndex = 0;
            label9.Text = "Cantidad ítems";
            // 
            // cardPromedio
            // 
            cardPromedio.BackColor = Color.White;
            cardPromedio.BorderStyle = BorderStyle.FixedSingle;
            cardPromedio.Controls.Add(lblKpiPromedio);
            cardPromedio.Controls.Add(label11);
            cardPromedio.Location = new Point(453, 9);
            cardPromedio.Margin = new Padding(3, 3, 12, 3);
            cardPromedio.Name = "cardPromedio";
            cardPromedio.Padding = new Padding(10, 8, 10, 8);
            cardPromedio.Size = new Size(200, 64);
            cardPromedio.TabIndex = 2;
            // 
            // lblKpiPromedio
            // 
            lblKpiPromedio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblKpiPromedio.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblKpiPromedio.Location = new Point(10, 30);
            lblKpiPromedio.Name = "lblKpiPromedio";
            lblKpiPromedio.Size = new Size(178, 23);
            lblKpiPromedio.TabIndex = 1;
            lblKpiPromedio.Text = "$0,00";
            lblKpiPromedio.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(10, 8);
            label11.Name = "label11";
            label11.Size = new Size(141, 20);
            label11.TabIndex = 0;
            label11.Text = "Promedio (importe)";
            // 
            // dgv
            // 
            dgv.BackgroundColor = Color.White;
            dgv.ColumnHeadersHeight = 29;
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(0, 210);
            dgv.Margin = new Padding(4, 3, 4, 3);
            dgv.MultiSelect = false;
            dgv.Name = "dgv";
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersWidth = 51;
            dgv.RowTemplate.Height = 28;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(1100, 470);
            dgv.TabIndex = 1;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(btnImprimir);
            panelBottom.Controls.Add(btnExportarCsv);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 680);
            panelBottom.Margin = new Padding(4, 3, 4, 3);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(12, 6, 12, 12);
            panelBottom.Size = new Size(1100, 56);
            panelBottom.TabIndex = 2;
            // 
            // btnImprimir
            // 
            btnImprimir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnImprimir.Location = new Point(904, 12);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(90, 32);
            btnImprimir.TabIndex = 1;
            btnImprimir.Text = "Imprimir";
            btnImprimir.UseVisualStyleBackColor = true;
            // 
            // btnExportarCsv
            // 
            btnExportarCsv.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportarCsv.Location = new Point(1000, 12);
            btnExportarCsv.Name = "btnExportarCsv";
            btnExportarCsv.Size = new Size(90, 32);
            btnExportarCsv.TabIndex = 0;
            btnExportarCsv.Text = "Exportar";
            btnExportarCsv.UseVisualStyleBackColor = true;
            // 
            // UcReportes
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.WhiteSmoke;
            Controls.Add(dgv);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Name = "UcReportes";
            Size = new Size(1100, 736);
            panelTop.ResumeLayout(false);
            gbFiltros.ResumeLayout(false);
            gbFiltros.PerformLayout();
            flowKpis.ResumeLayout(false);
            cardTotal.ResumeLayout(false);
            cardTotal.PerformLayout();
            cardCantidad.ResumeLayout(false);
            cardCantidad.PerformLayout();
            cardPromedio.ResumeLayout(false);
            cardPromedio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.GroupBox gbFiltros;
        private System.Windows.Forms.ComboBox cmbTipoReporte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dpHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dpDesde;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbInquilino;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbInmueble;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.FlowLayoutPanel flowKpis;
        private System.Windows.Forms.Panel cardTotal;
        private System.Windows.Forms.Label lblKpiTotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel cardCantidad;
        private System.Windows.Forms.Label lblKpiCantidad;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel cardPromedio;
        private System.Windows.Forms.Label lblKpiPromedio;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnExportarCsv;
        private System.Windows.Forms.Button btnImprimir;
    }
}
