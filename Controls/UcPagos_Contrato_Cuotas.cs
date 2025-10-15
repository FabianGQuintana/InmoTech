using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InmoTech.Models;
using InmoTech.Repositories;

namespace InmoTech.Controls
{
    public partial class UcPagos_Contrato_Cuotas : UserControl
    {
        // ======================================================
        //  REGIÓN: Campos y Eventos
        // ======================================================
        #region Campos y Eventos
        private readonly Contrato _contrato;
        private readonly CuotaRepository _repoCuotas = new CuotaRepository();
        private List<Cuota> _cuotas = new();

        public event EventHandler? Volver;

        // NUEVO: evento para solicitar el flujo de pago de una cuota
        public event EventHandler<(Contrato contrato, Cuota cuota)>? PagarCuotaSolicitado;
        #endregion

        // ======================================================
        //  REGIÓN: Constructor
        // ======================================================
        #region Constructor
        public UcPagos_Contrato_Cuotas(Contrato contrato)
        {
            InitializeComponent();
            _contrato = contrato;
            Load += UcPagos_Contrato_Cuotas_Load;
        }
        #endregion

        // ======================================================
        //  REGIÓN: Eventos y Métodos
        // ======================================================
        #region Eventos de Carga
        private void UcPagos_Contrato_Cuotas_Load(object? sender, EventArgs e)
        {
            CargarCabeceraContrato();
            CargarCuotas();
            ConfigurarGrilla();
        }
        #endregion

        // ======================================================
        //  REGIÓN: Metodos de Carga de Datos
        // ======================================================
        #region Métodos de Carga de Datos
        private void CargarCabeceraContrato()
        {
            lblContrato.Text = $"Contrato C-{_contrato.IdContrato}";
            lblInquilino.Text = $"Inquilino: {_contrato.NombreInquilino}";
            lblInmueble.Text = $"Inmueble: {_contrato.DireccionInmueble}";
            lblFechas.Text = $"Inicio – Fin:  {_contrato.FechaInicio:dd/MM/yyyy} – {_contrato.FechaFin:dd/MM/yyyy}";
            lblTotal.Text = $"{_contrato.Monto * ObtenerCantidadMeses():N0}";
            lblCuotas.Text = ObtenerCantidadMeses().ToString();

            ActualizarResumen();
        }

        private void CargarCuotas()
        {
            _cuotas = _repoCuotas.ObtenerPorContrato(_contrato.IdContrato);
            dgvCuotas.DataSource = _cuotas;
        }
        #endregion

        // ======================================================
        //  REGIÓN: Configuración y Eventos de Grilla
        // ======================================================
        #region Configuración de Grilla
        private void ConfigurarGrilla()
        {
            dgvCuotas.AutoGenerateColumns = false;
            dgvCuotas.Columns.Clear();

            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Cuota",
                DataPropertyName = "NroCuota",
                Width = 80
            });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Período",
                DataPropertyName = "FechaVencimiento",
                Width = 140
            });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Monto",
                DataPropertyName = "Importe",
                Width = 120
            });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Vencimiento",
                DataPropertyName = "FechaVencimiento",
                Width = 130
            });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 120
            });
            var colAccion = new DataGridViewButtonColumn
            {
                HeaderText = "Acciones",
                Text = "Pagar",
                UseColumnTextForButtonValue = true,
                Width = 120
            };
            dgvCuotas.Columns.Add(colAccion);

            dgvCuotas.CellFormatting += DgvCuotas_CellFormatting;
            dgvCuotas.CellContentClick += DgvCuotas_CellContentClick;
        }
        #endregion

        // ======================================================
        //  REGIÓN: Eventos de Grilla
        // ======================================================
        #region Eventos de Grilla
        private void DgvCuotas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvCuotas.Columns[e.ColumnIndex].DataPropertyName == "FechaVencimiento" && e.Value is DateTime dt)
            {
                e.Value = dt.ToString("dd/MM/yyyy");
            }
            else if (dgvCuotas.Columns[e.ColumnIndex].DataPropertyName == "Importe" && e.Value is decimal dec)
            {
                e.Value = $"$ {dec:N0}";
            }

            if (dgvCuotas.Columns[e.ColumnIndex].DataPropertyName == "Estado")
            {
                string estado = e.Value?.ToString() ?? "";
                var cell = dgvCuotas.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Style.ForeColor = Color.Black;

                if (estado == "Pagada")
                {
                    cell.Style.BackColor = Color.FromArgb(210, 243, 223); // verde suave
                }
                else if (estado == "Vencida")
                {
                    cell.Style.BackColor = Color.FromArgb(255, 230, 150);
                }
                else if (estado == "Pendiente")
                {
                    cell.Style.BackColor = Color.FromArgb(255, 245, 200);
                }
            }
        }

        private void DgvCuotas_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // si se hace click en el botón "Pagar", disparamos el evento hacia el MainForm
            if (dgvCuotas.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                var cuota = _cuotas[e.RowIndex];

                if (cuota.Estado == "Pagada")
                {
                    MessageBox.Show("La cuota ya está pagada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Nada de actualizar estado acá: delegamos al flujo de pago
                PagarCuotaSolicitado?.Invoke(this, (_contrato, cuota));
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Metodos Auxiliares
        // ======================================================
        #region Métodos Auxiliares
        private void ActualizarResumen()
        {
            int pagadas = _cuotas.Count(c => c.Estado == "Pagada");
            int vencidas = _cuotas.Count(c => c.Estado == "Vencida");
            int pendientes = _cuotas.Count(c => c.Estado == "Pendiente");

            decimal totalPagado = _cuotas.Where(c => c.Estado == "Pagada").Sum(c => c.Importe);
            decimal totalPendiente = _cuotas.Where(c => c.Estado != "Pagada").Sum(c => c.Importe);

            lblAtraso.Text = $"{totalPendiente:N0}";
            lblTotal.Text = $"{totalPagado + totalPendiente:N0}";
        }

        private int ObtenerCantidadMeses()
        {
            return ((_contrato.FechaFin.Year - _contrato.FechaInicio.Year) * 12) +
                       (_contrato.FechaFin.Month - _contrato.FechaInicio.Month);
        }
        #endregion

        // ======================================================
        //  REGIÓN: Eventos de Controles
        // ======================================================
        #region Eventos de Controles
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Volver?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}