using GenerativeAI;
using Google;
using System;
using System.Threading.Tasks;

namespace InmoTech.Services // O el namespace que prefieras para tus servicios
{
    public static class GeminiService
    {
        // El modelo que solicitaste
        private const string ModelName = "gemini-flash-latest";

        public static async Task<string> GenerateContentAsync(string apiKey, string prompt)
        {
            try
            {
                // 1. Inicializa el servicio con tu API Key
                var genAiService = new GenerativeModel(apiKey, model: ModelName);

                // 2. Envía el prompt
                var response = await genAiService.GenerateContentAsync(prompt);

                // 3. Devuelve la respuesta de texto
                return response.Text;
            }
            catch (Exception ex)
            {
                // Manejo básico de errores
                Console.WriteLine($"Error en GeminiService: {ex.Message}");
                return $"Error al conectar con la IA: {ex.Message}";
            }
        }
    }
}