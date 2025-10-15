// UcBackup.cs
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcBackup : UserControl
    {
        // ======================================================
        //  REGIÓN: Clase de configuración (Modelo)
        // ======================================================
        #region Clase de Configuración (Modelo)
        // ===== Modelo de configuración que se levanta desde la UI =====
        public class BackupConfig
        {
            public string Destino { get; set; } = "";
            public string NombreArchivo { get; set; } = "";
            public string Compresion { get; set; } = "Ninguna"; // Ninguna | Baja | Media | Alta
            public bool Verificar { get; set; }
            public bool Sobrescribir { get; set; }
            public bool AgregarFecha { get; set; }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Eventos
        // ======================================================
        #region Eventos
        // ===== Eventos para conectar backend más adelante =====
        public event EventHandler<BackupConfig>? SimularClicked;
        public event EventHandler<string>? ProbarRutaClicked;
        #endregion

        // ======================================================
        //  REGIÓN:Campos Privados
        // ======================================================
        #region Campos Privados
        private readonly ToolTip _tips = new ToolTip { IsBalloon = true };
        #endregion

        // ======================================================
        //  REGIÓN: Constructor
        // ======================================================
        #region Constructor
        public UcBackup()
        {
            InitializeComponent();

            // Valores por defecto
            cmbCompresion.SelectedIndex = 0; // "Ninguna"
            txtNombre.Text = $"backup_inmotech_{DateTime.Now:yyyyMMdd_HHmm}.bak";
            chkAgregarFecha.Checked = true;

            // Tips
            _tips.SetToolTip(btnElegirCarpeta, "Elegir carpeta de destino");
            _tips.SetToolTip(btnProbarRuta, "Verifica si la ruta existe y es escribible");
            _tips.SetToolTip(btnSimular, "Muestra el comando/acción que se ejecutaría (sin correrla)");

            // Handlers
            btnElegirCarpeta.Click += BtnElegirCarpeta_Click;
            btnProbarRuta.Click += BtnProbarRuta_Click;
            btnSimular.Click += BtnSimular_Click;

            chkAgregarFecha.CheckedChanged += (_, __) => RegenerarNombreSugerido();
        }
        #endregion

        // ======================================================
        //  REGIÓN: API Pública
        // ======================================================
        #region API Pública
        // ===== API pública para levantar la config desde afuera =====
        public BackupConfig GetConfig() => new BackupConfig
        {
            Destino = txtDestino.Text.Trim(),
            NombreArchivo = txtNombre.Text.Trim(),
            Compresion = (cmbCompresion.SelectedItem?.ToString() ?? "Ninguna").Trim(),
            Verificar = chkVerificar.Checked,
            Sobrescribir = chkSobrescribir.Checked,
            AgregarFecha = chkAgregarFecha.Checked
        };
        #endregion

        // ======================================================
        //  REGIÓN: Manejadores de Eventos (Botones)
        // ======================================================
        #region Manejadores de Eventos (Botones)
        // ===== Helpers internos (Manejadores de Eventos) =====
        private void BtnElegirCarpeta_Click(object? sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog
            {
                Description = "Seleccioná la carpeta donde guardar el backup",
                ShowNewFolderButton = true
            };
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                txtDestino.Text = fbd.SelectedPath;
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    RegenerarNombreSugerido();
            }
        }

        private void BtnProbarRuta_Click(object? sender, EventArgs e)
        {
            var ruta = txtDestino.Text.Trim();
            if (string.IsNullOrWhiteSpace(ruta))
            {
                MessageBox.Show("Ingresá una carpeta de destino.", "Ruta vacía",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDestino.Focus();
                return;
            }

            try
            {
                // Test básico: existencia + permiso de escritura (crea archivo temporal)
                if (!Directory.Exists(ruta))
                    throw new DirectoryNotFoundException("La carpeta no existe.");

                var testFile = Path.Combine(ruta, $"_test_write_{Guid.NewGuid():N}.tmp");
                File.WriteAllText(testFile, "ok");
                File.Delete(testFile);

                // Lanza evento para que un backend (si quiere) haga chequeos extra
                ProbarRutaClicked?.Invoke(this, ruta);

                lblEstado.Text = "Ruta verificada correctamente ✔";
                lblEstado.ForeColor = System.Drawing.Color.DarkGreen;

                MessageBox.Show("La ruta existe y es escribible.", "Ruta válida",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblEstado.Text = $"Error de ruta: {ex.Message}";
                lblEstado.ForeColor = System.Drawing.Color.Firebrick;

                MessageBox.Show($"No se pudo validar la ruta.\n\nDetalle: {ex.Message}",
                    "Ruta inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSimular_Click(object? sender, EventArgs e)
        {
            if (!Validar(out var msg))
            {
                MessageBox.Show(msg, "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cfg = GetConfig();

            // Construcción del nombre final (con fecha si aplica)
            var nombre = cfg.NombreArchivo;
            if (cfg.AgregarFecha)
            {
                var sinExt = Path.GetFileNameWithoutExtension(nombre);
                var ext = Path.GetExtension(nombre);
                nombre = $"{sinExt}_{DateTime.Now:yyyyMMdd_HHmm}{(string.IsNullOrEmpty(ext) ? ".bak" : ext)}";
            }

            var salida = Path.Combine(cfg.Destino, nombre);

            // Texto de simulación (para mostrar qué haríamos)
            var resumen =
$@"SIMULACIÓN DE BACKUP (no ejecuta nada)

Destino:       {cfg.Destino}
Archivo:       {nombre}
Compresión:    {cfg.Compresion}
Verificar:     {(cfg.Verificar ? "Sí" : "No")}
Sobrescribir:  {(cfg.Sobrescribir ? "Sí" : "No")}

Ruta final:    {salida}";

            // Dispara evento para que el backend futuro pueda, por ejemplo, construir un script T-SQL
            SimularClicked?.Invoke(this, cfg);

            txtPreview.Text = resumen;
            lblEstado.Text = "Simulación generada (previsualización actualizada).";
            lblEstado.ForeColor = System.Drawing.Color.DarkSlateGray;

            // También muestro un mensaje informativo
            MessageBox.Show("Se generó la simulación del backup.\nRevisá la previsualización.", "Simulación OK",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        // ======================================================
        //  REGIÓN: Metodos de Utilidad/Validación
        // ======================================================
        #region Métodos de Utilidad/Validación
        private bool Validar(out string mensaje)
        {
            if (string.IsNullOrWhiteSpace(txtDestino.Text))
            {
                mensaje = "Ingresá o elegí una carpeta de destino.";
                txtDestino.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                mensaje = "Ingresá un nombre de archivo para el backup (por ej. backup_inmotech.bak).";
                txtNombre.Focus();
                return false;
            }
            // Validación mínima del nombre (sin barras, etc.)
            var nombre = txtNombre.Text.Trim();
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                if (nombre.Contains(c.ToString()))
                {
                    mensaje = "El nombre de archivo contiene caracteres no válidos.";
                    txtNombre.Focus();
                    return false;
                }
            }

            mensaje = "";
            return true;
        }

        private void RegenerarNombreSugerido()
        {
            var baseName = "backup_inmotech";
            if (chkAgregarFecha.Checked)
                txtNombre.Text = $"{baseName}_{DateTime.Now:yyyyMMdd_HHmm}.bak";
            else
                txtNombre.Text = $"{baseName}.bak";
        }
        #endregion
    }
}