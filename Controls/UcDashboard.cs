
using InmoTech.Repositories; 
using InmoTech.Models;      
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization; // Para formatear el dinero

namespace InmoTech
{
    public partial class UcDashboard : UserControl
    {
        // ======================================================
        // REGIÓN: Repositorios (NUEVO)
        // ======================================================
        #region Repositorios
        private readonly DashboardRepository _dashboardRepository = new();
        #endregion

        // ======================================================
        // REGIÓN: Constructor y Inicialización
        // ======================================================
        #region Constructor y Inicialización
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

        // ... (IsDesigner se mantiene igual) ...
        private static bool IsDesigner()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                           || Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase);
        }
        #endregion


        // ======================================================
        // REGIÓN: Carga de Datos y Renderizado (Seed) - MODIFICADO
        // ======================================================
        #region Carga de Datos y Renderizado (Seed)
        // ------ DATA + RENDER ------
        private void Seed()
        {
            try
            {
                // 1. Obtener todos los datos del repositorio
                var data = _dashboardRepository.GetDashboardData();

                // 2. KPIs Dinámicos
                lblKpiProp.Text = data.TotalPropiedades.ToString();
                lblKpiInq.Text = data.TotalInquilinos.ToString();

                // Formateo del ingreso usando la cultura actual (ej: "$563.432,32")
                lblKpiIngreso.Text = data.IngresoTotalMes.ToString("C", CultureInfo.CurrentCulture);
                lblKpiPend.Text = data.PagosPendientes.ToString();

                // 3. Cards de Propiedades Dinámicas
                flPropiedades.Controls.Clear();
                flPropiedades.FlowDirection = FlowDirection.LeftToRight;
                flPropiedades.WrapContents = true;

                // Usamos la lista dinámica del repositorio
                foreach (var c in data.InmueblesDisponibles)
                    flPropiedades.Controls.Add(CreatePropertyCard(c));

                if (data.InmueblesDisponibles.Count == 0)
                {
                    flPropiedades.Controls.Add(new Label { Text = "No hay propiedades disponibles.", ForeColor = Color.White, AutoSize = true, Padding = new Padding(10) });
                }

                // 4. Contratos por Vencer Dinámicos
                dgvContratos.DataSource = data.ContratosVencer;

                // Asegurar que las columnas tengan encabezados amigables después de DataBinding
                if (dgvContratos.Columns.Contains("id_contrato")) dgvContratos.Columns["id_contrato"].HeaderText = "Contrato";
                if (dgvContratos.Columns.Contains("InquilinoNombre")) dgvContratos.Columns["InquilinoNombre"].HeaderText = "Inquilino";
                if (dgvContratos.Columns.Contains("InmuebleDireccion")) dgvContratos.Columns["InmuebleDireccion"].HeaderText = "Inmueble";
                if (dgvContratos.Columns.Contains("estado")) dgvContratos.Columns["estado"].HeaderText = "Estado";
                if (dgvContratos.Columns.Contains("fecha_fin")) dgvContratos.Columns["fecha_fin"].HeaderText = "Vencimiento";

            }
            catch (Exception ex)
            {
                // Manejo de errores de carga (ej. conexión a la BD)
                MessageBox.Show($"Error al cargar datos del Dashboard: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Opcional: Establecer valores predeterminados o vaciar KPIs en caso de fallo
                lblKpiProp.Text = "N/A";
                lblKpiIngreso.Text = "$0,00";
            }
        }
        #endregion

        // ======================================================
        // REGIÓN: Renderizado de Tarjeta (Card) - MODIFICADO
        // ======================================================
        #region Renderizado de Tarjeta (Card)
        // ------ UI CARD ------
        // El argumento ahora usa el modelo InmuebleCard
        private Control CreatePropertyCard(InmuebleCard d)
        {
            // PENDIENTE: Aquí debes añadir tu lógica para cargar la imagen real 
            // de la tabla dbo.inmueble_imagen usando d.IdInmueble.
            // Mientras tanto, se usa un recurso por defecto o Properties.Resources.casa_default
            Image imagenInmueble;
            try
            {
                // **IMPLEMENTAR CARGA DE IMAGEN POR IdInmueble AQUÍ**
                imagenInmueble = Properties.Resources.casa1; // Sustituir por la carga real
            }
            catch { imagenInmueble = Properties.Resources.casa1; } // Fallback

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
                Image = imagenInmueble,
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 150,
                Height = 120,
                Location = new Point(10, 10),
                BackColor = Color.White
            };
            card.Controls.Add(pic);

            // ... (resto del código del TableLayoutPanel right) ...

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
                UseVisualStyleBackColor = true,
                Tag = d.IdInmueble // Asignar el ID al botón para saber qué ver
            };
            actions.Resize += (_, __) =>
            {
                btn.Left = (actions.Width - btn.Width) / 2;
                btn.Top = actions.Height - btn.Height;
            };
            actions.Controls.Add(btn);

            btn.Click += (_, __) => MessageBox.Show(
                $"ID Inmueble: {d.IdInmueble}\n{d.Titulo}\n{d.Direccion}\n{d.Tipo} • {d.Ambientes} amb. • {d.Estado}",
                "Inmueble", MessageBoxButtons.OK, MessageBoxIcon.Information);

            right.Controls.Add(lblTituloCard, 0, 0);
            right.Controls.Add(lblDireccion, 0, 1);
            right.Controls.Add(lblLinea2, 0, 2);
            right.Controls.Add(actions, 0, 3);

            return card;
        }
        #endregion

        // ======================================================
        // REGIÓN: Handlers de Plantilla (Vacíos)
        // ======================================================
        #region Handlers de Plantilla (Vacíos)
        // Handlers plantilla
        private void lblKpiInqCap_Click(object sender, EventArgs e) { }
        private void lblKpiInq_Click(object sender, EventArgs e) { }
        private void lblTitulo_Click(object sender, EventArgs e) { }
        #endregion
    }
}