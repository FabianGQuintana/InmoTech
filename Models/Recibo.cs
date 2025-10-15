using System;

namespace InmoTech.Models
{
    public class Recibo
    {
        public int IdRecibo { get; set; }
        public DateTime FechaEmision { get; set; }
        public string NroComprobante { get; set; }
        public int IdPago { get; set; }
        public int IdUsuarioEmisor { get; set; }
        public int IdInquilino { get; set; }
        public int IdInmueble { get; set; }
        public string FormaPago { get; set; }
        public string? Observaciones { get; set; }

        // --- Propiedades extendidas (no están en la tabla, pero son útiles para mostrar) ---
        public string? NombreInquilino { get; set; }
        public string? DireccionInmueble { get; set; }
        public string? UsuarioEmisor { get; set; }
        public decimal MontoPagado { get; set; }
        public string? Concepto { get; set; }
    }
}