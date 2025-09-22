// MainForm.cs — versión completa con Reportes por rol (Admin/Operador/Propietario) + Botón Backup (solo Admin)
using InmoTech;
using InmoTech.Controls;
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

        // Historial para navegar hacia atrás (si querés usarlo luego)
        private readonly Stack<UserControl> _historial = new();

        // ⬇️ Mapa de vistas permitidas por rol (incluye Reportes según rol)
        private readonly Dictionary<RolUsuario, HashSet<Type>> _vistasPermitidasPorRol = new()
        {
            [RolUsuario.Administrador] = new()
            {
                typeof(UcDashboard),
                typeof(UcUsuarios),
                typeof(UcInmuebles),
                typeof(UcInquilinos),
                typeof(UcReportesAdmin)        // ✅ Reportes para Admin
            },
            [RolUsuario.Operador] = new()
            {
                typeof(UcDashboard),
                typeof(UcPagos_Contratos),
                typeof(UcContratos),
                typeof(UcReportesOperador)     // ✅ Reportes para Operador
            },
            [RolUsuario.Propietario] = new()
            {
                typeof(UcDashboard),
                typeof(UcContratos),
                typeof(UcInmuebles),
                typeof(UcReportesPropietario)  // ✅ Reportes para Propietario
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

            // Responsividad básica en Resize
            this.Resize -= EventoRedimensionarFormulario;
            this.Resize += EventoRedimensionarFormulario;

            // Botones visibles por rol (si luego querés ocultar por rol)
            _botonesVisiblesPorRol = new Dictionary<RolUsuario, Button[]>
            {
                [RolUsuario.Administrador] = new[] { BDashboard, BUsuarios, BInmuebles, BInquilinos, BReportes, BBackup },
                [RolUsuario.Operador] = new[] { BDashboard, BPagos, BContratos, BReportes /* Backup NO */ },
                [RolUsuario.Propietario] = new[] { BDashboard, BContratos, BInmuebles, BReportes /* Backup NO */ }
            };

            // Estilo y permisos
            AplicarEstiloMinimo();
            AplicarPermisosPorRol();
            ActualizarInfoUsuario();

            // Vista inicial
            CargarVista<UcDashboard>(BDashboard);

            // Flujo especial de Pagos → Contratos → Cuotas, sin tocar tu handler original
            BPagos.Click -= BPagos_Click_Ex; // evitar doble suscripción por si el diseñador engancha varias veces
            BPagos.Click += BPagos_Click_Ex;

            // Backup (solo vista por ahora)
            BBackup.Click -= BBackup_Click;
            BBackup.Click += BBackup_Click;
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

        // ⬇️ NUEVO: decide el UserControl de Reportes según rol
        private Type? TipoReportePorRol()
        {
            if (!AuthService.IsAuthenticated || RolActual is null) return null;

            return RolActual switch
            {
                RolUsuario.Administrador => typeof(UcReportesAdmin),
                RolUsuario.Operador => typeof(UcReportesOperador),
                RolUsuario.Propietario => typeof(UcReportesPropietario),
                _ => null
            };
        }

        private void AplicarPermisosPorRol()
        {
            Button[] todosLosBotones =
            {
                BDashboard, BUsuarios, BInmuebles, BInquilinos,
                BContratos, BPagos, BReportes, BBackup
            };

            foreach (var boton in todosLosBotones)
                boton.Visible = true;

            HashSet<Type>? vistasPermitidas = null;
            if (AuthService.IsAuthenticated && RolActual is RolUsuario rol &&
                _vistasPermitidasPorRol.TryGetValue(rol, out var set))
            {
                vistasPermitidas = set;
            }

            // Mapa normal (sin Reportes y sin Backup)
            var mapaBotonVista = new Dictionary<Button, Type>
            {
                { BDashboard,  typeof(UcDashboard)  },
                { BUsuarios,   typeof(UcUsuarios)   },
                { BInmuebles,  typeof(UcInmuebles)  },
                { BInquilinos, typeof(UcInquilinos) },
                { BContratos,  typeof(UcContratos)  },
                { BPagos,      typeof(UcPagos_Contratos) }
            };

            foreach (var (boton, vista) in mapaBotonVista)
            {
                var habilitado = vistasPermitidas != null && vistasPermitidas.Contains(vista);
                if (!AuthService.IsAuthenticated) habilitado = (vista == typeof(UcDashboard));
                DefinirEstadoBotonLateral(boton, habilitado);
            }

            // ===== Botón Reportes (dinámico por rol) =====
            var tipoReporte = TipoReportePorRol();
            bool habilitarReportes =
                tipoReporte != null &&
                vistasPermitidas != null &&
                vistasPermitidas.Contains(tipoReporte);

            if (!AuthService.IsAuthenticated) habilitarReportes = false;
            DefinirEstadoBotonLateral(BReportes, habilitarReportes);
            // =============================================

            // ===== Botón Backup (solo Administrador) =====
            bool habilitarBackup = AuthService.IsAuthenticated && RolActual == RolUsuario.Administrador;
            DefinirEstadoBotonLateral(BBackup, habilitarBackup);
            // =============================================

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

        // ⬇️ NUEVO: overload por Type (para Reportes por rol)
        private void CargarVista(Type tipoVista, Button botonOrigen)
        {
            if (!VistaPermitida(tipoVista))
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

            if (!_cacheVistas.TryGetValue(tipoVista, out var vista))
            {
                vista = (UserControl)Activator.CreateInstance(tipoVista)!;
                vista.Dock = DockStyle.Fill;
                vista.AutoScroll = true;
                _cacheVistas[tipoVista] = vista;
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

        // Muestra un control guardando el actual en el historial (por si luego querés Back)
        private void ShowInContent(Control uc)
        {
            uc.Dock = DockStyle.Fill;

            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is UserControl actual)
                _historial.Push(actual);

            pnlContent.SuspendLayout();
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(uc);
            pnlContent.ResumeLayout();
        }

        #endregion

        // ======================================================
        //  REGIÓN: Información de Usuario (UI)
        // ======================================================
        #region InfoUsuario

        private void ActualizarInfoUsuario()
        {
            var u = InmoTech.Security.AuthService.CurrentUser;
            var nombreCompleto = u is null
                ? "Invitado"
                : $"{(u?.Nombre ?? "").Trim()} {(u?.Apellido ?? "").Trim()}".Trim();

            LUsuarioNombre.Text = string.IsNullOrWhiteSpace(nombreCompleto) ? "Invitado" : nombreCompleto;
            LUsuarioEmail.Text = u?.Email ?? "—";
            LUsuarioRol.Text = ObtenerNombreRol(u?.IdRol);
        }

        private string ObtenerNombreRol(int? idRol)
        {
            return idRol switch
            {
                1 => "Administrador",
                2 => "Operador",
                3 => "Propietario",
                _ => "Sin rol"
            };
        }

        #endregion

        // ======================================================
        //  REGIÓN: Handlers de Click (tuyos)
        // ======================================================
        #region Handlers

        private void BDashboard_Click(object sender, EventArgs e) => CargarVista<UcDashboard>(BDashboard);
        private void BUsuarios_Click(object sender, EventArgs e) => CargarVista<UcUsuarios>(BUsuarios);
        private void BInmuebles_Click(object sender, EventArgs e) => CargarVista<UcInmuebles>(BInmuebles);
        private void BInquilinos_Click(object sender, EventArgs e) => CargarVista<UcInquilinos>(BInquilinos);
        private void BContratos_Click(object sender, EventArgs e) => CargarVista<UcContratos>(BContratos);
        private void BPagos_Click(object sender, EventArgs e) => CargarVista<UcPagos_Contratos>(BPagos);

        // ⬇️ MODIFICADO: Reportes abre la vista según rol
        private void BReportes_Click(object sender, EventArgs e)
        {
            var tipo = TipoReportePorRol();
            if (tipo == null)
            {
                MessageBox.Show("No tenés permisos para Reportes.", "Acceso denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CargarVista(tipo, BReportes);
        }

        // Reemplazá solo este método en MainForm.cs

        private void BBackup_Click(object? sender, EventArgs e)
        {
            // Restricción de rol (solo Administrador)
            if (!(Security.AuthService.IsAuthenticated && RolActual == (RolUsuario)1)) // 1 = Administrador
            {
                MessageBox.Show("Solo el Administrador puede acceder al Backup.", "Acceso denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Muestra la vista de Backup en el panel de contenido
            if (sender is Button btn) MarcarBotonLateralActivo(btn);

            var uc = new Controls.UcBackup();

            // (Enganches opcionales para cuando agregues backend)
            uc.ProbarRutaClicked += (_, ruta) =>
            {
                // TODO: acá podrías hacer chequeos extra (permisos, espacio libre, etc.)
                // Por ahora no hace nada adicional.
            };

            uc.SimularClicked += (_, cfg) =>
            {
                // TODO: acá podrías construir el T-SQL o script real usando cfg
                // Por ahora no hace nada adicional.
            };

            ShowInContent(uc);
        }


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
            pictureBox1.BackgroundImage = Properties.Resources.logoImnoTech; // usa tu recurso
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

            // Para "Backup" reutilizo el ícono de reportes (podés cambiarlo cuando tengas uno específico)
            EstilizarBotonSidebar(BBackup, Properties.Resources.reportesIcon, "Backup");

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

        // ======================================================
        //  REGIÓN: Flujo Pagos → Contratos → Cuotas → Recibo
        // ======================================================
        #region FlujoPagos

        /// <summary>
        /// Handler adicional para Pagos que monta el flujo Contratos→Cuotas→Recibo.
        /// Se suscribe en el constructor sin tocar tu BPagos_Click original.
        /// </summary>
        private void BPagos_Click_Ex(object? sender, EventArgs e)
        {
            if (sender is Button btn) MarcarBotonLateralActivo(btn);

            UcPagos_Contratos uc;
            if (_cacheVistas.TryGetValue(typeof(UcPagos_Contratos), out var vistaExistente))
            {
                uc = (UcPagos_Contratos)vistaExistente;
            }
            else
            {
                uc = new UcPagos_Contratos { Dock = DockStyle.Fill, AutoScroll = true };
                _cacheVistas[typeof(UcPagos_Contratos)] = uc;
            }

            // 👉 Siempre asegurar suscripción
            uc.VerCuotasClicked -= OnVerCuotas;
            uc.VerCuotasClicked += OnVerCuotas;

            ShowInContent(uc);
        }


        /// <summary>
        /// Navega a UcPagos_Cuotas y arma cabecera/colección. Luego permite ver recibo.
        /// </summary>
        private void OnVerCuotas(object? sender, string nroContrato)
        {
            var cuotas = new UcPagos_Cuotas();

            // Intentamos recuperar inquilino/inmueble desde el origen (si está disponible)
            string inquilino = "—";
            string inmueble = "—";
            if (sender is UcPagos_Contratos origen)
            {
                var header = origen.GetMinHeaderFor(nroContrato);
                if (header.HasValue)
                {
                    inquilino = header.Value.Inquilino;
                    inmueble = header.Value.Inmueble;
                }
            }

            // Cabecera (demo hasta conectar BD real)
            cuotas.LoadContrato(new UcPagos_Cuotas.CabeceraContratoVm
            {
                NumeroContrato = nroContrato,
                Inquilino = inquilino,
                Inmueble = inmueble,
                Inicio = new DateTime(2024, 05, 01),
                Fin = new DateTime(2025, 04, 30),
                Total = 600000,
                Atraso = 58000,
                CantidadCuotas = 12
            });

            // Cuotas demo
            cuotas.SetCuotas(new (int, int, string, decimal, DateTime, string)[]
            {
                (1,12,"Mayo 2024",       50000, new DateTime(2024,05,10), "Pagada"),
                (2,12,"Junio 2024",      50000, new DateTime(2024,06,10), "Pagada"),
                (3,12,"Julio 2024",      50000, new DateTime(2024,07,10), "Vencida"),
                (4,12,"Agosto 2024",     50000, new DateTime(2024,08,10), "Pendiente"),
                (5,12,"Septiembre 2024", 50000, new DateTime(2024,09,10), "Pendiente"),
            });

            cuotas.PagarClicked += (_, info) =>
            {
                // info: (string contrato, int nroCuota, string periodo, decimal monto, DateTime vencimiento)
                var pagar = new UcPagos_PagarCuota();

                pagar.LoadHeader(new UcPagos_PagarCuota.HeaderCuota
                {
                    Contrato = info.contrato,
                    Inquilino = inquilino,   // viene del header que armamos arriba en OnVerCuotas
                    Inmueble = inmueble,    // idem
                    NroCuota = info.nroCuota,
                    CantCuotas = 12,          // si ya lo tenés, poné el real
                    Periodo = info.periodo,
                    Monto = info.monto
                });

                // Guardar
                pagar.GuardarPagoClicked += (_, pago) =>
                {
                    // TODO: guardar en BD.

                    // Si quiere recibo automático o previa, mostramos Recibo
                    if (pago.EmitirRecibo)
                    {
                        var recibo = new UcPagos_Recibo();
                        recibo.LoadRecibo(new UcPagos_Recibo.ReciboVm
                        {
                            NroRecibo = "R-2025-000124",
                            Contrato = pago.Contrato,
                            NroCuota = pago.NroCuota,
                            Periodo = pago.Periodo,
                            FechaPago = pago.FechaPago,
                            MedioPago = pago.MedioPago,
                            Importe = pago.Monto,
                            Inquilino = inquilino,
                            Inmueble = inmueble
                        });
                        ShowInContent(recibo);
                    }
                    else
                    {
                        // Volvemos a la vista de cuotas para que se vea actualizado
                        ShowInContent(cuotas);
                        // Opcional: refrescar grilla de cuotas acá
                    }
                };

                // Vista previa del recibo (sin guardar)
                pagar.VistaPreviaReciboClicked += (_, pago) =>
                {
                    var recibo = new UcPagos_Recibo();
                    recibo.LoadRecibo(new UcPagos_Recibo.ReciboVm
                    {
                        NroRecibo = "R-2025-000123",
                        Contrato = pago.Contrato,
                        NroCuota = pago.NroCuota,
                        Periodo = pago.Periodo,
                        FechaPago = pago.FechaPago,
                        MedioPago = pago.MedioPago,
                        Importe = 50000m,
                        Inquilino = inquilino,
                        Inmueble = inmueble
                    });
                    ShowInContent(recibo);
                };

                // Cancelar: volvemos a cuotas
                pagar.CancelarClicked += (_, __) => ShowInContent(cuotas);

                ShowInContent(pagar);
            };


            cuotas.VerReciboClicked += (_, info) =>
            {
                var recibo = new UcPagos_Recibo();
                recibo.LoadRecibo(new UcPagos_Recibo.ReciboVm
                {
                    NroRecibo = "R-2025-000123",
                    Contrato = info.contrato,
                    NroCuota = info.nroCuota,
                    Periodo = "Junio 2024",
                    FechaPago = new DateTime(2024, 06, 10),
                    MedioPago = "Transferencia",
                    Importe = 50000m,
                    Inquilino = "—",
                    Inmueble = "—"
                });
                ShowInContent(recibo);
            };

            ShowInContent(cuotas);
        }

        #endregion
    }
}
