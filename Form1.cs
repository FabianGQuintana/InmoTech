using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class Form1 : Form
    {
        private Button _botonActivo;
        private readonly Dictionary<Type, UserControl> _cache = new();

        public Form1()
        {
            InitializeComponent();
            // Opcional: tamaño mínimo para que no se rompa el layout
            this.MinimumSize = new System.Drawing.Size(1200, 700);

            // Navegación inicial: Dashboard
            CargarVista<UcDashboard>(BDashboard);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { /* opcional */ }

        private void pictureBox1_Click(object sender, EventArgs e) { /* opcional */ }

        private void label2_Click(object sender, EventArgs e) { /* opcional */ }

        private void MarcarActivo(Button b)
        {
            if (_botonActivo != null && _botonActivo != b)
                _botonActivo.BackColor = System.Drawing.Color.Transparent;

            _botonActivo = b;
            _botonActivo.BackColor = System.Drawing.Color.FromArgb(211, 229, 211); // “activo”
        }

        private void CargarVista<T>(Button origen = null) where T : UserControl, new()
        {
            if (origen != null) MarcarActivo(origen);

            if (!_cache.TryGetValue(typeof(T), out var vista))
            {
                vista = new T { Dock = DockStyle.Fill };
                _cache[typeof(T)] = vista;
            }

            pnlContent.SuspendLayout();
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(vista);
            vista.BringToFront();
            pnlContent.ResumeLayout();
        }

        // === Botones del menú lateral ===
        private void BDashboard_Click(object sender, EventArgs e) => CargarVista<UcDashboard>(BDashboard);
        private void BUsuarios_Click(object sender, EventArgs e) => CargarVista<UcUsuarios>(BUsuarios);
        private void BInmuebles_Click(object sender, EventArgs e) => CargarVista<UcInmuebles>(BInmuebles);
        private void BInquilinos_Click(object sender, EventArgs e) => CargarVista<UcInquilinos>(BInquilinos);
        private void BPagos_Click(object sender, EventArgs e) => CargarVista<UcPagos>(BPagos);
        private void BReportes_Click(object sender, EventArgs e) => CargarVista<UcReportes>(BReportes);

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;   // o FixedSingle
            this.ControlBox = true;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Text = "InmoTech";
            this.WindowState = FormWindowState.Normal;        // opcional
            this.Region = null;                               // si antes definiste bordes redondeados

        }
    }
}
