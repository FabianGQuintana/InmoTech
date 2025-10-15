using InmoTech.Data;
using InmoTech.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace InmoTech.Repositories
{
    public class MetodoPagoRepository
    {
        public List<MetodoPago> ObtenerTodos()
        {
            using var cn = BDGeneral.GetConnection();

            const string sql = @"
                SELECT id_metodo_pago, tipo_pago, descripcion
                FROM metodo_pago
                ORDER BY id_metodo_pago;";

            using var cmd = new SqlCommand(sql, cn);
            using var rd = cmd.ExecuteReader();

            var list = new List<MetodoPago>();
            while (rd.Read())
            {
                list.Add(new MetodoPago
                {
                    IdMetodoPago = rd.GetInt32(0),
                    TipoPago = rd.GetString(1),
                    Descripcion = rd.IsDBNull(2) ? null : rd.GetString(2)
                });
            }

            return list;
        }

        public MetodoPago? ObtenerPorId(int id)
        {
            using var cn = BDGeneral.GetConnection();

            const string sql = @"
                SELECT id_metodo_pago, tipo_pago, descripcion
                FROM metodo_pago
                WHERE id_metodo_pago = @id;";

            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;

            return new MetodoPago
            {
                IdMetodoPago = rd.GetInt32(0),
                TipoPago = rd.GetString(1),
                Descripcion = rd.IsDBNull(2) ? null : rd.GetString(2)
            };
        }
    }
}
