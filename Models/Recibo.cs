using System;

namespace InmoTech.Models
{
    public class Recibo
    {
        // ======================================================
        //  REGIÓN: Propiedades de Persistencia (Base de Datos)
        // ======================================================
        #region Propiedades de Persistencia (Base de Datos)
        public int IdRecibo { get; set; }
        public DateTime FechaEmision { get; set; }
        public string NroComprobante { get; set; }
        public int IdPago { get; set; }
        public int IdUsuarioEmisor { get; set; }
        public int IdInquilino { get; set; }
        public int IdInmueble { get; set; }
        public string FormaPago { get; set; }
        public string? Observaciones { get; set; }
        #endregion

        // ======================================================
        //  REGIÓN: Propiedades Extendidas (Para la UI/Documento)
        // ======================================================
        #region Propiedades Extendidas (Para la UI/Documento)
        // --- Propiedades extendidas (no están en la tabla, pero son útiles para mostrar) ---
        public string? NombreInquilino { get; set; }
        public string? DireccionInmueble { get; set; }
        public string? UsuarioEmisor { get; set; }
        public decimal MontoPagado { get; set; }
        public string? Concepto { get; set; }
        #endregion
    }
}