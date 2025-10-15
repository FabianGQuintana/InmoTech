using System;

namespace InmoTech.Models
{
    public class Cuota
    {
        // ======================================================
        //  REGIÓN: Propiedades de Persistencia (BD)
        // ======================================================
        #region Propiedades de Persistencia (Base de Datos)
        public int IdContrato { get; set; }      // PK (parte 1)
        public int NroCuota { get; set; }        // PK (parte 2)
        public DateTime FechaVencimiento { get; set; }
        public decimal Importe { get; set; }     // decimal(12,2)
        public string Estado { get; set; } = ""; // varchar(50)
        public int? IdPago { get; set; }         // FK opcional a pago
        #endregion
    }
}