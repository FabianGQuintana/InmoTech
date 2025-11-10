using System;

namespace InmoTech.Models
{
    public class Contrato
    {
        // ======================================================
        //  REGIÓN: Propiedades de Persistencia (BD)
        // ======================================================
        #region Propiedades de Persistencia (Base de Datos)
        public int IdContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }  
        public decimal Monto { get; set; }

        public int IdInmueble { get; set; }
        public int IdPersona { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int DniUsuario { get; set; }
        public bool Estado { get; set; }
        #endregion

        // ======================================================
        //  REGIÓN: Propiedades Extendidas (Para la UI/Reports)
        // ======================================================
        #region Propiedades Extendidas (Para la UI/Reports)
        public string? NombreInquilino { get; set; }
        public string? NombreUsuario { get; set; }
        public string? DireccionInmueble { get; set; }
        #endregion
    }
}