using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class UcUsuarios : UserControl
    {
        public UcUsuarios()
        {
            InitializeComponent();

            // Engancho eventos SOLO en runtime, así el diseñador no se rompe
            if (!IsDesigner())
            {
                btnGuardar.Click += BtnGuardar_Click;
                btnCancelar.Click += BtnCancelar_Click;
                this.Load += UcUsuarios_Load;
            }
        }

        private static bool IsDesigner()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                   || Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase);
        }

        private void UcUsuarios_Load(object? sender, EventArgs e)
        {
            // Creo columnas de la grilla sólo en runtime
            if (dgvUsuarios.Columns.Count == 0)
            {
                dgvUsuarios.Columns.Add("colId", "id_cliente");
                dgvUsuarios.Columns.Add("colNombre", "Nombre");
                dgvUsuarios.Columns.Add("colApellido", "Apellido");
                dgvUsuarios.Columns.Add("colDni", "DNI");
                dgvUsuarios.Columns.Add("colTelefono", "Teléfono");
                dgvUsuarios.Columns.Add("colEmail", "Email");
                dgvUsuarios.Columns.Add("colRol", "Rol");
                dgvUsuarios.Columns.Add("colEstado", "Estado");
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            // Validaciones mínimas
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingresá el Nombre.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Ingresá el Apellido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return;
            }
            if (txtPass.Text != txtPass2.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass2.Focus();
                return;
            }

            // Rol seleccionado
            var rol = rbOperador.Checked ? "operador"
                    : rbAdministrador.Checked ? "administrador"
                    : "propietario";

            // Estado por defecto (podés cambiarlo según tu lógica)
            var estado = "activo";

            // ID simple incremental
            var nextId = dgvUsuarios.Rows.Count + 1;

            // Agrego fila
            dgvUsuarios.Rows.Add(
                nextId,
                txtNombre.Text.Trim(),
                txtApellido.Text.Trim(),
                txtDni.Text.Trim(),
                txtTelefono.Text.Trim(),
                txtEmail.Text.Trim(),
                rol,
                estado
            );

            // Limpio formulario
            LimpiarFormulario();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDni.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtPass.Clear();
            txtPass2.Clear();
            rbOperador.Checked = true;
            txtNombre.Focus();
        }
    }
}
