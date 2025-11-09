using System.Data;
using System.Collections.Generic; // Asegurar este using para List<T>

// Clase InmuebleCard pública e independiente
public class InmuebleCard
{
    public int IdInmueble { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int Ambientes { get; set; }
    public string Estado { get; set; } = string.Empty;
}

public class DashboardData
{
    public int TotalPropiedades { get; set; }
    public int TotalInquilinos { get; set; }
    public decimal IngresoTotalMes { get; set; }
    public int PagosPendientes { get; set; }

    // Ahora referencia a la clase InmuebleCard independiente
    public List<InmuebleCard> InmueblesDisponibles { get; set; } = new List<InmuebleCard>();
    public DataTable ContratosVencer { get; set; } = new DataTable();
}