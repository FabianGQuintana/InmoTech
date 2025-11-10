using InmoTech.Services;
using InmoTech.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    /// <summary>
    /// Asistente IA embebido: interfaz de chat con burbujas y envío de prompts
    /// que consulta <see cref="GeminiService"/> usando contratos y pagos.
    /// </summary>
    public partial class UcAIAssistant : UserControl
    {
        /// <summary>API key para el servicio de IA.</summary>
        private readonly string _apiKey;
        /// <summary>Datos de contratos disponibles para el contexto.</summary>
        private readonly List<ContratoDTO> _contratos;
        /// <summary>Datos de pagos disponibles para el contexto.</summary>
        private readonly List<PagoDTO> _pagos;

        // ---  CAMBIOS DE UI ---
        // Reemplazamos el TextBox por un TableLayoutPanel para las burbujas
        private TableLayoutPanel tlpChatHistory;
        private Panel pnlContainer; // Contenedor para el TLP con scroll
        private TextBox txtPrompt;
        private Button btnSend;
        private TableLayoutPanel root;
        // -------------------------

        /// <summary>
        /// Crea el asistente, aplica tema y construye la UI (datos de contexto recibidos por DI).
        /// </summary>
        public UcAIAssistant(string apiKey, List<ContratoDTO> contratos, List<PagoDTO> pagos)
        {
            _apiKey = apiKey;
            _contratos = contratos;
            _pagos = pagos;

            UiTheme.EnableHighDpi(this);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;

            BuildUi();
            HookEvents();

            //  CORRECCIÓN: Mensaje inicial movido al evento Load
            // AppendToHistory("Asistente IA: ", "¿Cómo puedo ayudarte a analizar tus reportes?");
        }

        /// <summary>
        /// Construye la interfaz: contenedor raíz, historial (burbujas) y panel de entrada.
        /// </summary>
        private void BuildUi()
        {
            SuspendLayout();

            root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = UiTheme.Paper,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 2
            };
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Controls.Add(root);

            // ---  NUEVO Historial de Chat (Panel con TLP) ---
            // Usamos un Panel exterior que NO se auto-redimensiona
            pnlContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 8),
                AutoScroll = true // 👈 El Panel exterior gestiona el scroll
            };

            // El TableLayoutPanel interior SÍ se auto-redimensiona
            tlpChatHistory = new TableLayoutPanel
            {
                Dock = DockStyle.Top, // Se ancla arriba del Panel
                AutoSize = true,       // Crecerá verticalmente
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.White,
                ColumnCount = 2, // Columna 0 para IA, Columna 1 para Usuario
                RowCount = 1
            };
            tlpChatHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpChatHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpChatHistory.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // La primera fila

            pnlContainer.Controls.Add(tlpChatHistory);
            root.Controls.Add(pnlContainer, 0, 0);
            // ----------------------------------------------------

            // --- Panel de Input (Sin cambios) ---
            var inputPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                RowCount = 1
            };
            inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            root.Controls.Add(inputPanel, 0, 1);

            txtPrompt = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11f),
                PlaceholderText = "Escribe tu consulta aquí...",
                Height = 38,
                Margin = new Padding(0, 0, 8, 0)
            };
            inputPanel.Controls.Add(txtPrompt, 0, 0);

            btnSend = UiTheme.PrimaryButton("Enviar");
            btnSend.Width = 120;
            inputPanel.Controls.Add(btnSend, 1, 0);

            ResumeLayout();
        }

        /// <summary>
        /// Conecta eventos de ciclo de vida y de UI (Load, resize, Enter para enviar, click de enviar).
        /// </summary>
        private void HookEvents()
        {
            //  CORRECCIÓN: Añadimos el manejador del evento Load
            this.Load += UcAIAssistant_Load;

            txtPrompt.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter && !e.Shift)
                {
                    e.SuppressKeyPress = true;
                    btnSend.PerformClick();
                }
            };

            btnSend.Click += async (s, e) => await SendPromptAsync();

            //  NUEVO: Hacemos que el TLP se ajuste al ancho del panel
            pnlContainer.Resize += (s, e) =>
            {
                tlpChatHistory.Width = pnlContainer.ClientSize.Width;
            };
        }

        /// <summary>
        /// Inicializa el ancho del historial y agrega el saludo inicial del asistente.
        /// </summary>
        private void UcAIAssistant_Load(object sender, EventArgs e)
        {
            // Forzamos el ajuste de ancho aquí para asegurar que tlpChatHistory.Width tenga un valor
            if (pnlContainer.ClientSize.Width > 0)
            {
                tlpChatHistory.Width = pnlContainer.ClientSize.Width;
            }

            // Añadimos el mensaje inicial ahora que el control está cargado y dimensionado
            AppendToHistory("Asistente IA: ", "¿Cómo puedo ayudarte a analizar tus reportes?");
        }

        /// <summary>
        /// Envía el prompt al servicio de IA, agrega burbujas (usuario/respuesta) y gestiona estados de carga.
        /// </summary>
        private async Task SendPromptAsync()
        {
            string prompt = txtPrompt.Text.Trim();
            if (string.IsNullOrEmpty(prompt) || !btnSend.Enabled)
            {
                return;
            }

            SetLoading(true);
            AppendToHistory("Usuario: ", prompt); //  Llama al nuevo método
            txtPrompt.Clear();

            string response = await GeminiService.GenerateContentAsync(_apiKey, prompt, _contratos, _pagos);

            AppendToHistory("Asistente IA: ", response); //  Llama al nuevo método
            SetLoading(false);
        }

        /// <summary>
        /// Habilita/deshabilita controles y cursor de espera durante operaciones async.
        /// </summary>
        private void SetLoading(bool isLoading)
        {
            if (isLoading)
            {
                btnSend.Text = "Pensando...";
                btnSend.Enabled = false;
                txtPrompt.Enabled = false;
                root.UseWaitCursor = true;
            }
            else
            {
                btnSend.Text = "Enviar";
                btnSend.Enabled = true;
                txtPrompt.Enabled = true;
                root.UseWaitCursor = false;
                txtPrompt.Focus();
            }
        }

        /// <summary>
        /// Agrega una “burbuja” al historial (IA a la izquierda, usuario a la derecha) y hace scroll al final.
        /// </summary>
        /// <param name="prefix">Prefijo que identifica emisor ("Usuario: " o "Asistente IA: ").</param>
        /// <param name="text">Contenido del mensaje (se respeta salto de línea).</param>
        private void AppendToHistory(string prefix, string text)
        {
            // 1. Crear la burbuja (un Label con estilo)
            var bubble = new Label
            {
                Text = text.Replace("\n", Environment.NewLine),
                Font = new Font("Segoe UI", 10.5f),
                ForeColor = Color.Black,
                Padding = new Padding(12),
                Margin = new Padding(10, 8, 10, 8),
                AutoSize = true,
                // El truco para el auto-wrap:
                //  CORRECCIÓN: Usar pnlContainer.ClientSize.Width en lugar de tlpChatHistory.Width
                // Esto asegura que usemos el ancho del contenedor padre, que es más fiable.
                MaximumSize = new Size((int)(pnlContainer.ClientSize.Width * 0.45), 0) // Max 45% del ancho
            };

            // 2. Añadir una nueva fila al TLP
            tlpChatHistory.RowCount++;
            tlpChatHistory.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // 3. Aplicar estilo y alinear
            if (prefix == "Usuario: ")
            {
                bubble.BackColor = UiTheme.Primary; // Color principal
                bubble.ForeColor = Color.White;
                // Añadir a la Columna 1 (Derecha)
                tlpChatHistory.Controls.Add(bubble, 1, tlpChatHistory.RowCount - 1);
                // Añadir un panel vacío a la Columna 0 (Izquierda) para empujar
                tlpChatHistory.Controls.Add(new Panel { Dock = DockStyle.Fill }, 0, tlpChatHistory.RowCount - 1);
            }
            else // Asistente IA
            {
                bubble.BackColor = Color.FromArgb(235, 237, 239); // Gris claro
                bubble.ForeColor = UiTheme.TextMain;
                // Añadir a la Columna 0 (Izquierda)
                tlpChatHistory.Controls.Add(bubble, 0, tlpChatHistory.RowCount - 1);
                // Añadir un panel vacío a la Columna 1 (Derecha)
                tlpChatHistory.Controls.Add(new Panel { Dock = DockStyle.Fill }, 1, tlpChatHistory.RowCount - 1);
            }

            // 4. Forzar el scroll hacia abajo
            pnlContainer.ScrollControlIntoView(bubble);
        }
    }
}
