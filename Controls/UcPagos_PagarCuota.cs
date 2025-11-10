using InmoTech.Documents;
using InmoTech.Models;
using InmoTech.Repositories;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    /// <summary>
    /// Control de UI para registrar el pago de una cuota de contrato.
    /// Permite validar datos, persistir Pago/Cuota/Recibo, emitir PDF y previsualizar recibo.
    /// </summary>
    public partial class UcPagos_PagarCuota : UserControl
    {
        // ======================================================
        //  REGIÓN: Propiedades y Repositorios
        // ======================================================
        #region Propiedades y Repositorios
        private readonly Contrato _contrato;
        private readonly Cuota _cuota;

        // Repositorios existentes
        private readonly MetodoPagoRepository _repoMetodos = new MetodoPagoRepository();
        private readonly PagoRepository _repoPago = new PagoRepository();
        private readonly CuotaRepository _repoCuota = new CuotaRepository();


        private readonly ReciboRepository _repoRecibo = new ReciboRepository();
        #endregion

        // ======================================================
        //  REGIÓN: Eventos
        // ======================================================
        #region Eventos
        /// <summary>
        /// Se dispara cuando el usuario cancela la operación de pago.
        /// </summary>
        public event EventHandler? Cancelado;

        /// <summary>
        /// Se dispara cuando el pago fue guardado exitosamente.
        /// Entrega el ID del nuevo pago persistido.
        /// </summary>
        public event EventHandler<int>? PagoGuardado;
        #endregion

        // ======================================================
        //  REGIÓN: Constructor y Carga Inicial
        // ======================================================
        #region Constructor y Carga Inicial
        /// <summary>
        /// Crea el control de pago de cuota asociado a un contrato y una cuota.
        /// Registra el manejador del evento Load.
        /// </summary>
        /// <param name="contrato">Contrato al que pertenece la cuota.</param>
        /// <param name="cuota">Cuota a pagar.</param>
        public UcPagos_PagarCuota(Contrato contrato, Cuota cuota)
        {
            InitializeComponent();
            _contrato = contrato;
            _cuota = cuota;
            Load += UcPagos_PagarCuota_Load;
        }

        /// <summary>
        /// Inicializa textos de cabecera, valores por defecto, llena métodos de pago,
        /// configura handlers de botones y enlace de vista previa de recibo.
        /// </summary>
        private void UcPagos_PagarCuota_Load(object? sender, EventArgs e)
        {

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
        #endregion

        // ======================================================
        //  REGIÓN: Handlers de Botones
        // ======================================================
        #region Handlers de Botones
        /// <summary>
        /// Valida el formulario, guarda Pago/Recibo, genera PDF si corresponde y notifica el evento <see cref="PagoGuardado"/>.
        /// </summary>
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

        /// <summary>
        /// Construye un recibo temporal con los datos actuales del formulario y abre el visor en modo vista previa.
        /// </summary>
        private void LnkVistaPrevia_Click(object sender, EventArgs e)
        {
            // Construye un recibo 'temporal' con los datos del formulario, sin guardarlo.
            var reciboPrevio = ConstruirRecibo(0); // id_pago = 0 porque aún no existe
            MostrarRecibo(reciboPrevio);
        }
        #endregion

        // ======================================================
        //  REGIÓN: Logica de Negocio y Persistencia
        // ======================================================
        #region Lógica de Negocio y Persistencia
        /// <summary>
        /// Ejecuta validaciones mínimas: método de pago seleccionado, monto &gt; 0 y usuario autenticado.
        /// </summary>
        /// <returns><c>true</c> si los datos son válidos; en caso contrario, <c>false</c>.</returns>
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

        /// <summary>
        /// Persiste el pago, actualiza el estado de la cuota y registra el recibo asociado.
        /// </summary>
        /// <returns>El identificador del nuevo pago agregado.</returns>
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

        /// <summary>
        /// Construye el objeto <see cref="Recibo"/> a partir de los datos actuales del formulario.
        /// Si <paramref name="idPago"/> es 0, genera una instancia de vista previa sin número de comprobante.
        /// </summary>
        /// <param name="idPago">Id del pago asociado (0 para vista previa).</param>
        /// <returns>Instancia de <see cref="Recibo"/> lista para mostrar/guardar.</returns>
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
        #endregion

        // ======================================================
        //  REGIÓN: Generación de Documentos y PDF
        // ======================================================
        #region Generación de Documentos y PDF
        /// <summary>
        /// Si el usuario marcó la casilla de emitir recibo, solicita ubicación,
        /// genera el PDF del recibo y ofrece abrirlo.
        /// </summary>
        /// <param name="nuevoIdPago">Id del pago recientemente creado.</param>
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
        #endregion

        // ======================================================
        //  REGIÓN: Utilitarios de UI y Datos
        // ======================================================
        #region Utilitarios de UI y Datos
        /// <summary>
        /// Abre un formulario flotante con el control visor de recibo y carga los datos provistos.
        /// </summary>
        /// <param name="recibo">Recibo a visualizar.</param>
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

        /// <summary>
        /// Construye el texto de detalle a partir de las observaciones, devolviendo <c>null</c> si está vacío.
        /// </summary>
        /// <param name="obs">Texto libre de observaciones.</param>
        /// <returns>Detalle normalizado o <c>null</c>.</returns>
        private string? ConstruirDetalle(string obs)
        {
            return string.IsNullOrWhiteSpace(obs) ? null : obs.Trim();
        }

        /// <summary>
        /// Calcula la cantidad de meses entre el inicio y el fin del contrato (mínimo 1).
        /// </summary>
        /// <returns>Número de meses del contrato.</returns>
        private int CalcularMesesContrato()
        {
            var m = ((_contrato.FechaFin.Year - _contrato.FechaInicio.Year) * 12)
                 + (_contrato.FechaFin.Month - _contrato.FechaInicio.Month);
            return Math.Max(1, m);
        }
        #endregion
    }
}
