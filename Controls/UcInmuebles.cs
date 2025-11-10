using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions; // <<< AGREGADO
using System.Windows.Forms;
using InmoTech.Data.Repositories;
using InmoTech.Domain.Models;
using InmoTech.Security; // <--- AGREGADO
using InmoTech.Repositories; // <<< AGREGADO (para consultar contratos)

namespace InmoTech
{
    /// <summary>
    /// UserControl para administrar inmuebles: alta, edición, baja/activación,
    /// carga de imágenes y listado con miniaturas. Incluye validaciones y normalización de entradas.
    /// </summary>
    public partial class UcInmuebles : UserControl
    {
        // ======================================================
        //  REGIÓN: Campos Privados
        // ======================================================
        #region Campos Privados
        private readonly InmuebleRepository _repo = new InmuebleRepository();
        private readonly ContratoRepository _repoContrato = new ContratoRepository(); // <<< AGREGADO
        private int? _editandoId = null;
        private string _imagenPendiente = null;

        // Proveedor de errores (solo se usa al guardar)
        private readonly ErrorProvider _err = new ErrorProvider();
        #endregion

        // ======================================================
        //  REGIÓN: Constructor y Inicialización
        // ======================================================
        #region Constructor y Inicialización
        /// <summary>
        /// Constructor. Configura validaciones, handlers de eventos y estado inicial de controles.
        /// Deshabilita la validación automática para controlarla manualmente.
        /// </summary>
        public UcInmuebles()
        {
            InitializeComponent();

            if (!IsDesigner())
            {
                // Desactivamos la validación automática de WinForms
                this.AutoValidate = AutoValidate.Disable;

                Load += UcInmuebles_Load;
                btnGuardar.Click += BtnGuardar_Click;
                btnCancelar.Click += (s, e) => LimpiarFormulario();
                btnCargarImagen.Click += BtnCargarImagen_Click;
                btnQuitarImagen.Click += (s, e) => { _imagenPendiente = null; pbFoto.Image = null; };

                // Reemplaza CellClick por doble clic y botón de estado
                dgvInmuebles.CellDoubleClick += DgvInmuebles_CellDoubleClick;
                BEstado.Click += BEstado_Click;

                // ---- Handlers de entrada (no validan, solo filtran caracteres)
                // Dirección: letras, números y signos básicos
                txtDireccion.KeyPress += LetrasNumerosBasicos_KeyPress;

                // Descripción: texto libre controlado (sin caracteres peligrosos)
                txtDescripcion.KeyPress += Descripcion_KeyPress;

                // NumericUpDown: rango recomendado
                nudAmbientes.Minimum = 0;
                nudAmbientes.Maximum = 50;

                // Configuración ErrorProvider
                _err.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                _err.ContainerControl = this;
            }
        }

        /// <summary>
        /// Indica si el control se está ejecutando en el Diseñador de Visual Studio.
        /// </summary>
        private static bool IsDesigner() =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv";

        /// <summary>
        /// Evento Load del control. Inicializa límites, combos, limpia el formulario y refresca el grid.
        /// </summary>
        private void UcInmuebles_Load(object sender, EventArgs e)
        {
            // Límites de longitud y ayudas
            txtDireccion.MaxLength = 120;
            txtDescripcion.MaxLength = 800;
            txtDireccion.ShortcutsEnabled = true;
            txtDescripcion.ShortcutsEnabled = true;

            // Combos
            cboTipo.Items.Clear();
            cboTipo.Items.AddRange(new object[] { "Casa", "Departamento", "PH", "Local", "Galpón", "Oficina", "Terreno" });

            cboEstado.Items.Clear();
            cboEstado.Items.AddRange(new object[] { "Disponible", "Reservado", "Ocupado", "A estrenar", "N/A" });

            LimpiarFormulario(); // Limpia y establece el modo de alta
            RefrescarGrid();
        }
        #endregion

