using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class UcPagos_Contratos : UserControl
    {
        // ViewModel simple para la grilla
        private class ContratoVm
        {
            public string Contrato { get; set; } = "";
            public string Inquilino { get; set; } = "";
            public string Inmueble { get; set; } = "";
            public string Estado { get; set; } = "Activo"; // Activo / Inactivo
        }

        private readonly BindingList<ContratoVm> _data = new BindingList<ContratoVm>();
        private readonly BindingSource _bs = new BindingSource();

        // Evento público para que el contenedor navegue a la vista de cuotas
        public event EventHandler<string>? VerCuotasClicked; // string = Nro de contrato

        public UcPagos_Contratos()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                // Datasource
                _bs.DataSource = _data;
                dgvContratos.AutoGenerateColumns = false;
                dgvContratos.DataSource = _bs;

                // Estilo y eventos
                StyleDataGridView();
                dgvContratos.CellPainting += DgvContratos_CellPainting;      // chip de Estado
                dgvContratos.CellContentClick += DgvContratos_CellContentClick;

                txtBuscar.TextChanged += (_, __) => ApplyFilter();
                btnFiltrar.Click += (_, __) => OnFiltrarClick();

                // (Opcional) datos demo para ver la UI
                CargarEjemplos();
            }
        }

        private void OnFiltrarClick()
        {
            // Hook para filtros avanzados (fecha, estado, etc.)
            // Por ahora mostramos un mensaje placeholder.
            MessageBox.Show("Aquí abriremos el panel de filtros…", "Filtros", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ApplyFilter()
        {
            string q = (txtBuscar.Text ?? "").Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(q))
            {
                _bs.DataSource = _data;
                return;
            }

            var filtrados = _data
                .Where(c =>
                    c.Contrato.ToLower().Contains(q) ||
                    c.Inquilino.ToLower().Contains(q) ||
                    c.Inmueble.ToLower().Contains(q) ||
                    c.Estado.ToLower().Contains(q))
                .ToList();

            _bs.DataSource = new BindingList<ContratoVm>(filtrados);
        }

        private void DgvContratos_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Columna botón "Ver cuotas"
            if (dgvContratos.Columns[e.ColumnIndex].Name == "colAcciones")
            {
                var contrato = dgvContratos.Rows[e.RowIndex].Cells["colContrato"].Value?.ToString() ?? "";
                VerCuotasClicked?.Invoke(this, contrato);
            }
        }

        // Dibuja un "badge" redondeado para la columna Estado
        private void DgvContratos_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvContratos.Columns[e.ColumnIndex].Name == "colEstado" && e.Value is string estadoText)
            {
                e.Handled = true;
                e.PaintBackground(e.CellBounds, true);

                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                var text = estadoText;
                var font = e.CellStyle.Font ?? dgvContratos.Font;

                // Medir texto y definir rect del "pill"
                var textSize = g.MeasureString(text, font);
                int padX = 10, padY = 4;
                var rect = new Rectangle(
                    e.CellBounds.X + (e.CellBounds.Width - (int)textSize.Width - padX * 2) / 2,
                    e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height - padY * 2) / 2,
                    (int)textSize.Width + padX * 2,
                    (int)textSize.Height + padY * 2
                );

                // Colores según estado
                Color back = text.Equals("Activo", StringComparison.OrdinalIgnoreCase)
                    ? Color.FromArgb(216, 245, 224)     // verde suave
                    : Color.FromArgb(234, 236, 239);    // gris suave

                Color border = text.Equals("Activo", StringComparison.OrdinalIgnoreCase)
                    ? Color.FromArgb(163, 230, 186)
                    : Color.FromArgb(208, 213, 221);

                using var path = RoundedRect(rect, 10);
                using var b = new SolidBrush(back);
                using var p = new Pen(border);

                g.FillPath(b, path);
                g.DrawPath(p, path);

                // Texto centrado
                using var tb = new SolidBrush(Color.FromArgb(42, 42, 42));
                var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(text, font, tb, rect, format);

                // Borde de celda (opcional)
                e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
            }
        }

        // Helpers
        private static System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void StyleDataGridView()
        {
            // Evitar parpadeo
            SetDoubleBuffered(dgvContratos);

            dgvContratos.EnableHeadersVisualStyles = false;
            dgvContratos.BackgroundColor = Color.White;
            dgvContratos.BorderStyle = BorderStyle.None;
            dgvContratos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvContratos.RowHeadersVisible = false;
            dgvContratos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContratos.MultiSelect = false;
            dgvContratos.AllowUserToAddRows = false;
            dgvContratos.AllowUserToDeleteRows = false;
            dgvContratos.AllowUserToResizeRows = false;
            dgvContratos.ReadOnly = true;

            dgvContratos.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvContratos.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 30, 30);
            dgvContratos.ColumnHeadersDefaultCellStyle.Font = new Font(dgvContratos.Font, FontStyle.Bold);
            dgvContratos.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 6, 0, 6);

            dgvContratos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 244, 234);
            dgvContratos.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30);
            dgvContratos.DefaultCellStyle.ForeColor = Color.FromArgb(45, 45, 45);
            dgvContratos.DefaultCellStyle.BackColor = Color.White;
            dgvContratos.RowTemplate.Height = 36;
        }

        private static void SetDoubleBuffered(Control control)
        {
            try
            {
                PropertyInfo prop =
                    typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)!;
                prop.SetValue(control, true, null);
            }
            catch { /* ignore */ }
        }

        // Demo data para visualizar (podés borrar)
        private void CargarEjemplos()
        {
            var ej = new List<ContratoVm>
            {
                new(){ Contrato="C-1024", Inquilino="Juan Pérez",   Inmueble="Calle 123 — Dpto 4B", Estado="Activo"},
                new(){ Contrato="C-1025", Inquilino="Ana Gómez",    Inmueble="Calle 123 — Dpo 4B", Estado="Activo"},
                new(){ Contrato="C-1027", Inquilino="Inquilino 3",  Inmueble="Avenida 456",        Estado="Inactivo"},
                new(){ Contrato="C-1028", Inquilino="María López",  Inmueble="Calle 123",          Estado="Activo"},
                new(){ Contrato="C-1026", Inquilino="Carlos Ortega",Inmueble="Avenida 456",        Estado="Activo"},
                new(){ Contrato="C-1029", Inquilino="Inquilino 6",  Inmueble="Avenida 456",        Estado="Inactivo"},
            };
            foreach (var c in ej) _data.Add(c);
        }

        // API mínima para que cargues datos reales
        public void SetContratos(IEnumerable<(string contrato, string inquilino, string inmueble, string estado)> contratos)
        {
            _data.Clear();
            foreach (var (contrato, inquilino, inmueble, estado) in contratos)
                _data.Add(new ContratoVm { Contrato = contrato, Inquilino = inquilino, Inmueble = inmueble, Estado = estado });
            ApplyFilter();
        }
        // Devuelve (Inquilino, Inmueble) del contrato mostrado en la grilla.
        // No rompe nada y la usaremos desde MainForm.
        public (string Inquilino, string Inmueble)? GetMinHeaderFor(string nroContrato)
        {
            // Buscar la fila por número de contrato
            var row = dgvContratos.Rows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(r => string.Equals(
                    r.Cells["colContrato"].Value?.ToString(),
                    nroContrato,
                    StringComparison.OrdinalIgnoreCase));

            if (row == null) return null;

            string inquilino = row.Cells["colInquilino"].Value?.ToString() ?? "";
            string inmueble = row.Cells["colInmueble"].Value?.ToString() ?? "";
            return (inquilino, inmueble);
        }

    }
}
