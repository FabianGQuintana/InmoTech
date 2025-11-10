
using System;

namespace InmoTech.Services
{
    public static class AppNotifier
    {
        // Evento que se dispara cuando algo que afecta el Dashboard cambia
        public static event Action? DashboardDataChanged;

        /// <summary>
        /// Llama a este método en cualquier parte de la aplicación 
        /// (ej. después de guardar un nuevo pago o propiedad)
        /// para notificar al Dashboard que debe actualizarse.
        /// </summary>
        public static void NotifyDashboardChange()
        {
            // Ejecuta el evento de forma segura
            DashboardDataChanged?.Invoke();
        }
    }
}