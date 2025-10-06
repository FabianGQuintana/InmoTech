using InmoTech.Models;
using InmoTech.Repositories;
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
        private readonly BindingList<Inquilino> _binding = new();
        private readonly InquilinoRepository _repo = new();
        private Inquilino? _enEdicion;

        // ErrorProvider para validaciones visuales
        private readonly ErrorProvider ep = new ErrorProvider();

        // Normalización
        private bool _normalizando;
        private static readonly TextInfo _ti = CultureInfo.GetCultureInfo("es-AR").TextInfo;

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

            // DataGrid (solo los que el diseñador NO engancha)
            dataGridInquilinos.CellFormatting += dataGridInquilinos_CellFormatting;

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

        // ====================== Guardar (Alta / Edición) ======================
        private void BtnGuardar_Click(object? sender, EventArgs e)
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
                    entidad.Estado = true; // el INSERT lo deja en 1 igual
                    int filas = _repo.AgregarInquilino(entidad);
                    if (filas == 1)
                    {
                        _binding.Add(entidad);
                        MessageBox.Show("Inquilino registrado correctamente.", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();
                        EstablecerModoAlta();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo insertar el inquilino.", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException ex) when (ex.Number is 2627 or 2601)
                {
                    // UNIQUE (DNI/Email)
                    MessageBox.Show("Ya existe un inquilino con ese DNI o Email.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ep.SetError(txtDni, "Duplicado.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar:\n{ex.Message}", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Edición
                entidad.Estado = _enEdicion.Estado;

                try
                {
                    int filas = _repo.ActualizarInquilino(entidad);
                    if (filas == 1)
                    {
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
                    }
                    else
                    {
                        MessageBox.Show("No se actualizó el inquilino (verificá el DNI).", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException ex) when (ex.Number is 2627 or 2601)
                {
                    MessageBox.Show("Ya existe un inquilino con ese Email.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ep.SetError(txtEmail, "Email duplicado.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar:\n{ex.Message}", "Inquilinos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            _enEdicion = null;
            LimpiarFormulario();
            EstablecerModoAlta();
        }

        // ====================== Grilla: Acciones/Formato ======================
        private void dataGridInquilinos_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridInquilinos.Rows[e.RowIndex].DataBoundItem is Inquilino i)
                CargarParaEditar(i);
        }

        private void dataGridInquilinos_CellContentClick(object? sender, DataGridViewCellEventArgs e)
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

                if (i.Estado && !nuevo)
                {
                    var ok = MessageBox.Show(
                        $"¿Seguro que querés dar de baja a {i.NombreCompleto} (DNI {i.Dni})?",
                        "Confirmar baja",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (ok != DialogResult.Yes) return;
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

        private void dataGridInquilinos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridInquilinos.Columns[e.ColumnIndex].Name == "colEstado" && e.Value is bool est)
            {
                e.Value = est ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
        }

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

        // Stub requerido por el diseñador (Click en el label del listado)
        private void label1_Click(object sender, EventArgs e) { /* no-op */ }
    }
}
