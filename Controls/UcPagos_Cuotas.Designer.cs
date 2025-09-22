using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    partial class UcPagos_Cuotas
    {
        private System.ComponentModel.IContainer components = null;

        private Panel cardHeader;
        private Label lblContrato;
        private Label lblInquilino;
        private Label lblInmueble;
        private Label lblPeriodo;

        private Panel cardMetrics;
        private Label lblTotalTitulo;
        private Label lblTotalValor;
        private Label lblAtrasoTitulo;
        private Label lblAtrasoValor;
        private Label lblCuotasTitulo;
        private Label lblCuotasValor;

        private Panel tabsHeader;
        private Button tabBtnCuotas;
        private Button tabBtnLineaTiempo;

        // 🔁 Cambiado a FlowLayoutPanel (mismo nombre)
        private FlowLayoutPanel filtersPanel;
        private Label lblEstado;
        private ComboBox cboEstado;
        private Label lblAnio;
        private ComboBox cboAnio;

        private TabControl tabs;
        private TabPage tabCuotas;
        private TabPage tabLineaTiempo;

        private DataGridView dgvCuotas;
        private Label lblPager;

        // Columns
        private DataGridViewTextBoxColumn colCuota;          // muestra "n / de"
        private DataGridViewTextBoxColumn colCuotaRaw;       // oculto
        private DataGridViewTextBoxColumn colDeCuotasRaw;    // oculto
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
            SuspendLayout();

            BackColor = Color.White;
            AutoScaleMode = AutoScaleMode.Dpi;
            Name = "UcPagos_Cuotas";
            Size = new Size(1000, 680);

            // ======= Card cabecera =======
            cardHeader = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Location = new Point(20, 16),
                Size = new Size(960, 110),
                Padding = new Padding(16)
            };
            cardHeader.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(235, 235, 235));
                e.Graphics.DrawRectangle(p, 0, 0, cardHeader.Width - 1, cardHeader.Height - 1);
            };

            lblContrato = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(35, 35, 35),
                Location = new Point(16, 12),
                Text = "Contrato C-XXXX"
            };

            lblInquilino = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(18, 46),
                Text = "Inquilino: —"
            };

            lblInmueble = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(18, 66),
                Text = "Inmueble: —"
            };

            lblPeriodo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(18, 86),
                Text = "Inicio – Fin: —"
            };

            // ======= Card métricas =======
            cardMetrics = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Location = new Point(640, 16),
                Size = new Size(340, 110),
                Padding = new Padding(12)
            };
            cardMetrics.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(235, 235, 235));
                e.Graphics.DrawRectangle(p, 0, 0, cardMetrics.Width - 1, cardMetrics.Height - 1);
            };

            lblTotalTitulo = new Label
            {
                Text = "Total",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                Location = new Point(14, 14),
                ForeColor = Color.FromArgb(90, 90, 90)
            };
            lblTotalValor = new Label
            {
                Text = "$0",
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(14, 34),
                ForeColor = Color.FromArgb(35, 35, 35)
            };

            lblAtrasoTitulo = new Label
            {
                Text = "Atraso",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                Location = new Point(180, 14),
                ForeColor = Color.FromArgb(90, 90, 90)
            };
            lblAtrasoValor = new Label
            {
                Text = "$0",
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(180, 34),
                ForeColor = Color.FromArgb(200, 60, 40)
            };

            lblCuotasTitulo = new Label
            {
                Text = "Cuotas",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                Location = new Point(14, 70),
                ForeColor = Color.FromArgb(90, 90, 90)
            };
            lblCuotasValor = new Label
            {
                Text = "0",
                AutoSize = true,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Location = new Point(70, 70),
                ForeColor = Color.FromArgb(35, 35, 35)
            };

            cardHeader.Controls.Add(lblContrato);
            cardHeader.Controls.Add(lblInquilino);
            cardHeader.Controls.Add(lblInmueble);
            cardHeader.Controls.Add(lblPeriodo);

            cardMetrics.Controls.Add(lblTotalTitulo);
            cardMetrics.Controls.Add(lblTotalValor);
            cardMetrics.Controls.Add(lblAtrasoTitulo);
            cardMetrics.Controls.Add(lblAtrasoValor);
            cardMetrics.Controls.Add(lblCuotasTitulo);
            cardMetrics.Controls.Add(lblCuotasValor);

            // ======= Tabs header =======
            tabsHeader = new Panel
            {
                BackColor = Color.White,
                Location = new Point(20, 140),
                Size = new Size(960, 36)
            };
            tabBtnCuotas = new Button
            {
                Text = "Cuotas",
                FlatStyle = FlatStyle.Flat,
                Location = new Point(0, 0),
                Size = new Size(90, 32)
            };
            tabBtnCuotas.FlatAppearance.BorderSize = 0;

            tabBtnLineaTiempo = new Button
            {
                Text = "Línea de tiempo",
                FlatStyle = FlatStyle.Flat,
                Location = new Point(100, 0),
                Size = new Size(140, 32)
            };
            tabBtnLineaTiempo.FlatAppearance.BorderSize = 0;

            tabsHeader.Controls.Add(tabBtnCuotas);
            tabsHeader.Controls.Add(tabBtnLineaTiempo);

            // ======= Filtros (FlowLayoutPanel) =======
            filtersPanel = new FlowLayoutPanel
            {
                BackColor = Color.White,
                Location = new Point(20, 176),
                Size = new Size(960, 36),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 4, 0, 4)
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

            // ======= TabControl =======
            tabs = new TabControl
            {
                Location = new Point(20, 212),
                Size = new Size(960, 420),
                Appearance = TabAppearance.Normal
            };
            tabCuotas = new TabPage("Cuotas") { BackColor = Color.White };
            tabLineaTiempo = new TabPage("Línea de tiempo") { BackColor = Color.White };

            // ======= DataGridView =======
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

            // Columnas
            colCuota = new DataGridViewTextBoxColumn
            {
                Name = "colCuota",
                HeaderText = "Cuota",
                FillWeight = 85
            };
            colCuotaRaw = new DataGridViewTextBoxColumn
            {
                Name = "colCuotaRaw",
                DataPropertyName = "NroCuota",
                Visible = false
            };
            colDeCuotasRaw = new DataGridViewTextBoxColumn
            {
                Name = "colDeCuotasRaw",
                DataPropertyName = "DeCuotas",
                Visible = false
            };
            colPeriodo = new DataGridViewTextBoxColumn
            {
                Name = "colPeriodo",
                HeaderText = "Período",
                DataPropertyName = "Periodo",
                FillWeight = 140
            };
            colMonto = new DataGridViewTextBoxColumn
            {
                Name = "colMonto",
                HeaderText = "Monto",
                DataPropertyName = "Monto",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C0" },
                FillWeight = 110
            };
            colVencimiento = new DataGridViewTextBoxColumn
            {
                Name = "colVencimiento",
                HeaderText = "Vencimiento",
                DataPropertyName = "Vencimiento",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" },
                FillWeight = 120
            };
            colEstado = new DataGridViewTextBoxColumn
            {
                Name = "colEstado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                FillWeight = 110
            };
            colAccion = new DataGridViewButtonColumn
            {
                Name = "colAccion",
                HeaderText = "Acciones",
                UseColumnTextForButtonValue = false,
                Text = "Pagar",
                FillWeight = 110
            };

            dgvCuotas.Columns.AddRange(new DataGridViewColumn[]
            {
                colCuota, colCuotaRaw, colDeCuotasRaw, colPeriodo, colMonto, colVencimiento, colEstado, colAccion
            });

            // Pager
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

            // Add controls to root
            Controls.Add(cardHeader);
            Controls.Add(cardMetrics);
            Controls.Add(tabsHeader);
            Controls.Add(filtersPanel);  // <- ahora es FlowLayoutPanel
            Controls.Add(tabs);

            ResumeLayout(false);
        }
        #endregion
    }
}
