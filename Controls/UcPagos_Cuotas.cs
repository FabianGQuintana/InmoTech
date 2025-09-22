using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace InmoTech
{
    [SupportedOSPlatform("windows")]
    public partial class UcPagos_Cuotas : UserControl
    {
        // ======= ViewModels =======
        public class CabeceraContratoVm
        {
            public string NumeroContrato { get; set; } = "";
            public string Inquilino { get; set; } = "";
            public string Inmueble { get; set; } = "";
            public DateTime Inicio { get; set; }
            public DateTime Fin { get; set; }
            public decimal Total { get; set; }
            public decimal Atraso { get; set; }
            public int CantidadCuotas { get; set; }
        }

        private class CuotaVm
        {
            public int NroCuota { get; set; }          // 1..n
            public int DeCuotas { get; set; }          // total del contrato (para col visual "1 / 12")
            public string Periodo { get; set; } = "";  // "Mayo 2024"
            public decimal Monto { get; set; }         // $ 50.000
            public DateTime Vencimiento { get; set; }
            public string Estado { get; set; } = "Pendiente"; // Pendiente / Vencida / Pagada
            public int AnioPeriodo { get; set; }       // para filtro por año
        }

        // ======= Datos & binding =======
        private readonly BindingList<CuotaVm> _data = new();
        private readonly BindingSource _bs = new();

        // ======= Eventos públicos =======
        public event EventHandler<(string contrato, int nroCuota, string periodo, decimal monto, DateTime vencimiento)>? PagarClicked;
        public event EventHandler<(string contrato, int nroCuota)>? VerReciboClicked;

        // ======= Estado =======
        private string _numeroContrato = "";

        public UcPagos_Cuotas()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                _bs.DataSource = _data;
                dgvCuotas.AutoGenerateColumns = false;
                dgvCuotas.DataSource = _bs;

                StyleDataGridView();

                dgvCuotas.CellPainting += DgvCuotas_CellPainting;         // badge Estado
                dgvCuotas.CellFormatting += DgvCuotas_CellFormatting;     // texto botón Acciones
                dgvCuotas.CellContentClick += DgvCuotas_CellContentClick; // click botón acciones

                cboEstado.SelectedIndexChanged += (_, __) => ApplyFilters();
                cboAnio.SelectedIndexChanged += (_, __) => ApplyFilters();

                tabBtnCuotas.Click += (_, __) => tabs.SelectedIndex = 0;
                tabBtnLineaTiempo.Click += (_, __) => ShowTimelinePlaceholder();

                LoadDemo(); // demo
            }
        }

        // ======= API pública =======
        public void LoadContrato(CabeceraContratoVm cabecera)
        {
            _numeroContrato = cabecera.NumeroContrato;

            lblContrato.Text = $"Contrato {cabecera.NumeroContrato}";
            lblInquilino.Text = $"Inquilino: {cabecera.Inquilino}";
            lblInmueble.Text = $"Inmueble: {cabecera.Inmueble}";
            lblPeriodo.Text = $"Inicio – Fin:  {cabecera.Inicio:dd/MM/yyyy} – {cabecera.Fin:dd/MM/yyyy}";

            lblTotalValor.Text = cabecera.Total.ToString("$ #,0.##", CultureInfo.GetCultureInfo("es-AR"));
            lblAtrasoValor.Text = cabecera.Atraso.ToString("$ #,0.##", CultureInfo.GetCultureInfo("es-AR"));
            lblCuotasValor.Text = $"{cabecera.CantidadCuotas}";
        }

        public void SetCuotas(IEnumerable<(int nroCuota, int deCuotas, string periodo, decimal monto, DateTime vencimiento, string estado)> cuotas)
        {
            _data.Clear();
            foreach (var c in cuotas)
            {
                _data.Add(new CuotaVm
                {
                    NroCuota = c.nroCuota,
                    DeCuotas = c.deCuotas,
                    Periodo = c.periodo,
                    Monto = c.monto,
                    Vencimiento = c.vencimiento,
                    Estado = c.estado,
                    AnioPeriodo = InferYearFromPeriodo(c.periodo, c.vencimiento)
                });
            }

            BuildFilterCombos();
            ApplyFilters();
            UpdatePager();
        }

        // ======= Filtros =======
        private void BuildFilterCombos()
        {
            var estados = new[] { "Todas" }
                .Concat(_data.Select(d => d.Estado).Distinct().OrderBy(s => s))
                .ToList();
            cboEstado.DataSource = estados;
            cboEstado.SelectedIndex = 0;

            var years = _data.Select(d => d.AnioPeriodo).Distinct().OrderBy(y => y).ToList();
            if (years.Count == 0) years = new List<int> { DateTime.Today.Year };
            var items = new List<string> { "Todos" };
            items.AddRange(years.Select(y => y.ToString()));
            cboAnio.DataSource = items;
            cboAnio.SelectedIndex = 0;
        }

        private void ApplyFilters()
        {
            string estadoSel = (cboEstado.SelectedItem?.ToString() ?? "Todas");
            string anioSel = (cboAnio.SelectedItem?.ToString() ?? "Todos");

            IEnumerable<CuotaVm> query = _data;

            if (!string.Equals(estadoSel, "Todas", StringComparison.OrdinalIgnoreCase))
                query = query.Where(c => c.Estado.Equals(estadoSel, StringComparison.OrdinalIgnoreCase));

            if (!string.Equals(anioSel, "Todos", StringComparison.OrdinalIgnoreCase) && int.TryParse(anioSel, out int anio))
                query = query.Where(c => c.AnioPeriodo == anio);

            var list = query.OrderBy(c => c.NroCuota).ToList();
            _bs.DataSource = new BindingList<CuotaVm>(list);
            UpdatePager();
        }

        private static int InferYearFromPeriodo(string periodo, DateTime fallback)
        {
            var tokens = (periodo ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length >= 2 && int.TryParse(tokens[^1], out int y)) return y;
            return fallback.Year;
        }

        // ======= Estilo y dibujo =======
        private void StyleDataGridView()
        {
            SetDoubleBuffered(dgvCuotas);

            dgvCuotas.EnableHeadersVisualStyles = false;
            dgvCuotas.BackgroundColor = Color.White;
            dgvCuotas.BorderStyle = BorderStyle.None;
            dgvCuotas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCuotas.RowHeadersVisible = false;
            dgvCuotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCuotas.MultiSelect = false;
            dgvCuotas.AllowUserToAddRows = false;
            dgvCuotas.AllowUserToDeleteRows = false;
            dgvCuotas.AllowUserToResizeRows = false;
            dgvCuotas.ReadOnly = true;

            dgvCuotas.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvCuotas.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 30, 30);
            dgvCuotas.ColumnHeadersDefaultCellStyle.Font = new Font(dgvCuotas.Font, FontStyle.Bold);
            dgvCuotas.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 6, 0, 6);

            dgvCuotas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 244, 234);
            dgvCuotas.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30);
            dgvCuotas.DefaultCellStyle.ForeColor = Color.FromArgb(45, 45, 45);
            dgvCuotas.DefaultCellStyle.BackColor = Color.White;
            dgvCuotas.RowTemplate.Height = 36;
        }

        private void DgvCuotas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var row = dgvCuotas.Rows[e.RowIndex];

            if (dgvCuotas.Columns[e.ColumnIndex].Name == "colCuota")
            {
                int.TryParse(row.Cells["colCuotaRaw"].Value?.ToString(), out int nro);
                int.TryParse(row.Cells["colDeCuotasRaw"].Value?.ToString(), out int de);
                e.Value = $"{nro} / {de}";
                e.FormattingApplied = true;
                return;
            }

            if (dgvCuotas.Columns[e.ColumnIndex].Name == "colAccion")
            {
                string estado = row.Cells["colEstado"].Value?.ToString() ?? string.Empty;
                e.Value = string.Equals(estado, "Pagada", StringComparison.OrdinalIgnoreCase) ? "Ver recibo" : "Pagar";
                e.FormattingApplied = true;
            }
        }

        private void DgvCuotas_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvCuotas.Columns[e.ColumnIndex].Name != "colEstado") return;
            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);

            var estadoText = e.Value?.ToString() ?? "";
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var font = e.CellStyle.Font ?? dgvCuotas.Font;
            var textSize = g.MeasureString(estadoText, font);
            int padX = 10, padY = 4;
            var rect = new Rectangle(
                e.CellBounds.X + (e.CellBounds.Width - (int)textSize.Width - padX * 2) / 2,
                e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height - padY * 2) / 2,
                (int)textSize.Width + padX * 2,
                (int)textSize.Height + padY * 2
            );

            Color back, border;
            if (estadoText.Equals("Pagada", StringComparison.OrdinalIgnoreCase))
            {
                back = Color.FromArgb(253, 226, 239);
                border = Color.FromArgb(250, 200, 220);
            }
            else if (estadoText.Equals("Vencida", StringComparison.OrdinalIgnoreCase))
            {
                back = Color.FromArgb(255, 228, 225);
                border = Color.FromArgb(255, 205, 205);
            }
            else
            {
                back = Color.FromArgb(255, 248, 220);
                border = Color.FromArgb(255, 235, 180);
            }

            using var path = RoundedRect(rect, 10);
            using var b = new SolidBrush(back);
            using var p = new Pen(border);
            g.FillPath(b, path);
            g.DrawPath(p, path);

            using var tb = new SolidBrush(Color.FromArgb(42, 42, 42));
            var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(estadoText, font, tb, rect, format);

            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
        }

        private void DgvCuotas_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var col = dgvCuotas.Columns[e.ColumnIndex];
            if (col == null || col.Name != "colAccion") return;

            var row = dgvCuotas.Rows[e.RowIndex];

            if (!int.TryParse(row.Cells["colCuotaRaw"].Value?.ToString(), out int nroCuota))
                return;

            string estado = row.Cells["colEstado"].Value?.ToString() ?? string.Empty;
            string periodo = row.Cells["colPeriodo"]?.Value?.ToString() ?? "";

            decimal monto = 0m;
            var montoText = (row.Cells["colMonto"]?.Value?.ToString() ?? "")
                            .Replace("$", "").Trim();
            decimal.TryParse(
                montoText,
                NumberStyles.Number | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint,
                CultureInfo.GetCultureInfo("es-AR"),
                out monto);

            DateTime venc = DateTime.MinValue;
            DateTime.TryParse(row.Cells["colVencimiento"]?.Value?.ToString(), out venc);

            if (estado.Equals("Pagada", StringComparison.OrdinalIgnoreCase))
                VerReciboClicked?.Invoke(this, (_numeroContrato, nroCuota));
            else
                PagarClicked?.Invoke(this, (_numeroContrato, nroCuota, periodo, monto, venc));
        }

        // ===== Utilitarios =====
        private static System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 0, 90);
            path.CloseFigure();
            return path;
        }

        private static void SetDoubleBuffered(Control control)
        {
            try
            {
                var prop = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)!;
                prop.SetValue(control, true, null);
            }
            catch { }
        }

        private void ShowTimelinePlaceholder()
        {
            tabs.SelectedIndex = 1;
            MessageBox.Show("La línea de tiempo se implementará más adelante.", "En construcción",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdatePager()
        {
            int total = _data.Count;
            int visibles = (_bs.List?.Count ?? 0);
            lblPager.Text = visibles == 0 ? "0 de 0" : $"1 - {visibles} de {total}";
        }

        // ===== Demo para visualizar sin BD =====
        private void LoadDemo()
        {
            LoadContrato(new CabeceraContratoVm
            {
                NumeroContrato = "C-1024",
                Inquilino = "Juan Pérez",
                Inmueble = "Calle 123 — Dpto 4B",
                Inicio = new DateTime(2024, 05, 01),
                Fin = new DateTime(2025, 04, 30),
                Total = 600000,
                Atraso = 58000,
                CantidadCuotas = 12
            });

            var demo = new List<(int, int, string, decimal, DateTime, string)>
            {
                (1,12,"Mayo 2024",       50000, new DateTime(2024,05,10), "Pagada"),
                (2,12,"Junio 2024",      50000, new DateTime(2024,06,10), "Pagada"),
                (3,12,"Julio 2024",      50000, new DateTime(2024,07,10), "Vencida"),
                (4,12,"Agosto 2024",     50000, new DateTime(2024,08,10), "Pendiente"),
                (5,12,"Septiembre 2024", 50000, new DateTime(2024,09,10), "Pendiente"),
            };
            SetCuotas(demo);
        }
    }
}
