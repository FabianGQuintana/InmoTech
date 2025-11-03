using System;
using System.Collections.Generic;

namespace InmoTech.Models
{
    // === VIEWMDEL PARA INFO DE DASHBOARD (KPIs) ===
    public class DashboardKpis
    {
        public int TotalPropiedades { get; set; } // (Inmuebles)
        public int TotalInquilinos { get; set; }  // (Personas)
        public int TotalUsuarios { get; set; }    // (Usuarios del sistema)
    }

    // === VIEWMDEL PARA REPORTE DE INQUILINOS ===
    public class InquilinoReporte
    {
        public int Dni { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Email { get; set; } = "";
        public string Direccion { get; set; } = ""; // Dirección del inquilino, no del inmueble
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; } // Activo/Inactivo
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreador { get; set; } = ""; // Nombre completo del usuario que lo creó
    }

    // === VIEWMDEL PARA REPORTE DE INMUEBLES (CORREGIDO) ===
    public class InmuebleReporte
    {
        public int IdInmueble { get; set; }
        public string Tipo { get; set; } = ""; // Casa, Departamento, Oficina, etc.
        public string Direccion { get; set; } = "";
        // public string Localidad { get; set; } = ""; // Eliminado
        // public string Provincia { get; set; } = ""; // Eliminado
        public int NroAmbientes { get; set; } // Usamos nro_ambientes
        // public decimal PrecioAlquiler { get; set; } // Eliminado
        // public bool Disponible { get; set; } // Eliminado
        public bool Amueblado { get; set; } // Usamos amueblado
        public string Condiciones { get; set; } = ""; // Usamos condiciones
        public bool Estado { get; set; } // Usamos estado (activo/inactivo)
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreador { get; set; } = ""; // Nombre completo del usuario que lo creó
    }

    // === VIEWMDEL PARA REPORTE DE USUARIOS ===
    public class UsuarioReporte
    {
        public int Dni { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Email { get; set; } = "";
        public string Rol { get; set; } = ""; // Administrador, Operador, Propietario
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreador { get; set; } = ""; // Nombre completo del usuario que lo creó
    }

    /// <summary>
    /// KPIs para el dashboard del Operador.
    /// </summary>
    public class OperadorDashboardKpis
    {
        public decimal IngresosGenerados { get; set; } // Suma de pagos registrados por el operador
        public int ContratosCreados { get; set; }      // Cantidad de contratos creados por el operador
        public int PagosRegistrados { get; set; }    // Cantidad de pagos registrados por el operador
    }

    /// <summary>
    /// ViewModel para mostrar clientes con cuotas vencidas (morosos)
    /// en contratos gestionados por el operador.
    /// </summary>
    public class ClienteMorosoViewModel
    {
        public int DniInquilino { get; set; }
        public string NombreCompletoInquilino { get; set; } = "";
        public string TelefonoInquilino { get; set; } = ""; // Útil para contacto
        public int IdContrato { get; set; }
        public string DireccionInmueble { get; set; } = ""; // Contexto del contrato
        public int CuotasVencidas { get; set; } // Cuántas cuotas de ESE contrato están vencidas
        public DateTime ProximoVencimiento { get; set; } // La fecha de la cuota vencida más antigua
    }

    /// <summary>
    /// ViewModel para el reporte detallado de Pagos registrados por el Operador.
    /// </summary>
    public class PagoOperadorReporte
    {
        public int IdPago { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal MontoTotal { get; set; }
        public string MetodoPago { get; set; } = "";
        public int IdContrato { get; set; }
        public int NroCuota { get; set; }
        public string Inquilino { get; set; } = ""; // Nombre del inquilino asociado al pago
        public DateTime FechaRegistro { get; set; } // Cuándo se registró el pago
    }

    /// <summary>
    /// ViewModel para el reporte detallado de Contratos creados por el Operador.
    /// </summary>
    public class ContratoOperadorReporte
    {
        public int IdContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal MontoMensual { get; set; }
        public string Inquilino { get; set; } = "";
        public string Inmueble { get; set; } = "";
        public bool Estado { get; set; } // Activo/Inactivo
        public DateTime FechaCreacion { get; set; }
    }

    /// <summary>
    /// Contratos que finalizan dentro de N días (por defecto 60) para el operador actual.
    /// </summary>
    public class ContratoPorVencerViewModel
    {
        public int IdContrato { get; set; }
        public string Inquilino { get; set; } = "";
        public string Inmueble { get; set; } = "";
        public DateTime FechaFin { get; set; }
        public int DiasRestantes { get; set; }
    }
    public class ContratoDTO
    {
        public int IdContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Monto { get; set; }
        public int IdInmueble { get; set; }
        public int IdInquilino { get; set; } // Asumo que id_persona se refiere al inquilino
        public bool Estado { get; set; } // 'Pagado', 'Activo', etc. (varchar en DB) -> CORREGIDO a bool (bit en DB)
    }
    public class PagoDTO
    {
        public int IdPago { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdContrato { get; set; }
        public string MetodoPago { get; set; } // Descripción del método de pago
        public string Estado { get; set; } // 'Pagado' (varchar en DB)
    }
}