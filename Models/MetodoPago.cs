namespace InmoTech.Models
{
    public class MetodoPago
    {
        public int IdMetodoPago { get; set; }
        public string TipoPago { get; set; } = "";
        public string? Descripcion { get; set; }
        public override string ToString() => TipoPago;
    }
}
