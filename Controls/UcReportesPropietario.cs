using DotNetEnv;
using InmoTech.Models;
using InmoTech.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DotNetEnv;
using System.Linq; // Para el .Any()

namespace InmoTech.Controls
{
    /// <summary>
    /// Panel principal de reportes para propietarios.
    /// Muestra KPIs (ingresos, pagos, contratos), dos grillas (inmuebles disponibles/A estrenar y contratos por vencer),
    /// filtros por rango de fechas, accesos a subreportes y un asistente IA para análisis.
    /// </summary>
    public partial class UcReportesPropietario : UserControl
    {
        private readonly ReportePropietarioRepository _repo = new();

        private DateTimePicker dtpDesde, dtpHasta;
        private Button btnRefrescar, btnExportVencidas, btnExportVencer;
        private Button btnPagos;
        private Button btnChatAI;
        private Label lblKpiIngresos, lblKpiPagos, lblKpiContratos;
        private DataGridView grdVencidas, grdPorVencer;

        // Accesos a subreportes
        private Button btnOperadores, btnInmuebles, btnMetodosPago, btnMorosidad, btnTopInquilinos;

        /// <summary>
        /// Constructor. Configura DPI/estilo, crea la UI, engancha eventos,
        /// setea valores por defecto de fechas y refresca los datos iniciales.
        /// </summary>
        public UcReportesPropietario()
        {
            UiTheme.EnableHighDpi(this);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;

            BuildUi();
            HookEvents();
            SetDefaults();
            Refrescar();
        }

