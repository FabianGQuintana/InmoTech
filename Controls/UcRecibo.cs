using InmoTech.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using InmoTech.Documents; // El namespace de nuestra nueva clase
using System.Diagnostics; // Para poder abrir el archivo al final

namespace InmoTech.Controls
{
    public partial class UcRecibo : UserControl
    {
        private Recibo _recibo;

        // Evento para notificar al contenedor que debe cerrarse
        public event EventHandler CerrarSolicitado;

        public UcRecibo()
        {
            InitializeComponent();
        }

        public void CargarDatos(Recibo recibo)
        {
            _recibo = recibo;

            // Cargar datos en los controles
            lblNroComprobante.Text = $"RECIBO N°: {_recibo.NroComprobante ?? "PENDIENTE"}";
            lblFechaEmision.Text = $"Fecha de Emisión: {_recibo.FechaEmision:dd/MM/yyyy}";

            lblNombreInquilino.Text = $"Recibí de: {_recibo.NombreInquilino ?? "No especificado"}";
            lblDireccionInmueble.Text = $"En concepto de alquiler del inmueble sito en: {_recibo.DireccionInmueble ?? "No especificada"}";

            lblConcepto.Text = _recibo.Concepto ?? $"Pago de alquiler. Contrato N° C-{_recibo.IdPago}";
            lblMonto.Text = $"{_recibo.MontoPagado:C}"; // Formato de moneda
            lblMontoTotal.Text = $"{_recibo.MontoPagado:C}";

            lblObservaciones.Text = string.IsNullOrWhiteSpace(_recibo.Observaciones)
                ? "Sin observaciones."
                : $"Observaciones: {_recibo.Observaciones}";

            lblFormaPago.Text = $"Forma de Pago: {_recibo.FormaPago}";

            // Botones
            btnCerrar.Click += (s, e) => CerrarSolicitado?.Invoke(this, EventArgs.Empty);
            btnImprimir.Click += BtnImprimir_Click;
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                var sfd = new SaveFileDialog
                {
                    Title = "Guardar Recibo en PDF",
                    Filter = "Archivo PDF|*.pdf",
                    FileName = $"Recibo-{_recibo.NroComprobante ?? "VISTA_PREVIA"}-{DateTime.Now:yyyyMMdd}.pdf"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // --- ÚNICO CAMBIO AQUÍ ---
                    // Creamos una instancia de nuestro nuevo generador de iText
                    var documentGenerator = new ReciboDocumentIText(_recibo);
                    // Y llamamos a su método para generar el PDF
                    documentGenerator.Generate(sfd.FileName);
                    // ------------------------

                    var result = MessageBox.Show(
                        "¡PDF generado correctamente!",
                        "Proceso completado"
                    );

                    if (result == DialogResult.Yes)
                    {
                        Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                // Construimos un mensaje de error más detallado
                string errorMessage = $"Error principal: {ex.Message}";

                // Si hay una excepción interna, la añadimos. ¡Esta es la pista clave!
                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nDetalles del error: {ex.InnerException.Message}";
                }

                MessageBox.Show(
                    errorMessage,
                    "Error Detallado", // Cambiamos el título para saber que es el nuevo mensaje
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}