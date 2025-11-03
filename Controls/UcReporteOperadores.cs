using InmoTech.Repositories;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcReporteOperadores : UserControl
    {
        private readonly DateTime _desde, _hasta;
        private readonly ReportePropietarioRepository _repo = new();
        private DataGridView grid;
        private Label lblTotal, lblPagos;

        public UcReporteOperadores(DateTime desde, DateTime hasta)
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
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // header
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // KPIs
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // grid + export
            Controls.Add(root);

            // HEADER
            var header = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = UiTheme.Primary, Padding = new Padding(12) };
            header.Controls.Add(new Label { Text = "Ingresos por Operador", AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold) });
            root.Controls.Add(header, 0, 0);

            // KPIs
            var kpis = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Padding = new Padding(8) };
            var c1 = UiTheme.KpiCard("Ingresos", out lblTotal, UiTheme.Primary);
            var c2 = UiTheme.KpiCard("Pagos", out lblPagos, UiTheme.Success);
            kpis.Controls.AddRange(new Control[] { c1, c2 });
            root.Controls.Add(kpis, 0, 1);

            // GRID + EXPORT
            var body = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, Padding = new Padding(0, 4, 0, 0) };
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

            grid = new DataGridView();
            UiTheme.StyleGrid(grid, UiTheme.Primary);
            body.Controls.Add(grid, 0, 0);

            var bar = new Panel { Dock = DockStyle.Fill, Height = 44 };
            var export = UiTheme.PrimaryButton("Exportar a Excel");
            export.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bar.Controls.Add(export);
            bar.Resize += (s, e) => export.Location = new Point(bar.Width - export.Width - 8, 4);
            export.Click += (s, e) => GridExporter.ExportToCsv(grid, "operadores.csv");

            body.Controls.Add(bar, 0, 1);
            root.Controls.Add(body, 0, 2);

            ResumeLayout();
        }


        private void LoadData()
        {
            var data = _repo.ObtenerIngresosPorOperador(_desde, _hasta);
            grid.DataSource = data;
            lblTotal.Text = data.Sum(x => x.TotalIngresos).ToString("C2");
            lblPagos.Text = data.Sum(x => x.CantidadPagos).ToString();
        }
    }
}
