using InmoTech.Data;
using InmoTech.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace InmoTech.Repositories
{
    /// <summary>
    /// Repositorio para gestionar recibos: creación, consulta por pago y utilitarios.
    /// </summary>
    public class ReciboRepository
    {
        // ======================================================
        //  REGIÓN: Operaciones de Creación (Create)
        // ======================================================
        #region Operaciones de Creación (Create)

        /// <summary>
        /// Inserta un recibo y devuelve el ID generado.
        /// </summary>
        /// <param name="recibo">Entidad Recibo a guardar.</param>
        /// <returns>ID del recibo insertado.</returns>
        public int Agregar(Recibo recibo)
        {
            using var cn = BDGeneral.GetConnection();
            const string sql = @"
                INSERT INTO dbo.recibo
                    (fecha_emision, nro_comprobante, id_pago, id_usuario_emisor,
                     id_inquilino, id_inmueble, forma_pago, observaciones)
                OUTPUT INSERTED.id_recibo
                VALUES
                    (@FechaEmision, @NroComprobante, @IdPago, @IdUsuarioEmisor,
                     @IdInquilino, @IdInmueble, @FormaPago, @Observaciones);";

            using var cmd = new SqlCommand(sql, cn);

            cmd.Parameters.Add("@FechaEmision", SqlDbType.Date).Value = recibo.FechaEmision.Date;
            cmd.Parameters.Add("@NroComprobante", SqlDbType.VarChar, 50).Value = recibo.NroComprobante;
            cmd.Parameters.Add("@IdPago", SqlDbType.Int).Value = recibo.IdPago;
            cmd.Parameters.Add("@IdUsuarioEmisor", SqlDbType.Int).Value = recibo.IdUsuarioEmisor;
            cmd.Parameters.Add("@IdInquilino", SqlDbType.Int).Value = recibo.IdInquilino;
            cmd.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = recibo.IdInmueble;
            cmd.Parameters.Add("@FormaPago", SqlDbType.VarChar, 50).Value = recibo.FormaPago;
            cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 250).Value = (object?)recibo.Observaciones ?? DBNull.Value;

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        #endregion

        // ======================================================
        //  REGIÓN: Operaciones de Consulta (Read)
        // ======================================================
        #region Operaciones de Consulta (Read)

        /// <summary>
        /// Obtiene un recibo por el <c>id_pago</c> asociado, incluyendo datos relacionados.
        /// </summary>
        /// <param name="idPago">ID del pago vinculado al recibo.</param>
        /// <returns>Instancia de <see cref="Recibo"/> o <c>null</c> si no existe.</returns>
        public Recibo? ObtenerPorIdPago(int idPago)
        {
            using var cn = BDGeneral.GetConnection();
            // Usamos JOINs para traer datos adicionales y mostrarlos en el recibo
            const string sql = @"
                SELECT
                    r.id_recibo, r.fecha_emision, r.nro_comprobante, r.id_pago,
                    r.id_usuario_emisor, r.id_inquilino, r.id_inmueble, r.forma_pago, r.observaciones,
                    pago.monto_total,
                    (inquilino.apellido + ' ' + inquilino.nombre) AS NombreInquilino,
                    inmueble.direccion AS DireccionInmueble,
                    (emisor.apellido + ' ' + emisor.nombre) AS UsuarioEmisor,
                    ('Pago de alquiler - Cuota N°' + CONVERT(varchar, pago.nro_cuota)) as Concepto
                FROM dbo.recibo r
                JOIN dbo.pago ON r.id_pago = pago.id_pago
                JOIN dbo.persona inquilino ON r.id_inquilino = inquilino.id_persona
                JOIN dbo.inmueble ON r.id_inmueble = inmueble.id_inmueble
                JOIN dbo.usuario emisor ON r.id_usuario_emisor = emisor.dni
                WHERE r.id_pago = @IdPago;";

            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@IdPago", SqlDbType.Int).Value = idPago;

            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;

            return new Recibo
            {
                IdRecibo = rd.GetInt32(rd.GetOrdinal("id_recibo")),
                FechaEmision = rd.GetDateTime(rd.GetOrdinal("fecha_emision")),
                NroComprobante = rd.GetString(rd.GetOrdinal("nro_comprobante")),
                IdPago = rd.GetInt32(rd.GetOrdinal("id_pago")),
                IdUsuarioEmisor = rd.GetInt32(rd.GetOrdinal("id_usuario_emisor")),
                IdInquilino = rd.GetInt32(rd.GetOrdinal("id_inquilino")),
                IdInmueble = rd.GetInt32(rd.GetOrdinal("id_inmueble")),
                FormaPago = rd.GetString(rd.GetOrdinal("forma_pago")),
                Observaciones = rd.IsDBNull(rd.GetOrdinal("observaciones")) ? null : rd.GetString(rd.GetOrdinal("observaciones")),
                MontoPagado = rd.GetDecimal(rd.GetOrdinal("monto_total")),
                NombreInquilino = rd.GetString(rd.GetOrdinal("NombreInquilino")),
                DireccionInmueble = rd.GetString(rd.GetOrdinal("DireccionInmueble")),
                UsuarioEmisor = rd.GetString(rd.GetOrdinal("UsuarioEmisor")),
                Concepto = rd.GetString(rd.GetOrdinal("Concepto"))
            };
        }
        #endregion

        // ======================================================
        //  REGIÓN: Utilitarios
        // ======================================================
        #region Utilitarios

        /// <summary>
        /// Genera un número de comprobante único, correlativo y formateado como <c>R-00000001</c>.
        /// </summary>
        /// <remarks>
        /// Calcula <c>MAX(id_recibo)+1</c>; con <c>ISNULL</c> para iniciar en 1 si la tabla está vacía.
        /// </remarks>
        /// <returns>Número de comprobante formateado.</returns>
        public string GenerarNumeroComprobante()
        {
            using var cn = BDGeneral.GetConnection();
            // ISNULL es importante para que si la tabla está vacía, empiece en 1.
            const string sql = "SELECT ISNULL(MAX(id_recibo), 0) + 1 FROM dbo.recibo;";

            using var cmd = new SqlCommand(sql, cn);
            int proximoId = Convert.ToInt32(cmd.ExecuteScalar());

            // El formato D8 asegura 8 dígitos con ceros a la izquierda.
            return $"R-{proximoId:D8}";
        }
        #endregion
    }
}
