// UcInquilinos.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using SD = System.Drawing;
using SWF = System.Windows.Forms;

namespace InmoTech
{
    public partial class UcInquilinos : SWF.UserControl
    {
        // Paleta
        private readonly SD.Color Teal = SD.Color.FromArgb(0, 128, 128);
        private readonly SD.Color TealLight = SD.Color.FromArgb(210, 236, 236);

        private BindingList<InquilinoVm> _data = new();
        private List<InquilinoVm> _all = new();

        private bool _editando = false;
        private string? _dniOriginal = null;

        public UcInquilinos()
        {
            InitializeComponent();
            this.Load += UcInquilinos_Load;
        }

        private void UcInquilinos_Load(object? sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            // === (ya lo tenías) asegurar 8 columnas del form en runtime ===
            formGrid.SuspendLayout();
            formGrid.ColumnStyles.Clear();
            for (int i = 0; i < 8; i++)
                formGrid.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.Percent, 12.5f));
            formGrid.ResumeLayout();

            // ===== PASO 2: alturas de filas de inputs + que los controles llenen la celda =====
            // filas 1 y 3 son las de inputs (0 y 2 son los labels)
            formGrid.RowStyles[1].SizeType = SWF.SizeType.Absolute;
            formGrid.RowStyles[1].Height = 32f;
            formGrid.RowStyles[3].SizeType = SWF.SizeType.Absolute;
            formGrid.RowStyles[3].Height = 32f;

            // que los inputs se acoplen a la celda
            txtName.Dock = SWF.DockStyle.Fill;
            txtLastName.Dock = SWF.DockStyle.Fill;
            txtDni.Dock = SWF.DockStyle.Fill;
            txtPhone.Dock = SWF.DockStyle.Fill;
            txtEmail.Dock = SWF.DockStyle.Fill;
            txtAddress.Dock = SWF.DockStyle.Fill;
            dtpDate.Dock = SWF.DockStyle.Fill;

            // márgenes inferiores suaves para la primera línea e iguales en la segunda
            var m6R = new SWF.Padding(0, 0, 6, 6); // con separación a la derecha
            var m6 = new SWF.Padding(0, 0, 0, 6);

            txtName.Margin = m6R;
            txtLastName.Margin = m6R;
            txtDni.Margin = m6R;
            txtPhone.Margin = m6;

            txtEmail.Margin = m6R;
            txtAddress.Margin = m6R;
            dtpDate.Margin = m6R;
            // chkActive queda tal cual (alineado a la izquierda de su celda)

            // ===== estilos generales =====
            this.BackColor = Teal;
            lblTitle.ForeColor = SD.Color.White;
            lblTitle.Font = new SD.Font("Segoe UI", 18f, SD.FontStyle.Bold);

            btnSave.BackColor = Teal;
            btnSave.ForeColor = SD.Color.White;
            btnSave.FlatAppearance.BorderSize = 0;

            // ===== PASO 3: respiro visual para el encabezado del listado =====
            listHeader.Margin = new SWF.Padding(0, 8, 0, 8);

            // combos y eventos
            cbFilter.Items.Clear();
            cbFilter.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" });
            cbFilter.SelectedIndex = 0;

            // grid
            ConfigGrid();

            // eventos
            btnNew.Click += BtnNew_Click;
            btnCancel.Click += BtnCancel_Click;
            btnSave.Click += BtnSave_Click;
            txtSearch.TextChanged += (_, __) => ApplyFilter();
            cbFilter.SelectedIndexChanged += (_, __) => ApplyFilter();

            txtDni.KeyPress += OnlyDigits_KeyPress;
            txtPhone.KeyPress += PhoneMask_KeyPress;

