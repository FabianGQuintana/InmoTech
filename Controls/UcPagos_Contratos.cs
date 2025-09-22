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
        // ================================
        // ViewModel simple para la grilla
        // ================================
        private class ContratoVm
        {
            public string Contrato { get; set; } = "";
            public string Inquilino { get; set; } = "";
            public string Inmueble { get; set; } = "";
            public string Estado { get; set; } = "Activo"; // Activo / Inactivo
        }

        private readonly BindingList<ContratoVm> _data = new();
        private readonly BindingSource _bs = new();

        // Evento público para que el contenedor navegue a la vista de cuotas
        public event EventHandler<string>? VerCuotasClicked; // string = Nro de contrato

        public UcPagos_Contratos()
        {
            InitializeComponent();

            // Evita parpadeo
            SetDoubleBuffered(this);
            SetDoubleBuffered(dgvContratos);

            if (!DesignMode)
            {
                // Datasource base
                _bs.DataSource = _data;

                // Grilla
                dgvContratos.AutoGenerateColumns = false;
                EnsureColumns();                 // crea columnas si faltan
                dgvContratos.DataSource = _bs;

                // Estilo y eventos
                StyleDataGridView();
                dgvContratos.CellPainting += DgvContratos_CellPainting; // chip Estado
                dgvContratos.CellClick += DgvContratos_CellClick;    // click seguro en botón
                dgvContratos.CellContentClick += DgvContratos_CellContentClick;

                // Búsqueda y filtros
                txtBuscar.TextChanged += (_, __) => ApplyFilter();
                btnFiltrar.Click += (_, __) => OnFiltrarClick();

                // Datos demo (borrar cuando conectes BD)
                CargarEjemplos();
            }
        }

        // =========================================
        // Columnas necesarias (creadas por código)
        // =========================================
        private void EnsureColumns()
        {
            if (dgvContratos.Columns.Count > 0)
                return;

            // Contrato
            var cContrato = new DataGridViewTextBoxColumn
            {
                Name = "colContrato",
                HeaderText = "Contrato",
                DataPropertyName = "Contrato",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                ReadOnly = true
            };

            // Inquilino
            var cInquilino = new DataGridViewTextBoxColumn
            {
                Name = "colInquilino",
                HeaderText = "Inquilino",
                DataPropertyName = "Inquilino",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };

            // Inmueble
            var cInmueble = new DataGridViewTextBoxColumn
            {
                Name = "colInmueble",
                HeaderText = "Inmueble",
                DataPropertyName = "Inmueble",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };

            // Estado (lo pintamos en CellPainting, pero ligamos el valor)
            var cEstado = new DataGridViewTextBoxColumn
            {
                Name = "colEstado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                ReadOnly = true
            };

            // Botón Acciones
            var cAcciones = new DataGridViewButtonColumn
            {
                Name = "colAcciones",
                HeaderText = "Acciones",
                Text = "Ver cuotas",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            dgvContratos.Columns.AddRange(cContrato, cInquilino, cInmueble, cEstado, cAcciones);
        }

        // =========================================
        // Filtros
        // =========================================
        private void OnFiltrarClick()
        {
            // Hook para filtros avanzados (fecha, estado, etc.)
            MessageBox.Show("Aquí abriremos el panel de filtros…", "Filtros",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // =========================================
        // Click en botón "Ver cuotas"
        // =========================================
        private void DgvContratos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var col = dgvContratos.Columns[e.ColumnIndex];
            if (col is DataGridViewButtonColumn && col.Name == "colAcciones")
            {
                var contrato = dgvContratos.Rows[e.RowIndex].Cells["colContrato"].Value?.ToString() ?? "";
                if (!string.IsNullOrWhiteSpace(contrato))
                    VerCuotasClicked?.Invoke(this, contrato);
            }
        }

        // (Opcional) Si apretás justo sobre el texto del botón, también funciona
        private void DgvContratos_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvContratos.Columns[e.ColumnIndex].Name == "colAcciones")
            {
                var contrato = dgvContratos.Rows[e.RowIndex].Cells["colContrato"].Value?.ToString() ?? "";
                if (!string.IsNullOrWhiteSpace(contrato))
                    VerCuotasClicked?.Invoke(this, contrato);
            }
        }

        // =========================================
        // Dibuja un "badge" redondeado para Estado
        // =========================================
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
                bool activo = text.Equals("Activo", StringComparison.OrdinalIgnoreCase);
                Color back = activo ? Color.FromArgb(216, 245, 224) : Color.FromArgb(234, 236, 239);
                Color border = activo ? Color.FromArgb(163, 230, 186) : Color.FromArgb(208, 213, 221);
                Color ink = Color.FromArgb(42, 42, 42);

                using var path = RoundedRect(rect, 10);
                using var b = new SolidBrush(back);
                using var p = new Pen(border);
                using var tb = new SolidBrush(ink);
                var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                g.FillPath(b, path);
                g.DrawPath(p, path);
                g.DrawString(text, font, tb, rect, format);

                // Borde de celda (opcional)
                e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
            }
        }

        // Helpers de dibujo
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

        // =========================================
        // Estilo de la grilla
        // =========================================
        private void StyleDataGridView()
        {
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
                PropertyInfo? prop = typeof(Control).GetProperty("DoubleBuffered",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                prop?.SetValue(control, true, null);
            }
            catch { /* ignore */ }
        }

        // =========================================
        // API pública para datos reales
        // =========================================
        public void SetContratos(IEnumerable<(string contrato, string inquilino, string inmueble, string estado)> contratos)
        {
            _data.Clear();
            foreach (var (contrato, inquilino, inmueble, estado) in contratos)
                _data.Add(new ContratoVm { Contrato = contrato, Inquilino = inquilino, Inmueble = inmueble, Estado = estado });
            ApplyFilter();
        }

        /// <summary>
        /// Devuelve (Inquilino, Inmueble) del contrato mostrado en la grilla.
        /// </summary>
        public (string Inquilino, string Inmueble)? GetMinHeaderFor(string nroContrato)
        {
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

        // =========================================
        // Datos demo (borrar cuando conectes BD)
        // =========================================
        private void CargarEjemplos()
        {
            var ej = new List<ContratoVm>
            {
                new(){ Contrato="C-1024", Inquilino="Juan Pérez",    Inmueble="Calle 123 — Dpto 4B", Estado="Activo"},
                new(){ Contrato="C-1025", Inquilino="Ana Gómez",     Inmueble="Calle 123 — Dpto 4B", Estado="Activo"},
                new(){ Contrato="C-1027", Inquilino="Sofía Méndez",  Inmueble="Avenida 456 — Casa",  Estado="Inactivo"},
                new(){ Contrato="C-1028", Inquilino="María López",   Inmueble="Calle 999 — PH",      Estado="Activo"},
                new(){ Contrato="C-1026", Inquilino="Carlos Ortega", Inmueble="Avenida 456 — Casa",  Estado="Activo"},
                new(){ Contrato="C-1029", Inquilino="Luis Romero",   Inmueble="Boulevard 12 — 1°A",  Estado="Inactivo"},
            };
            foreach (var c in ej) _data.Add(c);
        }
    }
}
