using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InmoTech.Models
{
    public class Usuario
    {
        public int Dni { get; set; }                // PK
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";  // varchar(600)
        public string Telefono { get; set; } = "";
        public bool Estado { get; set; }            // bit
        public DateTime FechaNacimiento { get; set; }
        public int IdRol { get; set; }              // FK a rol

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}