using System;
using System.Collections.Generic;

namespace InmoTech.Domain.Models
{
    public class Inmueble
    {
        public int IdInmueble { get; set; }
        public string Direccion { get; set; } = "";
        public string Tipo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Condiciones { get; set; } = "";
        public int? NroAmbientes { get; set; }
        public bool Amueblado { get; set; }
        /// <summary>
        /// 1 = Activo, 0 = Inactivo (baja lógica)
        /// </summary>
        public byte Estado { get; set; } = 1;

        // Opcional: para traer la galería
        public List<InmuebleImagen> Imagenes { get; set; } = new List<InmuebleImagen>();
    }
}
