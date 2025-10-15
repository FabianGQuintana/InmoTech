using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using InmoTech.Data.Repositories;
using InmoTech.Domain.Models;

namespace InmoTech
{
    public partial class UcInmuebles : UserControl
    {
        // ======================================================
        //  REGIÓN: Campos Privados
        // ======================================================
        #region Campos Privados
        private readonly InmuebleRepository _repo = new InmuebleRepository();
        private int? _editandoId = null;
        private string _imagenPendiente = null;
        #endregion

        // ======================================================
        //  REGIÓN: Constructor y Inicialización
        // ======================================================
        #region Constructor y Inicialización
        public UcInmuebles()
        {
            InitializeComponent();

            if (!IsDesigner())
            {
                Load += UcInmuebles_Load;
                btnGuardar.Click += BtnGuardar_Click;
                btnCancelar.Click += (s, e) => LimpiarFormulario();
                btnCargarImagen.Click += BtnCargarImagen_Click;
                btnQuitarImagen.Click += (s, e) => { _imagenPendiente = null; pbFoto.Image = null; };

                // >>> MODIFICADO <<<: Se reemplaza CellClick por CellDoubleClick y se agrega el evento para el botón de estado.
                dgvInmuebles.CellDoubleClick += DgvInmuebles_CellDoubleClick;
                BEstado.Click += BEstado_Click;
            }
        }

        private static bool IsDesigner() =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv";

        private void UcInmuebles_Load(object sender, EventArgs e)
        {
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

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario(out var error))
            {
                MessageBox.Show(error, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_editandoId == null)
                {
                    // Alta
                    var nuevo = LeerFormulario();
                    int id = _repo.CrearInmueble(nuevo);
                    if (!string.IsNullOrWhiteSpace(_imagenPendiente) && File.Exists(_imagenPendiente))
                        _repo.AgregarImagenDesdeArchivo(id, _imagenPendiente, "Portada", true, 0);
                }
                else
                {
                    // Edición
                    var entidad = LeerFormulario();
                    entidad.IdInmueble = _editandoId.Value;
                    // Mantenemos el estado (activo/inactivo) que ya tenía
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

        // >>> NUEVO <<<: Manejador para el doble clic en la grilla (inicia edición).
        private void DgvInmuebles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignorar clics en la cabecera

            int id = Convert.ToInt32(dgvInmuebles.Rows[e.RowIndex].Cells["colId"].Value);
            CargarParaEditar(id);
        }

        // >>> NUEVO <<<: Manejador para el botón de Baja/Activar.
        private void BEstado_Click(object sender, EventArgs e)
        {
            if (_editandoId == null) return;

            try
            {
                var inmueble = _repo.ObtenerPorId(_editandoId.Value);
                if (inmueble == null) return;

                bool activar = !inmueble.Estado;
                string accion = activar ? "ACTIVAR" : "dar de BAJA";

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
        private void RefrescarGrid()
        {
            try
            {
                var lista = _repo.Listar(null, false); // Muestro todos (activos e inactivos)

                dgvInmuebles.Rows.Clear();
                foreach (var x in lista)
                {
                    Image thumb = null;
                    var portada = _repo.ListarImagenes(x.IdInmueble).FirstOrDefault(im => im.EsPortada)
                                  ?? _repo.ListarImagenes(x.IdInmueble).FirstOrDefault();

                    if (portada != null)
                    {
                        var abs = Path.IsPathRooted(portada.Ruta)
                                    ? portada.Ruta
                                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, portada.Ruta);
                        if (File.Exists(abs)) thumb = Escalar(CargarBitmapSinLock(abs), 64, 48);
                    }

                    // >>> MODIFICADO <<<: Se pasan textos ("Sí"/"No") en lugar de booleanos.
                    dgvInmuebles.Rows.Add(
                        x.IdInmueble,
                        x.Direccion,
                        x.Tipo,
                        x.NroAmbientes ?? 0,
                        x.Amueblado ? "Amueblado" : "Sin Amueblar", // Convertir a texto
                        x.Condiciones,
                        x.Estado ? "Activo" : "Inactivo",      // Convertir a texto
                        thumb
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar inmuebles.\n" + ex.Message, "Inmuebles", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

                // >>> MODIFICADO <<<: Habilita el botón de estado y ajusta su texto.
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

        // >>> ELIMINADO <<<: El método AlternarEstado ya no es necesario, su lógica se movió a BEstado_Click.

        #endregion

        // ======================================================
        //  REGIÓN: Mapeo UI - Modelo y Validación
        // ======================================================
        #region Mapeo UI - Modelo y Validación

        private Inmueble LeerFormulario()
        {
            return new Inmueble
            {
                Direccion = (txtDireccion.Text ?? "").Trim(),
                Tipo = cboTipo.SelectedItem?.ToString() ?? "",
                Descripcion = (txtDescripcion.Text ?? "").Trim(),
                Condiciones = cboEstado.SelectedItem?.ToString() ?? "Disponible",
                NroAmbientes = (int)nudAmbientes.Value,
                Amueblado = chkAmueblado.Checked,
                Estado = true // Al crear, siempre está activo por defecto.
            };
        }

        private bool ValidarFormulario(out string error)
        {
            error = "";
            var direccion = (txtDireccion.Text ?? "").Trim();
            var tipoSel = cboTipo.SelectedItem?.ToString() ?? "";
            var condicionesSel = cboEstado.SelectedItem?.ToString() ?? "";
            var ambientes = (int)nudAmbientes.Value;

            if (direccion.Length == 0) { error = "La dirección es obligatoria."; txtDireccion.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(tipoSel)) { error = "Seleccioná un tipo de inmueble."; cboTipo.Focus(); return false; }
            if (ambientes < 0) { error = "El número de ambientes no puede ser negativo."; nudAmbientes.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(condicionesSel)) { error = "Seleccioná una condición."; cboEstado.Focus(); return false; }

            return true;
        }
        #endregion

        // ======================================================
        //  REGIÓN: Metodos de Utilidad
        // ======================================================
        #region Métodos de Utilidad
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

        private static Bitmap CargarBitmapSinLock(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var img = Image.FromStream(fs))
                return new Bitmap(img);
        }

        private static Image Escalar(Image img, int w, int h) =>
            img == null ? null : new Bitmap(img, new Size(w, h));

        private void LimpiarFormulario()
        {
            _editandoId = null;
            _imagenPendiente = null;

            btnGuardar.Text = "Guardar";

            // >>> MODIFICADO <<<: Se deshabilita y resetea el botón de estado.
            BEstado.Enabled = false;
            BEstado.Text = "Baja";

            txtDireccion.Clear();
            cboTipo.SelectedIndex = -1;
            txtDescripcion.Clear();
            cboEstado.SelectedIndex = 0;
            nudAmbientes.Value = 0;
            chkAmueblado.Checked = false;

            pbFoto.Image = null;
            txtDireccion.Focus();
        }
        #endregion

        #region Handlers de Plantilla (Vacíos)
        private void gbCrear_Enter(object sender, EventArgs e) { }
        private void txtDescripcion_TextChanged(object sender, EventArgs e) { }
        #endregion
    }
}