using InmoTech.Data;
using InmoTech.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace InmoTech.Repositories
{
    public class CuotaRepository
    {
        // ======================================================
        //  REGIONES: Operaciones CRUD
        // ======================================================
        #region Operaciones de Inserción (Create)
        // =====================
        // INSERTAR CUOTA
        // =====================
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

            while (rd.Read())
            {
                list.Add(new Cuota
                {
                    IdContrato = rd.GetInt32(0),
                    NroCuota = rd.GetInt32(1),
                    FechaVencimiento = rd.GetDateTime(2),
                    Importe = rd.GetDecimal(3),
                    Estado = rd.GetString(4),
                    IdPago = rd.IsDBNull(5) ? null : rd.GetInt32(5)
                });
            }

            return list;
        }
        #endregion

        #region Operaciones de Actualización (Update)
        // =====================
        // ACTUALIZAR ESTADO / PAGO
        // =====================
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