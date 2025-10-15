using InmoTech.Models;
using InmoTech.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace InmoTech
{

    public partial class UcUsuarios : UserControl
    {
        // ======================================================
        //  REGIÓN: Propiedades y Repositorios
        // ======================================================
        #region Propiedades y Repositorios
        private readonly BindingList<Usuario> _usuariosBindingList = new();
        private readonly UsuarioRepository _usuarioRepository = new();
        private Usuario? _usuarioEnEdicion;
        #endregion

        // ======================================================
        //  REGIÓN: Utilitarios de Texto y Cultura
        // ======================================================
        #region Utilitarios de Texto y Cultura
        private bool _estaNormalizandoTexto;
        private static readonly TextInfo _textoInfo = CultureInfo.GetCultureInfo("es-AR").TextInfo;
        #endregion

        // ======================================================
        //  REGIÓN: Constructor y Eventos de Inicialización
        // ======================================================
        #region Constructor y Eventos de Inicialización
        public UcUsuarios()
        {
            InitializeComponent();

            // Eventos principales
            Load += UcUsuarios_Load;
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += (_, __) => { LimpiarFormulario(); EstablecerModoAlta(); };

            // Se agrega el evento Click para el botón de Baja/Activar.
            BEstado.Click += BEstado_Click;

            // DataGrid: acciones y formato legible
            dgvUsuarios.CellDoubleClick += DataGridUsuarios_CellDoubleClick;
            dgvUsuarios.CellFormatting += DataGridUsuarios_CellFormatting;

            // Restricciones numéricas (tecleo y pegado) para DNI y Teléfono
            txtDni.KeyPress += TextBoxSoloDigitos_KeyPress;
            txtDni.TextChanged += TextBoxSoloDigitos_TextChanged;
            txtTelefono.KeyPress += TextBoxSoloDigitos_KeyPress;
            txtTelefono.TextChanged += TextBoxSoloDigitos_TextChanged;

            // Capitalización en vivo para Nombre y Apellido
            txtNombre.TextChanged += TextBoxCapitalizarEnVivo_TextChanged;
            txtApellido.TextChanged += TextBoxCapitalizarEnVivo_TextChanged;

            // Bloquea números en Nombre y Apellido al tipear
            txtNombre.KeyPress += TextBoxSoloLetras_KeyPress;
            txtApellido.KeyPress += TextBoxSoloLetras_KeyPress;

            AjustarInterfazUsuario();
        }

        private void UcUsuarios_Load(object? sender, EventArgs e)
        {
            if (DesignMode || (Site?.DesignMode ?? false)) return;

            ConfigurarGrillaUsuarios();
            dgvUsuarios.DataSource = _usuariosBindingList;

            rbOperador.Checked = true;
            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker1.Value = DateTime.Today.AddYears(-18);

            EstablecerModoAlta();
            CargarUsuariosDesdeBaseDeDatos();
        }
        #endregion

        // ======================================================
        //  REGIÓN: Guardar (Alta / Edición) y Bajas
        // ======================================================
        #region Guardar (Alta / Edición) y Bajas
        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            LimpiarErroresValidacion();
            if (!TryValidarFormulario(out var listaErrores))
            {
                MessageBox.Show(string.Join(Environment.NewLine, listaErrores), "Revisá los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var usuarioDesdeFormulario = MapearFormularioAUsuario();

            if (_usuarioEnEdicion == null)
            {
                // Alta
                if (_usuariosBindingList.Any(u => u.Dni == usuarioDesdeFormulario.Dni))
                {
                    ep.SetError(txtDni, "Ya existe un usuario con este DNI.");
                    MessageBox.Show("Ya existe un usuario con ese DNI.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    usuarioDesdeFormulario.Estado = true; // Siempre se crea como activo

                    int filasAfectadas = _usuarioRepository.AgregarUsuario(usuarioDesdeFormulario);
                    if (filasAfectadas == 1)
                    {
                        _usuariosBindingList.Add(usuarioDesdeFormulario);
                        MessageBox.Show("Usuario registrado correctamente.", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();
                        EstablecerModoAlta();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo insertar el usuario.", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException ex) when (ex.Number is 2627 or 2601)
                {
                    MessageBox.Show("Ya existe un usuario con ese DNI o Email.", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ep.SetError(txtDni, "Duplicado.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar el usuario:\n{ex.Message}", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Edición
                usuarioDesdeFormulario.Estado = _usuarioEnEdicion.Estado;

                bool debeActualizarPassword = !string.IsNullOrWhiteSpace(txtPass.Text);
                if (!debeActualizarPassword) usuarioDesdeFormulario.Password = _usuarioEnEdicion.Password;

                try
                {
                    int filasAfectadas = _usuarioRepository.ActualizarUsuario(usuarioDesdeFormulario, debeActualizarPassword);
                    if (filasAfectadas == 1)
                    {
                        _usuarioEnEdicion.Nombre = usuarioDesdeFormulario.Nombre;
                        _usuarioEnEdicion.Apellido = usuarioDesdeFormulario.Apellido;
                        _usuarioEnEdicion.Telefono = usuarioDesdeFormulario.Telefono;
                        _usuarioEnEdicion.Email = usuarioDesdeFormulario.Email;
                        _usuarioEnEdicion.IdRol = usuarioDesdeFormulario.IdRol;
                        _usuarioEnEdicion.Estado = usuarioDesdeFormulario.Estado;
                        _usuarioEnEdicion.FechaNacimiento = usuarioDesdeFormulario.FechaNacimiento;
                        if (debeActualizarPassword) _usuarioEnEdicion.Password = usuarioDesdeFormulario.Password;

                        int indiceUsuario = _usuariosBindingList.IndexOf(_usuarioEnEdicion);
                        if (indiceUsuario >= 0) _usuariosBindingList.ResetItem(indiceUsuario);

                        MessageBox.Show("Usuario actualizado correctamente.", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();
                        EstablecerModoAlta();
                    }
                    else
                    {
                        MessageBox.Show("No se actualizó el usuario (verificá el DNI).", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException ex) when (ex.Number is 2627 or 2601)
                {
                    MessageBox.Show("Ya existe un usuario con ese Email o DNI.", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ep.SetError(txtEmail, "Email duplicado.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar:\n{ex.Message}", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BEstado_Click(object sender, EventArgs e)
        {
            if (_usuarioEnEdicion == null) return;

            bool nuevoEstado = !_usuarioEnEdicion.Estado;
            string accion = nuevoEstado ? "activar" : "dar de baja";
            string accionPasado = nuevoEstado ? "activado" : "dado de baja";

            var confirmar = MessageBox.Show(
              $"¿Seguro que querés {accion} a {_usuarioEnEdicion.NombreCompleto} (DNI {_usuarioEnEdicion.Dni})?",
              $"Confirmar {accion}",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2);

            if (confirmar != DialogResult.Yes) return;

            try
            {
                int filasAfectadas = _usuarioRepository.ActualizarEstado(_usuarioEnEdicion.Dni, nuevoEstado);
                if (filasAfectadas == 1)
                {
                    _usuarioEnEdicion.Estado = nuevoEstado;

                    int indice = _usuariosBindingList.IndexOf(_usuarioEnEdicion);
                    if (indice >= 0) _usuariosBindingList.ResetItem(indice);

                    MessageBox.Show($"Usuario {accionPasado} correctamente.", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                    EstablecerModoAlta();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el estado del usuario.", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar estado:\n{ex.Message}", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Grilla: Acciones y Formato
        // ======================================================
        #region Grilla: Acciones y Formato
        private void DataGridUsuarios_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var usuarioSeleccionado = ObtenerUsuarioDeFila(e.RowIndex);
            if (usuarioSeleccionado != null) CargarFormularioParaEditar(usuarioSeleccionado);
        }

        private void DataGridUsuarios_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string nombreColumna = dgvUsuarios.Columns[e.ColumnIndex].Name;

            if (nombreColumna == "colEstado" && e.Value is bool estado)
            {
                e.Value = estado ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
            else if (nombreColumna == "colRol" && e.Value is int idRol)
            {
                e.Value = ObtenerNombreRol(idRol);
                e.FormattingApplied = true;
            }
        }

        private static string ObtenerNombreRol(int idRol) =>
          idRol switch
          {
              1 => "Administrador",
              2 => "Operador",
              3 => "Propietario",
              _ => $"Rol {idRol}"
          };
        #endregion

        // ======================================================
        //  REGIÓN: Mapeo, Validación y Flujo de Formulario
        // ======================================================
        #region Mapeo, Validación y Flujo de Formulario
        private Usuario? ObtenerUsuarioDeFila(int indiceFila) =>
      (indiceFila >= 0 && indiceFila < dgvUsuarios.Rows.Count)
        ? dgvUsuarios.Rows[indiceFila].DataBoundItem as Usuario
        : null;

        private void CargarFormularioParaEditar(Usuario usuario)
        {
            _usuarioEnEdicion = usuario;

            txtNombre.Text = usuario.Nombre;
            txtApellido.Text = usuario.Apellido;
            txtDni.Text = usuario.Dni.ToString();
            txtDni.Enabled = false;
            txtTelefono.Text = usuario.Telefono;
            txtEmail.Text = usuario.Email;

            rbAdministrador.Checked = usuario.IdRol == 1;
            rbOperador.Checked = usuario.IdRol == 2;
            rbPropietario.Checked = usuario.IdRol == 3;

            dateTimePicker1.Value = usuario.FechaNacimiento == default ? DateTime.Today.AddYears(-18) : usuario.FechaNacimiento;

            txtPass.Clear();
            txtPass2.Clear();

            btnGuardar.Text = "Actualizar";
            btnCancelar.Text = "Cancelar edición";
            lblListaTitulo.Text = $"Editando: {usuario.Nombre} {usuario.Apellido}";

            BEstado.Enabled = true;
            BEstado.Text = usuario.Estado ? "Baja" : "Activar";

            txtNombre.Focus();
        }

        private Usuario MapearFormularioAUsuario() => new()
        {
            Dni = int.Parse(txtDni.Text.Trim()),
            Nombre = txtNombre.Text.Trim(),
            Apellido = txtApellido.Text.Trim(),
            Telefono = txtTelefono.Text.Trim(),
            Email = txtEmail.Text.Trim(),
            Password = txtPass.Text.Trim(), // ideal: hash
            IdRol = rbAdministrador.Checked ? 1 : rbPropietario.Checked ? 3 : 2,
            FechaNacimiento = dateTimePicker1.Value.Date
        };

        private bool TryValidarFormulario(out List<string> erroresValidacion)
        {
            erroresValidacion = new();

            var expresionNombreRegex = new Regex(@"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ\s'\-]{2,}$");
            if (!expresionNombreRegex.IsMatch(txtNombre.Text.Trim()))
            {
                erroresValidacion.Add("Nombre inválido (mín. 2 letras, sin números).");
                ep.SetError(txtNombre, "Sólo letras y espacios (mín. 2).");
            }
            if (!expresionNombreRegex.IsMatch(txtApellido.Text.Trim()))
            {
                erroresValidacion.Add("Apellido inválido (mín. 2 letras, sin números).");
                ep.SetError(txtApellido, "Sólo letras y espacios (mín. 2).");
            }

            if (!int.TryParse(txtDni.Text.Trim(), out int dniParseado) || dniParseado <= 0)
            {
                erroresValidacion.Add("DNI inválido.");
                ep.SetError(txtDni, "Ingresá un DNI válido.");
            }

            string soloDigitosTelefono = new string(txtTelefono.Text.Where(char.IsDigit).ToArray());
            if (soloDigitosTelefono.Length < 6)
            {
                erroresValidacion.Add("Teléfono inválido (muy corto).");
                ep.SetError(txtTelefono, "Mínimo 6 dígitos.");
            }

            // ===== Email: obligatorio + formato =====
            string emailNormalizado = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(emailNormalizado))
            {
                erroresValidacion.Add("El email es obligatorio.");
                ep.SetError(txtEmail, "Ingresá un email.");
            }
            else
            {
                var expresionEmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!expresionEmailRegex.IsMatch(emailNormalizado))
                {
                    erroresValidacion.Add("Email inválido.");
                    ep.SetError(txtEmail, "Formato no válido.");
                }
            }

            bool debeExigirPassword = _usuarioEnEdicion == null || !string.IsNullOrEmpty(txtPass.Text) || !string.IsNullOrEmpty(txtPass2.Text);
            if (debeExigirPassword)
            {
                if (txtPass.Text.Length < 6)
                {
                    erroresValidacion.Add("La contraseña debe tener al menos 6 caracteres.");
                    ep.SetError(txtPass, "Mínimo 6 caracteres.");
                }
                if (txtPass.Text != txtPass2.Text)
                {
                    erroresValidacion.Add("Las contraseñas no coinciden.");
                    ep.SetError(txtPass2, "Debe coincidir.");
                }
            }

            DateTime fechaHoy = DateTime.Today;
            DateTime fechaNacimiento = dateTimePicker1.Value.Date;
            if (fechaNacimiento > fechaHoy)
            {
                erroresValidacion.Add("La fecha de nacimiento no puede ser futura.");
                ep.SetError(dateTimePicker1, "No puede ser futura.");
            }

            int edadCalculada = fechaHoy.Year - fechaNacimiento.Year - (fechaHoy < fechaNacimiento.AddYears(fechaHoy.Year - fechaNacimiento.Year) ? 1 : 0);
            if (edadCalculada is < 16 or > 100)
            {
                erroresValidacion.Add("Edad fuera de rango (16–100).");
                ep.SetError(dateTimePicker1, "Permitida: 16–100.");
            }

            return erroresValidacion.Count == 0;
        }

        private void EstablecerModoAlta()
        {
            _usuarioEnEdicion = null;
            txtDni.Enabled = true;
            btnGuardar.Text = "Guardar";
            btnCancelar.Text = "Cancelar";
            lblListaTitulo.Text = "Lista de usuarios";

            BEstado.Enabled = false;
            BEstado.Text = "Baja";
        }

        private void LimpiarFormulario()
        {
            LimpiarErroresValidacion();
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

        private void LimpiarErroresValidacion()
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

        private void CargarUsuariosDesdeBaseDeDatos()
        {
            try
            {
                var usuariosDesdeBase = _usuarioRepository.ObtenerUsuarios();
                _usuariosBindingList.RaiseListChangedEvents = false;
                _usuariosBindingList.Clear();
                foreach (var usuario in usuariosDesdeBase) _usuariosBindingList.Add(usuario);
                _usuariosBindingList.RaiseListChangedEvents = true;
                _usuariosBindingList.ResetBindings();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios:\n{ex.Message}", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Configuración de la Interfaz y Controles
        // ======================================================
        #region Configuración de la Interfaz y Controles
        private void ConfigurarGrillaUsuarios()
        {
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear();

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDni", HeaderText = "DNI", DataPropertyName = nameof(Usuario.Dni), Width = 100 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", HeaderText = "Nombre", DataPropertyName = nameof(Usuario.Nombre), Width = 140 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colApellido", HeaderText = "Apellido", DataPropertyName = nameof(Usuario.Apellido), Width = 140 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTelefono", HeaderText = "Teléfono", DataPropertyName = nameof(Usuario.Telefono), Width = 120 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEmail", HeaderText = "Email", DataPropertyName = nameof(Usuario.Email), Width = 200 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRol", HeaderText = "Rol", DataPropertyName = nameof(Usuario.IdRol), Width = 110 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colFechaNac",
                HeaderText = "Fecha Nac.",
                DataPropertyName = nameof(Usuario.FechaNacimiento),
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" },
                Width = 110
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado", HeaderText = "Estado", DataPropertyName = nameof(Usuario.Estado), Width = 100 });
        }

        private void AjustarInterfazUsuario()
        {
            var contenedorFormulario = Controls.Find("gbCrear", true).FirstOrDefault() as Control;
            if (contenedorFormulario != null)
                contenedorFormulario.Margin = new Padding(contenedorFormulario.Margin.Left, Math.Max(contenedorFormulario.Margin.Top, 14), contenedorFormulario.Margin.Right, contenedorFormulario.Margin.Bottom);

            var panelEncabezado = Controls.Find("pnlHeader", true).FirstOrDefault() as Control;
            if (panelEncabezado != null)
                panelEncabezado.Padding = new Padding(panelEncabezado.Padding.Left, panelEncabezado.Padding.Top, panelEncabezado.Padding.Right, Math.Max(panelEncabezado.Padding.Bottom, 10));

            var tableLayoutRaiz = Controls.Find("tlRoot", true).FirstOrDefault() as TableLayoutPanel;
            if (tableLayoutRaiz != null && tableLayoutRaiz.Padding.Top < 12)
                tableLayoutRaiz.Padding = new Padding(tableLayoutRaiz.Padding.Left, 12, tableLayoutRaiz.Padding.Right, tableLayoutRaiz.Padding.Bottom);
        }
        #endregion

        // ======================================================
        //  REGIÓN: Helpers: Restricciones y Capitalización de Input
        // ======================================================
        #region Helpers: Restricciones y Capitalización de Input
        private void TextBoxSoloDigitos_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void TextBoxSoloDigitos_TextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox textBox) return;

            string textoOriginal = textBox.Text;
            int posicionCaretOriginal = textBox.SelectionStart;
            int cantidadDigitosAntes = textoOriginal.Take(posicionCaretOriginal).Count(char.IsDigit);

            string soloDigitos = new string(textoOriginal.Where(char.IsDigit).ToArray());
            if (soloDigitos == textoOriginal) return;

            textBox.Text = soloDigitos;
            textBox.SelectionStart = Math.Min(cantidadDigitosAntes, textBox.Text.Length);
        }

        private void TextBoxCapitalizarEnVivo_TextChanged(object? sender, EventArgs e)
        {
            if (_estaNormalizandoTexto) return;
            if (sender is not TextBox textBox) return;

            string textoOriginal = textBox.Text;
            int posicionCaret = textBox.SelectionStart;

            string textoNormalizado = _textoInfo.ToTitleCase(textoOriginal.ToLower());
            if (textoNormalizado == textoOriginal) return;

            _estaNormalizandoTexto = true;
            textBox.Text = textoNormalizado;
            textBox.SelectionStart = Math.Min(posicionCaret, textBox.Text.Length);
            _estaNormalizandoTexto = false;
        }

        private void TextBoxSoloLetras_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            char c = e.KeyChar;
            bool esLetra = char.IsLetter(c);         // Incluye letras con acentos/ñ
            bool permitido = esLetra || char.IsWhiteSpace(c) || c == '\'' || c == '-';

            if (!permitido)
                e.Handled = true;
        }
        #endregion

        // ======================================================
        //  REGIÓN: Stubs del Diseñador (sin esto revienta)
        // ======================================================
        #region Stubs del Diseñador
        private void gbCrear_Enter(object sender, EventArgs e) { /* no-op */ }

        private void label1_Click(object sender, EventArgs e) { /* no-op */ }
        #endregion
    }
}