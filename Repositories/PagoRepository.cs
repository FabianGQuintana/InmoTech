using InmoTech.Data;
using InmoTech.Models;
using InmoTech.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using InmoTech.Services;

namespace InmoTech.Repositories
{
    public class PagoRepository
    {
        // ======================================================
        //  REGIÓN: Operaciones de Creación (Create)
        // ======================================================
        #region Operaciones de Creación (Create)
        public int Agregar(Pago p)
        {
            using var cn = BDGeneral.GetConnection();
            const string sql = @"
                INSERT INTO pago
                (fecha_pago, monto_total, id_usuario, id_metodo_pago, id_contrato, nro_cuota,
                 detalle, estado, fecha_registro, id_persona, usuario_creador, activo)
                OUTPUT INSERTED.id_pago
                VALUES
                (@FechaPago, @Monto, @IdUsuario, @IdMetodo, @IdContrato, @NroCuota,
                 @Detalle, @Estado, @FechaRegistro, @IdPersona, @UsuarioCreador, @Activo);";

            using var cmd = new SqlCommand(sql, cn);

            cmd.Parameters.Add("@FechaPago", SqlDbType.DateTime2).Value = p.FechaPago;
            var pm = cmd.Parameters.Add("@Monto", SqlDbType.Decimal); pm.Precision = 12; pm.Scale = 2; pm.Value = p.MontoTotal;
            cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = p.IdUsuario;
            cmd.Parameters.Add("@IdMetodo", SqlDbType.Int).Value = p.IdMetodoPago;
            cmd.Parameters.Add("@IdContrato", SqlDbType.Int).Value = p.IdContrato;
            cmd.Parameters.Add("@NroCuota", SqlDbType.Int).Value = p.NroCuota;
            cmd.Parameters.Add("@Detalle", SqlDbType.VarChar, 200).Value = (object?)p.Detalle ?? DBNull.Value;
            cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 20).Value = p.Estado;
            cmd.Parameters.Add("@FechaRegistro", SqlDbType.DateTime2).Value = p.FechaRegistro;
            cmd.Parameters.Add("@IdPersona", SqlDbType.Int).Value = p.IdPersona;
            cmd.Parameters.Add("@UsuarioCreador", SqlDbType.VarChar, 50).Value = p.UsuarioCreador;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = p.Activo;

            int newId = Convert.ToInt32(cmd.ExecuteScalar());

            // NOTIFICACIÓN: Se agrega un pago, lo que afecta Ingresos y Pendientes.
            if (newId > 0)
            {
                AppNotifier.NotifyDashboardChange();
            }

            return newId;
        }
        #endregion

        // ======================================================
        //  REGIÓN: Operaciones de Consulta (Read)
        // ======================================================
        #region Operaciones de Consulta (Read)
        public Pago? ObtenerPorId(int idPago)
        {
            using var cn = BDGeneral.GetConnection();
            const string sql = @"
            SELECT id_pago, fecha_pago, monto_total, id_usuario, id_metodo_pago, id_contrato, nro_cuota,
                   detalle, estado, fecha_registro, id_persona, usuario_creador, activo
            FROM pago WHERE id_pago = @Id;";

            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = idPago;

            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;

            return new Pago
            {
                IdPago = rd.GetInt32(0),
                FechaPago = rd.GetDateTime(1),
                MontoTotal = rd.GetDecimal(2),
                IdUsuario = rd.GetInt32(3),
                IdMetodoPago = rd.GetInt32(4),
                IdContrato = rd.GetInt32(5),
                NroCuota = rd.GetInt32(6),
                Detalle = rd.IsDBNull(7) ? null : rd.GetString(7),
                Estado = rd.GetString(8),
                FechaRegistro = rd.GetDateTime(9),
                IdPersona = rd.GetInt32(10),
                UsuarioCreador = rd.GetString(11),
                Activo = rd.GetBoolean(12)
            };
        }

        public List<Pago> ObtenerPorContrato(int idContrato)
        {
            using var cn = BDGeneral.GetConnection();
            const string sql = @"
                SELECT id_pago, fecha_pago, monto_total, id_usuario, id_metodo_pago, id_contrato, nro_cuota,
                       detalle, estado, fecha_registro, id_persona, usuario_creador, activo
                FROM pago WHERE id_contrato = @IdContrato
                ORDER BY fecha_pago DESC;";

            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@IdContrato", SqlDbType.Int).Value = idContrato;

            using var rd = cmd.ExecuteReader();
            var list = new List<Pago>();
            while (rd.Read())
            {
                list.Add(new Pago
                {
                    IdPago = rd.GetInt32(0),
                    FechaPago = rd.GetDateTime(1),
                    MontoTotal = rd.GetDecimal(2),
                    IdUsuario = rd.GetInt32(3),
                    IdMetodoPago = rd.GetInt32(4),
                    IdContrato = rd.GetInt32(5),
                    NroCuota = rd.GetInt32(6),
                    Detalle = rd.IsDBNull(7) ? null : rd.GetString(7),
                    Estado = rd.GetString(8),
                    FechaRegistro = rd.GetDateTime(9),
                    IdPersona = rd.GetInt32(10),
                    UsuarioCreador = rd.GetString(11),
                    Activo = rd.GetBoolean(12)
                });
            }
            return list;
        }
        #endregion
    }
}