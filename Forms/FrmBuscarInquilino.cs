using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InmoTech.Repositories;
using InmoTech.Models;

namespace InmoTech.Forms
{
    public partial class FrmBuscarInquilino : Form
    {
        // ======================================================
        //  REGIÓN: Propiedades y Estado Interno
        // ======================================================
        #region Propiedades y Estado Interno
        private readonly InquilinoRepository _repo = new InquilinoRepository();
        private int _page = 1, _pageSize = 20, _total = 0;

        public InquilinoLite? Seleccionado { get; private set; }

        private sealed class InquilinoRow
        {
            public int Dni { get; set; }
            public string ApellidoNombre { get; set; } = "";
            public string Telefono { get; set; } = "";
            public string Email { get; set; } = "";
            public bool Estado { get; set; }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Constructor y Eventos
        // ======================================================
        #region Constructor y Eventos
        public FrmBuscarInquilino()
        {
            InitializeComponent();
            WireEvents();
            Cargar();
        }

        private void WireEvents()
        {
            // búsqueda reactiva
            txtBuscar.TextChanged += (s, e) => { timerBuscar.Stop(); timerBuscar.Start(); };
            timerBuscar.Tick += (s, e) => { timerBuscar.Stop(); _page = 1; Cargar(); };

            // filtro estado
            cboEstado.SelectedIndexChanged += (s, e) => { _page = 1; Cargar(); };

            // paginación
            btnAnterior.Click += (s, e) => { if (_page > 1) { _page--; Cargar(); } };
            btnSiguiente.Click += (s, e) => { if (_page * _pageSize < _total) { _page++; Cargar(); } };

            // selección
            btnElegir.Click += (s, e) => ElegirActual();
            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            dgv.CellDoubleClick += (s, e) => ElegirActual();
            dgv.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.Handled = true; ElegirActual(); }
            };

            // formateo boolean  texto
            dgv.CellFormatting += Dgv_CellFormatting;

        }
        #endregion

        // ======================================================
        //  REGIÓN: Logica de Negocio y DataGrid
        // ======================================================
        #region Lógica de Negocio y DataGrid
        private bool? EstadoSeleccionado()
        {
            return cboEstado.SelectedIndex switch
            {
                0 => (bool?)null, // Todos
                1 => true,        // Activos
                2 => false,       // Inactivos
                _ => null
            };
        }

        private void Cargar()
        {
            var (items, total) = _repo.BuscarPaginado(
              txtBuscar.Text?.Trim() ?? "",
              EstadoSeleccionado(),
              _page,
              _pageSize
            );

            _total = total;

            // Proyectamos a InquilinoRow (propiedades coinciden con DataPropertyName del Designer)
            var view = new List<InquilinoRow>();
            foreach (var it in items)
            {
                view.Add(new InquilinoRow
                {
                    Dni = it.Dni,
                    ApellidoNombre = $"{it.Apellido}, {it.Nombre}",
                    Telefono = it.Telefono,
                    Email = it.Email,
                    Estado = it.Estado
                });
            }

            dgv.DataSource = view;

            // UI paginación
            var desde = (_page - 1) * _pageSize + 1;
            var hasta = Math.Min(_page * _pageSize, _total);
            if (_total == 0) { desde = 0; hasta = 0; }

            lblInfo.Text = $"Mostrando {desde}-{hasta} de {_total}";
            btnAnterior.Enabled = _page > 1;
            btnSiguiente.Enabled = _page * _pageSize < _total;
        }

        private void Dgv_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var prop = dgv.Columns[e.ColumnIndex].DataPropertyName;
            if (prop == "Estado" && e.Value is bool ac)
            {
                e.Value = ac ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Selección y Resultado
        // ======================================================
        #region Selección y Resultado
        private void ElegirActual()
        {
            if (dgv.CurrentRow?.DataBoundItem is InquilinoRow row)
            {
                // Devolvemos un InquilinoLite con lo necesario
                Seleccionado = new InquilinoLite
                {
                    Dni = row.Dni,
                    // ApellidoNombre lo teníamos en una sola propiedad visual; si quieres separar, deberíamos consultar al repo
                    Apellido = row.ApellidoNombre,
                    Telefono = row.Telefono,
                    Email = row.Email,
                    Estado = row.Estado
                };

                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion
    }
}