using System;

namespace InmoTech.Models
{
    public class Pago
    {
        // ======================================================
        //  REGIÓN: Propiedades de Persistencia e Identificación
        // ======================================================
        #region Propiedades de Persistencia e Identificación
        public int IdPago { get; set; }                 // PK
        public DateTime FechaPago { get; set; }         // datetime2
        public decimal MontoTotal { get; set; }         // decimal(12,2)
        public string? Detalle { get; set; }            // nro comprobante / notas cortas
        public string Estado { get; set; } = "Activo";  // “Activo / Anulado / etc.”
        public DateTime FechaRegistro { get; set; }     // datetime2
        public bool Activo { get; set; } = true;        // bit
        #endregion

        // ======================================================
        //  REGIÓN: Relaciones (Claves Foráneas y Datos de Apoyo)
        // ======================================================
        #region Relaciones (Claves Foráneas y Datos de Apoyo)
        public int IdUsuario { get; set; }              // FK usuario (quien registra)
        public int IdMetodoPago { get; set; }           // FK metodo_pago
        public int IdContrato { get; set; }             // FK contrato
        public int NroCuota { get; set; }               // FK cuota (parte PK)
        public int IdPersona { get; set; }              // inquilino asociado al contrato
        public string UsuarioCreador { get; set; } = ""; // nombre del usuario
        #endregion
    }
}