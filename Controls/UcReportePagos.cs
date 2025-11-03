// InmoTech.Controls.UcReportePagos.cs
using InmoTech.Models;
using InmoTech.Repositories;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcReportePagos : UserControl
    {
        private readonly DateTime _desde, _hasta;
        private readonly ReportePropietarioRepository _repo = new();

        private ComboBox cboUsuario, cboMetodo;
        private CheckBox chkTodosUsuarios, chkTodosMetodos;
        private DataGridView grid;
        private Label lblTotalImporte, lblCantidad;
        private Button btnBuscar, btnExport;

        public UcReportePagos(DateTime desde, DateTime hasta)
        {
            _desde = desde; _hasta = hasta;
            UiTheme.EnableHighDpi(this);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;

            BuildUi();
            CargarCombos();
            CargarDatos();
        }

        private void BuildUi()
        {
            SuspendLayout();

            // ===== LAYOUT RAÍZ (no se superponen secciones) =====
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = UiTheme.Paper,
                Padding = new Padding(10),
                ColumnCount = 1,
                RowCount = 4
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // Filtros
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // KPIs
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Grid
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // Footer (Exportar)
            Controls.Add(root);

            // ===== FILTROS (fluido / responsive) =====
            var filtros = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.White,
                Padding = new Padding(10),
                Margin = new Padding(0, 0, 0, 6),
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight
            };

            var lblUsuario = new Label { Text = "Usuario/Operador:", AutoSize = true, Margin = new Padding(0, 8, 6, 0) };
            cboUsuario = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 190, Margin = new Padding(0, 4, 10, 4) };
            chkTodosUsuarios = new CheckBox { Text = "Todos", Checked = true, AutoSize = true, Margin = new Padding(0, 8, 16, 4) };

            var lblMetodo = new Label { Text = "Método de pago:", AutoSize = true, Margin = new Padding(0, 8, 6, 0) };
            cboMetodo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 210, Margin = new Padding(0, 4, 10, 4) };
            chkTodosMetodos = new CheckBox { Text = "Todos", Checked = true, AutoSize = true, Margin = new Padding(0, 8, 16, 4) };

            btnBuscar = UiTheme.PrimaryButton("Buscar");
            btnBuscar.Width = 110;
            btnBuscar.Margin = new Padding(0, 4, 0, 4);

            filtros.Controls.AddRange(new Control[] { lblUsuario, cboUsuario, chkTodosUsuarios, lblMetodo, cboMetodo, chkTodosMetodos, btnBuscar });
            root.Controls.Add(filtros, 0, 0);

            // ===== KPIs =====
            var kpis = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(4),
                Margin = new Padding(0, 0, 0, 6),
                BackColor = UiTheme.Paper
            };
            var c1 = UiTheme.KpiCard("Total pagado", out lblTotalImporte, UiTheme.Success);
            c1.MinimumSize = new Size(250, 95);
            var c2 = UiTheme.KpiCard("Cantidad de pagos", out lblCantidad, UiTheme.Warning);
            c2.MinimumSize = new Size(250, 95);
            kpis.Controls.AddRange(new Control[] { c1, c2 });
            root.Controls.Add(kpis, 0, 1);

            // ===== GRID =====
            grid = new DataGridView();
            UiTheme.StyleGrid(grid, UiTheme.Primary);
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            grid.ColumnHeadersHeight = 34;
            grid.ScrollBars = ScrollBars.Both;
            grid.Margin = new Padding(0, 2, 0, 6);
            root.Controls.Add(grid, 0, 2);           // fila 2 (no se tapa con filtros ni KPIs)

            // ===== FOOTER (Exportar) =====
            var footer = new Panel { Dock = DockStyle.Top, Height = 46, Padding = new Padding(0, 4, 0, 4) };
            btnExport = UiTheme.PrimaryButton("Exportar a Excel");
            btnExport.Width = 160;
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            footer.Controls.Add(btnExport);
            footer.Resize += (s, e) =>
                btnExport.Location = new Point(footer.Width - btnExport.Width, 4);
            root.Controls.Add(footer, 0, 3);

            // ===== EVENTOS =====
            btnBuscar.Click += (s, e) => CargarDatos();
            chkTodosUsuarios.CheckedChanged += (s, e) => cboUsuario.Enabled = !chkTodosUsuarios.Checked;
            chkTodosMetodos.CheckedChanged += (s, e) => cboMetodo.Enabled = !chkTodosMetodos.Checked;
            btnExport.Click += (s, e) => GridExporter.ExportToCsv(grid, "pagos_realizados.csv");

            ResumeLayout();
        }

        private void CargarCombos()
        {
            var ops = _repo.ListarOperadores();   // solo rol=2 en el repo
            var mps = _repo.ListarMetodosPago();  // lista fija

            cboUsuario.DataSource = ops;
            cboUsuario.ValueMember = nameof(OpcionCombo.Id);
            cboUsuario.DisplayMember = nameof(OpcionCombo.Nombre);

            cboMetodo.DataSource = mps;
            cboMetodo.ValueMember = nameof(OpcionCombo.Id);
            cboMetodo.DisplayMember = nameof(OpcionCombo.Nombre);

            // “Todos” marcado deshabilita el combo correspondiente
            cboUsuario.Enabled = false;
            cboMetodo.Enabled = false;
        }

        private void CargarDatos()
        {
            int? idUsuario = chkTodosUsuarios.Checked ? (int?)null : (int?)((OpcionCombo)cboUsuario.SelectedItem).Id;
            int? idMetodo = chkTodosMetodos.Checked ? (int?)null : (int?)((OpcionCombo)cboMetodo.SelectedItem).Id;

            var desde = _desde.Date;
            var hasta = _hasta.Date;

            var data = _repo.ObtenerPagosRealizados(desde, hasta, idUsuario, idMetodo);
            grid.DataSource = data;

            // ===== Columnas visibles (solo las importantes) =====
            if (grid.Columns.Count > 0)
            {
                // Mostrar legibles
                grid.Columns[nameof(PagoRealizado.FechaPago)].HeaderText = "Fecha";
                grid.Columns[nameof(PagoRealizado.FechaPago)].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                grid.Columns[nameof(PagoRealizado.Inmueble)].HeaderText = "Inmueble";
                grid.Columns[nameof(PagoRealizado.Inquilino)].HeaderText = "Inquilino";
                grid.Columns[nameof(PagoRealizado.NroCuota)].HeaderText = "Cuota";
                grid.Columns[nameof(PagoRealizado.Detalle)].HeaderText = "Detalle";
                grid.Columns[nameof(PagoRealizado.Operador)].HeaderText = "Operador";
                grid.Columns[nameof(PagoRealizado.MetodoPago)].HeaderText = "Método";
                grid.Columns[nameof(PagoRealizado.MontoTotal)].HeaderText = "Importe";
                grid.Columns[nameof(PagoRealizado.MontoTotal)].DefaultCellStyle.Format = "C2";

                // Ocultar PK/FK y auxiliares
                HideIfExists(nameof(PagoRealizado.IdPago));        // PK
                HideIfExists(nameof(PagoRealizado.IdContrato));    // FK
                HideIfExists(nameof(PagoRealizado.IdInmueble));    // FK
                HideIfExists(nameof(PagoRealizado.IdPersona));     // FK
                HideIfExists(nameof(PagoRealizado.IdUsuario));     // FK
                HideIfExists(nameof(PagoRealizado.IdMetodoPago));  // FK

                // Ajustes de ancho: que "Importe" respire
                grid.Columns[nameof(PagoRealizado.MontoTotal)].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
            }

            lblCantidad.Text = data.Count.ToString("N0", CultureInfo.GetCultureInfo("es-AR"));
            lblTotalImporte.Text = data.Sum(x => x.MontoTotal).ToString("C2", CultureInfo.GetCultureInfo("es-AR"));
        }

        private void HideIfExists(string columnName)
        {
            if (grid.Columns.Contains(columnName))
                grid.Columns[columnName].Visible = false;
        }
    }
}
