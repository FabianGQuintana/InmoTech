using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InmoTech.Models
{
    public class Inquilino
    {
        // ======================================================
        //  REGIÓN: Propiedades de Persistencia e Identificación
        // ======================================================
        #region Propiedades de Persistencia e Identificación
        public int Dni { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Email { get; set; } = "";
        public string Direccion { get; set; } = "";
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; } // Activo/Inactivo

        // ==========================================
        //  NUEVAS PROPIEDADES DE AUDITORÍA
        // ==========================================
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreadorDni { get; set; } // FK a usuario(dni)
        // ==========================================

        #endregion

        // ======================================================
        //  REGIÓN: Propiedades Calculadas (ViewModel Helper)
        // ======================================================
        #region Propiedades Calculadas (ViewModel Helper)
        public string NombreCompleto => $"{Nombre} {Apellido}";
        #endregion
    }
}