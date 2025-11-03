namespace InmoTech.Controls
{
    partial class UcReportesOperador
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
            // --- INICIO: Definición de Colores del Tema ---
            System.Drawing.Color backColorPrincipal = System.Drawing.ColorTranslator.FromHtml("#FAFAFA");
            System.Drawing.Color backColorPaneles = System.Drawing.Color.White;
            System.Drawing.Color colorPrimario = System.Drawing.ColorTranslator.FromHtml("#17A2B8");
            System.Drawing.Color colorTextoPrincipal = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            System.Drawing.Color colorTextoClaro = System.Drawing.Color.White;
            System.Drawing.Color colorGridHeader = colorPrimario;
            System.Drawing.Color colorGridRow = System.Drawing.Color.White;
            System.Drawing.Color colorGridAlternatingRow = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            System.Drawing.Color colorGridSelection = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(216)))), ((int)(((byte)(230)))));
            System.Drawing.Color colorGridLines = System.Drawing.Color.LightGray;
            System.Drawing.Color colorMorososHeader = System.Drawing.Color.IndianRed;
            System.Drawing.Color colorMorososRow = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            System.Drawing.Color colorMorososSelection = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            // --- FIN: Definición de Colores del Tema ---

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle(); // Morosos Header
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle(); // Morosos Rows
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle(); // Centrado (para números)
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle(); // Fecha Corta (Morosos Prox Venc)
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle(); // Pagos Header
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle(); // Pagos Rows
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle(); // Fecha Corta (Pagos Fecha)
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle(); // Monto (Pagos)
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle(); // Centrado (Pago Cuota)
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle(); // Fecha Larga (Pago Fec Reg)
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle(); // Contratos Header
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle(); // Contratos Rows
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle(); // Fecha Corta (Contrato Inicio)
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle(); // Fecha Corta (Contrato Fin)
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle(); // Monto (Contrato)
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle(); // Fecha Larga (Contrato Fec Crea)

            // NUEVOS estilos para "Contratos por vencer"
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle(); // Header CPV
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle(); // Rows CPV
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle(); // Fecha Fin CPV
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle(); // Días restantes CPV (centrado)

            this.pnlFiltroGlobal = new System.Windows.Forms.Panel();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.lblTituloFiltro = new System.Windows.Forms.Label();
            this.tabControlReportes = new System.Windows.Forms.TabControl();
            this.tabPageResumen = new System.Windows.Forms.TabPage();
            this.tlpResumen = new System.Windows.Forms.TableLayoutPanel();
            this.pnlKpiPagos = new System.Windows.Forms.Panel();
            this.lblPagosRegistrados = new System.Windows.Forms.Label();
            this.lblTituloPagos = new System.Windows.Forms.Label();
            this.pnlKpiContratos = new System.Windows.Forms.Panel();
            this.lblContratosCreados = new System.Windows.Forms.Label();
            this.lblTituloContratos = new System.Windows.Forms.Label();
            this.pnlKpiIngresos = new System.Windows.Forms.Panel();
            this.lblTotalIngresos = new System.Windows.Forms.Label();
            this.lblTituloIngresos = new System.Windows.Forms.Label();
            this.gbMorosos = new System.Windows.Forms.GroupBox();
            this.dgvClientesMorosos = new System.Windows.Forms.DataGridView();
            this.colMorDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMorNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMorTel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMorContrato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMorInmueble = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMorCuotasVenc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMorProxVenc = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // NUEVOS controles: GroupBox + DataGridView "Contratos por vencer"
            this.gbContratosPorVencer = new System.Windows.Forms.GroupBox();
            this.dgvContratosPorVencer = new System.Windows.Forms.DataGridView();
            this.colCpvIdContrato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCpvInquilino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCpvInmueble = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCpvFechaFin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCpvDiasRestantes = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.tabPageMisPagos = new System.Windows.Forms.TabPage();
            this.dgvMisPagos = new System.Windows.Forms.DataGridView();
            this.colPagoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPagoFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPagoMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPagoMetodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPagoContrato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPagoCuota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPagoInquilino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPagoFecReg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageMisContratos = new System.Windows.Forms.TabPage();
            this.dgvMisContratos = new System.Windows.Forms.DataGridView();
            this.colContId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContFin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContInquilino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContInmueble = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContFecCrea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            // --- Inicialización Suspendida ---
            this.pnlFiltroGlobal.SuspendLayout();
            this.tabControlReportes.SuspendLayout();
            this.tabPageResumen.SuspendLayout();
            this.tlpResumen.SuspendLayout();
            this.pnlKpiPagos.SuspendLayout();
            this.pnlKpiContratos.SuspendLayout();
            this.pnlKpiIngresos.SuspendLayout();
            this.gbMorosos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientesMorosos)).BeginInit();

            // NUEVOS BeginInit
            this.gbContratosPorVencer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContratosPorVencer)).BeginInit();

            this.tabPageMisPagos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisPagos)).BeginInit();
            this.tabPageMisContratos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisContratos)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlFiltroGlobal ... btnRefrescar (Sin cambios)
            this.pnlFiltroGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFiltroGlobal.BackColor = backColorPaneles;
            this.pnlFiltroGlobal.Controls.Add(this.btnRefrescar);
            this.pnlFiltroGlobal.Controls.Add(this.dtpFechaFin);
            this.pnlFiltroGlobal.Controls.Add(this.lblHasta);
            this.pnlFiltroGlobal.Controls.Add(this.dtpFechaInicio);
            this.pnlFiltroGlobal.Controls.Add(this.lblDesde);
            this.pnlFiltroGlobal.Controls.Add(this.lblTituloFiltro);
            this.pnlFiltroGlobal.Location = new System.Drawing.Point(15, 15);
            this.pnlFiltroGlobal.Name = "pnlFiltroGlobal";
            this.pnlFiltroGlobal.Size = new System.Drawing.Size(870, 60);
            this.pnlFiltroGlobal.TabIndex = 0;

            this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefrescar.BackColor = colorPrimario;
            this.btnRefrescar.FlatAppearance.BorderSize = 0;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefrescar.ForeColor = colorTextoClaro;
            this.btnRefrescar.Location = new System.Drawing.Point(744, 15);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(110, 30);
            this.btnRefrescar.TabIndex = 5;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = false;

            this.dtpFechaFin.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(478, 18);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(110, 25);
            this.dtpFechaFin.TabIndex = 4;

            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblHasta.ForeColor = colorTextoPrincipal;
            this.lblHasta.Location = new System.Drawing.Point(427, 22);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(45, 17);
            this.lblHasta.TabIndex = 3;
            this.lblHasta.Text = "Hasta:";

            this.dtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(301, 18);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(110, 25);
            this.dtpFechaInicio.TabIndex = 2;

            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblDesde.ForeColor = colorTextoPrincipal;
            this.lblDesde.Location = new System.Drawing.Point(246, 22);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(49, 17);
            this.lblDesde.TabIndex = 1;
            this.lblDesde.Text = "Desde:";

            this.lblTituloFiltro.AutoSize = true;
            this.lblTituloFiltro.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloFiltro.ForeColor = colorTextoPrincipal;
            this.lblTituloFiltro.Location = new System.Drawing.Point(15, 19);
            this.lblTituloFiltro.Name = "lblTituloFiltro";
            this.lblTituloFiltro.Size = new System.Drawing.Size(160, 21);
            this.lblTituloFiltro.TabIndex = 0;
            this.lblTituloFiltro.Text = "Período del Reporte";
            // ...
            // 
            // tabControlReportes ... (Sin cambios estructurales)
            this.tabControlReportes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlReportes.Controls.Add(this.tabPageResumen);
            this.tabControlReportes.Controls.Add(this.tabPageMisPagos);
            this.tabControlReportes.Controls.Add(this.tabPageMisContratos);
            this.tabControlReportes.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.tabControlReportes.Location = new System.Drawing.Point(15, 81);
            this.tabControlReportes.Name = "tabControlReportes";
            this.tabControlReportes.SelectedIndex = 0;
            this.tabControlReportes.Size = new System.Drawing.Size(870, 506);
            this.tabControlReportes.TabIndex = 1;

            this.tabPageResumen.BackColor = backColorPrincipal;
            this.tabPageResumen.Controls.Add(this.tlpResumen);
            this.tabPageResumen.Location = new System.Drawing.Point(4, 26);
            this.tabPageResumen.Name = "tabPageResumen";
            this.tabPageResumen.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResumen.Size = new System.Drawing.Size(862, 476);
            this.tabPageResumen.TabIndex = 0;
            this.tabPageResumen.Text = "Resumen Operador";

            // --- TLP: ahora con 3 filas (KPIs + Morosos + Contratos por vencer) ---
            this.tlpResumen.ColumnCount = 3;
            this.tlpResumen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpResumen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpResumen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpResumen.Controls.Add(this.pnlKpiPagos, 1, 0);
            this.tlpResumen.Controls.Add(this.pnlKpiContratos, 2, 0);
            this.tlpResumen.Controls.Add(this.pnlKpiIngresos, 0, 0);
            this.tlpResumen.Controls.Add(this.gbMorosos, 0, 1);
            this.tlpResumen.Controls.Add(this.gbContratosPorVencer, 0, 2); // NUEVO
            this.tlpResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpResumen.Location = new System.Drawing.Point(3, 3);
            this.tlpResumen.Name = "tlpResumen";
            this.tlpResumen.RowCount = 3;
            this.tlpResumen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpResumen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpResumen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpResumen.Size = new System.Drawing.Size(856, 470);
            this.tlpResumen.TabIndex = 0;
            this.tlpResumen.SetColumnSpan(this.gbMorosos, 3);
            this.tlpResumen.SetColumnSpan(this.gbContratosPorVencer, 3); // NUEVO

            this.pnlKpiPagos.BackColor = backColorPaneles;
            this.pnlKpiPagos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiPagos.Controls.Add(this.lblPagosRegistrados);
            this.pnlKpiPagos.Controls.Add(this.lblTituloPagos);
            this.pnlKpiPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiPagos.Location = new System.Drawing.Point(295, 10);
            this.pnlKpiPagos.Margin = new System.Windows.Forms.Padding(10);
            this.pnlKpiPagos.Name = "pnlKpiPagos";
            this.pnlKpiPagos.Size = new System.Drawing.Size(265, 100);
            this.pnlKpiPagos.TabIndex = 1;

            this.lblPagosRegistrados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPagosRegistrados.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblPagosRegistrados.ForeColor = System.Drawing.Color.Green;
            this.lblPagosRegistrados.Location = new System.Drawing.Point(133, 40);
            this.lblPagosRegistrados.Name = "lblPagosRegistrados";
            this.lblPagosRegistrados.Size = new System.Drawing.Size(117, 45);
            this.lblPagosRegistrados.TabIndex = 1;
            this.lblPagosRegistrados.Text = "0";
            this.lblPagosRegistrados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.lblTituloPagos.AutoSize = true;
            this.lblTituloPagos.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloPagos.ForeColor = colorTextoPrincipal;
            this.lblTituloPagos.Location = new System.Drawing.Point(14, 14);
            this.lblTituloPagos.Name = "lblTituloPagos";
            this.lblTituloPagos.Size = new System.Drawing.Size(142, 21);
            this.lblTituloPagos.TabIndex = 0;
            this.lblTituloPagos.Text = "Pagos Registrados";

            this.pnlKpiContratos.BackColor = backColorPaneles;
            this.pnlKpiContratos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiContratos.Controls.Add(this.lblContratosCreados);
            this.pnlKpiContratos.Controls.Add(this.lblTituloContratos);
            this.pnlKpiContratos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiContratos.Location = new System.Drawing.Point(580, 10);
            this.pnlKpiContratos.Margin = new System.Windows.Forms.Padding(10);
            this.pnlKpiContratos.Name = "pnlKpiContratos";
            this.pnlKpiContratos.Size = new System.Drawing.Size(266, 100);
            this.pnlKpiContratos.TabIndex = 2;

            this.lblContratosCreados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContratosCreados.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblContratosCreados.ForeColor = System.Drawing.Color.Orange;
            this.lblContratosCreados.Location = new System.Drawing.Point(134, 40);
            this.lblContratosCreados.Name = "lblContratosCreados";
            this.lblContratosCreados.Size = new System.Drawing.Size(117, 45);
            this.lblContratosCreados.TabIndex = 1;
            this.lblContratosCreados.Text = "0";
            this.lblContratosCreados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.lblTituloContratos.AutoSize = true;
            this.lblTituloContratos.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloContratos.ForeColor = colorTextoPrincipal;
            this.lblTituloContratos.Location = new System.Drawing.Point(14, 14);
            this.lblTituloContratos.Name = "lblTituloContratos";
            this.lblTituloContratos.Size = new System.Drawing.Size(145, 21);
            this.lblTituloContratos.TabIndex = 0;
            this.lblTituloContratos.Text = "Contratos Creados";

            this.pnlKpiIngresos.BackColor = backColorPaneles;
            this.pnlKpiIngresos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiIngresos.Controls.Add(this.lblTotalIngresos);
            this.pnlKpiIngresos.Controls.Add(this.lblTituloIngresos);
            this.pnlKpiIngresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiIngresos.Location = new System.Drawing.Point(10, 10);
            this.pnlKpiIngresos.Margin = new System.Windows.Forms.Padding(10);
            this.pnlKpiIngresos.Name = "pnlKpiIngresos";
            this.pnlKpiIngresos.Size = new System.Drawing.Size(265, 100);
            this.pnlKpiIngresos.TabIndex = 0;

            this.lblTotalIngresos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalIngresos.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalIngresos.ForeColor = colorPrimario;
            this.lblTotalIngresos.Location = new System.Drawing.Point(15, 45);
            this.lblTotalIngresos.Name = "lblTotalIngresos";
            this.lblTotalIngresos.Size = new System.Drawing.Size(235, 40);
            this.lblTotalIngresos.TabIndex = 1;
            this.lblTotalIngresos.Text = "$ 0,00";
            this.lblTotalIngresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.lblTituloIngresos.AutoSize = true;
            this.lblTituloIngresos.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloIngresos.ForeColor = colorTextoPrincipal;
            this.lblTituloIngresos.Location = new System.Drawing.Point(14, 14);
            this.lblTituloIngresos.Name = "lblTituloIngresos";
            this.lblTituloIngresos.Size = new System.Drawing.Size(150, 21);
            this.lblTituloIngresos.TabIndex = 0;
            this.lblTituloIngresos.Text = "Ingresos Generados";

            // 
            // gbMorosos
            // 
            this.gbMorosos.Controls.Add(this.dgvClientesMorosos);
            this.gbMorosos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMorosos.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.gbMorosos.ForeColor = colorTextoPrincipal;
            this.gbMorosos.Location = new System.Drawing.Point(10, 130);
            this.gbMorosos.Margin = new System.Windows.Forms.Padding(10);
            this.gbMorosos.Name = "gbMorosos";
            this.gbMorosos.Padding = new System.Windows.Forms.Padding(10);
            this.gbMorosos.Size = new System.Drawing.Size(836, 184);
            this.gbMorosos.TabIndex = 3;
            this.gbMorosos.TabStop = false;
            this.gbMorosos.Text = "Clientes con Cuotas Vencidas";

            // 
            // dgvClientesMorosos
            // 
            this.dgvClientesMorosos.AllowUserToAddRows = false;
            this.dgvClientesMorosos.AllowUserToDeleteRows = false;
            this.dgvClientesMorosos.AllowUserToResizeRows = false;
            this.dgvClientesMorosos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientesMorosos.BackgroundColor = backColorPaneles;
            this.dgvClientesMorosos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvClientesMorosos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = colorMorososHeader; // Rojo para morosos
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = colorTextoClaro;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle1.SelectionForeColor = colorTextoClaro;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvClientesMorosos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvClientesMorosos.ColumnHeadersHeight = 35;
            this.dgvClientesMorosos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvClientesMorosos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMorDni,
            this.colMorNombre,
            this.colMorTel,
            this.colMorContrato,
            this.colMorInmueble,
            this.colMorCuotasVenc,
            this.colMorProxVenc});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = colorMorososRow; // Rosa claro
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F); // Usar fuente base del GroupBox
            dataGridViewCellStyle2.ForeColor = colorTextoPrincipal;
            dataGridViewCellStyle2.SelectionBackColor = colorMorososSelection; // Rosa más oscuro
            dataGridViewCellStyle2.SelectionForeColor = colorTextoPrincipal;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvClientesMorosos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvClientesMorosos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClientesMorosos.EnableHeadersVisualStyles = false;
            this.dgvClientesMorosos.GridColor = colorGridLines;
            this.dgvClientesMorosos.Location = new System.Drawing.Point(10, 27);
            this.dgvClientesMorosos.Name = "dgvClientesMorosos";
            this.dgvClientesMorosos.ReadOnly = true;
            this.dgvClientesMorosos.RowHeadersVisible = false;
            this.dgvClientesMorosos.RowTemplate.Height = 28;
            this.dgvClientesMorosos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientesMorosos.Size = new System.Drawing.Size(816, 147);
            this.dgvClientesMorosos.TabIndex = 0;

            // 
            // colMorDni
            // 
            this.colMorDni.DataPropertyName = "DniInquilino";
            this.colMorDni.HeaderText = "DNI Inq.";
            this.colMorDni.Name = "colMorDni";
            this.colMorDni.ReadOnly = true;
            this.colMorDni.FillWeight = 80F;
            // 
            // colMorNombre
            // 
            this.colMorNombre.DataPropertyName = "NombreCompletoInquilino";
            this.colMorNombre.HeaderText = "Inquilino";
            this.colMorNombre.Name = "colMorNombre";
            this.colMorNombre.ReadOnly = true;
            this.colMorNombre.FillWeight = 150F;
            // 
            // colMorTel
            // 
            this.colMorTel.DataPropertyName = "TelefonoInquilino";
            this.colMorTel.HeaderText = "Teléfono";
            this.colMorTel.Name = "colMorTel";
            this.colMorTel.ReadOnly = true;
            this.colMorTel.FillWeight = 90F;
            // 
            // colMorContrato
            // 
            this.colMorContrato.DataPropertyName = "IdContrato";
            this.colMorContrato.HeaderText = "Contrato ID";
            this.colMorContrato.Name = "colMorContrato";
            this.colMorContrato.ReadOnly = true;
            this.colMorContrato.FillWeight = 70F;
            // 
            // colMorInmueble
            // 
            this.colMorInmueble.DataPropertyName = "DireccionInmueble";
            this.colMorInmueble.HeaderText = "Inmueble";
            this.colMorInmueble.Name = "colMorInmueble";
            this.colMorInmueble.ReadOnly = true;
            this.colMorInmueble.FillWeight = 150F;
            // 
            // colMorCuotasVenc
            // 
            this.colMorCuotasVenc.DataPropertyName = "CuotasVencidas";
            this.colMorCuotasVenc.HeaderText = "Cuotas Venc.";
            this.colMorCuotasVenc.Name = "colMorCuotasVenc";
            this.colMorCuotasVenc.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter; // Centrar número
            this.colMorCuotasVenc.DefaultCellStyle = dataGridViewCellStyle3;
            this.colMorCuotasVenc.FillWeight = 60F;
            // 
            // colMorProxVenc (SIN FORMATO DE FECHA AQUÍ)
            // 
            this.colMorProxVenc.DataPropertyName = "ProximoVencimiento";
            this.colMorProxVenc.HeaderText = "Venc. Más Antiguo";
            this.colMorProxVenc.Name = "colMorProxVenc";
            this.colMorProxVenc.ReadOnly = true;
            // dataGridViewCellStyle4.Format = "dd/MM/yyyy"; // <-- ELIMINADO
            // this.colMorProxVenc.DefaultCellStyle = dataGridViewCellStyle4; // <-- ELIMINADO
            this.colMorProxVenc.FillWeight = 90F;

            // ============================================================
            // NUEVO: GroupBox "Contratos por vencer (≤ 60 días)"
            // ============================================================
            this.gbContratosPorVencer.Controls.Add(this.dgvContratosPorVencer);
            this.gbContratosPorVencer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbContratosPorVencer.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.gbContratosPorVencer.ForeColor = colorTextoPrincipal;
            this.gbContratosPorVencer.Location = new System.Drawing.Point(10, 324);
            this.gbContratosPorVencer.Margin = new System.Windows.Forms.Padding(10);
            this.gbContratosPorVencer.Name = "gbContratosPorVencer";
            this.gbContratosPorVencer.Padding = new System.Windows.Forms.Padding(10);
            this.gbContratosPorVencer.Size = new System.Drawing.Size(836, 136);
            this.gbContratosPorVencer.TabIndex = 4;
            this.gbContratosPorVencer.TabStop = false;
            this.gbContratosPorVencer.Text = "Contratos por vencer (≤ 60 días)";

            // DataGridView - Contratos por vencer
            this.dgvContratosPorVencer.AllowUserToAddRows = false;
            this.dgvContratosPorVencer.AllowUserToDeleteRows = false;
            this.dgvContratosPorVencer.AllowUserToResizeRows = false;
            this.dgvContratosPorVencer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvContratosPorVencer.BackgroundColor = backColorPaneles;
            this.dgvContratosPorVencer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvContratosPorVencer.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;

            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = colorGridHeader;             // mantiene tu colorPrimario
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle17.ForeColor = colorTextoClaro;
            dataGridViewCellStyle17.SelectionBackColor = colorGridHeader;
            dataGridViewCellStyle17.SelectionForeColor = colorTextoClaro;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvContratosPorVencer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvContratosPorVencer.ColumnHeadersHeight = 35;
            this.dgvContratosPorVencer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            this.dgvContratosPorVencer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colCpvIdContrato,
                this.colCpvInquilino,
                this.colCpvInmueble,
                this.colCpvFechaFin,
                this.colCpvDiasRestantes
            });

            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = colorGridRow;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle18.ForeColor = colorTextoPrincipal;
            dataGridViewCellStyle18.SelectionBackColor = colorGridSelection;
            dataGridViewCellStyle18.SelectionForeColor = colorTextoPrincipal;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvContratosPorVencer.DefaultCellStyle = dataGridViewCellStyle18;

            this.dgvContratosPorVencer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContratosPorVencer.EnableHeadersVisualStyles = false;
            this.dgvContratosPorVencer.GridColor = colorGridLines;
            this.dgvContratosPorVencer.Location = new System.Drawing.Point(10, 27);
            this.dgvContratosPorVencer.Name = "dgvContratosPorVencer";
            this.dgvContratosPorVencer.ReadOnly = true;
            this.dgvContratosPorVencer.RowHeadersVisible = false;
            this.dgvContratosPorVencer.RowTemplate.Height = 28;
            this.dgvContratosPorVencer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContratosPorVencer.Size = new System.Drawing.Size(816, 99);
            this.dgvContratosPorVencer.TabIndex = 0;

            // Columnas CPV
            this.colCpvIdContrato.DataPropertyName = "IdContrato";
            this.colCpvIdContrato.HeaderText = "Contrato ID";
            this.colCpvIdContrato.Name = "colCpvIdContrato";
            this.colCpvIdContrato.ReadOnly = true;
            this.colCpvIdContrato.FillWeight = 70F;

            this.colCpvInquilino.DataPropertyName = "Inquilino";
            this.colCpvInquilino.HeaderText = "Inquilino";
            this.colCpvInquilino.Name = "colCpvInquilino";
            this.colCpvInquilino.ReadOnly = true;
            this.colCpvInquilino.FillWeight = 150F;

            this.colCpvInmueble.DataPropertyName = "Inmueble";
            this.colCpvInmueble.HeaderText = "Inmueble";
            this.colCpvInmueble.Name = "colCpvInmueble";
            this.colCpvInmueble.ReadOnly = true;
            this.colCpvInmueble.FillWeight = 150F;

            this.colCpvFechaFin.DataPropertyName = "FechaFin";
            this.colCpvFechaFin.HeaderText = "Fecha Fin";
            this.colCpvFechaFin.Name = "colCpvFechaFin";
            this.colCpvFechaFin.ReadOnly = true;
            // dataGridViewCellStyle19.Format = "dd/MM/yyyy"; // lo aplicamos por CellFormatting
            this.colCpvFechaFin.DefaultCellStyle = dataGridViewCellStyle19;
            this.colCpvFechaFin.FillWeight = 90F;

            this.colCpvDiasRestantes.DataPropertyName = "DiasRestantes";
            this.colCpvDiasRestantes.HeaderText = "Días Rest.";
            this.colCpvDiasRestantes.Name = "colCpvDiasRestantes";
            this.colCpvDiasRestantes.ReadOnly = true;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colCpvDiasRestantes.DefaultCellStyle = dataGridViewCellStyle20;
            this.colCpvDiasRestantes.FillWeight = 70F;

            // 
            // tabPageMisPagos ... dgvMisPagos (Propiedades generales sin cambios)
            // 
            this.tabPageMisPagos.BackColor = backColorPrincipal;
            this.tabPageMisPagos.Controls.Add(this.dgvMisPagos);
            this.tabPageMisPagos.Location = new System.Drawing.Point(4, 26);
            this.tabPageMisPagos.Name = "tabPageMisPagos";
            this.tabPageMisPagos.Padding = new System.Windows.Forms.Padding(10);
            this.tabPageMisPagos.Size = new System.Drawing.Size(862, 476);
            this.tabPageMisPagos.TabIndex = 1;
            this.tabPageMisPagos.Text = "Mis Pagos Registrados";

            this.dgvMisPagos.AllowUserToAddRows = false;
            this.dgvMisPagos.AllowUserToDeleteRows = false;
            this.dgvMisPagos.AllowUserToResizeRows = false;
            this.dgvMisPagos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMisPagos.BackgroundColor = backColorPaneles;
            this.dgvMisPagos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMisPagos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = colorGridHeader;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = colorTextoClaro;
            dataGridViewCellStyle5.SelectionBackColor = colorGridHeader;
            dataGridViewCellStyle5.SelectionForeColor = colorTextoClaro;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMisPagos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMisPagos.ColumnHeadersHeight = 35;
            this.dgvMisPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMisPagos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPagoId,
            this.colPagoFecha,
            this.colPagoMonto,
            this.colPagoMetodo,
            this.colPagoContrato,
            this.colPagoCuota,
            this.colPagoInquilino,
            this.colPagoFecReg});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = colorGridRow;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = colorTextoPrincipal;
            dataGridViewCellStyle6.SelectionBackColor = colorGridSelection;
            dataGridViewCellStyle6.SelectionForeColor = colorTextoPrincipal;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMisPagos.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvMisPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMisPagos.EnableHeadersVisualStyles = false;
            this.dgvMisPagos.GridColor = colorGridLines;
            this.dgvMisPagos.Location = new System.Drawing.Point(10, 10);
            this.dgvMisPagos.Name = "dgvMisPagos";
            this.dgvMisPagos.ReadOnly = true;
            this.dgvMisPagos.RowHeadersVisible = false;
            this.dgvMisPagos.RowTemplate.Height = 28;
            this.dgvMisPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMisPagos.Size = new System.Drawing.Size(842, 456);
            this.dgvMisPagos.TabIndex = 0;

            // 
            // colPagoId (Sin cambios)
            // 
            this.colPagoId.DataPropertyName = "IdPago"; this.colPagoId.HeaderText = "ID Pago"; this.colPagoId.Name = "colPagoId"; this.colPagoId.ReadOnly = true; this.colPagoId.FillWeight = 70F;
            // 
            // colPagoFecha (SIN FORMATO DE FECHA AQUÍ)
            // 
            this.colPagoFecha.DataPropertyName = "FechaPago";
            this.colPagoFecha.HeaderText = "Fecha Pago";
            this.colPagoFecha.Name = "colPagoFecha";
            this.colPagoFecha.ReadOnly = true;
            // dataGridViewCellStyle7.Format = "dd/MM/yyyy"; // <-- ELIMINADO
            // this.colPagoFecha.DefaultCellStyle = dataGridViewCellStyle7; // <-- ELIMINADO
            this.colPagoFecha.FillWeight = 90F;
            // 
            // colPagoMonto (CORREGIDO: Sin Culture)
            // 
            this.colPagoMonto.DataPropertyName = "MontoTotal";
            this.colPagoMonto.HeaderText = "Monto";
            this.colPagoMonto.Name = "colPagoMonto";
            this.colPagoMonto.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "C2"; // Quitado Culture
            this.colPagoMonto.DefaultCellStyle = dataGridViewCellStyle8;
            this.colPagoMonto.FillWeight = 90F;
            // 
            // colPagoMetodo ... colPagoInquilino (Sin cambios)
            // 
            this.colPagoMetodo.DataPropertyName = "MetodoPago"; this.colPagoMetodo.HeaderText = "Método"; this.colPagoMetodo.Name = "colPagoMetodo"; this.colPagoMetodo.ReadOnly = true; this.colPagoMetodo.FillWeight = 90F;
            this.colPagoContrato.DataPropertyName = "IdContrato"; this.colPagoContrato.HeaderText = "Contrato ID"; this.colPagoContrato.Name = "colPagoContrato"; this.colPagoContrato.ReadOnly = true; this.colPagoContrato.FillWeight = 70F;
            this.colPagoCuota.DataPropertyName = "NroCuota"; this.colPagoCuota.HeaderText = "N° Cuota"; this.colPagoCuota.Name = "colPagoCuota"; this.colPagoCuota.ReadOnly = true; this.colPagoCuota.DefaultCellStyle = dataGridViewCellStyle3; this.colPagoCuota.FillWeight = 60F; // Usando estilo 3 para centrar
            this.colPagoInquilino.DataPropertyName = "Inquilino"; this.colPagoInquilino.HeaderText = "Inquilino"; this.colPagoInquilino.Name = "colPagoInquilino"; this.colPagoInquilino.ReadOnly = true; this.colPagoInquilino.FillWeight = 150F;
            // 
            // colPagoFecReg (SIN FORMATO DE FECHA AQUÍ)
            // 
            this.colPagoFecReg.DataPropertyName = "FechaRegistro";
            this.colPagoFecReg.HeaderText = "Fec. Registro";
            this.colPagoFecReg.Name = "colPagoFecReg";
            this.colPagoFecReg.ReadOnly = true;
            // dataGridViewCellStyle10.Format = "dd/MM/yyyy HH:mm"; // <-- ELIMINADO
            // this.colPagoFecReg.DefaultCellStyle = dataGridViewCellStyle10; // <-- ELIMINADO
            this.colPagoFecReg.FillWeight = 110F;

            // 
            // tabPageMisContratos ... dgvMisContratos (Propiedades generales sin cambios)
            // 
            this.tabPageMisContratos.BackColor = backColorPrincipal;
            this.tabPageMisContratos.Controls.Add(this.dgvMisContratos);
            this.tabPageMisContratos.Location = new System.Drawing.Point(4, 26);
            this.tabPageMisContratos.Name = "tabPageMisContratos";
            this.tabPageMisContratos.Padding = new System.Windows.Forms.Padding(10);
            this.tabPageMisContratos.Size = new System.Drawing.Size(862, 476);
            this.tabPageMisContratos.TabIndex = 2;
            this.tabPageMisContratos.Text = "Mis Contratos Creados";

            this.dgvMisContratos.AllowUserToAddRows = false;
            this.dgvMisContratos.AllowUserToDeleteRows = false;
            this.dgvMisContratos.AllowUserToResizeRows = false;
            this.dgvMisContratos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMisContratos.BackgroundColor = backColorPaneles;
            this.dgvMisContratos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMisContratos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = colorGridHeader;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle11.ForeColor = colorTextoClaro;
            dataGridViewCellStyle11.SelectionBackColor = colorGridHeader;
            dataGridViewCellStyle11.SelectionForeColor = colorTextoClaro;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMisContratos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvMisContratos.ColumnHeadersHeight = 35;
            this.dgvMisContratos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMisContratos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colContId,
            this.colContInicio,
            this.colContFin,
            this.colContMonto,
            this.colContInquilino,
            this.colContInmueble,
            this.colContEstado,
            this.colContFecCrea});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = colorGridRow;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle12.ForeColor = colorTextoPrincipal;
            dataGridViewCellStyle12.SelectionBackColor = colorGridSelection;
            dataGridViewCellStyle12.SelectionForeColor = colorTextoPrincipal;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMisContratos.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvMisContratos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMisContratos.EnableHeadersVisualStyles = false;
            this.dgvMisContratos.GridColor = colorGridLines;
            this.dgvMisContratos.Location = new System.Drawing.Point(10, 10);
            this.dgvMisContratos.Name = "dgvMisContratos";
            this.dgvMisContratos.ReadOnly = true;
            this.dgvMisContratos.RowHeadersVisible = false;
            this.dgvMisContratos.RowTemplate.Height = 28;
            this.dgvMisContratos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMisContratos.Size = new System.Drawing.Size(842, 456);
            this.dgvMisContratos.TabIndex = 1;

            // 
            // colContId (Sin cambios)
            // 
            this.colContId.DataPropertyName = "IdContrato"; this.colContId.HeaderText = "Contrato ID"; this.colContId.Name = "colContId"; this.colContId.ReadOnly = true; this.colContId.FillWeight = 70F;
            // 
            // colContInicio (SIN FORMATO DE FECHA AQUÍ)
            // 
            this.colContInicio.DataPropertyName = "FechaInicio";
            this.colContInicio.HeaderText = "Fecha Inicio";
            this.colContInicio.Name = "colContInicio";
            this.colContInicio.ReadOnly = true;
            // dataGridViewCellStyle13.Format = "dd/MM/yyyy"; // <-- ELIMINADO
            // this.colContInicio.DefaultCellStyle = dataGridViewCellStyle13; // <-- ELIMINADO
            this.colContInicio.FillWeight = 90F;
            // 
            // colContFin (SIN FORMATO DE FECHA AQUÍ)
            // 
            this.colContFin.DataPropertyName = "FechaFin";
            this.colContFin.HeaderText = "Fecha Fin";
            this.colContFin.Name = "colContFin";
            this.colContFin.ReadOnly = true;
            // dataGridViewCellStyle14.Format = "dd/MM/yyyy"; // <-- ELIMINADO
            // this.colContFin.DefaultCellStyle = dataGridViewCellStyle14; // <-- ELIMINADO
            this.colContFin.FillWeight = 90F;
            // 
            // colContMonto (CORREGIDO: Sin Culture)
            // 
            this.colContMonto.DataPropertyName = "MontoMensual";
            this.colContMonto.HeaderText = "Monto Mensual";
            this.colContMonto.Name = "colContMonto";
            this.colContMonto.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "C2"; // Quitado Culture
            this.colContMonto.DefaultCellStyle = dataGridViewCellStyle15;
            this.colContMonto.FillWeight = 90F;
            // 
            // colContInquilino ... colContEstado (Sin cambios)
            // 
            this.colContInquilino.DataPropertyName = "Inquilino"; this.colContInquilino.HeaderText = "Inquilino"; this.colContInquilino.Name = "colContInquilino"; this.colContInquilino.ReadOnly = true; this.colContInquilino.FillWeight = 150F;
            this.colContInmueble.DataPropertyName = "Inmueble"; this.colContInmueble.HeaderText = "Inmueble"; this.colContInmueble.Name = "colContInmueble"; this.colContInmueble.ReadOnly = true; this.colContInmueble.FillWeight = 150F;
            this.colContEstado.DataPropertyName = "Estado"; this.colContEstado.HeaderText = "Estado"; this.colContEstado.Name = "colContEstado"; this.colContEstado.ReadOnly = true; this.colContEstado.FillWeight = 70F;
            // 
            // colContFecCrea (SIN FORMATO DE FECHA AQUÍ)
            // 
            this.colContFecCrea.DataPropertyName = "FechaCreacion";
            this.colContFecCrea.HeaderText = "Fec. Creación";
            this.colContFecCrea.Name = "colContFecCrea";
            this.colContFecCrea.ReadOnly = true;
            // dataGridViewCellStyle16.Format = "dd/MM/yyyy HH:mm"; // <-- ELIMINADO
            // this.colContFecCrea.DefaultCellStyle = dataGridViewCellStyle16; // <-- ELIMINADO
            this.colContFecCrea.FillWeight = 110F;

            // 
            // btnExportarExcel (Sin cambios)
            // 
            this.btnExportarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportarExcel.BackColor = System.Drawing.ColorTranslator.FromHtml("#28A745");
            this.btnExportarExcel.FlatAppearance.BorderSize = 0;
            this.btnExportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarExcel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExportarExcel.ForeColor = colorTextoClaro;
            this.btnExportarExcel.Location = new System.Drawing.Point(743, 593);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(138, 30);
            this.btnExportarExcel.TabIndex = 6;
            this.btnExportarExcel.Text = "Exportar a Excel 📈";
            this.btnExportarExcel.UseVisualStyleBackColor = false;

            // 
            // UcReportesOperador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = backColorPrincipal;
            this.Controls.Add(this.btnExportarExcel);
            this.Controls.Add(this.tabControlReportes);
            this.Controls.Add(this.pnlFiltroGlobal);
            this.Name = "UcReportesOperador";
            this.Size = new System.Drawing.Size(900, 640);
            // --- SuspendLayout / ResumeLayout / EndInit ---
            this.pnlFiltroGlobal.ResumeLayout(false);
            this.pnlFiltroGlobal.PerformLayout();
            this.tabControlReportes.ResumeLayout(false);
            this.tabPageResumen.ResumeLayout(false);
            this.tlpResumen.ResumeLayout(false);
            this.pnlKpiPagos.ResumeLayout(false);
            this.pnlKpiPagos.PerformLayout();
            this.pnlKpiContratos.ResumeLayout(false);
            this.pnlKpiContratos.PerformLayout();
            this.pnlKpiIngresos.ResumeLayout(false);
            this.pnlKpiIngresos.PerformLayout();
            this.gbMorosos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientesMorosos)).EndInit();

            // NUEVOS ResumeLayout/EndInit
            this.gbContratosPorVencer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContratosPorVencer)).EndInit();

            this.tabPageMisPagos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisPagos)).EndInit();
            this.tabPageMisContratos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisContratos)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        // ... (Declaraciones de controles) ...
        private System.Windows.Forms.Panel pnlFiltroGlobal;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Label lblTituloFiltro;
        private System.Windows.Forms.TabControl tabControlReportes;
        private System.Windows.Forms.TabPage tabPageResumen;
        private System.Windows.Forms.TableLayoutPanel tlpResumen;
        private System.Windows.Forms.Panel pnlKpiPagos;
        private System.Windows.Forms.Label lblPagosRegistrados;
        private System.Windows.Forms.Label lblTituloPagos;
        private System.Windows.Forms.Panel pnlKpiContratos;
        private System.Windows.Forms.Label lblContratosCreados;
        private System.Windows.Forms.Label lblTituloContratos;
        private System.Windows.Forms.Panel pnlKpiIngresos;
        private System.Windows.Forms.Label lblTotalIngresos;
        private System.Windows.Forms.Label lblTituloIngresos;
        private System.Windows.Forms.GroupBox gbMorosos;
        private System.Windows.Forms.DataGridView dgvClientesMorosos;

        // NUEVOS: Declaraciones Contratos por vencer
        private System.Windows.Forms.GroupBox gbContratosPorVencer;
        private System.Windows.Forms.DataGridView dgvContratosPorVencer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpvIdContrato;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpvInquilino;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpvInmueble;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpvFechaFin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpvDiasRestantes;

        private System.Windows.Forms.TabPage tabPageMisPagos;
        private System.Windows.Forms.DataGridView dgvMisPagos;
        private System.Windows.Forms.TabPage tabPageMisContratos;
        private System.Windows.Forms.DataGridView dgvMisContratos;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMorDni;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMorNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMorTel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMorContrato;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMorInmueble;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMorCuotasVenc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMorProxVenc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPagoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPagoFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPagoMonto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPagoMetodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPagoContrato;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPagoCuota;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPagoInquilino;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPagoFecReg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContFin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContMonto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContInquilino;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContInmueble;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContFecCrea;
    }
}
