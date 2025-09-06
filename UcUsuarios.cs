using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class UcUsuarios : UserControl
    {
        private readonly BindingList<Usuario> _usuarios = new();
        private Usuario? _enEdicion;

        public UcUsuarios()
        {
            InitializeComponent();

            AgregarEspacioEntreHeaderYFormulario();

            if (!IsDesigner())
            {
                btnGuardar.Click += BtnGuardar_Click;
                btnCancelar.Click += BtnCancelar_Click;
                this.Load += UcUsuarios_Load;

                dgvUsuarios.CellContentClick += OnGridCellContentClick;
                dgvUsuarios.CellDoubleClick += OnGridCellDoubleClick;
            }
        }

        private static bool IsDesigner()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                   || Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase);
        }

        // ========================== Load ==========================
        private void UcUsuarios_Load(object? sender, EventArgs e)
        {
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear();

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "id_cliente", DataPropertyName = nameof(Usuario.Id) });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", HeaderText = "Nombre", DataPropertyName = nameof(Usuario.Nombre) });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colApellido", HeaderText = "Apellido", DataPropertyName = nameof(Usuario.Apellido) });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDni", HeaderText = "DNI", DataPropertyName = nameof(Usuario.Dni) });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTelefono", HeaderText = "Teléfono", DataPropertyName = nameof(Usuario.Telefono) });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEmail", HeaderText = "Email", DataPropertyName = nameof(Usuario.Email) });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRol", HeaderText = "Rol", DataPropertyName = nameof(Usuario.Rol) });

            // Nueva columna: Fecha Nacimiento
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colFechaNac",
                HeaderText = "Fecha Nacimiento",
                DataPropertyName = nameof(Usuario.FechaNacimiento),
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado", HeaderText = "Estado", DataPropertyName = nameof(Usuario.Estado) });

            // Botón de Editar
            dgvUsuarios.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colEditar",
                HeaderText = "Editar",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                Width = 70
            });

            // Botón de Activar/Inactivar
            dgvUsuarios.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colToggle",
                HeaderText = "Activar/Inactivar",
                Text = "Cambiar",
                UseColumnTextForButtonValue = true,
                Width = 120
            });

            dgvUsuarios.DataSource = _usuarios;

            rbOperador.Checked = true;
            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker1.Value = DateTime.Today.AddYears(-18);
            ModoAlta();
        }

        // ========================== Guardar / Cancelar ==========================
        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            LimpiarErrores();
            if (!TryValidar(out var errores))
            {
                MessageBox.Show(string.Join(Environment.NewLine, errores), "Revisá los datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dniNuevo = txtDni.Text.Trim();
            string emailNuevo = txtEmail.Text.Trim();

            bool dniDup = _usuarios.Any(u => u.Dni == dniNuevo && !_EsMismo(u));
            if (dniDup)
            {
                ep.SetError(txtDni, "Ya existe un usuario con este DNI.");
                MessageBox.Show("Ya existe un usuario con ese DNI.", "Duplicado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(emailNuevo))
            {
                bool mailDup = _usuarios.Any(u => u.Email.Equals(emailNuevo, StringComparison.OrdinalIgnoreCase) && !_EsMismo(u));
                if (mailDup)
                {
                    ep.SetError(txtEmail, "Ya existe un usuario con este Email.");
                    MessageBox.Show("Ya existe un usuario con ese Email.", "Duplicado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (_enEdicion == null)
            {
                var nuevo = new Usuario
                {
                    Id = _usuarios.Count == 0 ? 1 : _usuarios.Max(u => u.Id) + 1,
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Dni = dniNuevo,
                    Telefono = txtTelefono.Text.Trim(),
                    Email = emailNuevo,
                    Rol = RolSeleccionado(),
                    Estado = "activo",
                    FechaNacimiento = dateTimePicker1.Value.Date
                };
                _usuarios.Add(nuevo);
            }
            else
            {
                _enEdicion.Nombre = txtNombre.Text.Trim();
                _enEdicion.Apellido = txtApellido.Text.Trim();
                _enEdicion.Dni = dniNuevo;
                _enEdicion.Telefono = txtTelefono.Text.Trim();
                _enEdicion.Email = emailNuevo;
                _enEdicion.Rol = RolSeleccionado();
                _enEdicion.FechaNacimiento = dateTimePicker1.Value.Date;

                var idx = _usuarios.IndexOf(_enEdicion);
                if (idx >= 0) _usuarios.ResetItem(idx);
            }

            ModoAlta();
            LimpiarFormulario();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            ModoAlta();
            LimpiarFormulario();
        }

        private bool _EsMismo(Usuario u) => _enEdicion != null && u.Id == _enEdicion.Id;

        private void ModoAlta()
        {
            _enEdicion = null;
            btnGuardar.Text = "Guardar";
            btnCancelar.Text = "Cancelar";
            lblListaTitulo.Text = "Lista Usuarios";
        }

        private void ModoEdicion(Usuario u)
        {
            _enEdicion = u;
            btnGuardar.Text = "Actualizar";
            btnCancelar.Text = "Cancelar edición";
            lblListaTitulo.Text = $"Editando: {u.Nombre} {u.Apellido}";
        }

        private void CargarParaEditar(Usuario u)
        {
            txtNombre.Text = u.Nombre;
            txtApellido.Text = u.Apellido;
            txtDni.Text = u.Dni;
            txtTelefono.Text = u.Telefono;
            txtEmail.Text = u.Email;

            rbOperador.Checked = u.Rol == "operador";
            rbAdministrador.Checked = u.Rol == "administrador";
            rbPropietario.Checked = u.Rol == "propietario";

            dateTimePicker1.Value = u.FechaNacimiento == default
                ? DateTime.Today.AddYears(-18)
                : u.FechaNacimiento;

            txtPass.Clear();
            txtPass2.Clear();

            ModoEdicion(u);
            txtNombre.Focus();
        }

        // ========================== Acciones grilla ==========================
        private void OnGridCellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var u = FilaUsuario(e.RowIndex);
            if (u != null) CargarParaEditar(u);
        }

        private void OnGridCellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var col = dgvUsuarios.Columns[e.ColumnIndex].Name;
            var u = FilaUsuario(e.RowIndex);
            if (u == null) return;

            switch (col)
            {
                case "colEditar":
                    CargarParaEditar(u);
                    break;
                case "colToggle":
                    u.Estado = (u.Estado?.Equals("activo", StringComparison.OrdinalIgnoreCase) ?? false) ? "inactivo" : "activo";
                    _usuarios.ResetItem(e.RowIndex);
                    break;
            }
        }

        private Usuario? FilaUsuario(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvUsuarios.Rows.Count) return null;
            return dgvUsuarios.Rows[rowIndex].DataBoundItem as Usuario;
        }

        // ========================== Helpers ==========================
        private void LimpiarFormulario()
        {
            LimpiarErrores();
            txtNombre.Clear();
            txtApellido.Clear();
            txtDni.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtPass.Clear();
            txtPass2.Clear();
            rbOperador.Checked = true;
            dateTimePicker1.Value = DateTime.Today.AddYears(-18);
            txtNombre.Focus();
        }

        private void LimpiarErrores()
        {
            ep.SetError(txtNombre, string.Empty);
            ep.SetError(txtApellido, string.Empty);
            ep.SetError(txtDni, string.Empty);
            ep.SetError(txtTelefono, string.Empty);
            ep.SetError(txtEmail, string.Empty);
            ep.SetError(txtPass, string.Empty);
            ep.SetError(txtPass2, string.Empty);
            ep.SetError(dateTimePicker1, string.Empty);
        }

        private string RolSeleccionado()
        {
            if (rbAdministrador.Checked) return "administrador";
            if (rbPropietario.Checked) return "propietario";
            return "operador";
        }

        private bool TryValidar(out List<string> errores)
        {
            errores = new List<string>();
            var rxNombre = new Regex(@"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ\s'\-]{2,}$");

            if (!rxNombre.IsMatch(txtNombre.Text.Trim()))
            {
                errores.Add("Nombre inválido (mín. 2 letras, sin números).");
                ep.SetError(txtNombre, "Usá sólo letras y espacios (mín. 2).");
            }
            if (!rxNombre.IsMatch(txtApellido.Text.Trim()))
            {
                errores.Add("Apellido inválido (mín. 2 letras, sin números).");
                ep.SetError(txtApellido, "Usá sólo letras y espacios (mín. 2).");
            }

            var dni = txtDni.Text.Trim();
            if (dni.Length < 7 || dni.Length > 10 || !dni.All(char.IsDigit))
            {
                errores.Add("DNI inválido (7 a 10 dígitos).");
                ep.SetError(txtDni, "Ingresá sólo dígitos (7–10).");
            }

            var tel = txtTelefono.Text.Trim();
            var telLimpio = new string(tel.Where(char.IsDigit).ToArray());
            if (telLimpio.Length < 6)
            {
                errores.Add("Teléfono inválido (muy corto).");
                ep.SetError(txtTelefono, "Mínimo 6 dígitos.");
            }

            var email = txtEmail.Text.Trim();
            if (!string.IsNullOrWhiteSpace(email))
            {
                var rxMail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!rxMail.IsMatch(email))
                {
                    errores.Add("Email inválido.");
                    ep.SetError(txtEmail, "Formato de email no válido.");
                }
            }

            bool validarPass = _enEdicion == null || !string.IsNullOrEmpty(txtPass.Text) || !string.IsNullOrEmpty(txtPass2.Text);
            if (validarPass)
            {
                if (txtPass.Text.Length < 6)
                {
                    errores.Add("La contraseña debe tener al menos 6 caracteres.");
                    ep.SetError(txtPass, "Mínimo 6 caracteres.");
                }
                if (txtPass.Text != txtPass2.Text)
                {
                    errores.Add("Las contraseñas no coinciden.");
                    ep.SetError(txtPass2, "Debe coincidir con la contraseña.");
                }
            }

            var hoy = DateTime.Today;
            var fn = dateTimePicker1.Value.Date;
            if (fn > hoy)
            {
                errores.Add("La fecha de nacimiento no puede ser futura.");
                ep.SetError(dateTimePicker1, "No puede ser futura.");
            }
            var edad = hoy.Year - fn.Year - (hoy < fn.AddYears(hoy.Year - fn.Year) ? 1 : 0);
            if (edad < 16 || edad > 100)
            {
                errores.Add("Edad fuera de rango (16–100).");
                ep.SetError(dateTimePicker1, "Edad permitida: 16–100.");
            }

            return errores.Count == 0;
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void gbCrear_Enter(object sender, EventArgs e)
        {

        }

        private void AgregarEspacioEntreHeaderYFormulario()
        {
            // Si tu GroupBox del formulario se llama "gbCrear", le subimos el margen superior.
            var formBox = this.Controls.Find("gbCrear", true).FirstOrDefault() as Control;
            if (formBox != null)
            {
                var m = formBox.Margin;
                formBox.Margin = new Padding(m.Left, Math.Max(m.Top, 14), m.Right, m.Bottom);
            }

            // Si tu panel de cabecera se llama "pnlHeader", le damos padding inferior para separar.
            var header = this.Controls.Find("pnlHeader", true).FirstOrDefault() as Control;
            if (header != null)
            {
                var p = header.Padding;
                header.Padding = new Padding(p.Left, p.Top, p.Right, Math.Max(p.Bottom, 10));
            }

            // Si usás un TableLayoutPanel raíz llamado "tlRoot", aseguramos padding general.
            var tl = this.Controls.Find("tlRoot", true).FirstOrDefault() as TableLayoutPanel;
            if (tl != null && tl.Padding.Top < 12)
            {
                tl.Padding = new Padding(tl.Padding.Left, 12, tl.Padding.Right, tl.Padding.Bottom);
            }
        }

    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Dni { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Email { get; set; } = "";
        public string Rol { get; set; } = "";
        public string Estado { get; set; } = "activo";
        public DateTime FechaNacimiento { get; set; }
    }



}
