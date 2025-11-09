using GenerativeAI;
using Google;
using InmoTech.Models; // Para los DTOs
using System;
using System.Collections.Generic;
using System.Text; // Para StringBuilder
using System.Threading.Tasks;

namespace InmoTech.Services
{
    public static class GeminiService
    {
        private const string ModelName = "gemini-flash-latest";

        public static async Task<string> GenerateContentAsync(
            string apiKey,
            string userPrompt,
            List<ContratoDTO> contratos = null,
            List<PagoDTO> pagos = null) // 👈 Nuevos parámetros opcionales
        {
            try
            {
                var genAiService = new GenerativeModel(apiKey, model: ModelName);

                var fullPrompt = new StringBuilder();
                fullPrompt.AppendLine("Eres un asistente de análisis de informes de gestión de alquileres. Responde a las preguntas del usuario basándote en los datos proporcionados. Si no puedes responder con los datos actuales, dilo.");
                fullPrompt.AppendLine();
                fullPrompt.AppendLine("--- Datos disponibles ---");

                if (contratos != null && contratos.Any()) // Usa .Any()
                {
                    fullPrompt.AppendLine("### Contratos:");
                    foreach (var c in contratos)
                    {
                        // CORREGIDO: Formatear el bool a algo legible
                        string estadoTexto = c.Estado ? "Activo" : "Inactivo";
                        fullPrompt.AppendLine($"ID: {c.IdContrato}, Inicio: {c.FechaInicio:d}, Fin: {c.FechaFin:d}, Monto: {c.Monto:C}, Inmueble: {c.IdInmueble}, Inquilino: {c.IdInquilino}, Estado: {estadoTexto}");
                    }
                    fullPrompt.AppendLine();
                }

                if (pagos != null && pagos.Any()) // Usa .Any()
                {
                    fullPrompt.AppendLine("### Pagos:");
                    foreach (var p in pagos)
                    {
                        fullPrompt.AppendLine($"ID: {p.IdPago}, Monto: {p.MontoTotal:C}, Fecha: {p.FechaRegistro:d}, Contrato: {p.IdContrato}, Método: {p.MetodoPago}, Estado: {p.Estado}");
                    }
                    fullPrompt.AppendLine();
                }

                fullPrompt.AppendLine("--- Fin de Datos ---");
                fullPrompt.AppendLine();
                fullPrompt.AppendLine($"Pregunta del usuario: {userPrompt}"); // La pregunta real del usuario

                var response = await genAiService.GenerateContentAsync(fullPrompt.ToString());
                return response.Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GeminiService: {ex.Message}");
                return $"Error al conectar con la IA: {ex.Message}";
            }
        }
    }
}