            // datos mock
            LoadMock();
            ApplyFilter();
        }


        // ======= Modelo =======
        private class InquilinoVm
        {
            public string DNI { get; set; } = "";
            public string Nombre { get; set; } = "";
            public string Apellido { get; set; } = "";
            public string Telefono { get; set; } = "";
            public string Email { get; set; } = "";
            public string Direccion { get; set; } = "";
            public DateTime FechaEntrada { get; set; }
            public bool Activo { get; set; }
        }

        // ======= Grid =======
        private void ConfigGrid()
        {
            grid.AutoGenerateColumns = false;
            grid.AllowUserToAddRows = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = SWF.DataGridViewSelectionMode.FullRowSelect;
            grid.ReadOnly = true;
            grid.BackgroundColor = SD.Color.White;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = SD.Color.White;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = SD.Color.FromArgb(33, 37, 41);
            grid.ColumnHeadersDefaultCellStyle.Font = new SD.Font("Segoe UI", 10f, SD.FontStyle.Bold);
            grid.RowsDefaultCellStyle.Font = new SD.Font("Segoe UI", 10f, SD.FontStyle.Regular);

            grid.Columns.Clear();
            grid.Columns.Add(new SWF.DataGridViewTextBoxColumn { DataPropertyName = "DNI", HeaderText = "DNI", Width = 110 });
            grid.Columns.Add(new SWF.DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Nombre", Width = 140 });
            grid.Columns.Add(new SWF.DataGridViewTextBoxColumn { DataPropertyName = "Apellido", HeaderText = "Apellido", Width = 140 });
            grid.Columns.Add(new SWF.DataGridViewTextBoxColumn { DataPropertyName = "Telefono", HeaderText = "Teléfono", Width = 120 });
            grid.Columns.Add(new SWF.DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", AutoSizeMode = SWF.DataGridViewAutoSizeColumnMode.Fill });
            grid.Columns.Add(new SWF.DataGridViewTextBoxColumn { DataPropertyName = "Direccion", HeaderText = "Dirección", AutoSizeMode = SWF.DataGridViewAutoSizeColumnMode.Fill });
            grid.Columns.Add(new SWF.DataGridViewTextBoxColumn
            {
                DataPropertyName = "FechaEntrada",
                HeaderText = "Fecha",
                Width = 110,
                DefaultCellStyle = new SWF.DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
            grid.Columns.Add(new SWF.DataGridViewTextBoxColumn { DataPropertyName = "Activo", HeaderText = "Estado", Width = 90 });
            grid.Columns.Add(new SWF.DataGridViewButtonColumn { HeaderText = "Editar", Text = "Editar", UseColumnTextForButtonValue = true, Width = 70 });
            grid.Columns.Add(new SWF.DataGridViewButtonColumn { HeaderText = "Eliminar", Text = "Eliminar", UseColumnTextForButtonValue = true, Width = 80 });

            grid.CellFormatting += Grid_CellFormatting;
            grid.CellClick += Grid_CellClick;
        }

        private void Grid_CellFormatting(object? sender, SWF.DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (grid.Columns[e.ColumnIndex].HeaderText == "Estado" && e.Value is bool a)
            {
                e.Value = a ? "Activo" : "Inactivo";
                e.CellStyle.Alignment = SWF.DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.BackColor = a ? TealLight : SD.Color.FromArgb(234, 236, 239);
            }
        }

        private void Grid_CellClick(object? sender, SWF.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (!(grid.Columns[e.ColumnIndex] is SWF.DataGridViewButtonColumn)) return;

            var header = grid.Columns[e.ColumnIndex].HeaderText;
            var it = (InquilinoVm)grid.Rows[e.RowIndex].DataBoundItem;

            if (header == "Editar")
            {
                _editando = true;
                _dniOriginal = it.DNI;

                txtDni.Text = it.DNI;
                txtName.Text = it.Nombre;
                txtLastName.Text = it.Apellido;
                txtPhone.Text = it.Telefono;
                txtEmail.Text = it.Email;
                txtAddress.Text = it.Direccion;
                dtpDate.Value = it.FechaEntrada;
                chkActive.Checked = it.Activo;

                btnSave.Text = "Actualizar";
            }
            else if (header == "Eliminar")
            {
                var ok = SWF.MessageBox.Show($"¿Eliminar {it.Apellido}, {it.Nombre} (DNI {it.DNI})?",
                    "Confirmar", SWF.MessageBoxButtons.YesNo, SWF.MessageBoxIcon.Warning);
                if (ok == SWF.DialogResult.Yes)
                {
                    _all.RemoveAll(x => x.DNI == it.DNI);
                    ApplyFilter();
                    ClearForm();
                }
            }
        }

        // ======= Datos =======
        private void LoadMock()
        {
            _all = new List<InquilinoVm>
            {
                new() { DNI="30111222", Nombre="Juan",  Apellido="Pérez",  Telefono="3794123456", Email="juan.perez@mail.com",  Direccion="Av. Corrientes 1200", FechaEntrada=new DateTime(2024,5,2),  Activo=true },
                new() { DNI="33123456", Nombre="Ana",   Apellido="Gómez",  Telefono="3794556677", Email="ana.gomez@mail.com",   Direccion="San Martín 450",     FechaEntrada=new DateTime(2023,10,10), Activo=true },
                new() { DNI="28999888", Nombre="Diego", Apellido="Luna",   Telefono="3794232323", Email="diego.luna@mail.com",  Direccion="Avenida 3 #456",     FechaEntrada=new DateTime(2022,4,15),  Activo=false },
            };
        }

        private void ApplyFilter()
        {
            IEnumerable<InquilinoVm> q = _all;

            var t = (txtSearch.Text ?? "").Trim().ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(t))
            {
                q = q.Where(x =>
                    x.DNI.Contains(t, StringComparison.OrdinalIgnoreCase) ||
                    x.Nombre.ToLower().Contains(t) ||
                    x.Apellido.ToLower().Contains(t) ||
                    x.Email.ToLower().Contains(t) ||
                    x.Direccion.ToLower().Contains(t));
            }

            switch (cbFilter.SelectedItem?.ToString())
            {
                case "Activos": q = q.Where(x => x.Activo); break;
                case "Inactivos": q = q.Where(x => !x.Activo); break;
            }

            _data = new BindingList<InquilinoVm>(q.ToList());
            grid.DataSource = _data;
            lblCount.Text = $"{_data.Count} inquilino(s)";
        }

        // ======= Formulario =======
        private void BtnNew_Click(object? sender, EventArgs e) => ClearForm();
        private void BtnCancel_Click(object? sender, EventArgs e) => ClearForm();

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            var item = new InquilinoVm
            {
                DNI = txtDni.Text.Trim(),
                Nombre = Cap(txtName.Text),
                Apellido = Cap(txtLastName.Text),
                Telefono = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Direccion = txtAddress.Text.Trim(),
                FechaEntrada = dtpDate.Value.Date,
                Activo = chkActive.Checked
            };

            if (_editando)
            {
                if (_all.Any(x => x.DNI == item.DNI && x.DNI != _dniOriginal))
                {
                    err.SetError(txtDni, "Ya existe un inquilino con este DNI.");
                    txtDni.Focus();
                    return;
                }
                var idx = _all.FindIndex(x => x.DNI == _dniOriginal);
                if (idx >= 0) _all[idx] = item;
                SWF.MessageBox.Show("Inquilino actualizado.", "OK", SWF.MessageBoxButtons.OK, SWF.MessageBoxIcon.Information);
            }
            else
            {
                if (_all.Any(x => x.DNI == item.DNI))
                {
                    err.SetError(txtDni, "Ya existe un inquilino con este DNI.");
                    txtDni.Focus();
                    return;
                }
                _all.Add(item);
                SWF.MessageBox.Show("Inquilino agregado.", "OK", SWF.MessageBoxButtons.OK, SWF.MessageBoxIcon.Information);
            }

            ApplyFilter();
            ClearForm();
        }

        private void ClearForm()
        {
            _editando = false;
            _dniOriginal = null;
            err.Clear();

            txtDni.Clear();
            txtName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            dtpDate.Value = DateTime.Today;
            chkActive.Checked = true;

            btnSave.Text = "Guardar";
        }

        private bool ValidateForm()
        {
            err.Clear();
            bool ok = true;

            if (string.IsNullOrWhiteSpace(txtDni.Text) || !Regex.IsMatch(txtDni.Text.Trim(), @"^\d{7,10}$"))
            {
                err.SetError(txtDni, "DNI inválido (7-10 dígitos).");
                ok = false;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                err.SetError(txtName, "Nombre requerido.");
                ok = false;
            }
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                err.SetError(txtLastName, "Apellido requerido.");
                ok = false;
            }
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                err.SetError(txtEmail, "Email inválido.");
                ok = false;
            }
            return ok;
        }

        private void OnlyDigits_KeyPress(object? sender, SWF.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void PhoneMask_KeyPress(object? sender, SWF.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '+' && e.KeyChar != ' ' && e.KeyChar != '-' &&
                e.KeyChar != '(' && e.KeyChar != ')')
                e.Handled = true;
        }

        private static string Cap(string s)
        {
            s = s.Trim();
            return string.IsNullOrEmpty(s) ? s : char.ToUpper(s[0]) + (s.Length > 1 ? s[1..].ToLower() : "");
        }
    }
}
