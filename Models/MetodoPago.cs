namespace InmoTech.Models
{
    public class MetodoPago
    {
        // ======================================================
        //  REGIÓN: Propiedades de Persistencia (BD)
        // ======================================================
        #region Propiedades de Persistencia
        public int IdMetodoPago { get; set; }
        public string TipoPago { get; set; } = "";
        public string? Descripcion { get; set; }
        #endregion

        // ======================================================
        //  REGIÓN: Utilitarios
        // ======================================================
        #region Utilitarios
        public override string ToString() => TipoPago;
        #endregion
    }
}