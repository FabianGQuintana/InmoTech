using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InmoTech.Models
{
    public class Inquilino
    {
        public int Dni { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Email { get; set; } = "";
        public string Direccion { get; set; } = "";
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; } // Activo/Inactivo

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}
