using System; // Agregado para DateTime
using System.Collections.Generic;

namespace InmoTech.Domain.Models
{
    public class Inmueble
    {
        // ======================================================
        //  REGIÓN: Propiedades de Persistencia y Características
        // ======================================================
        #region Propiedades de Persistencia y Características
        public int IdInmueble { get; set; }
        public string Direccion { get; set; } = "";
        public string Tipo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Condiciones { get; set; } = ""; // Disponible/Reservado/Ocupado/Inactivo
        public int? NroAmbientes { get; set; }
        public bool Amueblado { get; set; }
        #endregion

        // ======================================================
        //  REGIÓN: Estado y Relaciones
        // ======================================================
        #region Estado y Relaciones
        public bool Estado { get; set; } = true;

        public List<InmuebleImagen> Imagenes { get; set; } = new List<InmuebleImagen>();
        #endregion

        // ======================================================
        //  REGIÓN: Auditoría (NUEVO)
        // ======================================================
        #region Auditoría
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreadorDni { get; set; }
        #endregion
    }
}