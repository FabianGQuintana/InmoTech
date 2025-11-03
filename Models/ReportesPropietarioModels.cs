using System;

namespace InmoTech.Models
{
    // ============================
    //  DTOs de lectura de reportes
    // ============================

    public class OperadorKpi
    {
        public string Operador { get; set; } = string.Empty;
        public int CantidadPagos { get; set; }
        public decimal TotalIngresos { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }

    public class InmuebleIngreso
    {
        public int IdInmueble { get; set; }
        public string Inmueble { get; set; } = string.Empty; // dirección
        public string Tipo { get; set; } = string.Empty;
        public decimal Ingresos { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }

    public class EstadoInmuebles
    {
        public int CantidadLibres { get; set; }
        public int CantidadOcupados { get; set; }
        public DateTime FechaCorte { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }

    public class MetodoPagoKpi
    {
        public string MetodoPago { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }

    public class InquilinoIngreso
    {
        public int IdPersona { get; set; }
        public string Inquilino { get; set; } = string.Empty;
        public decimal TotalPagado { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }

    public class CuotaVencida
    {
        public int IdContrato { get; set; }
        public int NroCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int DiasAtraso { get; set; }
        public string Inquilino { get; set; } = string.Empty;
        public int IdInmueble { get; set; }
        public string Inmueble { get; set; } = string.Empty; // dirección
        public decimal Importe { get; set; }
    }

    public class ContratoPorVencer
    {
        public int IdContrato { get; set; }
        public DateTime FechaFin { get; set; }
        public string Inquilino { get; set; } = string.Empty;
        public int IdInmueble { get; set; }
        public string Inmueble { get; set; } = string.Empty; // dirección
        public int DiasParaVencer { get; set; }
    }

    public class ProyeccionMensual
    {
        public string Periodo { get; set; } = string.Empty; // yyyy-MM
        public decimal MontoProyectado { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }

    public class InmuebleCondicion
    {
        public int IdInmueble { get; set; }        // se oculta en la grilla
        public string Inmueble { get; set; } = ""; // direccion
        public string Tipo { get; set; } = "";
        public int Ambientes { get; set; }         // nro_ambientes
        public string Condicion { get; set; } = ""; // columna 'Condiciones'
    }

    public class PagoRealizado
    {
        public int IdPago { get; set; }
        public DateTime FechaPago { get; set; }
        public int IdContrato { get; set; }
        public int IdInmueble { get; set; }
        public string Inmueble { get; set; } = string.Empty;     // dirección
        public int IdPersona { get; set; }
        public string Inquilino { get; set; } = string.Empty;    // Apellido, Nombre
        public int NroCuota { get; set; }
        public string Detalle { get; set; } = string.Empty;
        public string Operador { get; set; } = string.Empty;     // nombre + apellido
        public int IdUsuario { get; set; }                       // para filtrar
        public string MetodoPago { get; set; } = string.Empty;   // texto (mp.tipo_pago)
        public int IdMetodoPago { get; set; }                    // para filtrar
        public decimal MontoTotal { get; set; }
    }

    // Para llenar combos
    public class OpcionCombo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public override string ToString() => Nombre;
    }
}
