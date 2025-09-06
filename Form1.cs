using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class Form1 : Form
    {
        private Button _botonActivo;
        private readonly Dictionary<Type, UserControl> _cache = new();

        // Paleta
        private readonly Color BtnBg = Color.White;
        private readonly Color BtnHover = Color.FromArgb(245, 245, 245);
        private readonly Color BtnActive = Color.FromArgb(211, 229, 211);
        private readonly Color BtnText = Color.FromArgb(30, 30, 30);

        // Sidebar ancho (más grande para que no corte el título)
        private const int SidebarWidth = 320;

        public Form1()
        {
            InitializeComponent();

            // --- Ajustes base del Form ---
            MinimumSize = new Size(1200, 700);
            AutoScaleMode = AutoScaleMode.Dpi;
            DoubleBuffered = true;
            Text = "InmoTech";

            // Asegurar marcos de ventana normales (evitamos None del diseñador)
            FormBorderStyle = FormBorderStyle.Sizable;
            ControlBox = true;
            MaximizeBox = true;
            MinimizeBox = true;
            WindowState = FormWindowState.Normal;

            // Layout que se adapta
            ConfigurarLayoutBase();

            // Fix visual de márgenes/header
            ArreglarOffsetsYHeader();
            ReforzarPaddingDeContenido();

            // Estilo de sidebar
            AplicaEstilosSidebar();

            // Handlers (dejamos afuera BContratos: ya lo conectaste vos)
            BDashboard.Click += BDashboard_Click;
            BUsuarios.Click += BUsuarios_Click;
            BInmuebles.Click += BInmuebles_Click;
            BInquilinos.Click += BInquilinos_Click;
            BPagos.Click += BPagos_Click;
            BReportes.Click += BReportes_Click;
            BSalir.Click += BSalir_Click;

            // Vista inicial
            CargarVista<UcDashboard>(BDashboard);
        }

        // ====================== LAYOUT BASE ======================
        private void ConfigurarLayoutBase()
        {
            // Columnas: 0 absoluta (sidebar), 1 expansible
            if (tableLayoutPanel1.ColumnStyles.Count < 2)
            {
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, SidebarWidth));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            }
            else
            {
                tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
                tableLayoutPanel1.ColumnStyles[0].Width = SidebarWidth;
                tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[1].Width = 100;
            }

            if (tableLayoutPanel1.RowStyles.Count == 0)
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            else
            {
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 100;
            }

            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Margin = Padding.Empty;
            tableLayoutPanel1.Padding = Padding.Empty;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            PanelLateral.Dock = DockStyle.Fill;
            PanelLateral.Margin = Padding.Empty;
            PanelLateral.Padding = new Padding(16, 12, 16, 16);
            PanelLateral.AutoScroll = true;

            pnlContent.Dock = DockStyle.Fill;
            pnlContent.AutoScroll = true;
            pnlContent.Margin = Padding.Empty;
            pnlContent.Padding = Padding.Empty;

            this.Resize -= Form1_Resize_Recalc;
            this.Resize += Form1_Resize_Recalc;
            Form1_Resize_Recalc(this, EventArgs.Empty);
        }

        private void Form1_Resize_Recalc(object? sender, EventArgs e)
        {
            tableLayoutPanel1.PerformLayout();
            pnlContent.PerformLayout();
        }

        // ====================== FIX VISUAL ======================
        private void ArreglarOffsetsYHeader()
        {
            // Quitar márgenes heredados del TLP
            tableLayoutPanel1.Margin = Padding.Empty;
            tableLayoutPanel1.Padding = Padding.Empty;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            // Logo
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Size = new Size(64, 64);

            // Título en UNA SOLA LÍNEA
            LTituloLogo.AutoSize = true;
            LTituloLogo.Margin = Padding.Empty;
            LTituloLogo.AutoEllipsis = false;
            LTituloLogo.TextAlign = ContentAlignment.MiddleLeft;
            LTituloLogo.Location = new Point(pictureBox1.Right + 12, pictureBox1.Top + 12);
        }

        private void ReforzarPaddingDeContenido()
        {
            pnlContent.Padding = new Padding(16, 8, 16, 16);
        }

        // ====================== BOTONES ======================
        private Image ScaleIconToButton(Button b, Image original)
        {
            if (original == null) return null!;
            int maxIconSize = b.Height - 20;
            if (maxIconSize < 16) maxIconSize = 16;
            return new Bitmap(original, new Size(maxIconSize, maxIconSize));
        }

        private void EstilizarSidebarButton(Button b, Image icono, string texto)
        {
            b.Text = texto;
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.ImageAlign = ContentAlignment.MiddleLeft;
            b.TextImageRelation = TextImageRelation.ImageBeforeText;
            b.Padding = new Padding(16, 0, 12, 0);

            try { b.Font = new Font("Montserrat", 10f, FontStyle.Bold); } catch { }

            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseDownBackColor = BtnHover;
            b.FlatAppearance.MouseOverBackColor = BtnHover;

            b.BackColor = BtnBg;
            b.ForeColor = BtnText;

            b.Image = icono != null ? ScaleIconToButton(b, icono) : null;

            b.MouseEnter -= Boton_MouseEnter;
            b.MouseLeave -= Boton_MouseLeave;
            b.MouseEnter += Boton_MouseEnter;
            b.MouseLeave += Boton_MouseLeave;
        }

        private void Boton_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button b && b != _botonActivo) b.BackColor = BtnHover;
        }

        private void Boton_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button b && b != _botonActivo) b.BackColor = BtnBg;
        }

        private void MarcarActivo(Button b)
        {
            if (_botonActivo != null && _botonActivo != b) _botonActivo.BackColor = BtnBg;
            _botonActivo = b;
            _botonActivo.BackColor = BtnActive;
        }

        private void AplicaEstilosSidebar()
        {
            pictureBox1.BackgroundImage = Properties.Resources.logoImnoTech;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            try { LTituloLogo.Font = new Font("Montserrat", 16f, FontStyle.Bold); } catch { }

            EstilizarSidebarButton(BDashboard, Properties.Resources.dashboardIcon, "Dashboard");
            EstilizarSidebarButton(BUsuarios, Properties.Resources.usuariosIcon, "Usuarios");
            EstilizarSidebarButton(BInmuebles, Properties.Resources.inmueblesIcon, "Inmuebles");
            EstilizarSidebarButton(BInquilinos, Properties.Resources.inquilinosIcon, "Inquilinos");
            EstilizarSidebarButton(BContratos, Properties.Resources.contratosIcon, "Contratos");
            EstilizarSidebarButton(BPagos, Properties.Resources.pagosIcon, "Pagos");
            EstilizarSidebarButton(BReportes, Properties.Resources.reportesIcon, "Reportes");
            EstilizarSidebarButton(BSalir, Properties.Resources.botonSalir, "Salir");

            pnlContent.BackColor = Color.WhiteSmoke;
        }

        // ====================== NAVEGACIÓN ======================
        private void CargarVista<T>(Button origen = null) where T : UserControl, new()
        {
            if (origen != null) MarcarActivo(origen);

            if (!_cache.TryGetValue(typeof(T), out var vista))
            {
                vista = new T { Dock = DockStyle.Fill, AutoScroll = true };
                _cache[typeof(T)] = vista;
            }

            pnlContent.SuspendLayout();
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(vista);
            vista.BringToFront();
            pnlContent.ResumeLayout();
        }

        // Handlers de los botones
        private void BDashboard_Click(object sender, EventArgs e) => CargarVista<UcDashboard>(BDashboard);
        private void BUsuarios_Click(object sender, EventArgs e) => CargarVista<UcUsuarios>(BUsuarios);
        private void BInmuebles_Click(object sender, EventArgs e) => CargarVista<UcInmuebles>(BInmuebles);
        private void BInquilinos_Click(object sender, EventArgs e) => CargarVista<UcInquilinos>(BInquilinos);
        private void BContratos_Click(object sender, EventArgs e) => CargarVista<UcContratos>(BContratos);
        private void BPagos_Click(object sender, EventArgs e) => CargarVista<UcPagos>(BPagos);
        private void BReportes_Click(object sender, EventArgs e) => CargarVista<UcReportes>(BReportes);

        // Otros handlers vacíos (si no se usan, podés quitarlos)
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }

        private void BSalir_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("¿Desea salir de InmoTech?", "Confirmar salida",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes) Close();
        }
    }
}
