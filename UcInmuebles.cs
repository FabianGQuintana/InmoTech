using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class UcInmuebles : UserControl
    {
        // ===== Modelo simple =====
        private class Inmueble
        {
            public int id_inmueble { get; set; }
            public string direccion { get; set; } = "";
            public string tipo { get; set; } = "";
            public string descripcion { get; set; } = "";
            public string estado { get; set; } = "Disponible"; // Disponible / Ocupado / Inactivo
            public int nro_ambientes { get; set; }
            public bool amueblado { get; set; }
            public byte[]? imagen { get; set; }
        }

        private readonly BindingList<Inmueble> _datos = new();
        private int _nextId = 1;
        private int? _editandoId = null;

        public UcInmuebles()
        {
            InitializeComponent();

            // Engancho eventos SOLO en runtime
            if (!IsDesigner())
            {
                Load += UcInmuebles_Load;
                btnGuardar.Click += BtnGuardar_Click;
                btnCancelar.Click += BtnCancelar_Click;
                btnCargarImagen.Click += BtnCargarImagen_Click;
                btnQuitarImagen.Click += BtnQuitarImagen_Click;
                dgvInmuebles.CellClick += DgvInmuebles_CellClick;
            }
        }

        private static bool IsDesigner()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                   System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv";
        }

        // ====== Init ======
        private void UcInmuebles_Load(object? sender, EventArgs e)
        {
            // Combos
            cboTipo.Items.Clear();
            cboTipo.Items.AddRange(new object[]
            {
                "Casa", "Departamento", "PH", "Local", "Galpón", "Oficina", "Terreno"
            });

            cboEstado.Items.Clear();
            cboEstado.Items.AddRange(new object[]
            {
                "Disponible","Reservado","Ocupado","Inactivo"
            });

            cboTipo.SelectedIndex = -1;
            cboEstado.SelectedIndex = 0;
            nudAmbientes.Value = 0;
            chkAmueblado.Checked = false;

            // Bind grid
            RefrescarGrid();
        }

        // ====== Botones ======
        private void BtnCargarImagen_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog()
            {
                Title = "Seleccionar imagen",
                Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using var img = Image.FromFile(ofd.FileName);
                    pbFoto.Image = new Bitmap(img);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo cargar la imagen.\n" + ex.Message, "Imagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnQuitarImagen_Click(object? sender, EventArgs e)
        {
            pbFoto.Image = null;
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (!ValidarFormulario(out string error))
            {
                MessageBox.Show(error, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var entidad = new Inmueble
            {
                direccion = txtDireccion.Text.Trim(),
                tipo = cboTipo.SelectedItem?.ToString() ?? "",
                descripcion = txtDescripcion.Text.Trim(),
                estado = cboEstado.SelectedItem?.ToString() ?? "Disponible",
                nro_ambientes = (int)nudAmbientes.Value,
                amueblado = chkAmueblado.Checked,
                imagen = ImageToBytes(pbFoto.Image)
            };

            if (_editandoId == null)
            {
                // Alta
                entidad.id_inmueble = _nextId++;
                _datos.Add(entidad);
            }
            else
            {
                // Update
                var existente = _datos.FirstOrDefault(x => x.id_inmueble == _editandoId.Value);
                if (existente != null)
                {
                    existente.direccion = entidad.direccion;
                    existente.tipo = entidad.tipo;
                    existente.descripcion = entidad.descripcion;
                    existente.estado = entidad.estado;
                    existente.nro_ambientes = entidad.nro_ambientes;
                    existente.amueblado = entidad.amueblado;
                    existente.imagen = entidad.imagen;
                }
            }

            RefrescarGrid();
            LimpiarFormulario();
        }

        // ====== Grid ======
        private void DgvInmuebles_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var id = (int)dgvInmuebles.Rows[e.RowIndex].Cells["id_inmueble"].Value;

            if (dgvInmuebles.Columns[e.ColumnIndex].Name == "editar")
            {
                Editar(id);
            }
            else if (dgvInmuebles.Columns[e.ColumnIndex].Name == "activar")
            {
                CambiarEstado(id);
            }
        }

        private void RefrescarGrid()
        {
            dgvInmuebles.Rows.Clear();
            foreach (var x in _datos)
            {
                dgvInmuebles.Rows.Add(
                    x.id_inmueble,
                    x.direccion,
                    x.tipo,
                    x.nro_ambientes,
                    x.amueblado,
                    x.estado,
                    BytesToThumb(x.imagen, 64, 48),
                    "Editar",
                    "Cambiar"
                );
            }
        }

        private void Editar(int id)
        {
            var x = _datos.FirstOrDefault(d => d.id_inmueble == id);
            if (x == null) return;

            _editandoId = id;
            lblTitulo.Text = "Inmuebles · Editando";
            btnGuardar.Text = "Actualizar";

            txtDireccion.Text = x.direccion;
            SelectItem(cboTipo, x.tipo);
            txtDescripcion.Text = x.descripcion;
            SelectItem(cboEstado, x.estado);
            nudAmbientes.Value = Math.Min(Math.Max(nudAmbientes.Minimum, x.nro_ambientes), nudAmbientes.Maximum);
            chkAmueblado.Checked = x.amueblado;
            pbFoto.Image = BytesToImage(x.imagen);
        }

        private void CambiarEstado(int id)
        {
            var x = _datos.FirstOrDefault(d => d.id_inmueble == id);
            if (x == null) return;

            x.estado = x.estado == "Inactivo" ? "Disponible" : "Inactivo";
            RefrescarGrid();
        }

        // ====== Helpers ======
        private void LimpiarFormulario()
        {
            _editandoId = null;
            lblTitulo.Text = "Inmuebles";
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

        private bool ValidarFormulario(out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                error = "La dirección es obligatoria.";

            else if (cboTipo.SelectedIndex < 0)
                error = "Seleccioná un tipo de inmueble.";

            else if (nudAmbientes.Value < 1)
                error = "El número de ambientes debe ser al menos 1.";

            else if (txtDescripcion.Text.Length > 300)
                error = "La descripción no puede superar los 300 caracteres.";

            return string.IsNullOrEmpty(error);
        }

        private static void SelectItem(ComboBox cbo, string value)
        {
            for (int i = 0; i < cbo.Items.Count; i++)
                if (string.Equals(cbo.Items[i]?.ToString(), value, StringComparison.InvariantCultureIgnoreCase))
                { cbo.SelectedIndex = i; return; }
            cbo.SelectedIndex = -1;
        }

        private static byte[]? ImageToBytes(Image? img)
        {
            if (img == null) return null;
            using var ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private static Image? BytesToImage(byte[]? bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;
            using var ms = new MemoryStream(bytes);
            return Image.FromStream(ms);
        }

        private static Image? BytesToThumb(byte[]? bytes, int w, int h)
        {
            var img = BytesToImage(bytes);
            if (img == null) return null;
            return new Bitmap(img, new Size(w, h));
        }

        private void dgvInmuebles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gbCrear_Enter(object sender, EventArgs e)
        {

        }
    }
}
