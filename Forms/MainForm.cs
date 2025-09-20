using InmoTech;
using InmoTech.Models;
using InmoTech.Security;   // AuthService
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class MainForm : Form
    {
        // ======================================================
        //  REGIÓN: Tipos / Constantes / Paleta
        // ======================================================
        #region Tipos y Constantes

        private enum RolUsuario
        {
            Administrador = 1,
            Operador = 2,
            Propietario = 3
        }

        private const int AnchoBarraLateral = 320;

        private readonly Color ColorBotonFondo = Color.White;
        private readonly Color ColorBotonHover = Color.FromArgb(245, 245, 245);
        private readonly Color ColorBotonActivo = Color.FromArgb(211, 229, 211);
        private readonly Color ColorBotonTexto = Color.FromArgb(30, 30, 30);

        #endregion

        // ======================================================
        //  REGIÓN: Estado interno / Cache / Mapas de permisos
        // ======================================================
        #region Estado y Permisos

        private Button? _botonLateralActivo;
        private readonly Dictionary<Type, UserControl> _cacheVistas = new();

        private readonly Dictionary<RolUsuario, HashSet<Type>> _vistasPermitidasPorRol = new()
        {
            [RolUsuario.Administrador] = new()
            {
                typeof(UcDashboard),
                typeof(UcUsuarios),
                typeof(UcInmuebles),
                typeof(UcInquilinos),
                typeof(UcReportes)
            },
            [RolUsuario.Operador] = new()
            {
                typeof(UcDashboard),
                typeof(UcPagos),
                typeof(UcContratos),
                typeof(UcReportes)
            },
            [RolUsuario.Propietario] = new()
            {
                typeof(UcDashboard),
                typeof(UcContratos),
                typeof(UcInmuebles),
                typeof(UcReportes)
            }
        };

        private Dictionary<RolUsuario, Button[]> _botonesVisiblesPorRol = null!;

        #endregion

        // ======================================================
        //  REGIÓN: Constructor
        // ======================================================
        #region Constructor

        public MainForm()
        {
            InitializeComponent();

            this.Resize -= EventoRedimensionarFormulario;
            this.Resize += EventoRedimensionarFormulario;

            _botonesVisiblesPorRol = new Dictionary<RolUsuario, Button[]>
            {
                [RolUsuario.Administrador] = new[] { BDashboard, BUsuarios, BInmuebles, BInquilinos, BReportes },
                [RolUsuario.Operador] = new[] { BDashboard, BPagos, BContratos, BReportes },
                [RolUsuario.Propietario] = new[] { BDashboard, BContratos, BInmuebles, BReportes }
            };

            AplicarEstiloMinimo();
            AplicarPermisosPorRol();
            CargarVista<UcDashboard>(BDashboard);
        }

        #endregion

        // ======================================================
        //  REGIÓN: Autenticación / Autorización
        // ======================================================
        #region Autenticación y Autorización

        private RolUsuario? RolActual
        {
            get
            {
                var idRol = AuthService.CurrentUser?.IdRol;
                if (idRol is null) return null;
                return Enum.IsDefined(typeof(RolUsuario), idRol.Value) ? (RolUsuario)idRol.Value : null;
            }
        }

        private bool VistaPermitida(Type tipoVista)
        {
            if (!AuthService.IsAuthenticated)
                return tipoVista == typeof(UcDashboard);

            if (RolActual is RolUsuario rol && _vistasPermitidasPorRol.TryGetValue(rol, out var permitidas))
                return permitidas.Contains(tipoVista);

            return false;
        }

        private void AplicarPermisosPorRol()
        {
            Button[] todosLosBotones =
            {
                BDashboard, BUsuarios, BInmuebles, BInquilinos,
                BContratos, BPagos, BReportes
            };

            foreach (var boton in todosLosBotones)
                boton.Visible = true;

            HashSet<Type>? vistasPermitidas = null;
            if (AuthService.IsAuthenticated && RolActual is RolUsuario rol &&
                _vistasPermitidasPorRol.TryGetValue(rol, out var set))
            {
                vistasPermitidas = set;
            }

            var mapaBotonVista = new Dictionary<Button, Type>
            {
                { BDashboard,  typeof(UcDashboard)  },
                { BUsuarios,   typeof(UcUsuarios)   },
                { BInmuebles,  typeof(UcInmuebles)  },
                { BInquilinos, typeof(UcInquilinos) },
                { BContratos,  typeof(UcContratos)  },
                { BPagos,      typeof(UcPagos)      },
                { BReportes,   typeof(UcReportes)   }
            };

            foreach (var (boton, vista) in mapaBotonVista)
            {
                var habilitado = vistasPermitidas != null && vistasPermitidas.Contains(vista);
                if (!AuthService.IsAuthenticated) habilitado = (vista == typeof(UcDashboard));
                DefinirEstadoBotonLateral(boton, habilitado);
            }

            BSalir.Visible = true;
            DefinirEstadoBotonLateral(BSalir, true);

            if (_botonLateralActivo is null || !_botonLateralActivo.Enabled)
                _botonLateralActivo = null;
        }

        #endregion

        // ======================================================
        //  REGIÓN: Navegación
        // ======================================================
        #region Navegación

        private void CargarVista<TVista>(Button botonOrigen) where TVista : UserControl, new()
        {
            if (!VistaPermitida(typeof(TVista)))
            {
                MessageBox.Show(
                    "No tenés permisos para acceder a esta sección.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            MarcarBotonLateralActivo(botonOrigen);

            if (!_cacheVistas.TryGetValue(typeof(TVista), out var vista))
            {
                vista = new TVista { Dock = DockStyle.Fill, AutoScroll = true };
                _cacheVistas[typeof(TVista)] = vista;
            }

            pnlContent.SuspendLayout();
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(vista);
            vista.BringToFront();
            pnlContent.ResumeLayout();
        }

        private void MarcarBotonLateralActivo(Button boton)
        {
            if (!boton.Enabled) return;

            if (_botonLateralActivo != null && _botonLateralActivo != boton)
                _botonLateralActivo.BackColor = ColorBotonFondo;

            _botonLateralActivo = boton;
            _botonLateralActivo.BackColor = ColorBotonActivo;
        }

        private void EventoRedimensionarFormulario(object? sender, EventArgs e)
        {
            tableLayoutPanel1?.PerformLayout();
            pnlContent?.PerformLayout();
        }

        #endregion

        // ======================================================
        //  REGIÓN: Handlers de Click
        // ======================================================
        #region Handlers

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

        #endregion

        // ======================================================
        //  REGIÓN: Estilo / Sidebar / Iconos
        // ======================================================
        #region Estilo

        private sealed class IconosBotonLateral
        {
            public Image? Original { get; init; }
            public Image? Deshabilitado { get; init; }
        }

        private void AplicarEstiloMinimo()
        {
            ConfigurarLayoutBase();
            ConfigurarEncabezado();
            AplicarPaddingContenido();
            EstilizarBotonesSidebar();
        }

        private void ConfigurarLayoutBase()
        {
            if (tableLayoutPanel1.ColumnStyles.Count < 2)
            {
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, AnchoBarraLateral));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            }
            else
            {
                tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
                tableLayoutPanel1.ColumnStyles[0].Width = AnchoBarraLateral;
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

            PanelLateral.Dock = DockStyle.Fill;
            PanelLateral.Margin = Padding.Empty;
            PanelLateral.Padding = new Padding(12, 10, 12, 12);
            PanelLateral.AutoScroll = true;

            pnlContent.Dock = DockStyle.Fill;
            pnlContent.AutoScroll = true;
            pnlContent.Margin = Padding.Empty;
        }

        private void ConfigurarEncabezado()
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
            try { LTituloLogo.Font = new Font("Montserrat", 16f, FontStyle.Bold); } catch { }
        }

        private void AplicarPaddingContenido()
        {
            pnlContent.Padding = new Padding(16, 8, 16, 16);
            pnlContent.BackColor = Color.WhiteSmoke;
        }

        private void EstilizarBotonesSidebar()
        {
            EstilizarBotonSidebar(BDashboard, Properties.Resources.dashboardIcon, "Dashboard");
            EstilizarBotonSidebar(BUsuarios, Properties.Resources.usuariosIcon, "Usuarios");
            EstilizarBotonSidebar(BInmuebles, Properties.Resources.inmueblesIcon, "Inmuebles");
            EstilizarBotonSidebar(BInquilinos, Properties.Resources.inquilinosIcon, "Inquilinos");
            EstilizarBotonSidebar(BContratos, Properties.Resources.contratosIcon, "Contratos");
            EstilizarBotonSidebar(BPagos, Properties.Resources.pagosIcon, "Pagos");
            EstilizarBotonSidebar(BReportes, Properties.Resources.reportesIcon, "Reportes");
            EstilizarBotonSidebar(BSalir, Properties.Resources.botonSalir, "Salir");
        }

        private void EstilizarBotonSidebar(Button boton, Image icono, string texto)
        {
            boton.Text = texto;
            boton.TextAlign = ContentAlignment.MiddleLeft;
            boton.ImageAlign = ContentAlignment.MiddleLeft;
            boton.TextImageRelation = TextImageRelation.ImageBeforeText;
            boton.Padding = new Padding(14, 0, 10, 0);

            try { boton.Font = new Font("Montserrat", 10f, FontStyle.Bold); } catch { }

            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;

            boton.BackColor = ColorBotonFondo;
            boton.ForeColor = ColorBotonTexto;

            var iconoEscalado = icono != null ? EscalarIconoBoton(boton, icono) : null;
            var iconoDeshabilitado = iconoEscalado != null ? CrearIconoDeshabilitado(iconoEscalado) : null;
            boton.Tag = new IconosBotonLateral { Original = iconoEscalado, Deshabilitado = iconoDeshabilitado };
            boton.Image = iconoEscalado;

            boton.MouseEnter -= EventoBotonMouseEnter;
            boton.MouseLeave -= EventoBotonMouseLeave;
            boton.MouseEnter += EventoBotonMouseEnter;
            boton.MouseLeave += EventoBotonMouseLeave;
        }

        private void DefinirEstadoBotonLateral(Button boton, bool habilitado)
        {
            boton.Enabled = habilitado;

            if (habilitado)
            {
                boton.ForeColor = ColorBotonTexto;
                boton.BackColor = ColorBotonFondo;
                if (boton.Tag is IconosBotonLateral iconos && iconos.Original != null)
                    boton.Image = iconos.Original;
            }
            else
            {
                boton.ForeColor = Color.FromArgb(150, ColorBotonTexto);
                boton.BackColor = Color.FromArgb(248, 248, 248);
                if (boton.Tag is IconosBotonLateral iconos && iconos.Deshabilitado != null)
                    boton.Image = iconos.Deshabilitado;
            }
        }

        private void EventoBotonMouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button boton && boton.Enabled && boton != _botonLateralActivo)
                boton.BackColor = ColorBotonHover;
        }

        private void EventoBotonMouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button boton && boton.Enabled && boton != _botonLateralActivo)
                boton.BackColor = ColorBotonFondo;
        }

        private Image EscalarIconoBoton(Button boton, Image original)
        {
            int tamanoMax = Math.Max(16, boton.Height - 20);
            return new Bitmap(original, new Size(tamanoMax, tamanoMax));
        }

        private Image CrearIconoDeshabilitado(Image original)
        {
            var bmp = new Bitmap(original.Width, original.Height);
            using var g = Graphics.FromImage(bmp);
            using var ia = new System.Drawing.Imaging.ImageAttributes();

            var cm = new System.Drawing.Imaging.ColorMatrix(new float[][]
            {
                new float[] {0.35f,0.35f,0.35f,0,0},
                new float[] {0.35f,0.35f,0.35f,0,0},
                new float[] {0.35f,0.35f,0.35f,0,0},
                new float[] {0,0,0,0.55f,0},
                new float[] {0,0,0,0,0}
            });
            ia.SetColorMatrix(cm);

            g.DrawImage(original, new Rectangle(0, 0, bmp.Width, bmp.Height),
                        0, 0, original.Width, original.Height, GraphicsUnit.Pixel, ia);
            return bmp;
        }

        #endregion
    }
}
