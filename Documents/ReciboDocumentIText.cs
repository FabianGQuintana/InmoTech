using InmoTech.Models;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders; // Necesario para los bordes de la tabla

namespace InmoTech.Documents
{
    public class ReciboDocumentIText
    {
        // ======================================================
        //  REGIÓN: Propiedades y Constructor
        // ======================================================
        #region Propiedades y Constructor
        private readonly Recibo _recibo;

        public ReciboDocumentIText(Recibo recibo)
        {
            _recibo = recibo;
        }
        #endregion

        // ======================================================
        //  REGIÓN: Generación de Documento
        // ======================================================
        #region Generación de Documento
        public void Generate(string filePath)
        {
            // La estructura 'using' se encarga de cerrar todo automáticamente
            using (var writer = new PdfWriter(filePath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    using (var document = new Document(pdf))
                    {
                        // --- A partir de aquí, es tu código con algunas mejoras ---

                        // 2. Definir las fuentes que usaremos
                        var fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                        var fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                        // --- Cabecera ---
                        // Usamos una tabla para alinear el título a la izquierda y el N° a la derecha
                        var tableHeader = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1 })).UseAllAvailableWidth();
                        tableHeader.SetBorder(Border.NO_BORDER);

                        var cellTitulo = new Cell().Add(new Paragraph("RECIBO DE PAGO").SetFont(fontBold).SetFontSize(18));
                        cellTitulo.SetBorder(Border.NO_BORDER);
                        tableHeader.AddCell(cellTitulo);

                        var cellInfo = new Cell()
                          .Add(new Paragraph(_recibo.NroComprobante ?? "RECIBO Nº: PENDIENTE").SetFont(fontBold).SetFontSize(12))
                          .Add(new Paragraph($"Fecha de Emisión: {_recibo.FechaEmision:dd/MM/yyyy}").SetFont(fontNormal).SetFontSize(9).SetFontColor(ColorConstants.GRAY));
                        cellInfo.SetTextAlignment(TextAlignment.RIGHT);
                        cellInfo.SetBorder(Border.NO_BORDER);
                        tableHeader.AddCell(cellInfo);
                        document.Add(tableHeader);

                        // Línea separadora
                        document.Add(new Paragraph("").SetMarginTop(10));
                        document.Add(new LineSeparator(new iText.Kernel.Pdf.Canvas.Draw.SolidLine(1f)).SetFontColor(ColorConstants.LIGHT_GRAY));
                        document.Add(new Paragraph("").SetMarginBottom(10));

                        // --- Detalles del Cliente ---
                        document.Add(new Paragraph($"Recibí de: {_recibo.NombreInquilino}").SetFont(fontNormal).SetFontSize(10));
                        document.Add(new Paragraph($"En concepto de alquiler del inmueble sito en: {_recibo.DireccionInmueble}").SetFont(fontNormal).SetFontSize(10));

                        document.Add(new Paragraph("").SetMarginBottom(20)); // Espacio

                        // --- Tabla de Detalles ---
                        var table = new Table(UnitValue.CreatePercentArray(new float[] { 3, 1 })).UseAllAvailableWidth();

                        // Cabecera de la tabla
                        table.AddHeaderCell(new Cell().Add(new Paragraph("CONCEPTO").SetFont(fontBold)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 1)));
                        table.AddHeaderCell(new Cell().Add(new Paragraph("MONTO").SetFont(fontBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 1)));

                        // Fila de datos
                        table.AddCell(new Cell().Add(new Paragraph(_recibo.Concepto).SetFont(fontNormal)).SetBorder(Border.NO_BORDER).SetPaddingTop(5));
                        table.AddCell(new Cell().Add(new Paragraph($"{_recibo.MontoPagado:C}").SetFont(fontNormal).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER).SetPaddingTop(5));

                        // Fila de total
                        table.AddCell(new Cell().Add(new Paragraph("TOTAL").SetFont(fontBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER).SetBorderTop(new SolidBorder(ColorConstants.LIGHT_GRAY, 1)).SetPaddingTop(5));
                        table.AddCell(new Cell().Add(new Paragraph($"{_recibo.MontoPagado:C}").SetFont(fontBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER).SetBorderTop(new SolidBorder(ColorConstants.LIGHT_GRAY, 1)).SetPaddingTop(5));

                        document.Add(table);

                        // --- Pie de página del contenido ---
                        // (Este código lo he movido a una tabla para mejor alineación)
                        var tableFooter = new Table(1).UseAllAvailableWidth().SetMarginTop(25);
                        tableFooter.SetBorder(Border.NO_BORDER);

                        var cellFormaPago = new Cell().Add(new Paragraph($"Forma de Pago: {_recibo.FormaPago}").SetFont(fontNormal).SetFontSize(10));
                        cellFormaPago.SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER);
                        tableFooter.AddCell(cellFormaPago);

                        if (!string.IsNullOrWhiteSpace(_recibo.Observaciones))
                        {
                            var cellObservaciones = new Cell().Add(new Paragraph($"Observaciones: {_recibo.Observaciones}").SetFont(fontNormal).SetFontSize(10));
                            cellObservaciones.SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER);
                            tableFooter.AddCell(cellObservaciones);
                        }
                        document.Add(tableFooter);

                        // El 'document.Close()' ya no es necesario aquí. 
                        // El bloque 'using' lo gestiona todo al finalizar.
                    }
                }
            }
        }
        #endregion
    }
}