        // ======================================================
        //  REGIÓN: Manejadores de Eventos (UI Acciones)
        // ======================================================
        #region Manejadores de Eventos (UI Acciones)
        /// <summary>
        /// Abre un diálogo para seleccionar una imagen y la carga en el PictureBox sin bloquear el archivo.
        /// </summary>
        private void BtnCargarImagen_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Title = "Seleccionar imagen",
                Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.webp"
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _imagenPendiente = ofd.FileName;
                    pbFoto.Image = CargarBitmapSinLock(_imagenPendiente);
                }
            }
        }

        /// <summary>
        /// Guarda alta/edición del inmueble. Valida con ErrorProvider, lee el formulario,
        /// crea/actualiza el registro y asocia la imagen si fue seleccionada.
        /// </summary>
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Limpio errores previos cada vez que intento guardar
            LimpiarErrores();

            if (!ValidarFormulario(out var error))
            {
                // Mensaje general; los detalles quedan en cada control con ErrorProvider
                MessageBox.Show(error, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_editandoId == null)
                {
                    // Alta
                    if (AuthService.CurrentUser == null)
                    {
                        MessageBox.Show("Error de sesión. No se puede identificar al usuario creador. Inicie sesión de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int dniCreador = AuthService.CurrentUser.Dni;

                    var nuevo = LeerFormulario();
                    int id = _repo.CrearInmueble(nuevo, dniCreador);

                    if (!string.IsNullOrWhiteSpace(_imagenPendiente) && File.Exists(_imagenPendiente))
                        _repo.AgregarImagenDesdeArchivo(id, _imagenPendiente, "Portada", true, 0);
                }
                else
                {
                    // Edición
                    var entidad = LeerFormulario();
                    entidad.IdInmueble = _editandoId.Value;

                    var original = _repo.ObtenerPorId(_editandoId.Value);
                    if (original != null) entidad.Estado = original.Estado;

                    _repo.Actualizar(entidad);

                    if (!string.IsNullOrWhiteSpace(_imagenPendiente) && File.Exists(_imagenPendiente))
                        _repo.AgregarImagenDesdeArchivo(_editandoId.Value, _imagenPendiente, "Portada", true, 0);
                }

                LimpiarFormulario();
                RefrescarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar.\n" + ex.Message, "Inmuebles", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Abre el inmueble seleccionado (doble clic en la grilla) y carga sus datos para edición.
        /// </summary>
        private void DgvInmuebles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignorar cabecera
            int id = Convert.ToInt32(dgvInmuebles.Rows[e.RowIndex].Cells["colId"].Value);
            CargarParaEditar(id);
        }

        /// <summary>
        /// Activa o da de baja un inmueble. Impide dar de baja si hay contratos activos asociados.
        /// </summary>
        private void BEstado_Click(object sender, EventArgs e)
        {
            if (_editandoId == null) return;

            try
            {
                var inmueble = _repo.ObtenerPorId(_editandoId.Value);
                if (inmueble == null) return;

                bool activar = !inmueble.Estado;
                string accion = activar ? "ACTIVAR" : "dar de BAJA";

                // <<< VERIFICACIÓN AGREGADA: impedir baja si hay contratos activos >>>
                if (!activar) // estamos por dar de baja (estado = 0)
                {
                    bool tieneContratoActivo = _repoContrato.ExisteContratoActivoPorInmueble(inmueble.IdInmueble);
                    if (tieneContratoActivo)
                    {
                        MessageBox.Show(
                            "No podés dar de baja este inmueble porque tiene contratos ACTIVOS asociados.\n" +
                            "Anulá esos contratos y volvé a intentar.",
                            "Operación no permitida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }
                }
                // <<< FIN verificación >>>

                var resp = MessageBox.Show($"¿Deseas {accion} este inmueble?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp != DialogResult.Yes) return;

                inmueble.Estado = activar;
                _repo.Actualizar(inmueble);

                LimpiarFormulario();
                RefrescarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cambiar el estado.\n" + ex.Message, "Inmuebles", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Lógica Principal (CRUD)
        // ======================================================
        #region Lógica Principal (CRUD)
        /// <summary>
        /// Carga el listado de inmuebles (activos e inactivos), arma miniaturas si hay imágenes
        /// y dibuja cada fila en el DataGridView.
        /// </summary>
        private void RefrescarGrid()
        {
            try
            {
                var lista = _repo.Listar(null, false); // Muestro todos (activos e inactivos)

                dgvInmuebles.Rows.Clear();
                foreach (var x in lista)
                {
                    Image thumb = null;
                    var imgs = _repo.ListarImagenes(x.IdInmueble);
                    var portada = imgs.FirstOrDefault(im => im.EsPortada) ?? imgs.FirstOrDefault();

                    if (portada != null)
                    {
                        var abs = Path.IsPathRooted(portada.Ruta)
                                ? portada.Ruta
                                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, portada.Ruta);
                        if (File.Exists(abs)) thumb = Escalar(CargarBitmapSinLock(abs), 64, 48);
                    }

                    dgvInmuebles.Rows.Add(
                        x.IdInmueble,
                        x.Direccion,
                        x.Tipo,
                        x.NroAmbientes ?? 0,
                        x.Amueblado ? "Amueblado" : "Sin Amueblar",
                        x.Condiciones,
                        x.Estado ? "Activo" : "Inactivo",
                        thumb
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar inmuebles.\n" + ex.Message, "Inmuebles", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Carga un inmueble por Id y llena el formulario para edición, incluyendo la portada si existe.
        /// </summary>
        private void CargarParaEditar(int id)
        {
            try
            {
                var i = _repo.ObtenerPorId(id, true);
                if (i == null) return;

                _editandoId = id;
                btnGuardar.Text = "Actualizar";
                _imagenPendiente = null;

                txtDireccion.Text = i.Direccion;
                SelectItem(cboTipo, i.Tipo);
                txtDescripcion.Text = i.Descripcion ?? "";
                SelectItem(cboEstado, i.Condiciones);
                nudAmbientes.Value = Math.Max(nudAmbientes.Minimum, Math.Min(nudAmbientes.Maximum, i.NroAmbientes ?? 0));
                chkAmueblado.Checked = i.Amueblado;

                BEstado.Enabled = true;
                BEstado.Text = i.Estado ? "Baja" : "Activar";

                pbFoto.Image = null;
                var portada = i.Imagenes.FirstOrDefault(im => im.EsPortada) ?? i.Imagenes.FirstOrDefault();
                if (portada != null)
                {
                    var abs = Path.IsPathRooted(portada.Ruta) ? portada.Ruta : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, portada.Ruta);
                    if (File.Exists(abs)) pbFoto.Image = CargarBitmapSinLock(abs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar inmueble.\n" + ex.Message, "Inmuebles", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Mapeo UI - Modelo y Validación
        // ======================================================
        #region Mapeo UI - Modelo y Validación

        /// <summary>
        /// Lee y normaliza los valores del formulario, creando la instancia de <see cref="Inmueble"/>.
        /// </summary>
        private Inmueble LeerFormulario()
        {
            // Normalizamos espacios y caracteres peligrosos antes de crear el modelo
            var direccion = NormalizarDireccion(txtDireccion.Text);
            var descripcion = NormalizarDescripcion(txtDescripcion.Text);

            return new Inmueble
            {
                Direccion = direccion,
                Tipo = cboTipo.SelectedItem?.ToString() ?? "",
                Descripcion = descripcion,
                Condiciones = cboEstado.SelectedItem?.ToString() ?? "Disponible",
                NroAmbientes = (int)nudAmbientes.Value,
                Amueblado = chkAmueblado.Checked,
                Estado = true // Al crear, activo por defecto
            };
        }

        /// <summary>
        /// Valida campos del formulario y coloca mensajes en el <see cref="ErrorProvider"/>.
        /// Devuelve false si hay errores y expone un mensaje general en <c>error</c>.
        /// </summary>
        private bool ValidarFormulario(out string error)
        {
            error = "";
            bool ok = true;

            // Dirección
            var direccion = (txtDireccion.Text ?? "").Trim();
            if (direccion.Length == 0)
            {
                _err.SetError(txtDireccion, "La dirección es obligatoria.");
                ok = false;
            }
            else if (direccion.Length < 5)
            {
                _err.SetError(txtDireccion, "La dirección debe tener al menos 5 caracteres.");
                ok = false;
            }
            else if (!Regex.IsMatch(direccion, DireccionRegex, RegexOptions.CultureInvariant))
            {
                _err.SetError(txtDireccion, "Dirección inválida. Permitido: letras, números, espacios y .,#-/ºª°");
                ok = false;
            }

            // Tipo
            if (cboTipo.SelectedIndex < 0)
            {
                _err.SetError(cboTipo, "Seleccioná un tipo de inmueble.");
                ok = false;
            }

            // Ambientes
            var ambientes = (int)nudAmbientes.Value;
            if (ambientes < 0)
            {
                _err.SetError(nudAmbientes, "El número de ambientes no puede ser negativo.");
                ok = false;
            }

            // Condición
            if (cboEstado.SelectedIndex < 0)
            {
                _err.SetError(cboEstado, "Seleccioná una condición.");
                ok = false;
            }

            // Regla cruzada simple
            if (cboEstado.SelectedItem?.ToString() == "Ocupado" && ambientes == 0)
            {
                _err.SetError(nudAmbientes, "Si el inmueble está 'Ocupado', debe tener al menos 1 ambiente.");
                ok = false;
            }

            // Descripción (opcional, pero controlada si existe)
            var desc = (txtDescripcion.Text ?? "").Trim();
            if (desc.Length > 0 && !Regex.IsMatch(desc, DescripcionRegex, RegexOptions.CultureInvariant))
            {
                _err.SetError(txtDescripcion, "Descripción: se permiten letras, números, espacios y puntuación básica.");
                ok = false;
            }

            if (!ok)
                error = "Revisá los campos marcados.";

            return ok;
        }
        #endregion

        // ======================================================
        //  REGIÓN: Métodos de Utilidad y Validadores
        // ======================================================
        #region Métodos de Utilidad y Validadores

        // Regex precompilados
        private const string LetrasRegex = @"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ\s]+$";
        private const string DireccionRegex = @"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ0-9\s\.\,\#\-/ºª°]+$";
        private const string DescripcionRegex = @"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ0-9\s\.\,\;\:\-\(\)\/\#\!¿\?¡""'%]+$";

        /// <summary>
        /// Selecciona en un ComboBox el ítem cuyo texto coincide (case-insensitive) con <paramref name="value"/>.
        /// Si no encuentra coincidencia, deja el índice en -1.
        /// </summary>
        private static void SelectItem(ComboBox cbo, string value)
        {
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                var s = cbo.Items[i]?.ToString() ?? "";
                if (string.Equals(s, value ?? "", StringComparison.InvariantCultureIgnoreCase))
                { cbo.SelectedIndex = i; return; }
            }
            cbo.SelectedIndex = -1;
        }

        /// <summary>
        /// Carga un Bitmap desde disco permitiendo compartir el archivo (sin bloquearlo).
        /// </summary>
        private static Bitmap CargarBitmapSinLock(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var img = Image.FromStream(fs))
                return new Bitmap(img);
        }

        /// <summary>
        /// Devuelve una nueva imagen escalada a <paramref name="w"/>x<paramref name="h"/>.
        /// </summary>
        private static Image Escalar(Image img, int w, int h) =>
            img == null ? null : new Bitmap(img, new Size(w, h));

        /// <summary>
        /// Restablece el formulario al estado inicial (modo alta), limpia errores e imagen.
        /// </summary>
        private void LimpiarFormulario()
        {
            _editandoId = null;
            _imagenPendiente = null;

            btnGuardar.Text = "Guardar";

            BEstado.Enabled = false;
            BEstado.Text = "Baja";

            LimpiarErrores();

            txtDireccion.Clear();
            cboTipo.SelectedIndex = -1;
            txtDescripcion.Clear();
            cboEstado.SelectedIndex = 0;
            nudAmbientes.Value = 0;
            chkAmueblado.Checked = false;

            pbFoto.Image = null;
            txtDireccion.Focus();
        }

        /// <summary>
        /// Limpia todos los mensajes de error del <see cref="ErrorProvider"/>.
        /// </summary>
        private void LimpiarErrores()
        {
            _err.SetError(txtDireccion, "");
            _err.SetError(txtDescripcion, "");
            _err.SetError(cboTipo, "");
            _err.SetError(cboEstado, "");
            _err.SetError(nudAmbientes, "");
        }

        // -------- Normalizadores (evitan espacios dobles y caracteres raros)

        /// <summary>
        /// Recorta extremos y colapsa espacios múltiples a uno.
        /// </summary>
        private static string NormalizarEspacios(string s) =>
            Regex.Replace((s ?? "").Trim(), @"\s{2,}", " ");

        /// <summary>
        /// Normaliza la dirección, eliminando caracteres no permitidos y espacios de más.
        /// </summary>
        private static string NormalizarDireccion(string s)
        {
            s = NormalizarEspacios(s);
            // Evitar inyección de separadores raros
            s = Regex.Replace(s, @"[^\w\s\.\,\#\-/ºª°ÁÉÍÓÚÜÑáéíóúüñ]", "");
            return s;
        }

        /// <summary>
        /// Normaliza la descripción, permitiendo puntuación básica y removiendo caracteres de control.
        /// </summary>
        private static string NormalizarDescripcion(string s)
        {
            s = NormalizarEspacios(s);
            // Permitimos puntuación básica, eliminamos caracteres de control
            s = Regex.Replace(s, @"[^\w\s\.\,\;\:\-\(\)\/\#\!¿\?¡""'%ÁÉÍÓÚÜÑáéíóúüñ]", "");
            return s;
        }

        // -------- KeyPress handlers reutilizables (NO validan foco, solo filtran caracteres)

        /// <summary>
        /// Permite solo letras y espacios (se respetan teclas de control).
        /// </summary>
        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            e.Handled = !Regex.IsMatch(e.KeyChar.ToString(), @"[A-Za-zÁÉÍÓÚÜÑáéíóúüñ\s]");
        }

        /// <summary>
        /// Permite letras, números y signos básicos usados en direcciones (.,#-/ ºª°).
        /// </summary>
        private void LetrasNumerosBasicos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            e.Handled = !Regex.IsMatch(e.KeyChar.ToString(), @"[A-Za-zÁÉÍÓÚÜÑáéíóúüñ0-9\.\,\#\-/\sºª°]");
        }

        /// <summary>
        /// Permite solo dígitos (se respetan teclas de control).
        /// </summary>
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        /// <summary>
        /// Permite letras, números y puntuación básica para descripciones.
        /// </summary>
        private void Descripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            e.Handled = !Regex.IsMatch(e.KeyChar.ToString(), @"[A-Za-zÁÉÍÓÚÜÑáéíóúüñ0-9\.\,\;\:\-\(\)\/\#\!¿\?¡""'%\s]");
        }
        #endregion

        #region Handlers de Plantilla (Vacíos)
        /// <summary>
        /// Reservado para futuras acciones al entrar al GroupBox de creación.
        /// </summary>
        private void gbCrear_Enter(object sender, EventArgs e) { }

        /// <summary>
        /// Reservado para reaccionar a cambios de texto en la descripción.
        /// </summary>
        private void txtDescripcion_TextChanged(object sender, EventArgs e) { }
        #endregion
    }
}
