// UcContratos.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcContratos : UserControl
    {
        // ViewModel para el grid
        public class ContratoVm
        {
            public string NumeroContrato { get; set; } = "";
            public string Inquilino { get; set; } = "";
            public string Inmueble { get; set; } = "";
            public DateTime Inicio { get; set; }
            public DateTime Fin { get; set; }
            public decimal Monto { get; set; }
            public string Estado { get; set; } = "";
            public bool Activo { get; set; }
        }

        private readonly BindingSource _bs = new BindingSource();

        public UcContratos()
        {
            InitializeComponent();

            // Binding listo (sin datos inicialmente)
            _bs.DataSource = new BindingList<ContratoVm>();
            dataGridView1.DataSource = _bs;

            // Eventos
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += BtnCancelar_Click;

            dataGridView1.CellFormatting += DataGridView1_CellFormatting;

            // ===== Datos de prueba =====
            var demo = new List<ContratoVm>
    {
        new ContratoVm {
            NumeroContrato = "C-001",
            Inquilino = "Juan Pérez",
            Inmueble = "Depto 2A",
            Inicio = new DateTime(2025, 1, 1),
            Fin = new DateTime(2025, 12, 31),
            Monto = 250000m,
            Estado = "Vigente",
            Activo = true
        },
        new ContratoVm {
            NumeroContrato = "C-002",
            Inquilino = "María López",
            Inmueble = "Casa 5",
            Inicio = new DateTime(2024, 6, 1),
            Fin = new DateTime(2025, 5, 31),
            Monto = 300000m,
            Estado = "Finalizado",
            Activo = false
        },
        new ContratoVm {
            NumeroContrato = "C-003",
            Inquilino = "Carlos Gómez",
            Inmueble = "Local Comercial",
            Inicio = new DateTime(2025, 3, 15),
            Fin = new DateTime(2026, 3, 14),
            Monto = 450000m,
            Estado = "Vigente",
            Activo = true
        }
    };

            CargarDatos(demo);
        }


        private void DataGridView1_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "colActivo" && e.Value is bool valor)
            {
                e.Value = valor ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
        }


        // API pública para cargar/recargar datos reales desde tu repo/servicio
        public void CargarDatos(IEnumerable<ContratoVm> items)
        {
            if (_bs.DataSource is BindingList<ContratoVm> list)
            {
                list.Clear();
                foreach (var it in items)
                    list.Add(it);
            }
        }

        // API para limpiar
        public void LimpiarDatos()
        {
            if (_bs.DataSource is BindingList<ContratoVm> list)
                list.Clear();
        }

        // ==== Handlers de ejemplo (dejados vacíos) ====
        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            // acá iría tu lógica de guardado
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            // acá iría tu lógica de cancelación
        }
    }
}
