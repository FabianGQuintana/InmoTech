using InmoTech.Documents;
using InmoTech.Models;
using InmoTech.Repositories;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcPagos_PagarCuota : UserControl
    {
        private readonly Contrato _contrato;
        private readonly Cuota _cuota;

        // Repositorios existentes
        private readonly MetodoPagoRepository _repoMetodos = new MetodoPagoRepository();
        private readonly PagoRepository _repoPago = new PagoRepository();
        private readonly CuotaRepository _repoCuota = new CuotaRepository();

        // ¡NUEVO REPOSITORIO!
        private readonly ReciboRepository _repoRecibo = new ReciboRepository();

        public event EventHandler? Cancelado;
        public event EventHandler<int>? PagoGuardado;

        public UcPagos_PagarCuota(Contrato contrato, Cuota cuota)
        {
            InitializeComponent();
            _contrato = contrato;
            _cuota = cuota;
            Load += UcPagos_PagarCuota_Load;
        }

        private void UcPagos_PagarCuota_Load(object? sender, EventArgs e)
        {
            // ... (tu código de carga existente no cambia)
            lblTituloContrato.Text = $"Contrato N°: C-{_contrato.IdContrato}";
            lblLinea1.Text = $"Inquilino: {_contrato.NombreInquilino ?? "—"}";
            lblLinea2.Text = $"Inmueble: {_contrato.DireccionInmueble ?? "—"}";
            lblLinea3.Text = $"Cuota: {_cuota.NroCuota} / {CalcularMesesContrato()}   |   " +
                             $"Período: {_cuota.FechaVencimiento:MMMM yyyy}   |   Monto: $ {_cuota.Importe:N0}";

            dtpFechaPago.Value = DateTime.Now.Date;
            nudMonto.Value = _cuota.Importe;
            txtObservaciones.PlaceholderText = "Opcional";

            var metodos = _repoMetodos.ObtenerTodos();
            cboMetodoPago.DataSource = metodos;
            cboMetodoPago.DisplayMember = nameof(Models.MetodoPago.TipoPago);
            cboMetodoPago.ValueMember = nameof(Models.MetodoPago.IdMetodoPago);

            var idxEfectivo = metodos.FindIndex(m => m.IdMetodoPago == 1);
            cboMetodoPago.SelectedIndex = idxEfectivo >= 0 ? idxEfectivo : 0;

            btnCancelar.Click += (s, ev) => Cancelado?.Invoke(this, EventArgs.Empty);
            btnGuardar.Click += BtnGuardar_Click;

            // Lógica actualizada para vista previa
            lnkVistaPrevia.Click += LnkVistaPrevia_Click;
        }
        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            // 1. Validar los datos del formulario
            if (!SonDatosValidos())
            {
                return; // Si no son válidos, no continuamos.
            }

            try
            {
                // 2. Guardar el pago y el recibo en la base de datos
                int nuevoIdPago = GuardarPagoYRecibo();

                MessageBox.Show("Pago y recibo registrados correctamente.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 3. Generar el PDF si el usuario lo solicitó
                GenerarPdfSiEsNecesario(nuevoIdPago);

                // 4. Notificar que el pago fue guardado
                PagoGuardado?.Invoke(this, nuevoIdPago);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el pago y el recibo:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SonDatosValidos()
        {
            if (cboMetodoPago.SelectedItem is null || nudMonto.Value <= 0)
            {
                MessageBox.Show("Seleccione un método de pago y un monto mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (InmoTech.Security.AuthService.CurrentUser is null)
            {
                MessageBox.Show("No hay un usuario autenticado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private int GuardarPagoYRecibo()
        {
            var usr = InmoTech.Security.AuthService.CurrentUser;

            // Crear y guardar el Pago
            var pago = new Models.Pago
            {
                FechaPago = dtpFechaPago.Value.Date,
                MontoTotal = nudMonto.Value,
                IdUsuario = usr.Dni,
                IdMetodoPago = (int)cboMetodoPago.SelectedValue,
                IdContrato = _contrato.IdContrato,
                NroCuota = _cuota.NroCuota,
                Detalle = ConstruirDetalle(txtObservaciones.Text),
                Estado = "Pagado",
                FechaRegistro = DateTime.Now,
                IdPersona = _contrato.IdPersona,
                UsuarioCreador = $"{usr.Apellido} {usr.Nombre}".Trim(),
                Activo = true
            };
            int nuevoIdPago = _repoPago.Agregar(pago);

            // Actualizar la Cuota
            _repoCuota.AsignarPago(_contrato.IdContrato, _cuota.NroCuota, nuevoIdPago);
            _repoCuota.ActualizarEstado(_contrato.IdContrato, _cuota.NroCuota, "Pagada");

            // Crear y guardar el Recibo
            var recibo = ConstruirRecibo(nuevoIdPago);
            _repoRecibo.Agregar(recibo);

            return nuevoIdPago;
        }

        private void GenerarPdfSiEsNecesario(int nuevoIdPago)
        {
            if (chkEmitirRecibo.Checked)
            {
                var reciboCompletoParaPdf = _repoRecibo.ObtenerPorIdPago(nuevoIdPago);
                if (reciboCompletoParaPdf != null)
                {
                    var sfd = new SaveFileDialog
                    {
                        Title = "Guardar Recibo en PDF",
                        Filter = "Archivo PDF|*.pdf",
                        FileName = $"Recibo-{reciboCompletoParaPdf.NroComprobante}-{DateTime.Now:yyyyMMdd}.pdf"
                    };

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var documentGenerator = new ReciboDocumentIText(reciboCompletoParaPdf);
                        documentGenerator.Generate(sfd.FileName);

                        var result = MessageBox.Show(
                            "¡PDF generado correctamente!",
                            "Emisión de Recibo"
                        );

                        if (result == DialogResult.Yes)
                        {
                            Process.Start(sfd.FileName);
                        }
                    }
                }
            }
        }

        // --- MÉTODOS NUEVOS PARA EL RECIBO ---

        private void LnkVistaPrevia_Click(object sender, EventArgs e)
        {
            // Construye un recibo 'temporal' con los datos del formulario, sin guardarlo.
            var reciboPrevio = ConstruirRecibo(0); // id_pago = 0 porque aún no existe
            MostrarRecibo(reciboPrevio);
        }

        /// <summary>
        /// Crea un objeto Recibo a partir de los datos actuales en el formulario.
        /// </summary>
        // ... dentro de la clase UcPagos_PagarCuota ...

        /// <summary>
        /// Crea un objeto Recibo a partir de los datos actuales en el formulario.
        /// </summary>
        private Recibo ConstruirRecibo(int idPago)
        {
            var usr = InmoTech.Security.AuthService.CurrentUser;
            var metodoPago = cboMetodoPago.SelectedItem as Models.MetodoPago;

            return new Recibo
            {
                IdPago = idPago, // Puede ser 0 si es una vista previa
                FechaEmision = dtpFechaPago.Value.Date,

                // --- LÍNEA MODIFICADA ---
                // Llama al repositorio para generar el N° de comprobante solo si es un recibo real (idPago > 0).
                // Para la vista previa, el número de comprobante será nulo.
                NroComprobante = idPago > 0 ? _repoRecibo.GenerarNumeroComprobante() : null,

                IdUsuarioEmisor = usr?.Dni ?? 0,
                IdInquilino = _contrato.IdPersona,
                IdInmueble = _contrato.IdInmueble,
                FormaPago = metodoPago?.TipoPago ?? "Desconocido",
                Observaciones = ConstruirDetalle(txtObservaciones.Text),

                // Propiedades extendidas para la vista previa
                NombreInquilino = _contrato.NombreInquilino,
                DireccionInmueble = _contrato.DireccionInmueble,
                MontoPagado = nudMonto.Value,
                Concepto = $"Pago de alquiler - Cuota N°{_cuota.NroCuota}"
            };
        }

        /// <summary>
        /// Muestra el control del recibo en una ventana flotante.
        /// </summary>
        private void MostrarRecibo(Recibo recibo)
        {
            var ucRecibo = new UcRecibo();
            ucRecibo.CargarDatos(recibo);

            var formFlotante = new Form
            {
                Text = "Visor de Recibo",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Size = new Size(ucRecibo.Width + 20, ucRecibo.Height + 40), // Ajustar tamaño
                MaximizeBox = false,
                MinimizeBox = false,
            };

            ucRecibo.CerrarSolicitado += (s, e) => formFlotante.Close();
            formFlotante.Controls.Add(ucRecibo);
            ucRecibo.Dock = DockStyle.Fill;

            formFlotante.ShowDialog();
        }

        // --- Métodos existentes ---
        private string? ConstruirDetalle(string obs)
        {
            return string.IsNullOrWhiteSpace(obs) ? null : obs.Trim();
        }

        private int CalcularMesesContrato()
        {
            var m = ((_contrato.FechaFin.Year - _contrato.FechaInicio.Year) * 12)
                      + (_contrato.FechaFin.Month - _contrato.FechaInicio.Month);
            return Math.Max(1, m);
        }
    }
}