using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InmoTech.Models
{
    public sealed class InquilinoLite
    {
        // ======================================================
        //  REGIÓN: Propiedades de Transferencia de Datos
        // ======================================================
        #region Propiedades de Transferencia de Datos
        public int Dni { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Email { get; set; } = "";
        public bool Estado { get; set; } // Activo/Inactivo
        #endregion

        // ======================================================
        //  REGIÓN: Propiedades Calculadas
        // ======================================================
        #region Propiedades Calculadas
        public string NombreCompleto => $"{Apellido}, {Nombre}";
        #endregion
    }
}