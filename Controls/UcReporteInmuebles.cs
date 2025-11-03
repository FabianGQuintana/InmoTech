using InmoTech.Repositories;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcReporteInmuebles : UserControl
    {
        private readonly DateTime _desde, _hasta;
        private readonly ReportePropietarioRepository _repo = new();
        private DataGridView grid;
        private Label lblTotal;

        public UcReporteInmuebles(DateTime desde, DateTime hasta)
        {
            _desde = desde; _hasta = hasta;
            UiTheme.EnableHighDpi(this);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            BuildUi();
            LoadData();
        }

        private void BuildUi()
        {
            UiTheme.EnableHighDpi(this);
            SuspendLayout();

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8),
                ColumnCount = 1,
                RowCount = 3
            };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(root);

            var header = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = UiTheme.Primary, Padding = new Padding(12) };
            header.Controls.Add(new Label { Text = "Ranking de Inmuebles por Ingresos", AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold) });
            root.Controls.Add(header, 0, 0);

            var kpis = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Padding = new Padding(8) };
            var c1 = UiTheme.KpiCard("Ingresos Totales", out lblTotal, UiTheme.Primary);
            kpis.Controls.Add(c1);
            root.Controls.Add(kpis, 0, 1);

            var body = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, Padding = new Padding(0, 4, 0, 0) };
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

            grid = new DataGridView();
            UiTheme.StyleGrid(grid, UiTheme.Primary);
            body.Controls.Add(grid, 0, 0);

            var bar = new Panel { Dock = DockStyle.Fill };
            var export = UiTheme.PrimaryButton("Exportar a Excel");
            export.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bar.Controls.Add(export);
            bar.Resize += (s, e) => export.Location = new Point(bar.Width - export.Width - 8, 4);
            export.Click += (s, e) => GridExporter.ExportToCsv(grid, "inmuebles.csv");

            body.Controls.Add(bar, 0, 1);
            root.Controls.Add(body, 0, 2);

            ResumeLayout();
        }


        private void LoadData()
        {
            var data = _repo.ObtenerRankingInmuebles(_desde, _hasta);
            grid.DataSource = data;
            lblTotal.Text = data.Sum(x => x.Ingresos).ToString("C2");
        }
    }
}
