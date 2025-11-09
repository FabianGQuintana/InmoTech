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
        private readonly ReciboRepository _repoRecibo = new ReciboRepository();

        private List<Cuota> _cuotas = new();

        public event EventHandler? Volver;
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
        //  REGIÓN: Carga de Datos y Configuración
        // ======================================================
        #region Carga y Configuración
        private void UcPagos_Contrato_Cuotas_Load(object? sender, EventArgs e)
        {
            CargarCabeceraContrato();
            CargarCuotas();
            ConfigurarGrilla();
        }

        private void CargarCabeceraContrato()
        {
            lblContrato.Text = $"Contrato C-{_contrato.IdContrato}";
            lblInquilino.Text = $"Inquilino: {_contrato.NombreInquilino}";
            lblInmueble.Text = $"Inmueble: {_contrato.DireccionInmueble}";
            lblFechas.Text = $"Inicio – Fin:  {_contrato.FechaInicio:dd/MM/yyyy} – {_contrato.FechaFin:dd/MM/yyyy}";
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
        #region Configuración y Eventos de Grilla
        private void ConfigurarGrilla()
        {
            dgvCuotas.AutoGenerateColumns = false;
            dgvCuotas.Columns.Clear();

            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cuota", DataPropertyName = "NroCuota", Width = 80 });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Período", DataPropertyName = "FechaVencimiento", Width = 140 });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Monto", DataPropertyName = "Importe", Width = 120 });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Vencimiento", DataPropertyName = "FechaVencimiento", Width = 130 });
            dgvCuotas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estado", DataPropertyName = "Estado", Width = 120 });

            var colAccion = new DataGridViewButtonColumn
            {
                HeaderText = "Acciones",
                Name = "colAccion",
                UseColumnTextForButtonValue = false,
                Width = 120
            };
            dgvCuotas.Columns.Add(colAccion);

            dgvCuotas.CellFormatting += DgvCuotas_CellFormatting;
            dgvCuotas.CellContentClick += DgvCuotas_CellContentClick;
        }

        private void DgvCuotas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var cuota = _cuotas[e.RowIndex];

            // Formato de fecha y monto
            if (dgvCuotas.Columns[e.ColumnIndex].DataPropertyName == "FechaVencimiento" && e.Value is DateTime dt)
                e.Value = dt.ToString("dd/MM/yyyy");
            else if (dgvCuotas.Columns[e.ColumnIndex].DataPropertyName == "Importe" && e.Value is decimal dec)
                e.Value = $"$ {dec:N0}";

            // Color por estado
            if (dgvCuotas.Columns[e.ColumnIndex].DataPropertyName == "Estado")
            {
                string estado = e.Value?.ToString() ?? "";
                var cell = dgvCuotas.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Style.ForeColor = Color.Black;
                if (estado == "Pagada") cell.Style.BackColor = Color.FromArgb(210, 243, 223);
                else if (estado == "Vencida") cell.Style.BackColor = Color.FromArgb(255, 230, 150);
                else if (estado == "Pendiente") cell.Style.BackColor = Color.FromArgb(255, 245, 200);
            }

            // Texto del botón
            if (dgvCuotas.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                var buttonCell = (DataGridViewButtonCell)dgvCuotas.Rows[e.RowIndex].Cells[e.ColumnIndex];
                buttonCell.Value = (cuota.Estado == "Pagada") ? "Ver Recibo" : "Pagar";
            }
        }

        private void DgvCuotas_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || !(dgvCuotas.Columns[e.ColumnIndex] is DataGridViewButtonColumn)) return;

            var cuota = _cuotas[e.RowIndex];

            if (cuota.Estado == "Pagada")
            {
                if (cuota.IdPago.HasValue)
                    MostrarVistaRecibo(cuota.IdPago.Value);
                else
                    MessageBox.Show("Esta cuota está pagada pero no tiene un pago asociado.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ===========================
            // VALIDACIÓN: pagar fuera de orden
            // ===========================
            // 1) Cuota impaga más antigua (menor nro) — es la “siguiente a pagar”
            var primeraImpaga = _cuotas
                .Where(c => !EsPagada(c))
                .OrderBy(c => c.NroCuota)
                .FirstOrDefault();

            // 2) ¿Existen impagas anteriores al nro seleccionado?
            bool tieneImpagasPrevias = _cuotas
                .Any(c => c.NroCuota < cuota.NroCuota && !EsPagada(c));

            if (primeraImpaga != null && tieneImpagasPrevias && cuota.NroCuota != primeraImpaga.NroCuota)
            {
                var r = MessageBox.Show(
                    $"Tenés cuotas impagas anteriores (Cuota {primeraImpaga.NroCuota}).\n\n" +
                    $"¿Deseás continuar pagando la Cuota {cuota.NroCuota} igualmente?",
                    "Confirmar pago fuera de orden",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (r != DialogResult.Yes) return;
            }

            // Continuar flujo normal de pago
            PagarCuotaSolicitado?.Invoke(this, (_contrato, cuota));
        }
        #endregion

        // ======================================================
        //  REGIÓN: Métodos Auxiliares
        // ======================================================
        #region Métodos Auxiliares
        private static bool EsPagada(Cuota c) => string.Equals(c.Estado, "Pagada", StringComparison.OrdinalIgnoreCase);

        private void MostrarVistaRecibo(int idPago)
        {
            try
            {
                var recibo = _repoRecibo.ObtenerPorIdPago(idPago);
                if (recibo != null)
                {
                    var ucRecibo = new UcRecibo();
                    ucRecibo.CargarDatos(recibo);

                    var formFlotante = new Form
                    {
                        Text = "Visor de Recibo",
                        StartPosition = FormStartPosition.CenterParent,
                        FormBorderStyle = FormBorderStyle.FixedToolWindow,
                        Size = new System.Drawing.Size(ucRecibo.Width + 20, ucRecibo.Height + 40),
                        MaximizeBox = false,
                        MinimizeBox = false
                    };

                    ucRecibo.CerrarSolicitado += (s, ev) => formFlotante.Close();
                    formFlotante.Controls.Add(ucRecibo);
                    ucRecibo.Dock = DockStyle.Fill;

                    formFlotante.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar el recibo asociado a este pago.", "Recibo no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar mostrar el recibo:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarResumen()
        {
            decimal totalPagado = _cuotas.Where(c => EsPagada(c)).Sum(c => c.Importe);
            decimal totalPendiente = _cuotas.Where(c => !EsPagada(c)).Sum(c => c.Importe);
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
