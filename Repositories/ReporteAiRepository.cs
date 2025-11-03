using InmoTech.Data;
using InmoTech.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InmoTech.Repositories
{
    /// <summary>
    /// Repositorio unificado para obtener todos los datos
    /// que se enviarán como contexto al Asistente de IA.
    /// </summary>
    public class ReporteAiRepository : BaseRepository
    {
        /// <summary>
        /// Obtiene la lista de contratos según un rango de fechas.
        /// </summary>
        public async Task<List<ContratoDTO>> GetContratosAsync(DateTime? desde = null, DateTime? hasta = null)
        {
            var contratos = new List<ContratoDTO>();
            var query = "SELECT c.id_contrato, c.fecha_inicio, c.fecha_fin, c.monto, c.id_inmueble, c.id_persona, c.estado FROM contrato c WHERE 1=1";

            if (desde.HasValue)
            {
                query += $" AND c.fecha_inicio >= '{desde.Value:yyyy-MM-dd}'";
            }
            if (hasta.HasValue)
            {
                query += $" AND c.fecha_fin <= '{hasta.Value:yyyy-MM-dd}'";
            }

            return await ExecuteReaderAsync(query, reader =>
            {
                while (reader.Read())
                {
                    contratos.Add(new ContratoDTO
                    {
                        IdContrato = reader.GetInt32(reader.GetOrdinal("id_contrato")),
                        FechaInicio = reader.GetDateTime(reader.GetOrdinal("fecha_inicio")),
                        FechaFin = reader.GetDateTime(reader.GetOrdinal("fecha_fin")),
                        Monto = reader.GetDecimal(reader.GetOrdinal("monto")),
                        IdInmueble = reader.GetInt32(reader.GetOrdinal("id_inmueble")),
                        IdInquilino = reader.GetInt32(reader.GetOrdinal("id_persona")),
                        Estado = reader.GetBoolean(reader.GetOrdinal("estado")) // Corregido a bool (bit en DB)
                    });
                }
                return contratos;
            });
        }

        /// <summary>
        /// Obtiene la lista de pagos según un rango de fechas.
        /// </summary>
        public async Task<List<PagoDTO>> GetPagosAsync(DateTime? desde = null, DateTime? hasta = null)
        {
            var pagos = new List<PagoDTO>();
            // Nota: Se une con metodo_pago para obtener la descripción
            var query = @"
                SELECT p.id_pago, p.monto_total, p.fecha_registro, p.id_contrato, mp.descripcion AS MetodoPago, p.estado
                FROM pago p
                INNER JOIN metodo_pago mp ON p.id_metodo_pago = mp.id_metodo_pago
                WHERE 1=1";

            if (desde.HasValue)
            {
                query += $" AND p.fecha_registro >= '{desde.Value:yyyy-MM-dd}'";
            }
            if (hasta.HasValue)
            {
                query += $" AND p.fecha_registro <= '{hasta.Value:yyyy-MM-dd}'";
            }

            return await ExecuteReaderAsync(query, reader =>
            {
                while (reader.Read())
                {
                    pagos.Add(new PagoDTO
                    {
                        IdPago = reader.GetInt32(reader.GetOrdinal("id_pago")),
                        MontoTotal = reader.GetDecimal(reader.GetOrdinal("monto_total")),
                        FechaRegistro = reader.GetDateTime(reader.GetOrdinal("fecha_registro")),
                        IdContrato = reader.GetInt32(reader.GetOrdinal("id_contrato")),
                        MetodoPago = reader.GetString(reader.GetOrdinal("MetodoPago")),
                        Estado = reader.GetString(reader.GetOrdinal("estado")) // Asumo que el estado de pago es varchar
                    });
                }
                return pagos;
            });
        }
    }
}
