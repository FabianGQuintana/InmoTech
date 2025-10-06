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
        private readonly InmuebleRepository _repo = new InmuebleRepository();
        private int? _editandoId = null;
        private string _imagenPendiente = null; // ruta local seleccionada para guardar como portada

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
                dgvInmuebles.CellClick += DgvInmuebles_CellClick;
            }
        }

        private static bool IsDesigner() =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv";

        /* =========================
           Init
           ========================= */
        private void UcInmuebles_Load(object sender, EventArgs e)
        {
            // Combos
            cboTipo.Items.Clear();
            cboTipo.Items.AddRange(new object[] { "Casa", "Departamento", "PH", "Local", "Galpón", "Oficina", "Terreno" });

            cboEstado.Items.Clear();
            cboEstado.Items.AddRange(new object[] { "Disponible", "Reservado", "Ocupado", "Inactivo" });
            cboTipo.SelectedIndex = -1;
            cboEstado.SelectedIndex = 0;
            nudAmbientes.Value = 0;
            chkAmueblado.Checked = false;

            // Grid
            RefrescarGrid();
        }

        /* =========================
           Acciones UI
           ========================= */
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

        private void DgvInmuebles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dgvInmuebles.Rows[e.RowIndex].Cells["colId"].Value);
            var colName = dgvInmuebles.Columns[e.ColumnIndex].Name;

            if (string.Equals(colName, "colToggle", StringComparison.InvariantCultureIgnoreCase))
            {
                AlternarEstado(id);
            }
            else
            {
                CargarParaEditar(id);
            }
        }

        /* =========================
           Lógica principal
           ========================= */
        private void RefrescarGrid()
        {
            try
            {
                var lista = _repo.Listar(null, false); // muestro todos (activos e inactivos)

                dgvInmuebles.Rows.Clear();
                foreach (var x in lista)
                {
                    // portada
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

                    dgvInmuebles.Rows.Add(
                        x.IdInmueble,
                        x.Direccion,
                        x.Tipo,
                        x.NroAmbientes ?? 0,
                        x.Amueblado,
                        EstadoToTexto(x.Estado),
                        thumb,
                        "Editar",
                        "Cambiar"
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
                SelectItem(cboEstado, EstadoToTexto(i.Estado));
                nudAmbientes.Value = Math.Max(nudAmbientes.Minimum, Math.Min(nudAmbientes.Maximum, i.NroAmbientes ?? 0));
                chkAmueblado.Checked = i.Amueblado;

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

        private void AlternarEstado(int id)
        {
            try
            {
                var i = _repo.ObtenerPorId(id);
                if (i == null) return;

                // Toggle simple entre Disponible(1) e Inactivo(0)
                i.Estado = (byte)(i.Estado == 0 ? 1 : 0);
                _repo.Actualizar(i);

                // Si estábamos editando, reflejarlo
                if (_editandoId == id) SelectItem(cboEstado, EstadoToTexto(i.Estado));

                RefrescarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cambiar el estado.\n" + ex.Message, "Inmuebles", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* =========================
           Mapeo UI ↔ Modelo
           ========================= */
        private Inmueble LeerFormulario()
        {
            return new Inmueble
            {
                Direccion = (txtDireccion.Text ?? "").Trim(),
                Tipo = cboTipo.SelectedItem?.ToString() ?? "",
                Descripcion = (txtDescripcion.Text ?? "").Trim(),
                Condiciones = "N/A", // agregá txtCondiciones si lo sumás al UI
                NroAmbientes = (int)nudAmbientes.Value,
                Amueblado = chkAmueblado.Checked,
                Estado = TextoToEstado(cboEstado.SelectedItem?.ToString() ?? "Disponible")
            };
        }

        private bool ValidarFormulario(out string error)
        {
            error = "";
            var direccion = (txtDireccion.Text ?? "").Trim();
            var tipoSel = cboTipo.SelectedItem?.ToString() ?? "";
            var estadoSel = cboEstado.SelectedItem?.ToString() ?? "";
            var ambientes = (int)nudAmbientes.Value;

            if (direccion.Length == 0) { error = "La dirección es obligatoria."; txtDireccion.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(tipoSel)) { error = "Seleccioná un tipo de inmueble."; cboTipo.Focus(); return false; }
            if (ambientes < 1) { error = "El número de ambientes debe ser al menos 1."; nudAmbientes.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(estadoSel)) { error = "Seleccioná un estado."; cboEstado.Focus(); return false; }

            return true;
        }

        /* =========================
           Utilidades
           ========================= */
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

        // 0=Inactivo, 1=Disponible, 2=Reservado, 3=Ocupado
        private static string EstadoToTexto(byte estado)
        {
            switch (estado)
            {
                case 0: return "Inactivo";
                case 2: return "Reservado";
                case 3: return "Ocupado";
                default: return "Disponible";
            }
        }

        private static byte TextoToEstado(string texto)
        {
            if (string.Equals(texto, "Inactivo", StringComparison.InvariantCultureIgnoreCase)) return 0;
            if (string.Equals(texto, "Reservado", StringComparison.InvariantCultureIgnoreCase)) return 2;
            if (string.Equals(texto, "Ocupado", StringComparison.InvariantCultureIgnoreCase)) return 3;
            return 1; // Disponible
        }

        private static Bitmap CargarBitmapSinLock(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var img = Image.FromStream(fs))
                return new Bitmap(img);
        }

        private static Image Escalar(Image img, int w, int h) =>
            img == null ? null : new Bitmap(img, new Size(w, h));

        // Limpia el formulario y deja el control listo para un alta
        private void LimpiarFormulario()
        {
            _editandoId = null;
            _imagenPendiente = null;

            btnGuardar.Text = "Guardar";

            txtDireccion.Clear();
            cboTipo.SelectedIndex = -1;
            txtDescripcion.Clear();
            cboEstado.SelectedIndex = 0;
            nudAmbientes.Value = 0;
            chkAmueblado.Checked = false;

            pbFoto.Image = null;
            txtDireccion.Focus();
        }

        // El diseñador tiene: gbCrear.Enter += gbCrear_Enter;
        private void gbCrear_Enter(object sender, EventArgs e)
        {
            // Nada por ahora (stub para evitar error de compilación)
        }

    }
}
