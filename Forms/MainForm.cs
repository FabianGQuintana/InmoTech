using InmoTech;
using InmoTech.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class MainForm : Form
    {

        // CS8618 -> nullable porque se asigna luego
        private Button? _botonActivo;

        // Cache de vistas
        private readonly Dictionary<Type, UserControl> _cache = new();

        public MainForm()
        {
            InitializeComponent();

            // Lógica de layout (sin estilos)
            this.Resize -= Form_Resize_Recalc;
            this.Resize += Form_Resize_Recalc;

            // Vista inicial
            CargarVista<UcDashboard>(BDashboard);

            // ====== DISEÑO (visual mínimo necesario) ======
            AplicarDisenio();
        }

        // Navegación
        private void CargarVista<T>(Button origen) where T : UserControl, new()
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

        // Marca botón activo (el color se maneja en diseño)
        private void MarcarActivo(Button b)
        {
            if (_botonActivo != null && _botonActivo != b)
                _botonActivo.BackColor = BtnBg;

            _botonActivo = b;
            _botonActivo.BackColor = BtnActive;
        }

        private void Form_Resize_Recalc(object? sender, EventArgs e)
        {
            tableLayoutPanel1?.PerformLayout();
            pnlContent?.PerformLayout();
        }

        // Handlers (lógica pura)
        private void BDashboard_Click(object sender, EventArgs e) => CargarVista<UcDashboard>(BDashboard);
        private void BUsuarios_Click(object sender, EventArgs e) => CargarVista<UcUsuarios>(BUsuarios);
        private void BInmuebles_Click(object sender, EventArgs e) => CargarVista<UcInmuebles>(BInmuebles);
        private void BInquilinos_Click(object sender, EventArgs e) => CargarVista<UcInquilinos>(BInquilinos);
        private void BContratos_Click(object sender, EventArgs e) => CargarVista<UcContratos>(BContratos);
        private void BPagos_Click(object sender, EventArgs e) => CargarVista<UcPagos>(BPagos);
        private void BReportes_Click(object sender, EventArgs e) => CargarVista<UcReportes>(BReportes);

        private void BSalir_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("¿Desea salir de InmoTech?", "Confirmar salida",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes) Close();
        }


        // ======================================================
        //                   DISEÑO / ESTILO (mínimo)
        //     Responsivo + iconos correctos + botones legibles
        // ======================================================

        // Ancho fijo de sidebar
        private const int SidebarWidth = 320;

        // Paleta mínima necesaria
        private readonly Color BtnBg = Color.White;
        private readonly Color BtnHover = Color.FromArgb(245, 245, 245);
        private readonly Color BtnActive = Color.FromArgb(211, 229, 211);
        private readonly Color BtnText = Color.FromArgb(30, 30, 30);

        private void AplicarDisenio()
        {
            ConfigurarLayoutBase();        // Responsivo
            ConfigurarHeaderMinimo();      // Logo/Título básicos
            ReforzarPaddingContenido();    // Padding suave
            AplicaEstilosSidebar();        // Botones + iconos + hover
        }

        private void ConfigurarLayoutBase()
        {
            // Columnas: 0 (sidebar fijo), 1 (contenido flexible)
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
            PanelLateral.Padding = new Padding(12, 10, 12, 12);
            PanelLateral.AutoScroll = true;

            pnlContent.Dock = DockStyle.Fill;
            pnlContent.AutoScroll = true;
            pnlContent.Margin = Padding.Empty;
        }

        private void ConfigurarHeaderMinimo()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Size = new Size(64, 64);
            pictureBox1.BackgroundImage = Properties.Resources.logoImnoTech;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;

            LTituloLogo.AutoSize = true;
            LTituloLogo.Margin = Padding.Empty;
            LTituloLogo.AutoEllipsis = true;
            LTituloLogo.TextAlign = ContentAlignment.MiddleLeft;
            LTituloLogo.Location = new Point(pictureBox1.Right + 12, pictureBox1.Top + 12);
            try { LTituloLogo.Font = new Font("Montserrat", 16f, FontStyle.Bold); } catch { /* fuente opcional */ }
        }

        private void ReforzarPaddingContenido()
        {
            pnlContent.Padding = new Padding(16, 8, 16, 16);
            pnlContent.BackColor = Color.WhiteSmoke;
        }

        private void AplicaEstilosSidebar()
        {
            // Títulos de botones + iconos (escalados en runtime)
            EstilizarSidebarButton(BDashboard, Properties.Resources.dashboardIcon, "Dashboard");
            EstilizarSidebarButton(BUsuarios, Properties.Resources.usuariosIcon, "Usuarios");
            EstilizarSidebarButton(BInmuebles, Properties.Resources.inmueblesIcon, "Inmuebles");
            EstilizarSidebarButton(BInquilinos, Properties.Resources.inquilinosIcon, "Inquilinos");
            EstilizarSidebarButton(BContratos, Properties.Resources.contratosIcon, "Contratos");
            EstilizarSidebarButton(BPagos, Properties.Resources.pagosIcon, "Pagos");
            EstilizarSidebarButton(BReportes, Properties.Resources.reportesIcon, "Reportes");
            EstilizarSidebarButton(BSalir, Properties.Resources.botonSalir, "Salir");
        }

        private void EstilizarSidebarButton(Button b, Image icono, string texto)
        {
            b.Text = texto;
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.ImageAlign = ContentAlignment.MiddleLeft;
            b.TextImageRelation = TextImageRelation.ImageBeforeText;
            b.Padding = new Padding(14, 0, 10, 0);

            try { b.Font = new Font("Montserrat", 10f, FontStyle.Bold); } catch { }

            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;

            b.BackColor = BtnBg;
            b.ForeColor = BtnText;

            b.Image = icono != null ? ScaleIconToButton(b, icono) : null;

            // Hover simple
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

        private Image ScaleIconToButton(Button b, Image original)
        {
            // Ajusta el icono al alto del botón con un margen
            int maxIconSize = Math.Max(16, b.Height - 20);
            return new Bitmap(original, new Size(maxIconSize, maxIconSize));
        }
    }
}
