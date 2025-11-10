// ============================================================================
// InmoTech.Repositories.CuotaRepository
// ----------------------------------------------------------------------------
// Repositorio para CRUD de cuotas de contrato. Incluye:
//  - Inserción individual y múltiple
//  - Lectura por contrato (con cálculo en memoria de "Vencida")
//  - Actualización de estado/asignación de pago
//  - Eliminación por contrato
// Documentación breve. No se modifica la lógica original.
// ============================================================================

using InmoTech.Data;
using InmoTech.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace InmoTech.Repositories
{
    /// <summary>
    /// Provee operaciones de inserción, lectura, actualización y borrado
    /// para <see cref="Cuota"/> asociadas a contratos.
    /// </summary>
    public class CuotaRepository
    {
        // ======================================================
        //  REGIONES: Operaciones CRUD
        // ======================================================
        #region Operaciones de Inserción (Create)
        // =====================
        // INSERTAR CUOTA
        // =====================
        /// <summary>
        /// Inserta una cuota individual en la base de datos.
        /// </summary>
        /// <param name="cuota">Entidad <see cref="Cuota"/> a insertar.</param>
        /// <returns>Número de filas afectadas.</returns>
        public int AgregarCuota(Cuota cuota)
        {
            using var conexion = BDGeneral.GetConnection();

            const string sql = @"
            INSERT INTO cuota (id_contrato, nro_cuota, fecha_vencimiento, importe, estado, id_pago)
            VALUES (@IdContrato, @NroCuota, @FechaVencimiento, @Importe, @Estado, @IdPago);";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@IdContrato", SqlDbType.Int).Value = cuota.IdContrato;
            cmd.Parameters.Add("@NroCuota", SqlDbType.Int).Value = cuota.NroCuota;
            cmd.Parameters.Add("@FechaVencimiento", SqlDbType.Date).Value = cuota.FechaVencimiento.Date;
            cmd.Parameters.Add("@Importe", SqlDbType.Decimal).Value = cuota.Importe;
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 50).Value = cuota.Estado;
            cmd.Parameters.Add("@IdPago", SqlDbType.Int).Value = (object?)cuota.IdPago ?? DBNull.Value;

            return cmd.ExecuteNonQuery();
        }

        // =====================
        // INSERTAR VARIAS CUOTAS
        // =====================
        /// <summary>
        /// Inserta un conjunto de cuotas, sumando el total de filas afectadas.
        /// </summary>
        /// <param name="cuotas">Colección de cuotas a insertar.</param>
        /// <returns>Total de filas afectadas.</returns>
        public int AgregarCuotas(IEnumerable<Cuota> cuotas)
        {
            int total = 0;
            foreach (var c in cuotas)
                total += AgregarCuota(c);
            return total;
        }
        #endregion

        #region Operaciones de Consulta (Read)
        // =====================
        // OBTENER CUOTAS POR CONTRATO
        // =====================
        /// <summary>
        /// Devuelve todas las cuotas de un contrato, ordenadas por número.
        /// Marca como "Vencida" en memoria a las "Pendiente" cuyo vencimiento sea anterior a hoy.
        /// (No persiste ese cambio en la base de datos).
        /// </summary>
        /// <param name="idContrato">ID del contrato.</param>
        /// <returns>Lista de <see cref="Cuota"/>.</returns>
        public List<Cuota> ObtenerPorContrato(int idContrato)
        {
            using var conexion = BDGeneral.GetConnection();

            const string sql = @"
            SELECT id_contrato, nro_cuota, fecha_vencimiento, importe, estado, id_pago
            FROM cuota
            WHERE id_contrato = @IdContrato
            ORDER BY nro_cuota;";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@IdContrato", SqlDbType.Int).Value = idContrato;

            using var rd = cmd.ExecuteReader();
            var list = new List<Cuota>();

            // *** CAMBIO: Obtener fecha de hoy ANTES del bucle ***
            DateTime hoy = DateTime.Today;

            // Obtener los índices de las columnas una sola vez
            int ordIdContrato = rd.GetOrdinal("id_contrato");
            int ordNroCuota = rd.GetOrdinal("nro_cuota");
            int ordFechaVenc = rd.GetOrdinal("fecha_vencimiento");
            int ordImporte = rd.GetOrdinal("importe");
            int ordEstado = rd.GetOrdinal("estado");
            int ordIdPago = rd.GetOrdinal("id_pago");

            while (rd.Read())
            {
                var cuota = new Cuota
                {
                    IdContrato = rd.GetInt32(ordIdContrato),
                    NroCuota = rd.GetInt32(ordNroCuota),
                    FechaVencimiento = rd.GetDateTime(ordFechaVenc),
                    Importe = rd.GetDecimal(ordImporte),
                    Estado = rd.GetString(ordEstado),
                    IdPago = rd.IsDBNull(ordIdPago) ? null : rd.GetInt32(ordIdPago)
                };

                // *** INICIO: Lógica para calcular estado Vencida ***
                // Si la cuota está 'Pendiente', no tiene pago asociado, y la fecha de vencimiento es anterior a hoy,
                // se marca como 'Vencida' SOLO en el objeto retornado (no en DB).
                if (cuota.Estado == "Pendiente" && cuota.IdPago == null && cuota.FechaVencimiento.Date < hoy)
                {
                    cuota.Estado = "Vencida";
                }
                // *** FIN: Lógica para calcular estado Vencida ***

                list.Add(cuota);
            }

            return list;
        }
        #endregion

        #region Operaciones de Actualización (Update)
        // =====================
        // ACTUALIZAR ESTADO / PAGO
        // =====================
        /// <summary>
        /// Actualiza el estado textual de una cuota (por contrato y número de cuota).
        /// </summary>
        /// <param name="idContrato">ID del contrato.</param>
        /// <param name="nroCuota">Número de cuota.</param>
        /// <param name="nuevoEstado">Nuevo estado (e.g. "Pendiente", "Pagada").</param>
        /// <returns>Filas afectadas.</returns>
        public int ActualizarEstado(int idContrato, int nroCuota, string nuevoEstado)
        {
            using var conexion = BDGeneral.GetConnection();

            const string sql = @"
            UPDATE cuota
               SET estado = @Estado
             WHERE id_contrato = @IdContrato AND nro_cuota = @NroCuota;";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 50).Value = nuevoEstado;
            cmd.Parameters.Add("@IdContrato", SqlDbType.Int).Value = idContrato;
            cmd.Parameters.Add("@NroCuota", SqlDbType.Int).Value = nroCuota;

            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Asocia un pago a una cuota específica (por contrato y número de cuota).
        /// </summary>
        /// <param name="idContrato">ID del contrato.</param>
        /// <param name="nroCuota">Número de cuota.</param>
        /// <param name="idPago">ID del pago a asociar.</param>
        /// <returns>Filas afectadas.</returns>
        public int AsignarPago(int idContrato, int nroCuota, int idPago)
        {
            using var conexion = BDGeneral.GetConnection();

            const string sql = @"
            UPDATE cuota
               SET id_pago = @IdPago
             WHERE id_contrato = @IdContrato AND nro_cuota = @NroCuota;";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@IdPago", SqlDbType.Int).Value = idPago;
            cmd.Parameters.Add("@IdContrato", SqlDbType.Int).Value = idContrato;
            cmd.Parameters.Add("@NroCuota", SqlDbType.Int).Value = nroCuota;

            return cmd.ExecuteNonQuery();
        }
        #endregion

        #region Operaciones de Eliminación (Delete)
        // =====================
        // ELIMINAR CUOTAS (si se borra contrato)
        // =====================
        /// <summary>
        /// Elimina todas las cuotas asociadas a un contrato.
        /// </summary>
        /// <param name="idContrato">ID del contrato.</param>
        /// <returns>Filas afectadas.</returns>
        public int EliminarPorContrato(int idContrato)
        {
            using var conexion = BDGeneral.GetConnection();
            const string sql = @"DELETE FROM cuota WHERE id_contrato = @IdContrato;";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@IdContrato", SqlDbType.Int).Value = idContrato;

            return cmd.ExecuteNonQuery();
        }
        #endregion
    }
}
