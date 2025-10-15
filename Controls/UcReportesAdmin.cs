using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcReportesAdmin : UserControl
    {

        // ======================================================
        //  REGIÓN: Campos
        // ======================================================
        #region Campos

        // Header creado en runtime (no interfiere con el diseñador)
        private TableLayoutPanel _header;

        #endregion

        // ======================================================
        //  REGIÓN: Constructor
        // ======================================================

        #region Constructor

        public UcReportesAdmin()
        {
            InitializeComponent();
            Load += UcReportesAdmin_Load;
        }

        #endregion

        // ======================================================
        //  REGIÓN: Ciclo de vida 
        // ======================================================

        #region Ciclo de vida

        private void UcReportesAdmin_Load(object? sender, EventArgs e)
        {
            // Fondo TEAL en toda la vista
            BackColor = Color.Teal;
            tlpKpis.BackColor = Color.Teal;
            tlpGrids.BackColor = Color.Teal;

            EnsureHeaderBuilt();  // header con título + exportar + fecha/hoy/actualizar
            EnsureKpisBuilt();    // cards KPI si faltan
            EnsureGridsBuilt();   // columnas de las grillas

            dtpFecha.Value = DateTime.Today;

            CargarDatosPrueba();
            ConfigurarColumnas();
            ActualizarKpis();
        }

        #endregion

        // ======================================================
        //  REGIÓN: Header
        // ======================================================

        #region Header

        private void EnsureHeaderBuilt()
        {
            if (_header != null && !_header.IsDisposed) return;

            // Header: [ Título ][ Exportar ][espaciador][ Fecha ][ Hoy ][ Actualizar ]
            _header = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(12, 8, 12, 8),
                ColumnCount = 6,
                RowCount = 1
            };
            _header.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));          // Título
            _header.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));          // Exportar
            _header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));    // Espaciador
            _header.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));          // Fecha
            _header.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));          // Hoy
            _header.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));          // Actualizar

            // Aseguro instancias (el diseñador puede no haberlas creado aún en este punto)
            if (lblTitulo == null || lblTitulo.IsDisposed)
                lblTitulo = new Label();

            lblTitulo.AutoSize = true;
            lblTitulo.Text = "Reportes diarios (Administrador)";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            lblTitulo.Margin = new Padding(0, 6, 12, 0);
            try { lblTitulo.Font = new Font("Segoe UI", 12f, FontStyle.Bold); } catch { }

            if (btnExportar == null || btnExportar.IsDisposed)
                btnExportar = new Button();
            btnExportar.Text = "Exportar";
            btnExportar.AutoSize = true;
            btnExportar.Margin = new Padding(0, 2, 12, 0);
            btnExportar.Click -= BtnExportar_Click;
            btnExportar.Click += BtnExportar_Click;

            // Espaciador
            var spacer = new Panel { Dock = DockStyle.Fill };

            if (dtpFecha == null || dtpFecha.IsDisposed)
                dtpFecha = new DateTimePicker();
            dtpFecha.Format = DateTimePickerFormat.Long;
            dtpFecha.Width = 220;
            dtpFecha.Margin = new Padding(0, 2, 8, 0);

            if (btnHoy == null || btnHoy.IsDisposed)
                btnHoy = new Button();
            btnHoy.Text = "Hoy";
            btnHoy.AutoSize = true;
            btnHoy.Margin = new Padding(0, 2, 8, 0);
            btnHoy.Click -= BtnHoy_Click;
            btnHoy.Click += BtnHoy_Click;

            if (btnActualizar == null || btnActualizar.IsDisposed)
                btnActualizar = new Button();
            btnActualizar.Text = "Actualizar";
            btnActualizar.AutoSize = true;
            btnActualizar.Margin = new Padding(0, 2, 0, 0);
            btnActualizar.Click -= BtnActualizar_Click;
            btnActualizar.Click += BtnActualizar_Click;

            _header.Controls.Add(lblTitulo, 1, 0);
            _header.Controls.Add(btnExportar, 2, 0);
            _header.Controls.Add(dtpFecha, 3, 0);
            _header.Controls.Add(btnHoy, 4, 0);
            _header.Controls.Add(btnActualizar, 5, 0);

            Controls.Add(_header);
        }

        #endregion

        // ======================================================
        //  REGIÓN: Headlers (Métodos con eventos)
        // ======================================================

        #region Handlers

        private void BtnHoy_Click(object? sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Today;
            CargarDatosPrueba();
            ActualizarKpis();
        }

        private void BtnActualizar_Click(object? sender, EventArgs e)
        {
            CargarDatosPrueba();
            ActualizarKpis();
        }

        private void BtnExportar_Click(object? sender, EventArgs e)
        {
            try
            {
                var baseDir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                    "reportes_admin");
                if (!Directory.Exists(baseDir)) Directory.CreateDirectory(baseDir);

                ExportGridCsv(dgvUsuarios, Path.Combine(baseDir, "usuarios_recientes.csv"));
                ExportGridCsv(dgvInmuebles, Path.Combine(baseDir, "inmuebles_recientes.csv"));
                ExportGridCsv(dgvInquilinos, Path.Combine(baseDir, "inquilinos_recientes.csv"));
                ExportGridCsv(dgvBackups, Path.Combine(baseDir, "backups_realizados.csv"));

                MessageBox.Show("Exportación realizada en 'reportes_admin' del Escritorio.",
                    "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo exportar.\n" + ex.Message,
                    "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        // ======================================================
        //  REGIÓN: KPIs y Grids (construcción)
        // ======================================================

        #region KPIs y Grids (construcción)

        private void EnsureKpisBuilt()
        {
            // Layout 4 columnas
            if (tlpKpis.ColumnCount != 4)
            {
                tlpKpis.ColumnCount = 4;
                tlpKpis.RowCount = 1;
                tlpKpis.ColumnStyles.Clear();
                for (int i = 0; i < 4; i++)
                    tlpKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
                tlpKpis.RowStyles.Clear();
                tlpKpis.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            }

            tlpKpis.SuspendLayout();

            if (lblKpiUsuarios == null || lblKpiUsuarios.IsDisposed)
            {
                cardUsuarios = BuildCard("Usuarios creados", out lblKpiUsuarios, out lblKpiUsuariosTitulo);
                tlpKpis.Controls.Add(cardUsuarios, 0, 0);
                cardUsuarios.Dock = DockStyle.Fill;
            }
            if (lblKpiInmuebles == null || lblKpiInmuebles.IsDisposed)
            {
                cardInmuebles = BuildCard("Inmuebles creados", out lblKpiInmuebles, out lblKpiInmueblesTitulo);
                tlpKpis.Controls.Add(cardInmuebles, 1, 0);
                cardInmuebles.Dock = DockStyle.Fill;
            }
            if (lblKpiInquilinos == null || lblKpiInquilinos.IsDisposed)
            {
                cardInquilinos = BuildCard("Inquilinos creados", out lblKpiInquilinos, out lblKpiInquilinosTitulo);
                tlpKpis.Controls.Add(cardInquilinos, 2, 0);
                cardInquilinos.Dock = DockStyle.Fill;
            }
            if (lblKpiBackups == null || lblKpiBackups.IsDisposed)
            {
                cardBackups = BuildCard("Backups realizados", out lblKpiBackups, out lblKpiBackupsTitulo);
                tlpKpis.Controls.Add(cardBackups, 3, 0);
                cardBackups.Dock = DockStyle.Fill;
            }

            tlpKpis.ResumeLayout();
        }

        private void EnsureGridsBuilt()
        {
            // ===== Usuarios =====
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear();
            dgvUsuarios.BackgroundColor = Color.White;

            dgvUsuarios.Columns.AddRange(new DataGridViewColumn[]
            {
                BuildTextCol("Usuario", "Usuario", 120, "Usuario"),
                BuildTextCol("Nombre", "Nombre", 220, "Nombre"),
                BuildTextCol("Rol", "Rol", 100, "Rol"),
                BuildTextCol("FechaCreacion", "Fecha creación", 160, "FechaCreacion")
            });

            // ===== Inmuebles =====
            dgvInmuebles.AutoGenerateColumns = false;
            dgvInmuebles.Columns.Clear();
            dgvInmuebles.BackgroundColor = Color.White;

            dgvInmuebles.Columns.AddRange(new DataGridViewColumn[]
            {
                BuildTextCol("Descripcion", "Descripción", 360, "Descripcion"),
                BuildTextCol("Hora", "Hora", 90, "Hora")
            });

            // ===== Inquilinos =====
            dgvInquilinos.AutoGenerateColumns = false;
            dgvInquilinos.Columns.Clear();
            dgvInquilinos.BackgroundColor = Color.White;

            dgvInquilinos.Columns.AddRange(new DataGridViewColumn[]
            {
                BuildTextCol("ApellidoNombre", "Apellido y Nombre", 280, "ApellidoNombre"),
                BuildTextCol("Email", "Email", 260, "Email"),
                BuildTextCol("Telefono", "Teléfono", 150, "Telefono"),
                BuildTextCol("FechaAlta", "Fecha alta", 170, "FechaAlta")
            });

            // ===== Backups =====
            dgvBackups.AutoGenerateColumns = false;
            dgvBackups.Columns.Clear();
            dgvBackups.BackgroundColor = Color.White;

            dgvBackups.Columns.AddRange(new DataGridViewColumn[]
            {
                BuildTextCol("Fecha", "Fecha", 130, "Fecha"),
                BuildTextCol("Hora", "Hora", 90, "Hora"),
                BuildTextCol("Archivo", "Archivo", 380, "Archivo"),
                BuildTextCol("Tamanio", "Tamaño", 120, "Tamanio")
            });
        }

        #endregion

        // ======================================================
        //  REGIÓN: Demo (Datos Estáticos)
        // ======================================================

        #region Demo (datos estáticos)

        private void CargarDatosPrueba()
        {
            // Usuarios
            var dtU = new DataTable();
            dtU.Columns.Add("Usuario");
            dtU.Columns.Add("Nombre");
            dtU.Columns.Add("Rol");
            dtU.Columns.Add("FechaCreacion");
            dtU.Rows.Add("usuario1", "Usuario 1", "Operador", $"{dtpFecha.Value:dd/MM/yyyy} 16:24");
            dtU.Rows.Add("usuario2", "Usuario 2", "Propietario", "{dtpFecha.Value:dd/MM/yyyy} 09:09".Replace("{dtpFecha.Value:dd/MM/yyyy}", $"{dtpFecha.Value:dd/MM/yyyy}"));
            dtU.Rows.Add("usuario3", "Usuario 3", "Operador", $"{dtpFecha.Value:dd/MM/yyyy} 14:14");
            dtU.Rows.Add("usuario4", "Usuario 4", "Propietario", $"{dtpFecha.Value:dd/MM/yyyy} 16:39");
            dgvUsuarios.DataSource = dtU;

            // Inmuebles
            var dtI = new DataTable();
            dtI.Columns.Add("Descripcion");
            dtI.Columns.Add("Hora");
            dtI.Rows.Add("Depto 5 - Piso 4", "19:07");
            dtI.Rows.Add("Depto 39 - Piso 6", "11:11");
            dtI.Rows.Add("Depto 51 - Piso 9", "09:03");
            dtI.Rows.Add("Depto 28 - Piso 3", "12:20");
            dgvInmuebles.DataSource = dtI;

            // Inquilinos
            var dtQ = new DataTable();
            dtQ.Columns.Add("ApellidoNombre");
            dtQ.Columns.Add("Email");
            dtQ.Columns.Add("Telefono");
            dtQ.Columns.Add("FechaAlta");
            dtQ.Rows.Add("Carla Méndez", "carla@mail.com", "11-5555-1111", $"{dtpFecha.Value:dd/MM/yyyy} 10:32");
            dtQ.Rows.Add("Juan Pérez", "juan@mail.com", "11-5555-2222", $"{dtpFecha.Value:dd/MM/yyyy} 11:45");
            dtQ.Rows.Add("Ana Ruiz", "ana@mail.com", "11-5555-3333", $"{dtpFecha.Value:dd/MM/yyyy} 14:10");
            dtQ.Rows.Add("Lucas Pereyra", "lucas@mail.com", "11-5555-4444", $"{dtpFecha.Value:dd/MM/yyyy} 16:55");
            dgvInquilinos.DataSource = dtQ;

            // Backups (orden descendente por fecha/hora)
            var dtB = new DataTable();
            dtB.Columns.Add("Fecha");
            dtB.Columns.Add("Hora");
            dtB.Columns.Add("Archivo");
            dtB.Columns.Add("Tamanio");
            dtB.Rows.Add($"{dtpFecha.Value:dd/MM/yyyy}", "19:40", "InmoTech_FULL_20251012_1940.bak", "312 MB");
            dtB.Rows.Add($"{dtpFecha.Value:dd/MM/yyyy}", "12:05", "InmoTech_DIFF_20251012_1205.bak", "57 MB");
            dtB.Rows.Add($"{dtpFecha.Value.AddDays(-1):dd/MM/yyyy}", "20:01", "InmoTech_FULL_20251011_2001.bak", "309 MB");
            dtB.Rows.Add($"{dtpFecha.Value.AddDays(-2):dd/MM/yyyy}", "19:58", "InmoTech_FULL_20251010_1958.bak", "307 MB");
            var dv = dtB.DefaultView;
            dv.Sort = "Fecha DESC, Hora DESC";
            dgvBackups.DataSource = dv.ToTable();
        }

        #endregion

        // ======================================================
        //  REGIÓN:  Configuración de columnas de las grillas
        // ======================================================

        #region Configuración de columnas (aspecto)

        private void ConfigurarColumnas()
        {
            void TocarGrid(DataGridView g)
            {
                g.ReadOnly = true;
                g.AllowUserToAddRows = false;
                g.AllowUserToDeleteRows = false;
                g.AllowUserToOrderColumns = false;
                g.AllowUserToResizeRows = false;
                g.RowHeadersVisible = false;
                g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // repartir ancho
                g.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }

            void SetFill(DataGridView g, params (string col, float weight, int minWidth)[] spec)
            {
                TocarGrid(g);
                foreach (var (col, weight, min) in spec)
                {
                    if (!g.Columns.Contains(col)) continue;
                    var c = g.Columns[col];
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    c.FillWeight = weight;
                    c.MinimumWidth = min;
                }
            }

            // Usuarios
            SetFill(dgvUsuarios,
                ("Usuario", 18, 120),
                ("Nombre", 38, 200),
                ("Rol", 16, 100),
                ("FechaCreacion", 28, 160));

            // Inmuebles
            SetFill(dgvInmuebles,
                ("Descripcion", 82, 360),
                ("Hora", 18, 90));

            // Inquilinos (más holgado)
            SetFill(dgvInquilinos,
                ("ApellidoNombre", 40, 280),
                ("Email", 32, 260),
                ("Telefono", 12, 150),
                ("FechaAlta", 16, 170));

            // Backups
            SetFill(dgvBackups,
                ("Fecha", 16, 130),
                ("Hora", 10, 90),
                ("Archivo", 54, 380),
                ("Tamanio", 20, 120));
        }

        #endregion

        // ======================================================
        //  REGIÓN:  KPIs (refresco)
        // ======================================================

        #region KPIs (refresco)

        private void ActualizarKpis()
        {
            int u = (dgvUsuarios.DataSource as DataTable)?.Rows.Count ?? 0;
            int im = (dgvInmuebles.DataSource as DataTable)?.Rows.Count ?? 0;
            int iq = (dgvInquilinos.DataSource as DataTable)?.Rows.Count ?? 0;
            int bk = (dgvBackups.DataSource as DataTable)?.Rows.Count ?? 0;

            lblKpiUsuarios.Text = u.ToString();
            lblKpiInmuebles.Text = im.ToString();
            lblKpiInquilinos.Text = iq.ToString();
            lblKpiBackups.Text = bk.ToString();
        }

        #endregion

        // ======================================================
        //  REGIÓN:  Helpers UI
        // ======================================================

        #region Helpers UI

        private Panel BuildCard(string titulo, out Label lblValor, out Label lblTitulo)
        {
            var p = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(0, 96, 96), // TEAL más oscuro para contraste
                Padding = new Padding(12),
                Margin = new Padding(8, 0, 8, 0)
            };

            lblTitulo = new Label
            {
                Text = titulo,
                Dock = DockStyle.Top,
                Height = 18,
                ForeColor = Color.White
            };

            lblValor = new Label
            {
                Text = "0",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 20f, FontStyle.Bold),
                ForeColor = Color.White
            };

            p.Controls.Add(lblValor);
            p.Controls.Add(lblTitulo);
            return p;
        }

        private DataGridViewTextBoxColumn BuildTextCol(
            string name, string header, int minWidth, string dataPropertyName)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                MinimumWidth = minWidth,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = dataPropertyName
            };
        }

        #endregion

        // ======================================================
        //  REGIÓN:  Exportar CSV (Botón)
        // ======================================================

        #region Utilidad: exportar CSV

        private void ExportGridCsv(DataGridView grid, string path)
        {
            if (grid == null) return;

            using var sw = new StreamWriter(path, false, System.Text.Encoding.UTF8);

            var headers = grid.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText);
            sw.WriteLine(string.Join(",", headers.Select(EscapeCsv)));

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                var cells = row.Cells.Cast<DataGridViewCell>()
                    .Select(c => c.Value == null ? "" : c.Value.ToString());
                sw.WriteLine(string.Join(",", cells.Select(EscapeCsv)));
            }
        }

        private static string EscapeCsv(string? s)
        {
            s ??= "";
            return (s.Contains("\"") || s.Contains(",")) ? "\"" + s.Replace("\"", "\"\"") + "\"" : s;
        }

        #endregion
    }
}
