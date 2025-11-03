using InmoTech.Services; // El servicio que acabamos de crear
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcAIAssistant : UserControl
    {
        private readonly string _apiKey;

        private TextBox txtChatHistory;
        private TextBox txtPrompt;
        private Button btnSend;
        private TableLayoutPanel root;

        // Pasamos la API key al abrir el chat
        public UcAIAssistant(string apiKey)
        {
            _apiKey = apiKey;

            UiTheme.EnableHighDpi(this);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;

            BuildUi();
            HookEvents();
        }

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
            // Fila 0: Historial (ocupa todo el espacio)
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // Fila 1: Input y botón (tamaño fijo)
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            Controls.Add(root);

            // --- Historial de Chat ---
            txtChatHistory = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10f),
                Margin = new Padding(0, 0, 0, 8),
                Text = "Asistente IA: ¿Cómo puedo ayudarte a analizar tus reportes?" + Environment.NewLine
            };
            root.Controls.Add(txtChatHistory, 0, 0);

            // --- Panel de Input ---
            var inputPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                RowCount = 1
            };
            inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // Input
            inputPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // Botón
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
            btnSend.Width = 120; // Ancho más pequeño
            inputPanel.Controls.Add(btnSend, 1, 0);

            ResumeLayout();
        }

        private void HookEvents()
        {
            // Evento para enviar con 'Enter'
            txtPrompt.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter && !e.Shift)
                {
                    e.SuppressKeyPress = true; // Evita el 'ding' de Windows
                    btnSend.PerformClick();
                }
            };

            btnSend.Click += async (s, e) => await SendPromptAsync();
        }

        private async Task SendPromptAsync()
        {
            string prompt = txtPrompt.Text.Trim();
            if (string.IsNullOrEmpty(prompt) || !btnSend.Enabled)
            {
                return;
            }

            // Deshabilitar UI mientras piensa
            SetLoading(true);
            AppendToHistory("Usuario: ", prompt);
            txtPrompt.Clear();

            // Llamar al servicio
            string response = await GeminiService.GenerateContentAsync(_apiKey, prompt);

            // Mostrar respuesta
            AppendToHistory("Asistente IA: ", response);
            SetLoading(false);
        }

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

        private void AppendToHistory(string prefix, string text)
        {
            var sb = new StringBuilder();
            sb.Append(prefix);

            // Formatea la respuesta para que se vea bien en el TextBox
            sb.Append(text.Replace("\n", Environment.NewLine));

            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            txtChatHistory.AppendText(sb.ToString());
            txtChatHistory.ScrollToCaret();
        }
    }
}