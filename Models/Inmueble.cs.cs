using System.Collections.Generic;

namespace InmoTech.Domain.Models
{
    public class Inmueble
    {
        public int IdInmueble { get; set; }
        public string Direccion { get; set; } = "";
        public string Tipo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Condiciones { get; set; } = ""; // Disponible/Reservado/Ocupado/Inactivo
        public int? NroAmbientes { get; set; }
        public bool Amueblado { get; set; }

        /// <summary>
        /// Activo = true (1) / Inactivo = false (0) — baja lógica.
        /// </summary>
        public bool Estado { get; set; } = true;

        public List<InmuebleImagen> Imagenes { get; set; } = new List<InmuebleImagen>();
    }
}
