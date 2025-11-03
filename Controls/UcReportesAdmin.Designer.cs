namespace InmoTech.Controls
{
    partial class UcReportesAdmin
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
            // --- FIN: Definición de Colores del Tema ---

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlFiltroGlobal = new System.Windows.Forms.Panel();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.lblTituloFiltro = new System.Windows.Forms.Label();
            this.tabControlReportes = new System.Windows.Forms.TabControl();
            this.tabPageDashboard = new System.Windows.Forms.TabPage();
            this.tlpDashboard = new System.Windows.Forms.TableLayoutPanel();
            this.pnlKpiInquilinos = new System.Windows.Forms.Panel();
            this.lblTotalInquilinos = new System.Windows.Forms.Label();
            this.lblTituloInquilinos = new System.Windows.Forms.Label();
            this.pnlKpiUsuarios = new System.Windows.Forms.Panel();
            this.lblTotalUsuarios = new System.Windows.Forms.Label();
            this.lblTituloUsuarios = new System.Windows.Forms.Label();
            this.pnlKpiPropiedades = new System.Windows.Forms.Panel();
            this.lblTotalPropiedades = new System.Windows.Forms.Label();
            this.lblTituloPropiedades = new System.Windows.Forms.Label();
            this.tabPageInquilinos = new System.Windows.Forms.TabPage();
            this.dgvReporteInquilinos = new System.Windows.Forms.DataGridView();
            this.colInqDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInqNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInqApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInqTelefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInqEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInqEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInqFCreacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInqCreador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFiltroInquilinos = new System.Windows.Forms.Panel();
            this.cboInquilinosEstado = new System.Windows.Forms.ComboBox();
            this.lblFiltroInquilinoEstado = new System.Windows.Forms.Label();
            this.tabPageInmuebles = new System.Windows.Forms.TabPage();
            this.dgvReporteInmuebles = new System.Windows.Forms.DataGridView();
            this.colInmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInmTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInmDireccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInmAmbientes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInmAmueblado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInmCondiciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInmEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInmFCreacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInmCreador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFiltroInmuebles = new System.Windows.Forms.Panel();
            this.cboInmueblesTipo = new System.Windows.Forms.ComboBox();
            this.lblFiltroInmuebleTipo = new System.Windows.Forms.Label();
            this.tabPageUsuarios = new System.Windows.Forms.TabPage();
            this.dgvReporteUsuarios = new System.Windows.Forms.DataGridView();
            this.colUsrDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsrNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsrApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsrEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsrRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsrActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsrFCreacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsrCreador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFiltroUsuarios = new System.Windows.Forms.Panel();
            this.cboUsuariosActivo = new System.Windows.Forms.ComboBox();
            this.lblFiltroUsuarioActivo = new System.Windows.Forms.Label();
            this.cboUsuariosRol = new System.Windows.Forms.ComboBox();
            this.lblFiltroUsuarioRol = new System.Windows.Forms.Label();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.pnlFiltroGlobal.SuspendLayout();
            this.tabControlReportes.SuspendLayout();
            this.tabPageDashboard.SuspendLayout();
            this.tlpDashboard.SuspendLayout();
            this.pnlKpiInquilinos.SuspendLayout();
            this.pnlKpiUsuarios.SuspendLayout();
            this.pnlKpiPropiedades.SuspendLayout();
            this.tabPageInquilinos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteInquilinos)).BeginInit();
            this.pnlFiltroInquilinos.SuspendLayout();
            this.tabPageInmuebles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteInmuebles)).BeginInit();
            this.pnlFiltroInmuebles.SuspendLayout();
            this.tabPageUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteUsuarios)).BeginInit();
            this.pnlFiltroUsuarios.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFiltroGlobal
            // 
            this.pnlFiltroGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
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
            // 
            // btnRefrescar
            // 
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
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(478, 18);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(110, 25);
            this.dtpFechaFin.TabIndex = 4;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblHasta.ForeColor = colorTextoPrincipal;
            this.lblHasta.Location = new System.Drawing.Point(427, 22);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(45, 17);
            this.lblHasta.TabIndex = 3;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(301, 18);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(110, 25);
            this.dtpFechaInicio.TabIndex = 2;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblDesde.ForeColor = colorTextoPrincipal;
            this.lblDesde.Location = new System.Drawing.Point(246, 22);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(49, 17);
            this.lblDesde.TabIndex = 1;
            this.lblDesde.Text = "Desde:";
            // 
            // lblTituloFiltro
            // 
            this.lblTituloFiltro.AutoSize = true;
            this.lblTituloFiltro.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloFiltro.ForeColor = colorTextoPrincipal;
            this.lblTituloFiltro.Location = new System.Drawing.Point(15, 19);
            this.lblTituloFiltro.Name = "lblTituloFiltro";
            this.lblTituloFiltro.Size = new System.Drawing.Size(160, 21);
            this.lblTituloFiltro.TabIndex = 0;
            this.lblTituloFiltro.Text = "Período del Reporte";
            // 
            // tabControlReportes
            // 
            this.tabControlReportes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlReportes.Controls.Add(this.tabPageDashboard);
            this.tabControlReportes.Controls.Add(this.tabPageInquilinos);
            this.tabControlReportes.Controls.Add(this.tabPageInmuebles);
            this.tabControlReportes.Controls.Add(this.tabPageUsuarios);
            this.tabControlReportes.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.tabControlReportes.Location = new System.Drawing.Point(15, 81);
            this.tabControlReportes.Name = "tabControlReportes";
            this.tabControlReportes.SelectedIndex = 0;
            this.tabControlReportes.Size = new System.Drawing.Size(870, 506);
            this.tabControlReportes.TabIndex = 1;
            // 
            // tabPageDashboard
            // 
            this.tabPageDashboard.BackColor = backColorPrincipal;
            this.tabPageDashboard.Controls.Add(this.tlpDashboard);
            this.tabPageDashboard.Location = new System.Drawing.Point(4, 26);
            this.tabPageDashboard.Name = "tabPageDashboard";
            this.tabPageDashboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDashboard.Size = new System.Drawing.Size(862, 476);
            this.tabPageDashboard.TabIndex = 0;
            this.tabPageDashboard.Text = "Dashboard";
            // 
            // tlpDashboard
            // 
            this.tlpDashboard.ColumnCount = 3;
            this.tlpDashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpDashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpDashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpDashboard.Controls.Add(this.pnlKpiInquilinos, 1, 0);
            this.tlpDashboard.Controls.Add(this.pnlKpiUsuarios, 2, 0);
            this.tlpDashboard.Controls.Add(this.pnlKpiPropiedades, 0, 0);
            this.tlpDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDashboard.Location = new System.Drawing.Point(3, 3);
            this.tlpDashboard.Name = "tlpDashboard";
            this.tlpDashboard.RowCount = 2;
            this.tlpDashboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpDashboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDashboard.Size = new System.Drawing.Size(856, 470);
            this.tlpDashboard.TabIndex = 0;
            // 
            // pnlKpiInquilinos
            // 
            this.pnlKpiInquilinos.BackColor = backColorPaneles;
            this.pnlKpiInquilinos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiInquilinos.Controls.Add(this.lblTotalInquilinos);
            this.pnlKpiInquilinos.Controls.Add(this.lblTituloInquilinos);
            this.pnlKpiInquilinos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiInquilinos.Location = new System.Drawing.Point(295, 10); // Ajuste de margen
            this.pnlKpiInquilinos.Margin = new System.Windows.Forms.Padding(10);
            this.pnlKpiInquilinos.Name = "pnlKpiInquilinos";
            this.pnlKpiInquilinos.Size = new System.Drawing.Size(265, 100); // Ajuste de tamaño
            this.pnlKpiInquilinos.TabIndex = 1;
            // 
            // lblTotalInquilinos
            // 
            this.lblTotalInquilinos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalInquilinos.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalInquilinos.ForeColor = System.Drawing.Color.Green;
            this.lblTotalInquilinos.Location = new System.Drawing.Point(133, 40); // Ajuste de posición
            this.lblTotalInquilinos.Name = "lblTotalInquilinos";
            this.lblTotalInquilinos.Size = new System.Drawing.Size(117, 45);
            this.lblTotalInquilinos.TabIndex = 1;
            this.lblTotalInquilinos.Text = "0";
            this.lblTotalInquilinos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTituloInquilinos
            // 
            this.lblTituloInquilinos.AutoSize = true;
            this.lblTituloInquilinos.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloInquilinos.ForeColor = colorTextoPrincipal;
            this.lblTituloInquilinos.Location = new System.Drawing.Point(14, 14);
            this.lblTituloInquilinos.Name = "lblTituloInquilinos";
            this.lblTituloInquilinos.Size = new System.Drawing.Size(121, 21);
            this.lblTituloInquilinos.TabIndex = 0;
            this.lblTituloInquilinos.Text = "Total Inquilinos";
            // 
            // pnlKpiUsuarios
            // 
            this.pnlKpiUsuarios.BackColor = backColorPaneles;
            this.pnlKpiUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiUsuarios.Controls.Add(this.lblTotalUsuarios);
            this.pnlKpiUsuarios.Controls.Add(this.lblTituloUsuarios);
            this.pnlKpiUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiUsuarios.Location = new System.Drawing.Point(580, 10); // Ajuste de margen
            this.pnlKpiUsuarios.Margin = new System.Windows.Forms.Padding(10);
            this.pnlKpiUsuarios.Name = "pnlKpiUsuarios";
            this.pnlKpiUsuarios.Size = new System.Drawing.Size(266, 100); // Ajuste de tamaño
            this.pnlKpiUsuarios.TabIndex = 2;
            // 
            // lblTotalUsuarios
            // 
            this.lblTotalUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalUsuarios.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalUsuarios.ForeColor = System.Drawing.Color.Orange;
            this.lblTotalUsuarios.Location = new System.Drawing.Point(134, 40); // Ajuste de posición
            this.lblTotalUsuarios.Name = "lblTotalUsuarios";
            this.lblTotalUsuarios.Size = new System.Drawing.Size(117, 45);
            this.lblTotalUsuarios.TabIndex = 1;
            this.lblTotalUsuarios.Text = "0";
            this.lblTotalUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTituloUsuarios
            // 
            this.lblTituloUsuarios.AutoSize = true;
            this.lblTituloUsuarios.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloUsuarios.ForeColor = colorTextoPrincipal;
            this.lblTituloUsuarios.Location = new System.Drawing.Point(14, 14);
            this.lblTituloUsuarios.Name = "lblTituloUsuarios";
            this.lblTituloUsuarios.Size = new System.Drawing.Size(112, 21);
            this.lblTituloUsuarios.TabIndex = 0;
            this.lblTituloUsuarios.Text = "Total Usuarios";
            // 
            // pnlKpiPropiedades
            // 
            this.pnlKpiPropiedades.BackColor = backColorPaneles;
            this.pnlKpiPropiedades.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiPropiedades.Controls.Add(this.lblTotalPropiedades);
            this.pnlKpiPropiedades.Controls.Add(this.lblTituloPropiedades);
            this.pnlKpiPropiedades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiPropiedades.Location = new System.Drawing.Point(10, 10);
            this.pnlKpiPropiedades.Margin = new System.Windows.Forms.Padding(10);
            this.pnlKpiPropiedades.Name = "pnlKpiPropiedades";
            this.pnlKpiPropiedades.Size = new System.Drawing.Size(265, 100); // Ajuste de tamaño
            this.pnlKpiPropiedades.TabIndex = 0;
            // 
            // lblTotalPropiedades
            // 
            this.lblTotalPropiedades.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalPropiedades.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalPropiedades.ForeColor = colorPrimario;
            this.lblTotalPropiedades.Location = new System.Drawing.Point(133, 40); // Ajuste de posición
            this.lblTotalPropiedades.Name = "lblTotalPropiedades";
            this.lblTotalPropiedades.Size = new System.Drawing.Size(117, 45);
            this.lblTotalPropiedades.TabIndex = 1;
            this.lblTotalPropiedades.Text = "0";
            this.lblTotalPropiedades.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTituloPropiedades
            // 
            this.lblTituloPropiedades.AutoSize = true;
            this.lblTituloPropiedades.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloPropiedades.ForeColor = colorTextoPrincipal;
            this.lblTituloPropiedades.Location = new System.Drawing.Point(14, 14);
            this.lblTituloPropiedades.Name = "lblTituloPropiedades";
            this.lblTituloPropiedades.Size = new System.Drawing.Size(140, 21);
            this.lblTituloPropiedades.TabIndex = 0;
            this.lblTituloPropiedades.Text = "Total Propiedades";
            // 
            // tabPageInquilinos
            // 
            this.tabPageInquilinos.BackColor = backColorPrincipal;
            this.tabPageInquilinos.Controls.Add(this.dgvReporteInquilinos);
            this.tabPageInquilinos.Controls.Add(this.pnlFiltroInquilinos);
            this.tabPageInquilinos.Location = new System.Drawing.Point(4, 26);
            this.tabPageInquilinos.Name = "tabPageInquilinos";
            this.tabPageInquilinos.Padding = new System.Windows.Forms.Padding(10);
            this.tabPageInquilinos.Size = new System.Drawing.Size(862, 476);
            this.tabPageInquilinos.TabIndex = 1;
            this.tabPageInquilinos.Text = "Reporte Inquilinos";
            // 
            // dgvReporteInquilinos
            // 
            this.dgvReporteInquilinos.AllowUserToAddRows = false;
            this.dgvReporteInquilinos.AllowUserToDeleteRows = false;
            this.dgvReporteInquilinos.AllowUserToResizeRows = false; // Mejor visualización
            this.dgvReporteInquilinos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReporteInquilinos.BackgroundColor = backColorPaneles;
            this.dgvReporteInquilinos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReporteInquilinos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single; // Estilo cabecera
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = colorGridHeader;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold); // Negrita
            dataGridViewCellStyle1.ForeColor = colorTextoClaro;
            dataGridViewCellStyle1.SelectionBackColor = colorGridHeader;
            dataGridViewCellStyle1.SelectionForeColor = colorTextoClaro;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReporteInquilinos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReporteInquilinos.ColumnHeadersHeight = 35; // Altura cabecera
            this.dgvReporteInquilinos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReporteInquilinos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colInqDni,
            this.colInqNombre,
            this.colInqApellido,
            this.colInqTelefono,
            this.colInqEmail,
            this.colInqEstado,
            this.colInqFCreacion,
            this.colInqCreador});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = colorGridRow;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F); // Fuente estándar
            dataGridViewCellStyle2.ForeColor = colorTextoPrincipal;
            dataGridViewCellStyle2.SelectionBackColor = colorGridSelection;
            dataGridViewCellStyle2.SelectionForeColor = colorTextoPrincipal;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReporteInquilinos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvReporteInquilinos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReporteInquilinos.EnableHeadersVisualStyles = false;
            this.dgvReporteInquilinos.GridColor = colorGridLines;
            this.dgvReporteInquilinos.Location = new System.Drawing.Point(10, 60);
            this.dgvReporteInquilinos.Name = "dgvReporteInquilinos";
            this.dgvReporteInquilinos.ReadOnly = true;
            this.dgvReporteInquilinos.RowHeadersVisible = false;
            this.dgvReporteInquilinos.RowTemplate.Height = 28;
            this.dgvReporteInquilinos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReporteInquilinos.Size = new System.Drawing.Size(842, 406);
            this.dgvReporteInquilinos.TabIndex = 1;
            // 
            // colInqDni
            // 
            this.colInqDni.DataPropertyName = "Dni";
            this.colInqDni.HeaderText = "DNI";
            this.colInqDni.Name = "colInqDni";
            this.colInqDni.ReadOnly = true;
            this.colInqDni.FillWeight = 80F;
            // 
            // colInqNombre
            // 
            this.colInqNombre.DataPropertyName = "Nombre";
            this.colInqNombre.HeaderText = "Nombre";
            this.colInqNombre.Name = "colInqNombre";
            this.colInqNombre.ReadOnly = true;
            this.colInqNombre.FillWeight = 110F;
            // 
            // colInqApellido
            // 
            this.colInqApellido.DataPropertyName = "Apellido";
            this.colInqApellido.HeaderText = "Apellido";
            this.colInqApellido.Name = "colInqApellido";
            this.colInqApellido.ReadOnly = true;
            this.colInqApellido.FillWeight = 110F;
            // 
            // colInqTelefono
            // 
            this.colInqTelefono.DataPropertyName = "Telefono";
            this.colInqTelefono.HeaderText = "Teléfono";
            this.colInqTelefono.Name = "colInqTelefono";
            this.colInqTelefono.ReadOnly = true;
            this.colInqTelefono.FillWeight = 90F;
            // 
            // colInqEmail
            // 
            this.colInqEmail.DataPropertyName = "Email";
            this.colInqEmail.HeaderText = "Email";
            this.colInqEmail.Name = "colInqEmail";
            this.colInqEmail.ReadOnly = true;
            this.colInqEmail.FillWeight = 150F;
            // 
            // colInqEstado
            // 
            this.colInqEstado.DataPropertyName = "Estado";
            this.colInqEstado.HeaderText = "Estado";
            this.colInqEstado.Name = "colInqEstado";
            this.colInqEstado.ReadOnly = true;
            this.colInqEstado.FillWeight = 70F;
            // 
            // colInqFCreacion
            // 
            this.colInqFCreacion.DataPropertyName = "FechaCreacion";
            this.colInqFCreacion.HeaderText = "F. Creación";
            this.colInqFCreacion.Name = "colInqFCreacion";
            this.colInqFCreacion.ReadOnly = true;
            this.colInqFCreacion.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Format = "dd/MM/yyyy" };
            this.colInqFCreacion.FillWeight = 90F;
            // 
            // colInqCreador
            // 
            this.colInqCreador.DataPropertyName = "UsuarioCreador";
            this.colInqCreador.HeaderText = "Creador";
            this.colInqCreador.Name = "colInqCreador";
            this.colInqCreador.ReadOnly = true;
            this.colInqCreador.FillWeight = 110F;
            // 
            // pnlFiltroInquilinos
            // 
            this.pnlFiltroInquilinos.BackColor = backColorPaneles;
            this.pnlFiltroInquilinos.Controls.Add(this.cboInquilinosEstado);
            this.pnlFiltroInquilinos.Controls.Add(this.lblFiltroInquilinoEstado);
            this.pnlFiltroInquilinos.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltroInquilinos.Location = new System.Drawing.Point(10, 10);
            this.pnlFiltroInquilinos.Name = "pnlFiltroInquilinos";
            this.pnlFiltroInquilinos.Size = new System.Drawing.Size(842, 50);
            this.pnlFiltroInquilinos.TabIndex = 0;
            // 
            // cboInquilinosEstado
            // 
            this.cboInquilinosEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInquilinosEstado.FormattingEnabled = true;
            this.cboInquilinosEstado.Location = new System.Drawing.Point(62, 12);
            this.cboInquilinosEstado.Name = "cboInquilinosEstado";
            this.cboInquilinosEstado.Size = new System.Drawing.Size(121, 25);
            this.cboInquilinosEstado.TabIndex = 1;
            // 
            // lblFiltroInquilinoEstado
            // 
            this.lblFiltroInquilinoEstado.AutoSize = true;
            this.lblFiltroInquilinoEstado.ForeColor = colorTextoPrincipal;
            this.lblFiltroInquilinoEstado.Location = new System.Drawing.Point(7, 15);
            this.lblFiltroInquilinoEstado.Name = "lblFiltroInquilinoEstado";
            this.lblFiltroInquilinoEstado.Size = new System.Drawing.Size(49, 17);
            this.lblFiltroInquilinoEstado.TabIndex = 0;
            this.lblFiltroInquilinoEstado.Text = "Estado:";
            // 
            // tabPageInmuebles
            // 
            this.tabPageInmuebles.BackColor = backColorPrincipal;
            this.tabPageInmuebles.Controls.Add(this.dgvReporteInmuebles);
            this.tabPageInmuebles.Controls.Add(this.pnlFiltroInmuebles);
            this.tabPageInmuebles.Location = new System.Drawing.Point(4, 26);
            this.tabPageInmuebles.Name = "tabPageInmuebles";
            this.tabPageInmuebles.Padding = new System.Windows.Forms.Padding(10);
            this.tabPageInmuebles.Size = new System.Drawing.Size(862, 476);
            this.tabPageInmuebles.TabIndex = 2;
            this.tabPageInmuebles.Text = "Reporte Inmuebles";
            // 
            // dgvReporteInmuebles
            // 
            this.dgvReporteInmuebles.AllowUserToAddRows = false;
            this.dgvReporteInmuebles.AllowUserToDeleteRows = false;
            this.dgvReporteInmuebles.AllowUserToResizeRows = false;
            this.dgvReporteInmuebles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReporteInmuebles.BackgroundColor = backColorPaneles;
            this.dgvReporteInmuebles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReporteInmuebles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = colorGridHeader;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = colorTextoClaro;
            dataGridViewCellStyle3.SelectionBackColor = colorGridHeader;
            dataGridViewCellStyle3.SelectionForeColor = colorTextoClaro;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReporteInmuebles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvReporteInmuebles.ColumnHeadersHeight = 35;
            this.dgvReporteInmuebles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReporteInmuebles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colInmId,
            this.colInmTipo,
            this.colInmDireccion,
            this.colInmAmbientes,
            this.colInmAmueblado,
            this.colInmCondiciones,
            this.colInmEstado,
            this.colInmFCreacion,
            this.colInmCreador});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = colorGridRow;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = colorTextoPrincipal;
            dataGridViewCellStyle4.SelectionBackColor = colorGridSelection;
            dataGridViewCellStyle4.SelectionForeColor = colorTextoPrincipal;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReporteInmuebles.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvReporteInmuebles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReporteInmuebles.EnableHeadersVisualStyles = false;
            this.dgvReporteInmuebles.GridColor = colorGridLines;
            this.dgvReporteInmuebles.Location = new System.Drawing.Point(10, 60);
            this.dgvReporteInmuebles.Name = "dgvReporteInmuebles";
            this.dgvReporteInmuebles.ReadOnly = true;
            this.dgvReporteInmuebles.RowHeadersVisible = false;
            this.dgvReporteInmuebles.RowTemplate.Height = 28;
            this.dgvReporteInmuebles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReporteInmuebles.Size = new System.Drawing.Size(842, 406);
            this.dgvReporteInmuebles.TabIndex = 2;
            // 
            // colInmId
            // 
            this.colInmId.DataPropertyName = "IdInmueble";
            this.colInmId.HeaderText = "ID";
            this.colInmId.Name = "colInmId";
            this.colInmId.ReadOnly = true;
            this.colInmId.FillWeight = 50F;
            // 
            // colInmTipo
            // 
            this.colInmTipo.DataPropertyName = "Tipo";
            this.colInmTipo.HeaderText = "Tipo";
            this.colInmTipo.Name = "colInmTipo";
            this.colInmTipo.ReadOnly = true;
            this.colInmTipo.FillWeight = 90F;
            // 
            // colInmDireccion
            // 
            this.colInmDireccion.DataPropertyName = "Direccion";
            this.colInmDireccion.HeaderText = "Dirección";
            this.colInmDireccion.Name = "colInmDireccion";
            this.colInmDireccion.ReadOnly = true;
            this.colInmDireccion.FillWeight = 180F;
            // 
            // colInmAmbientes
            // 
            this.colInmAmbientes.DataPropertyName = "NroAmbientes";
            this.colInmAmbientes.HeaderText = "Amb.";
            this.colInmAmbientes.Name = "colInmAmbientes";
            this.colInmAmbientes.ReadOnly = true;
            this.colInmAmbientes.FillWeight = 50F;
            // 
            // colInmAmueblado
            // 
            this.colInmAmueblado.DataPropertyName = "Amueblado";
            this.colInmAmueblado.HeaderText = "Amueblado";
            this.colInmAmueblado.Name = "colInmAmueblado";
            this.colInmAmueblado.ReadOnly = true;
            this.colInmAmueblado.FillWeight = 70F;
            // 
            // colInmCondiciones
            // 
            this.colInmCondiciones.DataPropertyName = "Condiciones";
            this.colInmCondiciones.HeaderText = "Condiciones";
            this.colInmCondiciones.Name = "colInmCondiciones";
            this.colInmCondiciones.ReadOnly = true;
            this.colInmCondiciones.FillWeight = 90F;
            // 
            // colInmEstado
            // 
            this.colInmEstado.DataPropertyName = "Estado";
            this.colInmEstado.HeaderText = "Estado";
            this.colInmEstado.Name = "colInmEstado";
            this.colInmEstado.ReadOnly = true;
            this.colInmEstado.FillWeight = 70F;
            // 
            // colInmFCreacion
            // 
            this.colInmFCreacion.DataPropertyName = "FechaCreacion";
            this.colInmFCreacion.HeaderText = "F. Creación";
            this.colInmFCreacion.Name = "colInmFCreacion";
            this.colInmFCreacion.ReadOnly = true;
            this.colInmFCreacion.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Format = "dd/MM/yyyy" };
            this.colInmFCreacion.FillWeight = 90F;
            // 
            // colInmCreador
            // 
            this.colInmCreador.DataPropertyName = "UsuarioCreador";
            this.colInmCreador.HeaderText = "Creador";
            this.colInmCreador.Name = "colInmCreador";
            this.colInmCreador.ReadOnly = true;
            this.colInmCreador.FillWeight = 110F;
            // 
            // pnlFiltroInmuebles
            // 
            this.pnlFiltroInmuebles.BackColor = backColorPaneles;
            this.pnlFiltroInmuebles.Controls.Add(this.cboInmueblesTipo);
            this.pnlFiltroInmuebles.Controls.Add(this.lblFiltroInmuebleTipo);
            this.pnlFiltroInmuebles.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltroInmuebles.Location = new System.Drawing.Point(10, 10);
            this.pnlFiltroInmuebles.Name = "pnlFiltroInmuebles";
            this.pnlFiltroInmuebles.Size = new System.Drawing.Size(842, 50);
            this.pnlFiltroInmuebles.TabIndex = 1;
            // 
            // cboInmueblesTipo
            // 
            this.cboInmueblesTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInmueblesTipo.FormattingEnabled = true;
            this.cboInmueblesTipo.Location = new System.Drawing.Point(51, 12);
            this.cboInmueblesTipo.Name = "cboInmueblesTipo";
            this.cboInmueblesTipo.Size = new System.Drawing.Size(142, 25);
            this.cboInmueblesTipo.TabIndex = 1;
            // 
            // lblFiltroInmuebleTipo
            // 
            this.lblFiltroInmuebleTipo.AutoSize = true;
            this.lblFiltroInmuebleTipo.ForeColor = colorTextoPrincipal;
            this.lblFiltroInmuebleTipo.Location = new System.Drawing.Point(7, 15);
            this.lblFiltroInmuebleTipo.Name = "lblFiltroInmuebleTipo";
            this.lblFiltroInmuebleTipo.Size = new System.Drawing.Size(38, 17);
            this.lblFiltroInmuebleTipo.TabIndex = 0;
            this.lblFiltroInmuebleTipo.Text = "Tipo:";
            // 
            // tabPageUsuarios
            // 
            this.tabPageUsuarios.BackColor = backColorPrincipal;
            this.tabPageUsuarios.Controls.Add(this.dgvReporteUsuarios);
            this.tabPageUsuarios.Controls.Add(this.pnlFiltroUsuarios);
            this.tabPageUsuarios.Location = new System.Drawing.Point(4, 26);
            this.tabPageUsuarios.Name = "tabPageUsuarios";
            this.tabPageUsuarios.Padding = new System.Windows.Forms.Padding(10);
            this.tabPageUsuarios.Size = new System.Drawing.Size(862, 476);
            this.tabPageUsuarios.TabIndex = 3;
            this.tabPageUsuarios.Text = "Reporte Usuarios";
            // 
            // dgvReporteUsuarios
            // 
            this.dgvReporteUsuarios.AllowUserToAddRows = false;
            this.dgvReporteUsuarios.AllowUserToDeleteRows = false;
            this.dgvReporteUsuarios.AllowUserToResizeRows = false;
            this.dgvReporteUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReporteUsuarios.BackgroundColor = backColorPaneles;
            this.dgvReporteUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReporteUsuarios.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = colorGridHeader;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = colorTextoClaro;
            dataGridViewCellStyle5.SelectionBackColor = colorGridHeader;
            dataGridViewCellStyle5.SelectionForeColor = colorTextoClaro;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReporteUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvReporteUsuarios.ColumnHeadersHeight = 35;
            this.dgvReporteUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReporteUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colUsrDni,
            this.colUsrNombre,
            this.colUsrApellido,
            this.colUsrEmail,
            this.colUsrRol,
            this.colUsrActivo,
            this.colUsrFCreacion,
            this.colUsrCreador});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = colorGridRow;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = colorTextoPrincipal;
            dataGridViewCellStyle6.SelectionBackColor = colorGridSelection;
            dataGridViewCellStyle6.SelectionForeColor = colorTextoPrincipal;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReporteUsuarios.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvReporteUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReporteUsuarios.EnableHeadersVisualStyles = false;
            this.dgvReporteUsuarios.GridColor = colorGridLines;
            this.dgvReporteUsuarios.Location = new System.Drawing.Point(10, 60);
            this.dgvReporteUsuarios.Name = "dgvReporteUsuarios";
            this.dgvReporteUsuarios.ReadOnly = true;
            this.dgvReporteUsuarios.RowHeadersVisible = false;
            this.dgvReporteUsuarios.RowTemplate.Height = 28;
            this.dgvReporteUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReporteUsuarios.Size = new System.Drawing.Size(842, 406);
            this.dgvReporteUsuarios.TabIndex = 3;
            // 
            // colUsrDni
            // 
            this.colUsrDni.DataPropertyName = "Dni";
            this.colUsrDni.HeaderText = "DNI";
            this.colUsrDni.Name = "colUsrDni";
            this.colUsrDni.ReadOnly = true;
            this.colUsrDni.FillWeight = 80F;
            // 
            // colUsrNombre
            // 
            this.colUsrNombre.DataPropertyName = "Nombre";
            this.colUsrNombre.HeaderText = "Nombre";
            this.colUsrNombre.Name = "colUsrNombre";
            this.colUsrNombre.ReadOnly = true;
            this.colUsrNombre.FillWeight = 110F;
            // 
            // colUsrApellido
            // 
            this.colUsrApellido.DataPropertyName = "Apellido";
            this.colUsrApellido.HeaderText = "Apellido";
            this.colUsrApellido.Name = "colUsrApellido";
            this.colUsrApellido.ReadOnly = true;
            this.colUsrApellido.FillWeight = 110F;
            // 
            // colUsrEmail
            // 
            this.colUsrEmail.DataPropertyName = "Email";
            this.colUsrEmail.HeaderText = "Email";
            this.colUsrEmail.Name = "colUsrEmail";
            this.colUsrEmail.ReadOnly = true;
            this.colUsrEmail.FillWeight = 150F;
            // 
            // colUsrRol
            // 
            this.colUsrRol.DataPropertyName = "Rol";
            this.colUsrRol.HeaderText = "Rol";
            this.colUsrRol.Name = "colUsrRol";
            this.colUsrRol.ReadOnly = true;
            this.colUsrRol.FillWeight = 80F;
            // 
            // colUsrActivo
            // 
            this.colUsrActivo.DataPropertyName = "Activo";
            this.colUsrActivo.HeaderText = "Activo";
            this.colUsrActivo.Name = "colUsrActivo";
            this.colUsrActivo.ReadOnly = true;
            this.colUsrActivo.FillWeight = 70F;
            // 
            // colUsrFCreacion
            // 
            this.colUsrFCreacion.DataPropertyName = "FechaCreacion";
            this.colUsrFCreacion.HeaderText = "F. Creación";
            this.colUsrFCreacion.Name = "colUsrFCreacion";
            this.colUsrFCreacion.ReadOnly = true;
            this.colUsrFCreacion.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Format = "dd/MM/yyyy" };
            this.colUsrFCreacion.FillWeight = 90F;
            // 
            // colUsrCreador
            // 
            this.colUsrCreador.DataPropertyName = "UsuarioCreador";
            this.colUsrCreador.HeaderText = "Creador";
            this.colUsrCreador.Name = "colUsrCreador";
            this.colUsrCreador.ReadOnly = true;
            this.colUsrCreador.FillWeight = 110F;
            // 
            // pnlFiltroUsuarios
            // 
            this.pnlFiltroUsuarios.BackColor = backColorPaneles;
            this.pnlFiltroUsuarios.Controls.Add(this.cboUsuariosActivo);
            this.pnlFiltroUsuarios.Controls.Add(this.lblFiltroUsuarioActivo);
            this.pnlFiltroUsuarios.Controls.Add(this.cboUsuariosRol);
            this.pnlFiltroUsuarios.Controls.Add(this.lblFiltroUsuarioRol);
            this.pnlFiltroUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltroUsuarios.Location = new System.Drawing.Point(10, 10);
            this.pnlFiltroUsuarios.Name = "pnlFiltroUsuarios";
            this.pnlFiltroUsuarios.Size = new System.Drawing.Size(842, 50);
            this.pnlFiltroUsuarios.TabIndex = 2;
            // 
            // cboUsuariosActivo
            // 
            this.cboUsuariosActivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsuariosActivo.FormattingEnabled = true;
            this.cboUsuariosActivo.Location = new System.Drawing.Point(267, 12);
            this.cboUsuariosActivo.Name = "cboUsuariosActivo";
            this.cboUsuariosActivo.Size = new System.Drawing.Size(121, 25);
            this.cboUsuariosActivo.TabIndex = 3;
            // 
            // lblFiltroUsuarioActivo
            // 
            this.lblFiltroUsuarioActivo.AutoSize = true;
            this.lblFiltroUsuarioActivo.ForeColor = colorTextoPrincipal;
            this.lblFiltroUsuarioActivo.Location = new System.Drawing.Point(211, 15);
            this.lblFiltroUsuarioActivo.Name = "lblFiltroUsuarioActivo";
            this.lblFiltroUsuarioActivo.Size = new System.Drawing.Size(49, 17);
            this.lblFiltroUsuarioActivo.TabIndex = 2;
            this.lblFiltroUsuarioActivo.Text = "Estado:";
            // 
            // cboUsuariosRol
            // 
            this.cboUsuariosRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsuariosRol.FormattingEnabled = true;
            this.cboUsuariosRol.Location = new System.Drawing.Point(51, 12);
            this.cboUsuariosRol.Name = "cboUsuariosRol";
            this.cboUsuariosRol.Size = new System.Drawing.Size(142, 25);
            this.cboUsuariosRol.TabIndex = 1;
            // 
            // lblFiltroUsuarioRol
            // 
            this.lblFiltroUsuarioRol.AutoSize = true;
            this.lblFiltroUsuarioRol.ForeColor = colorTextoPrincipal;
            this.lblFiltroUsuarioRol.Location = new System.Drawing.Point(7, 15);
            this.lblFiltroUsuarioRol.Name = "lblFiltroUsuarioRol";
            this.lblFiltroUsuarioRol.Size = new System.Drawing.Size(29, 17);
            this.lblFiltroUsuarioRol.TabIndex = 0;
            this.lblFiltroUsuarioRol.Text = "Rol:";
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportarExcel.BackColor = System.Drawing.ColorTranslator.FromHtml("#28A745"); // Verde Excel
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
            // UcReportesAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = backColorPrincipal;
            this.Controls.Add(this.btnExportarExcel);
            this.Controls.Add(this.tabControlReportes);
            this.Controls.Add(this.pnlFiltroGlobal);
            this.Name = "UcReportesAdmin";
            this.Size = new System.Drawing.Size(900, 640);
            this.pnlFiltroGlobal.ResumeLayout(false);
            this.pnlFiltroGlobal.PerformLayout();
            this.tabControlReportes.ResumeLayout(false);
            this.tabPageDashboard.ResumeLayout(false);
            this.tlpDashboard.ResumeLayout(false);
            this.pnlKpiInquilinos.ResumeLayout(false);
            this.pnlKpiInquilinos.PerformLayout();
            this.pnlKpiUsuarios.ResumeLayout(false);
            this.pnlKpiUsuarios.PerformLayout();
            this.pnlKpiPropiedades.ResumeLayout(false);
            this.pnlKpiPropiedades.PerformLayout();
            this.tabPageInquilinos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteInquilinos)).EndInit();
            this.pnlFiltroInquilinos.ResumeLayout(false);
            this.pnlFiltroInquilinos.PerformLayout();
            this.tabPageInmuebles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteInmuebles)).EndInit();
            this.pnlFiltroInmuebles.ResumeLayout(false);
            this.pnlFiltroInmuebles.PerformLayout();
            this.tabPageUsuarios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteUsuarios)).EndInit();
            this.pnlFiltroUsuarios.ResumeLayout(false);
            this.pnlFiltroUsuarios.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFiltroGlobal;
        private System.Windows.Forms.Label lblTituloFiltro;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.TabControl tabControlReportes;
        private System.Windows.Forms.TabPage tabPageDashboard;
        private System.Windows.Forms.TabPage tabPageInquilinos;
        private System.Windows.Forms.TabPage tabPageInmuebles;
        private System.Windows.Forms.TabPage tabPageUsuarios;
        private System.Windows.Forms.TableLayoutPanel tlpDashboard;
        private System.Windows.Forms.Panel pnlKpiPropiedades;
        private System.Windows.Forms.Label lblTotalPropiedades;
        private System.Windows.Forms.Label lblTituloPropiedades;
        private System.Windows.Forms.Panel pnlKpiInquilinos;
        private System.Windows.Forms.Label lblTotalInquilinos;
        private System.Windows.Forms.Label lblTituloInquilinos;
        private System.Windows.Forms.Panel pnlKpiUsuarios;
        private System.Windows.Forms.Label lblTotalUsuarios;
        private System.Windows.Forms.Label lblTituloUsuarios;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.Panel pnlFiltroInquilinos;
        private System.Windows.Forms.ComboBox cboInquilinosEstado;
        private System.Windows.Forms.Label lblFiltroInquilinoEstado;
        private System.Windows.Forms.DataGridView dgvReporteInquilinos;
        private System.Windows.Forms.Panel pnlFiltroInmuebles;
        private System.Windows.Forms.ComboBox cboInmueblesTipo;
        private System.Windows.Forms.Label lblFiltroInmuebleTipo;
        private System.Windows.Forms.DataGridView dgvReporteInmuebles;
        private System.Windows.Forms.Panel pnlFiltroUsuarios;
        private System.Windows.Forms.ComboBox cboUsuariosActivo;
        private System.Windows.Forms.Label lblFiltroUsuarioActivo;
        private System.Windows.Forms.ComboBox cboUsuariosRol;
        private System.Windows.Forms.Label lblFiltroUsuarioRol;
        private System.Windows.Forms.DataGridView dgvReporteUsuarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInqDni;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInqNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInqApellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInqTelefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInqEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInqEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInqFCreacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInqCreador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInmId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInmTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInmDireccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInmAmbientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInmAmueblado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInmCondiciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInmEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInmFCreacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInmCreador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsrDni;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsrNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsrApellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsrEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsrRol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsrActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsrFCreacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsrCreador;
    }
}