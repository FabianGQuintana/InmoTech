using InmoTech.Models;
using InmoTech.Repositories;
using InmoTech.Security; // <--- AGREGADO
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcInquilinos : UserControl
    {
        // ======================================================
        //  REGIÓN: Privados y Repositorios
        // ======================================================
        #region Campos Privados y Repositorios
        private readonly BindingList<Inquilino> _binding = new();
        private readonly InquilinoRepository _repo = new();
        private readonly ContratoRepository _repoContrato = new(); // <<< NUEVO
        private Inquilino? _enEdicion;

        // ErrorProvider para validaciones visuales
        private readonly ErrorProvider ep = new ErrorProvider();

        // Normalización
        private bool _normalizando;
        private static readonly TextInfo _ti = CultureInfo.GetCultureInfo("es-AR").TextInfo;

        // >>> NUEVO: guardia para evitar doble ejecución de Guardar
        private bool _guardando = false;

        // >>> NUEVO: guardia de reentrancia para el toggle del grid
        private bool _toggleEnCurso = false;
        #endregion

        // ======================================================
        //  REGIÓN: Constructor y Inicialización
        // ======================================================
        #region Constructor y Inicialización
        public UcInquilinos()
        {
            InitializeComponent();

            // Config ErrorProvider
            ep.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            ep.ContainerControl = this;

            // Suaviza el scroll del grid
            HabilitarDoubleBuffer(dataGridInquilinos);

            // Eventos principales
            Load += UcInquilinos_Load;
            Resize += (_, __) => AjustarLayout();

            // DataGrid (asegurar 1 sola suscripción)
            dataGridInquilinos.CellFormatting -= dataGridInquilinos_CellFormatting;
            dataGridInquilinos.CellFormatting += dataGridInquilinos_CellFormatting;

            dataGridInquilinos.CellDoubleClick -= dataGridInquilinos_CellDoubleClick;
            dataGridInquilinos.CellDoubleClick += dataGridInquilinos_CellDoubleClick;

            dataGridInquilinos.CellContentClick -= dataGridInquilinos_CellContentClick;
            dataGridInquilinos.CellContentClick += dataGridInquilinos_CellContentClick;

            // >>> CAMBIO MINIMO: limpiar y suscribir SOLO Guardar para evitar doble ejecución
            btnGuardar.Click -= BtnGuardar_Click;
            btnGuardar.Click += BtnGuardar_Click;

            // Cancelar queda como estaba
            btnCancelar.Click += BtnCancelar_Click;

            // Validaciones de entrada
            txtDni.KeyPress += SoloDigitos_KeyPress;
            txtDni.TextChanged += SoloDigitos_TextChanged;

            txtTelefono.KeyPress += SoloDigitosPermisivos_KeyPress; // permite + - espacio además de dígitos
            txtTelefono.TextChanged += SoloTelefonos_TextChanged;

            txtNombre.TextChanged += TitleCase_TextChanged;
            txtApellido.TextChanged += TitleCase_TextChanged;
            txtNombre.KeyPress += SoloLetras_KeyPress;
            txtApellido.KeyPress += SoloLetras_KeyPress;
        }

        private void UcInquilinos_Load(object? sender, EventArgs e)
        {
            if (DesignMode || (Site?.DesignMode ?? false)) return;

            // Dirección nunca debe tener password char
            txtDireccion.UseSystemPasswordChar = false;

            ConfigurarGrilla();
            dataGridInquilinos.DataSource = _binding;

            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker1.Value = DateTime.Today.AddYears(-21);

            EstablecerModoAlta();
            CargarDesdeBase();
            AjustarLayout();
        }
        #endregion

        // ======================================================
        //  REGIÓN: Persistencia de Datos (Guardar / Cargar)
        // ======================================================
        #region Persistencia de Datos (Guardar / Cargar)
        // ====================== Guardar (Alta / Edición) ======================
        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            // >>> NUEVO: guardia y UX
            if (_guardando) return;
            _guardando = true;
            btnGuardar.Enabled = false;

            try
            {
                LimpiarErrores();
                if (!TryValidarFormulario(out var errores))
                {
                    MessageBox.Show(string.Join(Environment.NewLine, errores), "Revisá los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var entidad = MapearFormulario();

                if (_enEdicion == null)
                {
                    // Alta — duplicados locales y por SQL unique
                    if (_binding.Any(x => x.Dni == entidad.Dni))
                    {
                        ep.SetError(txtDni, "DNI duplicado.");
                        MessageBox.Show("Ya existe un inquilino con ese DNI.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        // Sesión
                        if (AuthService.CurrentUser == null)
                        {
                            MessageBox.Show("Error de sesión. No se puede identificar al usuario creador. Inicie sesión de nuevo.", "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        int dniCreador = AuthService.CurrentUser.Dni;
                        entidad.Estado = true;

                        int filas = _repo.AgregarInquilino(entidad, dniCreador);

                        if (filas == 1)
                        {
                            CargarDesdeBase(); // Recarga para tener IDs y datos de auditoría
                            MessageBox.Show("Inquilino registrado correctamente.", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarFormulario();
                            EstablecerModoAlta();
                            return; // <<< salimos acá; nada más que hacer
                        }
                        else
                        {
                            MessageBox.Show("No se pudo insertar el inquilino.", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (SqlException ex) when (ex.Number is 2627 or 2601)
                    {
                        // UNIQUE (DNI/Email)
                        MessageBox.Show("Ya existe un inquilino con ese DNI o Email.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ep.SetError(txtDni, "Duplicado.");
                        return;
                    }
                }
                else
                {
                    // Edición (Sin cambios)
                    entidad.Estado = _enEdicion.Estado;

                    try
                    {
                        int filas = _repo.ActualizarInquilino(entidad);
                        if (filas == 1)
                        {
                            // Actualizar la instancia en el BindingList
                            _enEdicion.Nombre = entidad.Nombre;
                            _enEdicion.Apellido = entidad.Apellido;
                            _enEdicion.Telefono = entidad.Telefono;
                            _enEdicion.Email = entidad.Email;
                            _enEdicion.Direccion = entidad.Direccion;
                            _enEdicion.FechaNacimiento = entidad.FechaNacimiento;

                            int idx = _binding.IndexOf(_enEdicion);
                            if (idx >= 0) _binding.ResetItem(idx);

                            MessageBox.Show("Inquilino actualizado correctamente.", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarFormulario();
                            EstablecerModoAlta();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("No se actualizó el inquilino (verificá el DNI).", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (SqlException ex) when (ex.Number is 2627 or 2601)
                    {
                        MessageBox.Show("Ya existe un inquilino con ese Email.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ep.SetError(txtEmail, "Email duplicado.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar/actualizar:\n{ex.Message}", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _guardando = false;
                btnGuardar.Enabled = true;
            }
        }

        private void CargarDesdeBase()
        {
            try
            {
                var datos = _repo.ObtenerInquilinos();
                _binding.RaiseListChangedEvents = false;
                _binding.Clear();
                foreach (var x in datos) _binding.Add(x);
                _binding.RaiseListChangedEvents = true;
                _binding.ResetBindings();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar inquilinos:\n{ex.Message}", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Mapeo, Validación y Estado de UI
        // ======================================================
        #region Mapeo, Validación y Estado de UI
        // ====================== Mapeo / Validación / Estados ======================
        private Inquilino MapearFormulario() => new()
        {
            Dni = int.Parse(txtDni.Text.Trim()),
            Nombre = txtNombre.Text.Trim(),
            Apellido = txtApellido.Text.Trim(),
            Telefono = txtTelefono.Text.Trim(),
            Email = txtEmail.Text.Trim(),
            Direccion = txtDireccion.Text.Trim(),
            FechaNacimiento = dateTimePicker1.Value.Date
        };

        private void CargarParaEditar(Inquilino i)
        {
            _enEdicion = i;

            txtDni.Text = i.Dni.ToString();
            txtDni.Enabled = false;

            txtNombre.Text = i.Nombre;
            txtApellido.Text = i.Apellido;
            txtTelefono.Text = i.Telefono;
            txtEmail.Text = i.Email;
            txtDireccion.Text = i.Direccion;
            dateTimePicker1.Value = i.FechaNacimiento == default ? DateTime.Today.AddYears(-21) : i.FechaNacimiento;

            btnGuardar.Text = "Actualizar";
            btnCancelar.Text = "Cancelar edición";
            LListaInquilinos.Text = $"Editando: {i.NombreCompleto}";
            txtNombre.Focus();
        }

        private bool TryValidarFormulario(out List<string> errores)
        {
            errores = new();

            var rgxNombre = new Regex(@"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ\s'\-]{2,}$");
            if (!rgxNombre.IsMatch(txtNombre.Text.Trim()))
            {
                errores.Add("Nombre inválido (mín. 2 letras, sin números).");
                ep.SetError(txtNombre, "Sólo letras y espacios (mín. 2).");
            }
            if (!rgxNombre.IsMatch(txtApellido.Text.Trim()))
            {
                errores.Add("Apellido inválido (mín. 2 letras, sin números).");
                ep.SetError(txtApellido, "Sólo letras y espacios (mín. 2).");
            }

            if (!int.TryParse(txtDni.Text.Trim(), out int dni) || dni <= 0)
            {
                errores.Add("DNI inválido.");
                ep.SetError(txtDni, "Ingresá un DNI válido.");
            }

            string telDigits = new string(txtTelefono.Text.Where(char.IsDigit).ToArray());
            if (telDigits.Length < 6)
            {
                errores.Add("Teléfono inválido (muy corto).");
                ep.SetError(txtTelefono, "Mínimo 6 dígitos.");
            }

            string email = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                errores.Add("El email es obligatorio.");
                ep.SetError(txtEmail, "Ingresá un email.");
            }
            else
            {
                var rgxEmail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!rgxEmail.IsMatch(email))
                {
                    errores.Add("Email inválido.");
                    ep.SetError(txtEmail, "Formato no válido.");
                }
            }

            var hoy = DateTime.Today;
            var fn = dateTimePicker1.Value.Date;
            if (fn > hoy)
            {
                errores.Add("La fecha de nacimiento no puede ser futura.");
                ep.SetError(dateTimePicker1, "No puede ser futura.");
            }

            int edad = hoy.Year - fn.Year - (hoy < fn.AddYears(hoy.Year - fn.Year) ? 1 : 0);
            if (edad is < 18 or > 110)
            {
                errores.Add("Edad fuera de rango (18–110).");
                ep.SetError(dateTimePicker1, "Permitida: 18–110.");
            }

            return errores.Count == 0;
        }

        private void EstablecerModoAlta()
        {
            _enEdicion = null;
            txtDni.Enabled = true;
            btnGuardar.Text = "Guardar";
            btnCancelar.Text = "Cancelar";
            LListaInquilinos.Text = "Lista de inquilinos";
        }

        private void LimpiarFormulario()
        {
            LimpiarErrores();
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            dateTimePicker1.Value = DateTime.Today.AddYears(-21);
            txtNombre.Focus();
            dataGridInquilinos.ClearSelection();
        }

        private void LimpiarErrores()
        {
            ep.SetError(txtNombre, "");
            ep.SetError(txtApellido, "");
            ep.SetError(txtDni, "");
            ep.SetError(txtTelefono, "");
            ep.SetError(txtEmail, "");
            ep.SetError(dateTimePicker1, "");
            ep.SetError(txtDireccion, "");
        }
        #endregion

        // ======================================================
        //  REGIÓN: Grilla: Acciones y Formato
        // ======================================================
        #region Grilla: Acciones y Formato
        // ====================== Grilla: Acciones/Formato ======================
        private void dataGridInquilinos_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridInquilinos.Rows[e.RowIndex].DataBoundItem is Inquilino i)
                CargarParaEditar(i);
        }

        private void dataGridInquilinos_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            // Guardia de reentrancia: evita dobles ejecuciones
            if (_toggleEnCurso) return;
            _toggleEnCurso = true;

            try
            {
                if (e.RowIndex < 0) return;

                string name = dataGridInquilinos.Columns[e.ColumnIndex].Name;
                if (name == "colEditar")
                {
                    if (dataGridInquilinos.Rows[e.RowIndex].DataBoundItem is Inquilino i)
                        CargarParaEditar(i);
                }
                else if (name == "colToggle")
                {
                    if (dataGridInquilinos.Rows[e.RowIndex].DataBoundItem is not Inquilino i) return;
                    bool nuevo = !i.Estado;

                    // Si estamos por dar de baja, verificar contratos activos
                    if (i.Estado && !nuevo)
                    {
                        // 1) Confirmación de baja
                        var ok = MessageBox.Show(
                            $"¿Seguro que querés dar de baja a {i.NombreCompleto} (DNI {i.Dni})?",
                            "Confirmar baja",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (ok != DialogResult.Yes) return;

                        // 2) Buscar id_persona y validar contratos activos
                        int? idPersona = _repo.ObtenerIdPersonaPorDni(i.Dni);
                        if (idPersona == null)
                        {
                            MessageBox.Show("No se encontró el registro de persona asociado al DNI.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (_repoContrato.ExisteContratoActivoPorPersona(idPersona.Value))
                        {
                            MessageBox.Show(
                                "No podés dar de baja este inquilino porque tiene contratos ACTIVOS asociados.\n" +
                                "Anulá esos contratos y volvé a intentar.",
                                "Operación no permitida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    try
                    {
                        int filas = _repo.ActualizarEstado(i.Dni, nuevo);
                        if (filas == 1)
                        {
                            i.Estado = nuevo;
                            _binding.ResetItem(e.RowIndex);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo cambiar el estado.", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al actualizar estado:\n{ex.Message}", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            finally
            {
                _toggleEnCurso = false;
            }
        }

        private void dataGridInquilinos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridInquilinos.Columns[e.ColumnIndex].Name == "colEstado" && e.Value is bool est)
            {
                e.Value = est ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: UI / Grid / Helpers
        // ======================================================
        #region UI / Grid / Helpers
        // ====================== UI / Grid / Helpers ======================
        private void ConfigurarGrilla()
        {
            dataGridInquilinos.AutoGenerateColumns = false;
            dataGridInquilinos.Columns.Clear();

            dataGridInquilinos.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDni", HeaderText = "DNI", DataPropertyName = nameof(Inquilino.Dni), Width = 100 });
            dataGridInquilinos.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", HeaderText = "Nombre", DataPropertyName = nameof(Inquilino.Nombre), Width = 140 });
            dataGridInquilinos.Columns.Add(new DataGridViewTextBoxColumn { Name = "colApellido", HeaderText = "Apellido", DataPropertyName = nameof(Inquilino.Apellido), Width = 140 });
            dataGridInquilinos.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTelefono", HeaderText = "Teléfono", DataPropertyName = nameof(Inquilino.Telefono), Width = 120 });
            dataGridInquilinos.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEmail", HeaderText = "Email", DataPropertyName = nameof(Inquilino.Email), Width = 200 });
            dataGridInquilinos.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDireccion", HeaderText = "Dirección", DataPropertyName = nameof(Inquilino.Direccion), Width = 180 });
            dataGridInquilinos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colFechaNacimiento",
                HeaderText = "Fecha Nac.",
                DataPropertyName = nameof(Inquilino.FechaNacimiento),
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" },
                Width = 110
            });
            dataGridInquilinos.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado", HeaderText = "Estado", DataPropertyName = nameof(Inquilino.Estado), Width = 100 });
            dataGridInquilinos.Columns.Add(new DataGridViewButtonColumn { Name = "colEditar", HeaderText = "Editar", Text = "Editar", UseColumnTextForButtonValue = true, Width = 70 });
            dataGridInquilinos.Columns.Add(new DataGridViewButtonColumn { Name = "colToggle", HeaderText = "Activar/Inactivar", Text = "Cambiar", UseColumnTextForButtonValue = true, Width = 120 });
        }

        private void AjustarLayout()
        {
            int margen = 15;
            int margenInferior = 20;

            // Encabezado (si existen esos nombres en tu diseñador)
            var panelHeader = Controls.Find("panel1", true).FirstOrDefault();
            if (panelHeader != null)
            {
                panelHeader.Left = margen;
                panelHeader.Width = Width - (margen * 2);
            }

            var panelForm = Controls.Find("panel2", true).FirstOrDefault();
            if (panelForm != null)
            {
                panelForm.Left = margen;
                panelForm.Width = Width - (margen * 2);
            }

            var lblLista = Controls.Find("LListaInquilinos", true).FirstOrDefault();
            if (lblLista != null) lblLista.Left = margen;

            dataGridInquilinos.Left = margen;
            dataGridInquilinos.Width = Width - (margen * 2);

            int altoDisponible = Height - dataGridInquilinos.Top - margenInferior;
            if (altoDisponible < 120) altoDisponible = 120;
            dataGridInquilinos.Height = altoDisponible;
        }

        private static void HabilitarDoubleBuffer(DataGridView dgv)
        {
            try
            {
                dgv.GetType()
                   .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)!
                   .SetValue(dgv, true, null);
            }
            catch { /* no-op */ }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Normalización y Manejo de Input
        // ======================================================
        #region Normalización y Manejo de Input
        // --- Normalización/Inputs ---
        private void SoloDigitos_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }
        private void SoloDigitos_TextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox tb) return;
            string orig = tb.Text;
            int caret = tb.SelectionStart;
            int countBefore = orig.Take(caret).Count(char.IsDigit);
            string digits = new string(orig.Where(char.IsDigit).ToArray());
            if (digits == orig) return;
            tb.Text = digits;
            tb.SelectionStart = Math.Min(countBefore, tb.Text.Length);
        }

        private void SoloDigitosPermisivos_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            char c = e.KeyChar;
            bool ok = char.IsDigit(c) || c == '+' || c == '-' || char.IsWhiteSpace(c);
            if (!ok) e.Handled = true;
        }
        private void SoloTelefonos_TextChanged(object? sender, EventArgs e)
        {
            // Permite dígitos, +, -, espacios
            if (sender is not TextBox tb) return;
            string s = tb.Text;
            if (s.All(ch => char.IsDigit(ch) || ch == '+' || ch == '-' || char.IsWhiteSpace(ch))) return;

            int caret = tb.SelectionStart;
            string limpio = new string(s.Where(ch => char.IsDigit(ch) || ch == '+' || ch == '-' || char.IsWhiteSpace(ch)).ToArray());
            tb.Text = limpio;
            tb.SelectionStart = Math.Min(caret, tb.Text.Length);
        }

        private void TitleCase_TextChanged(object? sender, EventArgs e)
        {
            if (_normalizando || sender is not TextBox tb) return;
            string orig = tb.Text;
            int caret = tb.SelectionStart;
            string norm = _ti.ToTitleCase(orig.ToLower());
            if (norm == orig) return;
            _normalizando = true;
            tb.Text = norm;
            tb.SelectionStart = Math.Min(caret, tb.Text.Length);
            _normalizando = false;
        }

        private void SoloLetras_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            char c = e.KeyChar;
            bool ok = char.IsLetter(c) || char.IsWhiteSpace(c) || c == '\'' || c == '-';
            if (!ok) e.Handled = true;
        }
        #endregion

        // ======================================================
        //  REGIÓN: Handlers de Plantilla (Vacíos/Designer)
        // ======================================================
        #region Handlers de Plantilla (Vacíos/Designer)
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            _enEdicion = null;
            LimpiarFormulario();
            EstablecerModoAlta();
        }

        // Stub requerido por el diseñador (Click en el label del listado)
        private void label1_Click(object sender, EventArgs e) { /* no-op */ }
        #endregion
    }
}
