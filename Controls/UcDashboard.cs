
using InmoTech.Data.Repositories;
using InmoTech.Repositories;      // Para DashboardRepository
using InmoTech.Models;            // Para DashboardData, InmuebleCard
using InmoTech.Services;          // Para AppNotifier
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class UcDashboard : UserControl
    {
        // ======================================================
        // REGIÓN: Repositorios
        // ======================================================
        #region Repositorios
        private readonly DashboardRepository _dashboardRepository = new();
        private readonly InmuebleRepository _inmuebleRepository = new(); // ¡NUEVO! Para cargar imágenes
        #endregion

        // ======================================================
        // REGIÓN: Constructor y Inicialización
        // ======================================================
        #region Constructor y Inicialización
        public UcDashboard()
        {
            InitializeComponent();
            this.Load += UcDashboard_Load;

            // Suscribirse al evento de notificación (Dinamismo)
            AppNotifier.DashboardDataChanged += AppNotifier_DashboardDataChanged;
        }

        private void UcDashboard_Load(object? sender, EventArgs e)
        {
            if (IsDesigner()) return;
            Seed();
        }

        // Manejador del evento de notificación para actualizar la UI desde cualquier hilo
        private void AppNotifier_DashboardDataChanged()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(Seed));
            }
            else
            {
                Seed(); // Actualiza la interfaz
            }
        }

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
                var data = _dashboardRepository.GetDashboardData();

                // 2. KPIs Dinámicos
                lblKpiProp.Text = data.TotalPropiedades.ToString();
                lblKpiInq.Text = data.TotalInquilinos.ToString();
                lblKpiIngreso.Text = data.IngresoTotalMes.ToString("C", CultureInfo.CurrentCulture);

                // 3. Cards de Propiedades Dinámicas
                flPropiedades.Controls.Clear();
                flPropiedades.FlowDirection = FlowDirection.LeftToRight;
                flPropiedades.WrapContents = true;

                foreach (var c in data.InmueblesDisponibles)
                    flPropiedades.Controls.Add(CreatePropertyCard(c));

                if (data.InmueblesDisponibles.Count == 0)
                {
                    flPropiedades.Controls.Add(new Label { Text = "No hay propiedades disponibles.", ForeColor = Color.White, AutoSize = true, Padding = new Padding(10) });
                }

                // 4. Contratos por Vencer Dinámicos
                dgvContratos.DataSource = data.ContratosVencer;

                if (dgvContratos.Columns.Contains("id_contrato")) dgvContratos.Columns["id_contrato"].HeaderText = "Contrato";
                if (dgvContratos.Columns.Contains("InquilinoNombre")) dgvContratos.Columns["InquilinoNombre"].HeaderText = "Inquilino";
                if (dgvContratos.Columns.Contains("InmuebleDireccion")) dgvContratos.Columns["InmuebleDireccion"].HeaderText = "Inmueble";
                if (dgvContratos.Columns.Contains("estado")) dgvContratos.Columns["estado"].HeaderText = "Estado";
                if (dgvContratos.Columns.Contains("fecha_fin")) dgvContratos.Columns["fecha_fin"].HeaderText = "Vencimiento";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del Dashboard: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private Control CreatePropertyCard(InmuebleCard d)
        {
            Image? imagenInmueble = null;

            // 1. Intentar cargar la imagen dinámica y manejar excepciones de carga
            try
            {
                imagenInmueble = _inmuebleRepository.ObtenerImagenPortada(d.IdInmueble);
            }
            catch (Exception ex)
            {
                // Opcional: Registrar el error de carga de imagen para el ID específico
                System.Diagnostics.Debug.WriteLine($"Error al cargar imagen del Inmueble {d.IdInmueble}: {ex.Message}");
            }

            // 2. Fallback: Si la carga es nula o falló, usar un recurso estático (fallback)
            if (imagenInmueble == null)
            {
                try
                {
                    // Intentar usar el recurso estático. Asegúrate que este recurso existe.
                    imagenInmueble = Properties.Resources.casa1;
                }
                catch
                {
                    // Fallback extremo: Si ni siquiera el recurso estático funciona, crear un Bitmap vacío.
                    // Esto garantiza que el PictureBox no lance una excepción por recibir null.
                    imagenInmueble = new Bitmap(150, 120);
                }
            }

            var card = new Panel
            {
                Width = 340,
                Height = 150,
                BackColor = Color.White, // Asegúrate de que el fondo sea blanco o un color neutro
                Margin = new Padding(8),
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            var pic = new PictureBox
            {
                Image = imagenInmueble, // ¡IMAGEN GARANTIZADA NO ES NULL!
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 150,
                Height = 120,
                Location = new Point(10, 10),
                BackColor = Color.White
            };
            card.Controls.Add(pic);

            // Inicialización de TableLayoutPanel (right)
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
                btn.Top = actions.Height - btn.Height; // pegado abajo y centrado
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
        private void lblKpiInqCap_Click(object sender, EventArgs e) { }
        private void lblKpiInq_Click(object sender, EventArgs e) { }
        private void lblTitulo_Click(object sender, EventArgs e) { }
        #endregion
    }
}