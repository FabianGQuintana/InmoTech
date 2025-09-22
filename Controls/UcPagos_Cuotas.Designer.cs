// UcPagos_Cuotas.Designer.cs — header arreglado para que se vea completo
using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    partial class UcPagos_Cuotas
    {
        private System.ComponentModel.IContainer components = null;

        // Layout raíz
        private TableLayoutPanel tlRoot;

        // Encabezado + Métricas
        private TableLayoutPanel tlTop;   // 2 columnas: header | métricas
        private Panel cardHeader;
        private TableLayoutPanel tlHeader;
        private Label lblContrato;
        private Label lblInquilino;
        private Label lblInmueble;
        private Label lblPeriodo;

        private Panel cardMetrics;
        private TableLayoutPanel tlMetrics;
        private Label lblTotalTitulo;
        private Label lblTotalValor;
        private Label lblAtrasoTitulo;
        private Label lblAtrasoValor;
        private Label lblCuotasTitulo;
        private Label lblCuotasValor;

        // Tabs header
        private FlowLayoutPanel tabsHeader;
        private Button tabBtnCuotas;
        private Button tabBtnLineaTiempo;

        // Filtros
        private FlowLayoutPanel filtersPanel;
        private Label lblEstado;
        private ComboBox cboEstado;
        private Label lblAnio;
        private ComboBox cboAnio;

        // Contenido
        private TabControl tabs;
        private TabPage tabCuotas;
        private TabPage tabLineaTiempo;
        private DataGridView dgvCuotas;
        private Label lblPager;

        // Columnas
        private DataGridViewTextBoxColumn colCuota;
        private DataGridViewTextBoxColumn colCuotaRaw;
        private DataGridViewTextBoxColumn colDeCuotasRaw;
        private DataGridViewTextBoxColumn colPeriodo;
        private DataGridViewTextBoxColumn colMonto;
        private DataGridViewTextBoxColumn colVencimiento;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewButtonColumn colAccion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Código del Diseñador
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // ===== root =====
            BackColor = Color.White;
            AutoScaleMode = AutoScaleMode.Dpi;
            Name = "UcPagos_Cuotas";
            Padding = new Padding(16);
            MinimumSize = new Size(700, 420);

            tlRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Color.White
            };
            tlRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));       // header + metrics (alto auto)
            tlRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));       // tabs header
            tlRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));       // filtros
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));  // contenido (llena resto)

            // ===== Encabezado + métricas =====
            tlTop = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(0, 0, 0, 8),
                AutoSize = true
            };
            tlTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            tlTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tlTop.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // -- Card header
            cardHeader = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Dock = DockStyle.Fill,
                Padding = new Padding(16),
                Margin = new Padding(0, 0, 8, 0),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            cardHeader.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(235, 235, 235));
                e.Graphics.DrawRectangle(p, 0, 0, cardHeader.Width - 1, cardHeader.Height - 1);
            };

            // Tabla interna para texto (evita recortes)
            tlHeader = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                AutoSize = true
            };
            tlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlHeader.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // contrato
            tlHeader.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // inquilino
            tlHeader.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // inmueble
            tlHeader.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // periodo

            lblContrato = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(35, 35, 35),
                Margin = new Padding(0, 0, 0, 4),
                Text = "Contrato C-XXXX"
            };
            lblInquilino = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(70, 70, 70),
                Margin = new Padding(0, 2, 0, 0),
                Text = "Inquilino: —"
            };
            lblInmueble = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(70, 70, 70),
                Margin = new Padding(0, 2, 0, 0),
                Text = "Inmueble: —"
            };
            lblPeriodo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(70, 70, 70),
                Margin = new Padding(0, 2, 0, 0),
                Text = "Inicio – Fin: —"
            };

            tlHeader.Controls.Add(lblContrato, 0, 0);
            tlHeader.Controls.Add(lblInquilino, 0, 1);
            tlHeader.Controls.Add(lblInmueble, 0, 2);
            tlHeader.Controls.Add(lblPeriodo, 0, 3);
            cardHeader.Controls.Add(tlHeader);

            // -- Card métricas
            cardMetrics = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                Margin = new Padding(8, 0, 0, 0),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            cardMetrics.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(235, 235, 235));
                e.Graphics.DrawRectangle(p, 0, 0, cardMetrics.Width - 1, cardMetrics.Height - 1);
            };

            tlMetrics = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                AutoSize = true
            };
            tlMetrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlMetrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlMetrics.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlMetrics.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            lblTotalTitulo = new Label
            {
                Text = "Total",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(90, 90, 90),
                Margin = new Padding(0, 0, 0, 0)
            };
            lblTotalValor = new Label
            {
                Text = "$0",
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                Margin = new Padding(0, 2, 0, 6)
            };

            lblAtrasoTitulo = new Label
            {
                Text = "Atraso",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(90, 90, 90),
                Margin = new Padding(0, 0, 0, 0)
            };
            lblAtrasoValor = new Label
            {
                Text = "$0",
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(200, 60, 40),
                Margin = new Padding(0, 2, 0, 6)
            };

            lblCuotasTitulo = new Label
            {
                Text = "Cuotas",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(90, 90, 90),
                Margin = new Padding(0, 0, 0, 0)
            };
            lblCuotasValor = new Label
            {
                Text = "0",
                AutoSize = true,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                Margin = new Padding(0, 2, 0, 0)
            };

            // Columna izquierda (Total / Cuotas)
            var leftPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                WrapContents = false,
                Dock = DockStyle.Fill
            };
            leftPanel.Controls.Add(lblTotalTitulo);
            leftPanel.Controls.Add(lblTotalValor);
            leftPanel.Controls.Add(lblCuotasTitulo);
            leftPanel.Controls.Add(lblCuotasValor);

            // Columna derecha (Atraso)
            var rightPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                WrapContents = false,
                Dock = DockStyle.Fill
            };
            rightPanel.Controls.Add(lblAtrasoTitulo);
            rightPanel.Controls.Add(lblAtrasoValor);

            tlMetrics.Controls.Add(leftPanel, 0, 0);
            tlMetrics.SetRowSpan(leftPanel, 2);
            tlMetrics.Controls.Add(rightPanel, 1, 0);
            tlMetrics.SetRowSpan(rightPanel, 2);

            cardMetrics.Controls.Add(tlMetrics);

            tlTop.Controls.Add(cardHeader, 0, 0);
            tlTop.Controls.Add(cardMetrics, 1, 0);

            // ===== Tabs header =====
            tabsHeader = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Dock = DockStyle.Top,
                Height = 40,
                Padding = new Padding(0, 4, 0, 4),
                BackColor = Color.White
            };
            tabBtnCuotas = new Button
            {
                Text = "Cuotas",
                FlatStyle = FlatStyle.Flat,
                AutoSize = true,
                Margin = new Padding(0, 0, 8, 0)
            };
            tabBtnCuotas.FlatAppearance.BorderSize = 0;

            tabBtnLineaTiempo = new Button
            {
                Text = "Línea de tiempo",
                FlatStyle = FlatStyle.Flat,
                AutoSize = true,
                Margin = new Padding(0)
            };
            tabBtnLineaTiempo.FlatAppearance.BorderSize = 0;

            tabsHeader.Controls.Add(tabBtnCuotas);
            tabsHeader.Controls.Add(tabBtnLineaTiempo);

            // ===== Filtros =====
            filtersPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 2, 0, 8),
                BackColor = Color.White
            };
            lblEstado = new Label
            {
                Text = "Estado:",
                AutoSize = true,
                Margin = new Padding(0, 8, 6, 0),
                ForeColor = Color.FromArgb(80, 80, 80)
            };
            cboEstado = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 160,
                Margin = new Padding(0, 4, 20, 0)
            };
            lblAnio = new Label
            {
                Text = "Año:",
                AutoSize = true,
                Margin = new Padding(0, 8, 6, 0),
                ForeColor = Color.FromArgb(80, 80, 80)
            };
            cboAnio = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 120,
                Margin = new Padding(0, 4, 0, 0)
            };
            filtersPanel.Controls.Add(lblEstado);
            filtersPanel.Controls.Add(cboEstado);
            filtersPanel.Controls.Add(lblAnio);
            filtersPanel.Controls.Add(cboAnio);

            // ===== Contenido: TabControl (fill) =====
            tabs = new TabControl
            {
                Dock = DockStyle.Fill,
                Appearance = TabAppearance.Normal
            };
            tabCuotas = new TabPage("Cuotas") { BackColor = Color.White };
            tabLineaTiempo = new TabPage("Línea de tiempo") { BackColor = Color.White };

            dgvCuotas = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                GridColor = Color.FromArgb(235, 235, 235),
            };
            colCuota = new DataGridViewTextBoxColumn { Name = "colCuota", HeaderText = "Cuota", FillWeight = 85 };
            colCuotaRaw = new DataGridViewTextBoxColumn { Name = "colCuotaRaw", DataPropertyName = "NroCuota", Visible = false };
            colDeCuotasRaw = new DataGridViewTextBoxColumn { Name = "colDeCuotasRaw", DataPropertyName = "DeCuotas", Visible = false };
            colPeriodo = new DataGridViewTextBoxColumn { Name = "colPeriodo", HeaderText = "Período", DataPropertyName = "Periodo", FillWeight = 140 };
            colMonto = new DataGridViewTextBoxColumn { Name = "colMonto", HeaderText = "Monto", DataPropertyName = "Monto", DefaultCellStyle = new DataGridViewCellStyle { Format = "C0" }, FillWeight = 110 };
            colVencimiento = new DataGridViewTextBoxColumn { Name = "colVencimiento", HeaderText = "Vencimiento", DataPropertyName = "Vencimiento", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }, FillWeight = 120 };
            colEstado = new DataGridViewTextBoxColumn { Name = "colEstado", HeaderText = "Estado", DataPropertyName = "Estado", FillWeight = 110 };
            colAccion = new DataGridViewButtonColumn { Name = "colAccion", HeaderText = "Acciones", UseColumnTextForButtonValue = false, Text = "Pagar", FillWeight = 110 };
            dgvCuotas.Columns.AddRange(new DataGridViewColumn[]
            {
                colCuota, colCuotaRaw, colDeCuotasRaw, colPeriodo, colMonto, colVencimiento, colEstado, colAccion
            });

            lblPager = new Label
            {
                Dock = DockStyle.Bottom,
                Text = "1 - 0 de 0",
                TextAlign = ContentAlignment.MiddleRight,
                Height = 28,
                ForeColor = Color.FromArgb(90, 90, 90)
            };

            tabCuotas.Controls.Add(dgvCuotas);
            tabCuotas.Controls.Add(lblPager);
            tabs.TabPages.Add(tabCuotas);
            tabs.TabPages.Add(tabLineaTiempo);

            // ===== Compose raíz =====
            tlTop.Controls.Add(cardHeader, 0, 0);
            tlTop.Controls.Add(cardMetrics, 1, 0);

            tlRoot.Controls.Add(tlTop, 0, 0);
            tlRoot.Controls.Add(tabsHeader, 0, 1);
            tlRoot.Controls.Add(filtersPanel, 0, 2);
            tlRoot.Controls.Add(tabs, 0, 3);

            Controls.Add(tlRoot);
        }
        #endregion
    }
}
