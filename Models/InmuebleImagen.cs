using System;

namespace InmoTech.Domain.Models
{
    public class InmuebleImagen
    {
        // ======================================================
        //  REGIÓN: Propiedades de Identificación y Relación
        // ======================================================
        #region Propiedades de Identificación y Relación
        public int IdImagen { get; set; }
        public int IdInmueble { get; set; }
        #endregion

        // ======================================================
        //  REGIÓN: Propiedades de Metadatos de Archivo
        // ======================================================
        #region Propiedades de Metadatos de Archivo
        public string Titulo { get; set; }
        public string NombreArchivo { get; set; }

        public string Ruta { get; set; }
        public string ContentType { get; set; }
        public int? BytesSize { get; set; }
        public int? AnchoPx { get; set; }
        public int? AltoPx { get; set; }
        #endregion

        // ======================================================
        //  REGIÓN: Propiedades de Visualización y Estado
        // ======================================================
        #region Propiedades de Visualización y Estado
        public bool EsPortada { get; set; }
        public short Posicion { get; set; }
        public DateTime CreadoEn { get; set; }
        public bool Activo { get; set; }
        #endregion
    }
}