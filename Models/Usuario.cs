using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InmoTech.Models
{
    public class Usuario
    {
        // ======================================================
        //  REGIÓN: Propiedades de Persistencia e Identificación
        // ======================================================
        #region Propiedades de Persistencia e Identificación
        public int Dni { get; set; }           
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";  
        public string Telefono { get; set; } = "";
        public bool Estado { get; set; }           
        public DateTime FechaNacimiento { get; set; }
        public int IdRol { get; set; }           

        // ==========================================
        //  NUEVAS PROPIEDADES DE AUDITORÍA
        // ==========================================
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreadorDni { get; set; }
        // ==========================================

        #endregion

        // ======================================================
        //  REGIÓN: Propiedades Calculadas
        // ======================================================
        #region Propiedades Calculadas
        public string NombreCompleto => $"{Nombre} {Apellido}";
        #endregion
    }
}