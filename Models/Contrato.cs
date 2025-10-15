using System;

namespace InmoTech.Models
{
    public class Contrato
    {
        public int IdContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }   // NOT NULL

        public decimal Monto { get; set; }
        public string? Condiciones { get; set; }
        public int IdInmueble { get; set; }
        public int IdPersona { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int DniUsuario { get; set; }
        public bool Estado { get; set; }

        // Para la UI
        public string? NombreInquilino { get; set; }

        public string? NombreUsuario { get; set; }
        public string? DireccionInmueble { get; set; }
    }
}
