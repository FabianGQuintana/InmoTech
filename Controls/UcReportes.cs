using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class UcReportes : UserControl
    {
        private readonly BindingList<ReporteItem> _data = new();
        private readonly BindingSource _bs = new();
        private List<ReporteItem> _allItems = new();

        public UcReportes()
        {
            InitializeComponent();
            DoubleBuffered = true;

            // Grilla
            dgv.AutoGenerateColumns = false;
            dgv.DataSource = _bs;

            // Formatos y columnas
            dgv.Columns.Clear();
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Fecha", HeaderText = "Fecha", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Tipo", HeaderText = "Tipo", Width = 90 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Estado", HeaderText = "Estado", Width = 110 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Inquilino", HeaderText = "Inquilino", Width = 180 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Inmueble", HeaderText = "Inmueble", Width = 220 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Metodo", HeaderText = "Método", Width = 110 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Monto", HeaderText = "Monto", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "C2" } });

            // Valores por defecto de filtros
            cmbTipoReporte.Items.AddRange(new[] { "Todos", "Pagos", "Contratos" });
            cmbTipoReporte.SelectedIndex = 0;

            cmbEstado.Items.AddRange(new[] { "Todos", "Activo", "Vencido", "Pagado", "Pendiente" });
            cmbEstado.SelectedIndex = 0;

            dpDesde.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dpHasta.Value = DateTime.Today;

            // Eventos
            btnBuscar.Click += (_, __) => Refrescar();
            btnLimpiar.Click += (_, __) => LimpiarFiltros();
            btnExportarCsv.Click += (_, __) => ExportarCsv();
            btnImprimir.Click += (_, __) => Imprimir();

            dpDesde.ValueChanged += (_, __) => Refrescar();
            dpHasta.ValueChanged += (_, __) => Refrescar();
            cmbTipoReporte.SelectedIndexChanged += (_, __) => Refrescar();
            cmbEstado.SelectedIndexChanged += (_, __) => Refrescar();
            cmbInquilino.SelectedIndexChanged += (_, __) => Refrescar();
            cmbInmueble.SelectedIndexChanged += (_, __) => Refrescar();

            Load += UcReportes_Load;
        }

        private void UcReportes_Load(object? sender, EventArgs e)
        {
            // Datos de muestra (reemplazar luego por tu capa de datos/servicios)
            _allItems = FakeData();

            // Poblar combos dependientes
            RellenarCombosAuxiliares();

            Refrescar();
        }

        private void RellenarCombosAuxiliares()
        {
            var inquilinos = _allItems.Select(x => x.Inquilino).Distinct().OrderBy(x => x).ToList();
            var inmuebles = _allItems.Select(x => x.Inmueble).Distinct().OrderBy(x => x).ToList();

            cmbInquilino.Items.Clear();
            cmbInmueble.Items.Clear();

            cmbInquilino.Items.Add("Todos");
            cmbInmueble.Items.Add("Todos");

            cmbInquilino.Items.AddRange(inquilinos.Cast<object>().ToArray());
            cmbInmueble.Items.AddRange(inmuebles.Cast<object>().ToArray());

            cmbInquilino.SelectedIndex = 0;
            cmbInmueble.SelectedIndex = 0;
        }

        private void LimpiarFiltros()
        {
            cmbTipoReporte.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
            cmbInquilino.SelectedIndex = 0;
            cmbInmueble.SelectedIndex = 0;
            dpDesde.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dpHasta.Value = DateTime.Today;
            Refrescar();
        }

        private void Refrescar()
        {
            var desde = dpDesde.Value.Date;
            var hasta = dpHasta.Value.Date.AddDays(1).AddTicks(-1);

            var query = _allItems.Where(x => x.Fecha >= desde && x.Fecha <= hasta);

            if (cmbTipoReporte.SelectedIndex > 0)
            {
                var tipo = cmbTipoReporte.SelectedItem?.ToString();
                query = query.Where(x => x.Tipo.Equals(tipo, StringComparison.OrdinalIgnoreCase));
            }

            if (cmbEstado.SelectedIndex > 0)
            {
                var estado = cmbEstado.SelectedItem?.ToString();
                query = query.Where(x => x.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
            }

            if (cmbInquilino.SelectedIndex > 0)
            {
                var inq = cmbInquilino.SelectedItem?.ToString();
                query = query.Where(x => x.Inquilino.Equals(inq, StringComparison.OrdinalIgnoreCase));
            }

            if (cmbInmueble.SelectedIndex > 0)
            {
                var inm = cmbInmueble.SelectedItem?.ToString();
                query = query.Where(x => x.Inmueble.Equals(inm, StringComparison.OrdinalIgnoreCase));
            }

            var result = query.OrderByDescending(x => x.Fecha).ToList();

            _data.Clear();
            foreach (var it in result) _data.Add(it);
            _bs.DataSource = _data;

            // KPIs
            var total = result.Sum(x => x.Monto);
            var cantidad = result.Count;
            var promedio = cantidad > 0 ? result.Average(x => x.Monto) : 0;

            lblKpiTotal.Text = total.ToString("C2");
            lblKpiCantidad.Text = cantidad.ToString();
            lblKpiPromedio.Text = promedio.ToString("C2");
        }

        private void ExportarCsv()
        {
            if (_data.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = $"Reporte_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            using var sw = new StreamWriter(sfd.FileName);
            sw.WriteLine("Fecha,Tipo,Estado,Inquilino,Inmueble,Método,Monto");
            foreach (var it in _data)
            {
                sw.WriteLine($"{it.Fecha:yyyy-MM-dd},{it.Tipo},{it.Estado},{Escape(it.Inquilino)},{Escape(it.Inmueble)},{it.Metodo},{it.Monto.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
            }

            MessageBox.Show("Exportación completada.", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string Escape(string s)
        {
            if (s.Contains(',') || s.Contains('"'))
                return $"\"{s.Replace("\"", "\"\"")}\"";
            return s;
        }

        private void Imprimir()
        {
            if (_data.Count == 0)
            {
                MessageBox.Show("No hay datos para imprimir.", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var doc = new PrintDocument();
            doc.DocumentName = "Reporte InmoTech";
            int rowIndex = 0;
            int y = 0;

            doc.PrintPage += (s, e) =>
            {
                var g = e.Graphics;
                var margin = new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, e.MarginBounds.Height);

                var titleFont = new Font(FontFamily.GenericSansSerif, 12f, FontStyle.Bold);
                var cellFont = new Font(FontFamily.GenericSansSerif, 9f);

                y = margin.Top;
                g.DrawString("Reporte InmoTech", titleFont, Brushes.Black, margin.Left, y);
                y += 28;

                // Encabezados
                string[] headers = { "Fecha", "Tipo", "Estado", "Inquilino", "Inmueble", "Método", "Monto" };
                int[] widths = { 80, 70, 90, 160, 210, 90, 80 };
                int x = margin.Left;

                for (int i = 0; i < headers.Length; i++)
                {
                    g.DrawString(headers[i], cellFont, Brushes.Black, x, y);
                    x += widths[i];
                }
                y += 22;
                g.DrawLine(Pens.Black, margin.Left, y, margin.Right, y);
                y += 6;

                // Filas
                while (rowIndex < _data.Count)
                {
                    x = margin.Left;
                    var it = _data[rowIndex];

                    string[] cells = {
                        it.Fecha.ToString("dd/MM/yyyy"),
                        it.Tipo,
                        it.Estado,
                        it.Inquilino,
                        it.Inmueble,
                        it.Metodo,
                        it.Monto.ToString("C0")
                    };

                    for (int i = 0; i < cells.Length; i++)
                    {
                        g.DrawString(cells[i], cellFont, Brushes.Black, x, y);
                        x += widths[i];
                    }

                    y += 18;
                    if (y > margin.Bottom - 40)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                    rowIndex++;
                }

                // Resumen
                y += 10;
                g.DrawLine(Pens.Black, margin.Left, y, margin.Right, y);
                y += 8;
                g.DrawString($"Total: {lblKpiTotal.Text}   •   Registros: {lblKpiCantidad.Text}   •   Promedio: {lblKpiPromedio.Text}",
                             cellFont, Brushes.Black, margin.Left, y);

                e.HasMorePages = false;
            };

            using var printDlg = new PrintPreviewDialog { Document = doc, Width = 1200, Height = 800 };
            try { printDlg.ShowDialog(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // -------------------------------------------------------
        // Datos de ejemplo (reemplazar por tu capa de repositorio)
        // -------------------------------------------------------
        private static List<ReporteItem> FakeData()
        {
            var rnd = new Random(7);
            string[] inqs = { "Iván Romero", "Carla Méndez", "Lucas Pereyra", "Ana Ruiz" };
            string[] inmus = { "Rivadavia 567 - Casa Jardín", "Alvear 1917 4B - Dpto", "Junín 1645 - Casa Moderna", "San Martín 802 - Dúplex" };
            string[] metodos = { "Transferencia", "Efectivo", "Tarjeta", "Débito" };

            var list = new List<ReporteItem>();
            var hoy = DateTime.Today;

            // Pagos
            for (int i = 0; i < 80; i++)
            {
                list.Add(new ReporteItem
                {
                    Fecha = hoy.AddDays(-rnd.Next(0, 180)),
                    Tipo = "Pagos",
                    Estado = rnd.Next(0, 10) < 8 ? "Pagado" : "Pendiente",
                    Inquilino = inqs[rnd.Next(inqs.Length)],
                    Inmueble = inmus[rnd.Next(inmus.Length)],
                    Metodo = metodos[rnd.Next(metodos.Length)],
                    Monto = 150000 + rnd.Next(0, 100) * 1000
                });
            }

            // Contratos
            for (int i = 0; i < 35; i++)
            {
                list.Add(new ReporteItem
                {
                    Fecha = hoy.AddDays(-rnd.Next(0, 365)),
                    Tipo = "Contratos",
                    Estado = rnd.Next(0, 3) switch { 0 => "Activo", 1 => "Vencido", _ => "Activo" },
                    Inquilino = inqs[rnd.Next(inqs.Length)],
                    Inmueble = inmus[rnd.Next(inmus.Length)],
                    Metodo = "-",
                    Monto = 0
                });
            }

            return list;
        }
    }

    public class ReporteItem
    {
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = "";
        public string Estado { get; set; } = "";
        public string Inquilino { get; set; } = "";
        public string Inmueble { get; set; } = "";
        public string Metodo { get; set; } = "";
        public decimal Monto { get; set; }
    }
}
