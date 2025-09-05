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

        private void Seed()
        {
            // Encabezado
            lblTitulo.Text = "Dashboard";
            lblUserName.Text = "Iván Romero Maurin";
            lblRol.Text = "Rol: administrador";

            // KPIs (ejemplo)
            lblKpiProp.Text = "12";
            lblKpiInq.Text = "11";
            lblKpiIngreso.Text = "$563.432,32";
            lblKpiPend.Text = "4";

            // Propiedades (cards simples)
            AddPropertyCard("Apartamento centro", "Calle Alvear 1917");
            AddPropertyCard("Casa Jardín", "Rivadavia 567");
            AddPropertyCard("Casa Moderna", "Junín 1645");

            // Contratos por vencer
            dgvContratos.Rows.Add("C-1024", "Juan Pérez", "Calle 123 – Dpto 4B", "Activo");
            dgvContratos.Rows.Add("C-1025", "Ana Gómez", "Calle 123 – Dpto 4B", "Activo");
            dgvContratos.Rows.Add("C-1027", "Inquilino 3", "Avenida 456", "Inactivo");
            dgvContratos.Rows.Add("C-1028", "María López", "Calle 123", "Activo");
            dgvContratos.Rows.Add("C-1029", "Inquilino 6", "Avenida 456", "Inactivo");
        }

        private void AddPropertyCard(string titulo, string direccion)
        {
            var card = new Panel
            {
                Width = 260,
                Height = 110,
                BackColor = Color.White,
                Margin = new Padding(8),
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            var l1 = new Label
            {
                Text = titulo,
                AutoSize = true,
                Font = new Font("Verdana", 10f, FontStyle.Bold),
                Dock = DockStyle.Top
            };

            var l2 = new Label
            {
                Text = direccion,
                AutoSize = true,
                Dock = DockStyle.Top
            };

            var btn = new Button
            {
                Text = "Ver detalles",
                Dock = DockStyle.Bottom,
                Height = 28
            };

            card.Controls.Add(btn);
            card.Controls.Add(l2);
            card.Controls.Add(l1);
            flPropiedades.Controls.Add(card);
        }
    }
}
