// UcDashboard.cs (cards + botón "Ver" centrado)
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class UcDashboard : UserControl
    {
        public UcDashboard()
        {
            InitializeComponent();
            this.Load += UcDashboard_Load;
        }

        private void UcDashboard_Load(object? sender, EventArgs e)
        {
            if (IsDesigner()) return;
            Seed();
        }

        private static bool IsDesigner()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                   || Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase);
        }

        // ------ MODELO ------
        private record CardInmueble(
            string Titulo,
            string Direccion,
            string Tipo,
            int Ambientes,
            string Estado,
            Image Foto
        );

        // ------ DATA + RENDER ------
        private void Seed()
        {
            // Encabezado
            lblTitulo.Text = "Dashboard";
            lblUserName.Text = "Iván Romero Maurin";
            lblRol.Text = "Rol: administrador";

            // KPIs
            lblKpiProp.Text = "12";
            lblKpiInq.Text = "11";
            lblKpiIngreso.Text = "$563.432,32";
            lblKpiPend.Text = "4";

            // Cards
            flPropiedades.Controls.Clear();
            flPropiedades.FlowDirection = FlowDirection.LeftToRight;
            flPropiedades.WrapContents = true;

            var cards = new[]
            {
                new CardInmueble("Casa Jardín", "Rivadavia 567", "Casa", 4, "Disponible", Properties.Resources.casa1),
                new CardInmueble("Apartamento Centro", "Alvear 1917 – 4B", "Departamento", 2, "Disponible", Properties.Resources.casa2),
                new CardInmueble("Casa Moderna", "Junín 1645", "Casa", 5, "Reservado", Properties.Resources.casa3),
                new CardInmueble("Dúplex Norte", "San Martín 802", "Dúplex", 3, "Alquilado", Properties.Resources.casa4)
            };

            foreach (var c in cards)
                flPropiedades.Controls.Add(CreatePropertyCard(c));

            // Contratos (demo)
            dgvContratos.Rows.Clear();
            dgvContratos.Rows.Add("C-1024", "Juan Pérez", "Calle 123 – Dpto 4B", "Activo");
            dgvContratos.Rows.Add("C-1025", "Ana Gómez", "Calle 123 – Dpto 4B", "Activo");
            dgvContratos.Rows.Add("C-1027", "Inquilino 3", "Avenida 456", "Inactivo");
            dgvContratos.Rows.Add("C-1028", "María López", "Calle 123", "Activo");
            dgvContratos.Rows.Add("C-1029", "Inquilino 6", "Avenida 456", "Inactivo");
        }

        // ------ UI CARD ------
        private Control CreatePropertyCard(CardInmueble d)
        {
            var card = new Panel
            {
                Width = 340,
                Height = 150,
                BackColor = Color.White,
                Margin = new Padding(8),
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            var pic = new PictureBox
            {
                Image = d.Foto,
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 150,
                Height = 120,
                Location = new Point(10, 10),
                BackColor = Color.White
            };
            card.Controls.Add(pic);

            var right = new TableLayoutPanel
            {
                Location = new Point(pic.Right + 10, 10),
                Size = new Size(card.Width - pic.Width - 30, 120),
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Color.White
            };
            right.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            right.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
            right.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
            right.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
            right.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            card.Controls.Add(right);

            Label L(string text, float size, FontStyle style = FontStyle.Regular)
            {
                return new Label
                {
                    Text = text,
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Verdana", size, style, GraphicsUnit.Point),
                    AutoEllipsis = true,
                    Padding = new Padding(0),
                    Margin = new Padding(0)
                };
            }

            var lblTituloCard = L(d.Titulo, 10f, FontStyle.Bold);
            var lblDireccion = L(d.Direccion, 9f);
            var lblLinea2 = L($"{d.Tipo} • {d.Ambientes} amb. • {d.Estado}", 9f);
            lblLinea2.ForeColor = Color.DimGray;

            var actions = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            var btn = new Button
            {
                Text = "Ver",
                Width = 76,
                Height = 26,
                TabStop = false,
                UseVisualStyleBackColor = true
            };
            actions.Resize += (_, __) =>
            {
                btn.Left = (actions.Width - btn.Width) / 2;
                btn.Top = actions.Height - btn.Height; // pegado abajo y centrado
            };
            actions.Controls.Add(btn);

            btn.Click += (_, __) => MessageBox.Show(
                $"{d.Titulo}\n{d.Direccion}\n{d.Tipo} • {d.Ambientes} amb. • {d.Estado}",
                "Inmueble", MessageBoxButtons.OK, MessageBoxIcon.Information);

            right.Controls.Add(lblTituloCard, 0, 0);
            right.Controls.Add(lblDireccion, 0, 1);
            right.Controls.Add(lblLinea2, 0, 2);
            right.Controls.Add(actions, 0, 3);

            return card;
        }

        // Handlers plantilla
        private void lblKpiInqCap_Click(object sender, EventArgs e) { }
        private void lblKpiInq_Click(object sender, EventArgs e) { }
        private void lblTitulo_Click(object sender, EventArgs e) { }
    }
}
