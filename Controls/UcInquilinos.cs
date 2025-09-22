using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcInquilinos : UserControl
    {
        // ===== Modelo simple para la vista =====
        private class InquilinoVm
        {
            public int Dni { get; set; }
            public string Nombre { get; set; } = "";
            public string Apellido { get; set; } = "";
            public string Telefono { get; set; } = "";
            public string Email { get; set; } = "";
            public string Direccion { get; set; } = "";
            public DateTime? FechaNacimiento { get; set; }
        }

        private readonly BindingList<InquilinoVm> _datos = new();
        private readonly BindingSource _bs = new();
        private int? _editIndex = null;

        public UcInquilinos()
        {
            InitializeComponent();

            // Suaviza el scroll del grid
            HabilitarDoubleBuffer(dataGridInquilinos);

            // Ajuste inicial + al redimensionar
            this.Load += (_, __) => AjustarLayout();
            this.Resize += (_, __) => AjustarLayout();
        }

        private void UcInquilinos_Load(object sender, EventArgs e)
        {
            // Corrijo el “password char” de Dirección
            txtDireccion.UseSystemPasswordChar = false;

            // Bind
            _bs.DataSource = _datos;
            dataGridInquilinos.DataSource = _bs;

            // === Datos de prueba ===
            _datos.Add(new InquilinoVm
            {
                Dni = 12345678,
                Nombre = "Juan",
                Apellido = "Pérez",
                Telefono = "3794111111",
                Email = "juanperez@email.com",
                Direccion = "San Martín 123",
                FechaNacimiento = new DateTime(1990, 5, 12)
            });

            _datos.Add(new InquilinoVm
            {
                Dni = 87654321,
                Nombre = "María",
                Apellido = "Gómez",
                Telefono = "3794222222",
                Email = "maria.gomez@email.com",
                Direccion = "Belgrano 456",
                FechaNacimiento = new DateTime(1985, 9, 23)
            });

            _datos.Add(new InquilinoVm
            {
                Dni = 11223344,
                Nombre = "Pedro",
                Apellido = "López",
                Telefono = "3794333333",
                Email = "pedro.lopez@email.com",
                Direccion = "Corrientes 789",
                FechaNacimiento = new DateTime(1995, 1, 8)
            });

            // “Toco” las columnas para que el compilador las considere usadas
            _ = colDni; _ = colNombre; _ = colApellido; _ = colTelefono; _ = colEmail; _ = colDireccion; _ = colFechaNacimiento;

        }


        // ============================
        // Eventos del Grid / Botones
        // ============================
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Reservado si luego agregamos botones en celdas (editar/eliminar).
        }

        private void dataGridInquilinos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _bs.Count) return;
            CargarEnFormularioParaEditar(e.RowIndex);
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (!ValidarFormulario(out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var vm = new InquilinoVm
            {
                Dni = int.Parse(txtDni.Text.Trim()),
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                FechaNacimiento = dateTimePicker1.Value.Date
            };

            if (_editIndex is int idx && idx >= 0 && idx < _datos.Count)
            {
                // Editar
                _datos[idx] = vm;
                _bs.ResetItem(idx);
                _editIndex = null;
            }
            else
            {
                // Alta (evitar duplicados por DNI)
                if (ExisteDni(vm.Dni))
                {
                    MessageBox.Show("Ya existe un inquilino con ese DNI.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _datos.Add(vm);
            }

            LimpiarFormulario();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            _editIndex = null;
            LimpiarFormulario();
        }

        // ============================
        // Helpers
        // ============================
        private bool ExisteDni(int dni)
        {
            foreach (var x in _datos)
                if (x.Dni == dni) return true;
            return false;
        }

        private void CargarEnFormularioParaEditar(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= _bs.Count) return;
            if (_bs[rowIndex] is not InquilinoVm vm) return;

            _editIndex = rowIndex;

            txtDni.Text = vm.Dni.ToString();
            txtNombre.Text = vm.Nombre;
            txtApellido.Text = vm.Apellido;
            txtTelefono.Text = vm.Telefono;
            txtEmail.Text = vm.Email;
            txtDireccion.Text = vm.Direccion;
            dateTimePicker1.Value = vm.FechaNacimiento ?? DateTime.Today;
        }

        private void LimpiarFormulario()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            dateTimePicker1.Value = DateTime.Today;
            txtNombre.Focus();
        }

        private bool ValidarFormulario(out string mensaje)
        {
            mensaje = "";

            // Nombre/Apellido
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) { mensaje = "Ingrese el Nombre."; return false; }
            if (string.IsNullOrWhiteSpace(txtApellido.Text)) { mensaje = "Ingrese el Apellido."; return false; }

            // DNI
            if (!int.TryParse(txtDni.Text.Trim(), out var dni) || dni <= 0)
            {
                mensaje = "Ingrese un DNI válido (solo números).";
                return false;
            }

            // Teléfono (opcional pero si viene, que tenga números)
            var tel = txtTelefono.Text.Trim();
            if (tel.Length > 0 && !Regex.IsMatch(tel, @"^[0-9+\-\s]{6,20}$"))
            {
                mensaje = "Teléfono inválido.";
                return false;
            }

            // Email (opcional pero si viene, validar formato básico)
            var email = txtEmail.Text.Trim();
            if (email.Length > 0 && !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                mensaje = "Email inválido.";
                return false;
            }

            // Dirección opcional

            return true;
        }

        private void label1_Click(object sender, EventArgs e) { /* sin uso */ }

        // ============================
        // Responsive
        // ============================
        private void AjustarLayout()
        {
            int margen = 15;
            int margenInferior = 20;

            // Panel header
            panel1.Left = margen;
            panel1.Width = this.Width - (margen * 2);

            // Panel formulario
            panel2.Left = margen;
            panel2.Width = this.Width - (margen * 2);

            // Label Lista
            LListaInquilinos.Left = margen;

            // Grid
            dataGridInquilinos.Left = margen;
            dataGridInquilinos.Width = this.Width - (margen * 2);

            int altoDisponible = this.Height - dataGridInquilinos.Top - margenInferior;
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
            catch { }
        }
    }
}