        /// <summary>
        /// Construye toda la interfaz de usuario (filtros, botones de acceso, KPIs y grillas).
        /// No carga datos; solo crea y ubica controles.
        /// </summary>
        private void BuildUi()
        {
            SuspendLayout();

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = UiTheme.Paper,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 4
            };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // filtros + accesos
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // KPIs
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 50));  // grid 1
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 50));  // grid 2
            Controls.Add(root);

            // ===== Filtros =====
            var filtros = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.White,
                Padding = new Padding(16),
                ColumnCount = 6,
                Margin = new Padding(0, 0, 0, 8)
            };
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            var lblTitulo = new Label
            {
                Text = "Período del Reporte",
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblDesde = new Label { Text = "Desde:", AutoSize = true, Anchor = AnchorStyles.Left };
            var lblHasta = new Label { Text = "Hasta:", AutoSize = true, Anchor = AnchorStyles.Left };

            dtpDesde = new DateTimePicker { Format = DateTimePickerFormat.Short, Anchor = AnchorStyles.Left | AnchorStyles.Right };
            dtpHasta = new DateTimePicker { Format = DateTimePickerFormat.Short, Anchor = AnchorStyles.Left | AnchorStyles.Right };
            btnRefrescar = UiTheme.PrimaryButton("Refrescar");

            filtros.RowCount = 2;
            filtros.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            filtros.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            filtros.Controls.Add(lblTitulo, 0, 0);
            filtros.SetColumnSpan(lblTitulo, 6);

            filtros.Controls.Add(lblDesde, 0, 1);
            filtros.Controls.Add(dtpDesde, 1, 1);
            filtros.Controls.Add(lblHasta, 2, 1);
            filtros.Controls.Add(dtpHasta, 3, 1);
            filtros.Controls.Add(new Panel() { Dock = DockStyle.Fill }, 4, 1);
            filtros.Controls.Add(btnRefrescar, 5, 1);

            // Botonera
            var accesos = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 8, 0, 0),
                BackColor = Color.White
            };
            btnOperadores = UiTheme.PrimaryButton("Operador");
            btnInmuebles = UiTheme.PrimaryButton("Inmuebles");
            btnMetodosPago = UiTheme.PrimaryButton("Métodos de");
            btnMorosidad = UiTheme.PrimaryButton("Morosidad");
            btnTopInquilinos = UiTheme.PrimaryButton("Top");
            btnPagos = UiTheme.PrimaryButton("Pagos");
            btnChatAI = UiTheme.PrimaryButton("Asistente IA");
            btnChatAI.BackColor = UiTheme.Success;
            accesos.Controls.AddRange(new Control[] {
                btnOperadores, btnInmuebles, btnMetodosPago, btnMorosidad, btnTopInquilinos, btnPagos,btnChatAI   // 👈 incluir
            });

            var filtrosContainer = new Panel { Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            filtrosContainer.Controls.Add(accesos);
            filtrosContainer.Controls.Add(filtros);
            root.Controls.Add(filtrosContainer, 0, 0);

            // ===== KPIs =====
            var kpis = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(4),
                Margin = new Padding(0, 0, 0, 8),
                BackColor = UiTheme.Paper
            };
            var card1 = UiTheme.KpiCard("Ingresos Generados", out lblKpiIngresos, UiTheme.Primary);
            var card2 = UiTheme.KpiCard("Pagos Registrados", out lblKpiPagos, UiTheme.Success);
            var card3 = UiTheme.KpiCard("Contratos Creados", out lblKpiContratos, UiTheme.Warning);
            kpis.Controls.Add(card1);
            kpis.Controls.Add(card2);
            kpis.Controls.Add(card3);
            root.Controls.Add(kpis, 0, 1);

            // ===== Grid 1 =====
            var secVencidas = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, Padding = new Padding(4) };
            secVencidas.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            secVencidas.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

            var boxVencidas = new GroupBox
            {
                Text = "Inmuebles disponibles / a estrenar",
                Dock = DockStyle.Fill,
                Padding = new Padding(8),
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };

            grdVencidas = new DataGridView();
            UiTheme.StyleGrid(grdVencidas, UiTheme.Danger);
            boxVencidas.Controls.Add(grdVencidas);

            var barVencidas = new Panel { Dock = DockStyle.Fill, Height = 44 };
            btnExportVencidas = UiTheme.PrimaryButton("Exportar a Excel");
            btnExportVencidas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            barVencidas.Controls.Add(btnExportVencidas);
            barVencidas.Resize += (s, e) =>
                btnExportVencidas.Location = new Point(barVencidas.Width - btnExportVencidas.Width - 8, 4);

            secVencidas.Controls.Add(boxVencidas, 0, 0);
            secVencidas.Controls.Add(barVencidas, 0, 1);
            root.Controls.Add(secVencidas, 0, 2);

            // ===== Grid 2 =====
            var secVencer = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, Padding = new Padding(4) };
            secVencer.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            secVencer.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

            var boxVencer = new GroupBox
            {
                Text = "Contratos por vencer (≤ 60 días)",
                Dock = DockStyle.Fill,
                Padding = new Padding(8),
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };

            grdPorVencer = new DataGridView();
            UiTheme.StyleGrid(grdPorVencer, UiTheme.Primary);
            boxVencer.Controls.Add(grdPorVencer);

            var barVencer = new Panel { Dock = DockStyle.Fill, Height = 44 };
            btnExportVencer = UiTheme.PrimaryButton("Exportar a Excel");
            btnExportVencer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            barVencer.Controls.Add(btnExportVencer);
            barVencer.Resize += (s, e) =>
                btnExportVencer.Location = new Point(barVencer.Width - btnExportVencer.Width - 8, 4);

            secVencer.Controls.Add(boxVencer, 0, 0);
            secVencer.Controls.Add(barVencer, 0, 1);
            root.Controls.Add(secVencer, 0, 3);

            ResumeLayout();
        }

        /// <summary>
        /// Conecta eventos de botones, exportaciones, manejo de errores de grilla,
        /// navegación a subreportes y apertura del asistente IA.
        /// </summary>
        private void HookEvents()
        {
            btnRefrescar.Click += (s, e) => Refrescar();

            btnExportVencidas.Click += (s, e) => GridExporter.ExportToCsv(grdVencidas, "inmuebles_disponibles_estrenar.csv");
            btnExportVencer.Click += (s, e) => GridExporter.ExportToCsv(grdPorVencer, "contratos_por_vencer.csv");

            // Evita pop-ups de formato si llegara un dato inesperado
            grdVencidas.DataError += (s, e) => e.ThrowException = false;

            btnOperadores.Click += (s, e) => AbrirVentana(new UcReporteOperadores(dtpDesde.Value, dtpHasta.Value));
            btnInmuebles.Click += (s, e) => AbrirVentana(new UcReporteInmuebles(dtpDesde.Value, dtpHasta.Value));
            btnMetodosPago.Click += (s, e) => AbrirVentana(new UcReporteMetodosPago(dtpDesde.Value, dtpHasta.Value));
            btnMorosidad.Click += (s, e) => AbrirVentana(new UcReporteMorosidad(dtpDesde.Value, dtpHasta.Value));
            btnTopInquilinos.Click += (s, e) => AbrirVentana(new UcReporteInquilinos(dtpDesde.Value, dtpHasta.Value));
            btnPagos.Click += (s, e) => AbrirVentana(new UcReportePagos(dtpDesde.Value, dtpHasta.Value)); // 👈 nuevo
            btnChatAI.Click += (s, e) => AbrirAsistenteIA();
        }

        /// <summary>
        /// Abre el asistente de IA en una ventana modal, cargando variables de entorno (.env),
        /// obteniendo datos (contratos/pagos) del periodo seleccionado y pasando todo al control IA.
        /// Maneja errores de configuración y acceso a DB.
        /// </summary>
        private async void AbrirAsistenteIA() // 👈 Cambiado a async
        {
            
            // Declaramos las listas aquí para que existan fuera del bloque try
            List<ContratoDTO> contratosParaAI = new List<ContratoDTO>();
            List<PagoDTO> pagosParaAI = new List<PagoDTO>();
            string apiKey = "";
            // -------------------------------------

            try
            {
                Env.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: No se pudo encontrar o cargar el archivo .env.\n{ex.Message}",
                                "Error de Configuración",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            apiKey = Env.GetString("GEMINI_API_KEY");

            if (string.IsNullOrEmpty(apiKey) || apiKey == "AQUI_VA_TU_API_KEY_DE_GEMINI")
            {
                MessageBox.Show("Error: Debes configurar tu 'GEMINI_API_KEY' en el archivo .env",
                                "Configuración Requerida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Instanciamos el repositorio unificado
                var repoAI = new ReporteAiRepository();

                // Puedes usar los filtros de fecha de tu reporte principal
                var desde = dtpDesde.Value.Date;
                var hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

                // Llamamos a los métodos del mismo repositorio
                contratosParaAI = await repoAI.GetContratosAsync(desde, hasta);
                pagosParaAI = await repoAI.GetPagosAsync(desde, hasta);

                if (!contratosParaAI.Any() && !pagosParaAI.Any())
                {
                    MessageBox.Show("Advertencia: No se encontraron datos de contratos o pagos para el período seleccionado. La IA tendrá información limitada.",
                                    "Sin Datos",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de la base de datos para la IA: {ex.Message}",
                                "Error de DB",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // ¡NUEVO! Pasar los datos al constructor del UcAIAssistant
            // Ahora 'apiKey', 'contratosParaAI' y 'pagosParaAI' son visibles aquí
            var chatControl = new UcAIAssistant(apiKey, contratosParaAI, pagosParaAI);

            var f = new Form
            {
                StartPosition = FormStartPosition.CenterParent,
                Width = 600,
                Height = 740,
                Text = "Asistente IA",
                AutoScaleMode = AutoScaleMode.Dpi
            };
            chatControl.Dock = DockStyle.Fill;
            f.Controls.Add(chatControl);
            f.ShowDialog();
        }

        /// <summary>
        /// Establece valores por defecto de fechas para el filtro (primer día del mes actual hasta hoy).
        /// </summary>
        private void SetDefaults()
        {
            var hoy = DateTime.Today;
            dtpHasta.Value = hoy;
            dtpDesde.Value = new DateTime(hoy.Year, hoy.Month, 1);
        }

        /// <summary>
        /// Abre una ventana modal conteniendo el control de subreporte indicado.
        /// </summary>
        /// <param name="content">UserControl del subreporte a hospedar.</param>
        private void AbrirVentana(UserControl content)
        {
            var f = new Form
            {
                StartPosition = FormStartPosition.CenterParent,
                Width = 1200,
                Height = 740,
                Text = content.GetType().Name,
                AutoScaleMode = AutoScaleMode.Dpi
            };
            content.Dock = DockStyle.Fill;
            f.Controls.Add(content);
            f.ShowDialog();
        }

        /// <summary>
        /// Recalcula y muestra KPIs, rellena las grillas de “inmuebles disponibles/A estrenar”
        /// y “contratos por vencer” usando el rango de fechas seleccionado.
        /// </summary>
        private void Refrescar()
        {
            var desde = dtpDesde.Value.Date;
            var hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

            // KPIs
            var ingresosPorMetodo = _repo.ObtenerIngresosPorMetodo(desde, hasta)
                                   ?? new List<MetodoPagoKpi>();
            lblKpiIngresos.Text = ingresosPorMetodo.Sum(x => x.Total)
                                  .ToString("C2", CultureInfo.GetCultureInfo("es-AR"));

            var porOperador = _repo.ObtenerIngresosPorOperador(desde, hasta)
                             ?? new List<OperadorKpi>();
            lblKpiPagos.Text = porOperador.Sum(x => x.CantidadPagos).ToString();

            lblKpiContratos.Text = _repo.ContarContratosCreados(desde, hasta).ToString();

            // Grid 1: inmuebles disponibles / a estrenar
            var inmuebles = _repo.ObtenerInmueblesDisponiblesOAestrenar();
            grdVencidas.DataSource = inmuebles;

            if (grdVencidas.Columns.Count > 0)
            {
                grdVencidas.Columns[nameof(InmuebleCondicion.IdInmueble)].Visible = false;
                grdVencidas.Columns[nameof(InmuebleCondicion.Inmueble)].HeaderText = "Dirección";
                grdVencidas.Columns[nameof(InmuebleCondicion.Tipo)].HeaderText = "Tipo";
                grdVencidas.Columns[nameof(InmuebleCondicion.Ambientes)].HeaderText = "Ambientes";
                grdVencidas.Columns[nameof(InmuebleCondicion.Condicion)].HeaderText = "Condición";
            }

            // Grid 2: contratos por vencer (≤ 60 días)
            var porVencer60 = _repo.ObtenerContratosPorVencer(DateTime.Today, 60);
            grdPorVencer.DataSource = porVencer60 ?? new List<ContratoPorVencer>();

            if (grdPorVencer.Columns.Count > 0)
            {
                grdPorVencer.Columns[nameof(ContratoPorVencer.IdContrato)].HeaderText = "Contrato ID";
                grdPorVencer.Columns[nameof(ContratoPorVencer.Inquilino)].HeaderText = "Inquilino";
                grdPorVencer.Columns[nameof(ContratoPorVencer.IdInmueble)].HeaderText = "Inmueble ID";
                grdPorVencer.Columns[nameof(ContratoPorVencer.Inmueble)].HeaderText = "Inmueble";
                grdPorVencer.Columns[nameof(ContratoPorVencer.FechaFin)].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }
    }
}
