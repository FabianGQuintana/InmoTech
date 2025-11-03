using InmoTech.Repositories;
using InmoTech.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcReporteMorosidad : UserControl
    {
        private readonly DateTime _desde, _hasta;
        private readonly ReportePropietarioRepository _repo = new();
        private DataGridView grid;
        private Label lblCant, lblMonto;

        public UcReporteMorosidad(DateTime desde, DateTime hasta)
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

            var header = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = UiTheme.Danger, Padding = new Padding(12) };
            header.Controls.Add(new Label { Text = "Cuotas Vencidas e Impagas", AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold) });
            root.Controls.Add(header, 0, 0);

            var kpis = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Padding = new Padding(8) };
            var c1 = UiTheme.KpiCard("Cuotas Vencidas", out lblCant, UiTheme.Danger);
            var c2 = UiTheme.KpiCard("Monto Estimado", out lblMonto, UiTheme.Warning);
            kpis.Controls.AddRange(new Control[] { c1, c2 });
            root.Controls.Add(kpis, 0, 1);

            var body = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, Padding = new Padding(0, 4, 0, 0) };
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

            grid = new DataGridView();
            UiTheme.StyleGrid(grid, UiTheme.Danger);
            body.Controls.Add(grid, 0, 0);

            var bar = new Panel { Dock = DockStyle.Fill };
            var export = UiTheme.PrimaryButton("Exportar a Excel");
            export.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bar.Controls.Add(export);
            bar.Resize += (s, e) => export.Location = new Point(bar.Width - export.Width - 8, 4);
            export.Click += (s, e) => GridExporter.ExportToCsv(grid, "morosidad.csv");

            body.Controls.Add(bar, 0, 1);
            root.Controls.Add(body, 0, 2);

            ResumeLayout();
        }


        private void LoadData()
        {
            var data = _repo.ObtenerCuotasVencidasImpagas(_desde, _hasta);
            grid.DataSource = data;
            lblCant.Text = data.Count.ToString();
            lblMonto.Text = data.Sum(x => x.Importe).ToString("C2");
        }
    }
}
