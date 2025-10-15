using System.Drawing;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    partial class UcReportesAdmin
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private Panel panelHeader;
        private Label lblTitulo;
        private Panel headerRight;
        private DateTimePicker dtpFecha;
        private Button btnHoy;
        private Button btnActualizar;
        private Button btnExportar;

        // KPIs
        private TableLayoutPanel tlpKpis;
        private Panel cardUsuarios, cardInmuebles, cardInquilinos, cardBackups;
        private Label lblKpiUsuarios, lblKpiUsuariosTitulo;
        private Label lblKpiInmuebles, lblKpiInmueblesTitulo;
        private Label lblKpiInquilinos, lblKpiInquilinosTitulo;
        private Label lblKpiBackups, lblKpiBackupsTitulo;

        // Grids
        private TableLayoutPanel tlpGrids;
        private GroupBox gbUsuarios, gbInmuebles, gbInquilinos, gbBackups;
        private DataGridView dgvUsuarios, dgvInmuebles, dgvInquilinos, dgvBackups;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // ===== ROOT =====
            SuspendLayout();
            BackColor = Color.Teal;                 // WEB TEAL
            ForeColor = Color.Black;
            Dock = DockStyle.Fill;

            // ===== Header =====
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(12, 8, 12, 8)
            };

            lblTitulo = new Label
            {
                Dock = DockStyle.Left,
                AutoSize = false,
                Text = "Reportes diarios (Administrador)",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = 420
            };
            try { lblTitulo.Font = new Font("Segoe UI", 12f, FontStyle.Bold); } catch { }

            headerRight = new Panel
            {
                Dock = DockStyle.Right,
                Width = 420
            };

            dtpFecha = new DateTimePicker
            {
                Format = DateTimePickerFormat.Long,
                Width = 220,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(headerRight.Width - 220 - 180, 12)
            };

            btnHoy = new Button
            {
                Text = "Hoy",
                Width = 70,
                Location = new Point(headerRight.Width - 170, 10)
            };
            btnHoy.Click += BtnHoy_Click;

            btnActualizar = new Button
            {
                Text = "Actualizar",
                Width = 90,
                Location = new Point(headerRight.Width - 95, 10)
            };
            btnActualizar.Click += BtnActualizar_Click;

            btnExportar = new Button
            {
                Text = "Exportar",
                Width = 90,
                Location = new Point(headerRight.Width - 95, 30)  // se reubica por Dock fill
            };
            btnExportar.Click += BtnExportar_Click;

            headerRight.Controls.Add(dtpFecha);
            headerRight.Controls.Add(btnHoy);
            headerRight.Controls.Add(btnActualizar);
            headerRight.Controls.Add(btnExportar);

            panelHeader.Controls.Add(headerRight);
            panelHeader.Controls.Add(lblTitulo);

            // ===== KPIs =====
            tlpKpis = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 92,
                BackColor = Color.Teal,
                Padding = new Padding(8, 8, 8, 8),
                ColumnCount = 4,
                RowCount = 1
            };
            tlpKpis.ColumnStyles.Clear();
            for (int i = 0; i < 4; i++)
                tlpKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tlpKpis.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            // “cards” se crean en runtime por BuildCard()

            // ===== Grids 2x2 =====
            tlpGrids = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Teal,
                Padding = new Padding(8, 0, 8, 8),
                ColumnCount = 2,
                RowCount = 2
            };
            tlpGrids.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tlpGrids.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tlpGrids.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            tlpGrids.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));

            // GroupBoxes
            gbUsuarios = new GroupBox { Text = "Usuarios recientes", Dock = DockStyle.Fill, BackColor = Color.White };
            gbInmuebles = new GroupBox { Text = "Inmuebles recientes", Dock = DockStyle.Fill, BackColor = Color.White };
            gbInquilinos = new GroupBox { Text = "Inquilinos recientes", Dock = DockStyle.Fill, BackColor = Color.White };
            gbBackups = new GroupBox { Text = "Backups realizados", Dock = DockStyle.Fill, BackColor = Color.White };

            // DataGrids (se terminarán de configurar en el .cs)
            dgvUsuarios = new DataGridView { Dock = DockStyle.Fill };
            dgvInmuebles = new DataGridView { Dock = DockStyle.Fill };
            dgvInquilinos = new DataGridView { Dock = DockStyle.Fill };
            dgvBackups = new DataGridView { Dock = DockStyle.Fill };

            gbUsuarios.Controls.Add(dgvUsuarios);
            gbInmuebles.Controls.Add(dgvInmuebles);
            gbInquilinos.Controls.Add(dgvInquilinos);
            gbBackups.Controls.Add(dgvBackups);

            tlpGrids.Controls.Add(gbUsuarios, 0, 0);
            tlpGrids.Controls.Add(gbInmuebles, 1, 0);
            tlpGrids.Controls.Add(gbInquilinos, 0, 1);
            tlpGrids.Controls.Add(gbBackups, 1, 1);

            // ===== Add to root =====
            Controls.Add(tlpGrids);
            Controls.Add(tlpKpis);
            Controls.Add(panelHeader);

            Name = "UcReportesAdmin";
            Size = new Size(1162, 832);
            ResumeLayout(false);
        }
    }
}
