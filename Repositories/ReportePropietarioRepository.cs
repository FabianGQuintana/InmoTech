using InmoTech.Data;
using InmoTech.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace InmoTech.Repositories
{
    public class ReportePropietarioRepository
    {
        // =======================================
        //  Constantes de estados de negocio
        // =======================================
        private const string ESTADO_PAGO_PAGADO = "pagado";
        private const string ESTADO_PAGO_ANULADO = "anulado";

        private const string ESTADO_CUOTA_PENDIENTE = "pendiente";
        private const string ESTADO_CUOTA_PAGADA = "pagada";

        // =======================================
        //  1) Ingresos por Operador (usuario)
        //     Filtra por p.fecha_pago BETWEEN @Desde AND @Hasta
        // =======================================
        public List<OperadorKpi> ObtenerIngresosPorOperador(DateTime desde, DateTime hasta)
        {
            var lista = new List<OperadorKpi>();

            const string sql = @"
                SELECT 
                    (u.nombre + ' ' + u.apellido) AS Operador,
                    COUNT(p.id_pago) AS CantidadPagos,
                    ISNULL(SUM(p.monto_total),0) AS TotalIngresos
                FROM pago p
                INNER JOIN usuario u ON u.dni = p.id_usuario
                WHERE p.estado = @EstadoPagado
                  AND p.fecha_pago BETWEEN @Desde AND @Hasta
                GROUP BY u.nombre, u.apellido
                ORDER BY TotalIngresos DESC;";

            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@EstadoPagado", SqlDbType.VarChar, 50).Value = ESTADO_PAGO_PAGADO;
            cmd.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@Hasta", SqlDbType.DateTime).Value = hasta;

            using var rd = cmd.ExecuteReader();
            int ordOperador = rd.GetOrdinal("Operador");
            int ordCant = rd.GetOrdinal("CantidadPagos");
            int ordTotal = rd.GetOrdinal("TotalIngresos");

            while (rd.Read())
            {
                lista.Add(new OperadorKpi
                {
                    Operador = rd.IsDBNull(ordOperador) ? "" : rd.GetString(ordOperador),
                    CantidadPagos = rd.IsDBNull(ordCant) ? 0 : rd.GetInt32(ordCant),
                    TotalIngresos = rd.IsDBNull(ordTotal) ? 0m : rd.GetDecimal(ordTotal),
                    Desde = desde,
                    Hasta = hasta
                });
            }

            return lista;
        }

        // =======================================
        //  2) Ranking de Inmuebles por ingresos
        //     Filtra por p.fecha_pago BETWEEN @Desde AND @Hasta
        // =======================================
        public List<InmuebleIngreso> ObtenerRankingInmuebles(DateTime desde, DateTime hasta)
        {
            var lista = new List<InmuebleIngreso>();

            const string sql = @"
                SELECT 
                    i.id_inmueble,
                    i.direccion AS Inmueble,
                    i.tipo,
                    ISNULL(SUM(p.monto_total),0) AS Ingresos
                FROM pago p
                INNER JOIN contrato c ON c.id_contrato = p.id_contrato
                INNER JOIN inmueble i ON i.id_inmueble = c.id_inmueble
                WHERE p.estado = @EstadoPagado
                  AND p.fecha_pago BETWEEN @Desde AND @Hasta
                GROUP BY i.id_inmueble, i.direccion, i.tipo
                ORDER BY Ingresos DESC;";

            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@EstadoPagado", SqlDbType.VarChar, 50).Value = ESTADO_PAGO_PAGADO;
            cmd.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@Hasta", SqlDbType.DateTime).Value = hasta;

            using var rd = cmd.ExecuteReader();
            int ordId = rd.GetOrdinal("id_inmueble");
            int ordInm = rd.GetOrdinal("Inmueble");
            int ordTipo = rd.GetOrdinal("tipo");
            int ordIng = rd.GetOrdinal("Ingresos");

            while (rd.Read())
            {
                lista.Add(new InmuebleIngreso
                {
                    IdInmueble = rd.GetInt32(ordId),
                    Inmueble = rd.IsDBNull(ordInm) ? "" : rd.GetString(ordInm),
                    Tipo = rd.IsDBNull(ordTipo) ? "" : rd.GetString(ordTipo),
                    Ingresos = rd.IsDBNull(ordIng) ? 0m : rd.GetDecimal(ordIng),
                    Desde = desde,
                    Hasta = hasta
                });
            }

            return lista;
        }

        // =======================================
        //  3) Inmuebles libres / ocupados (a corte)
        //     Usa @Hasta como fecha de corte para el snapshot
        //     contrato.estado es BIT (1 = activo)
        // =======================================
        public EstadoInmuebles ObtenerEstadoInmuebles(DateTime desde, DateTime hasta)
        {
            var res = new EstadoInmuebles { Desde = desde, Hasta = hasta, FechaCorte = hasta };

            const string sql = @"
                SELECT 
                    SUM(CASE WHEN c.id_contrato IS NULL THEN 1 ELSE 0 END) AS Libres,
                    SUM(CASE WHEN c.id_contrato IS NOT NULL THEN 1 ELSE 0 END) AS Ocupados
                FROM inmueble i
                LEFT JOIN contrato c 
                    ON c.id_inmueble = i.id_inmueble
                   AND c.estado = 1
                   AND @Corte BETWEEN c.fecha_inicio AND c.fecha_fin;";

            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@Corte", SqlDbType.DateTime).Value = hasta;

            using var rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                res.CantidadLibres = rd.IsDBNull(0) ? 0 : rd.GetInt32(0);
                res.CantidadOcupados = rd.IsDBNull(1) ? 0 : rd.GetInt32(1);
            }

            return res;
        }

        // =======================================
        //  4) Ingresos por método de pago
        //     Filtra por p.fecha_pago BETWEEN @Desde AND @Hasta
        // =======================================
        public List<MetodoPagoKpi> ObtenerIngresosPorMetodo(DateTime desde, DateTime hasta)
        {
            var lista = new List<MetodoPagoKpi>();

            const string sql = @"
                SELECT 
                    mp.tipo_pago AS MetodoPago,
                    COUNT(*) AS Cantidad,
                    ISNULL(SUM(p.monto_total),0) AS Total
                FROM pago p
                INNER JOIN metodo_pago mp ON mp.id_metodo_pago = p.id_metodo_pago
                WHERE p.estado = @EstadoPagado
                  AND p.fecha_pago BETWEEN @Desde AND @Hasta
                GROUP BY mp.tipo_pago
                ORDER BY Total DESC;";

            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@EstadoPagado", SqlDbType.VarChar, 50).Value = ESTADO_PAGO_PAGADO;
            cmd.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@Hasta", SqlDbType.DateTime).Value = hasta;

            using var rd = cmd.ExecuteReader();
            int ordMetodo = rd.GetOrdinal("MetodoPago");
            int ordCant = rd.GetOrdinal("Cantidad");
            int ordTotal = rd.GetOrdinal("Total");

            while (rd.Read())
            {
                lista.Add(new MetodoPagoKpi
                {
                    MetodoPago = rd.IsDBNull(ordMetodo) ? "" : rd.GetString(ordMetodo),
                    Cantidad = rd.IsDBNull(ordCant) ? 0 : rd.GetInt32(ordCant),
                    Total = rd.IsDBNull(ordTotal) ? 0m : rd.GetDecimal(ordTotal),
                    Desde = desde,
                    Hasta = hasta
                });
            }

            return lista;
        }

        // =======================================
        //  5) Top inquilinos por monto pagado
        //     Filtra por p.fecha_pago BETWEEN @Desde AND @Hasta
        // =======================================
        public List<InquilinoIngreso> ObtenerTopInquilinos(DateTime desde, DateTime hasta, int top = 10)
        {
            var lista = new List<InquilinoIngreso>();

            string sql = $@"
                SELECT TOP ({top})
                    per.id_persona,
                    (per.apellido + ', ' + per.nombre) AS Inquilino,
                    ISNULL(SUM(p.monto_total),0) AS TotalPagado
                FROM pago p
                INNER JOIN contrato c   ON c.id_contrato = p.id_contrato
                INNER JOIN persona per  ON per.id_persona = c.id_persona
                WHERE p.estado = @EstadoPagado
                  AND p.fecha_pago BETWEEN @Desde AND @Hasta
                GROUP BY per.id_persona, per.apellido, per.nombre
                ORDER BY TotalPagado DESC;";

            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@EstadoPagado", SqlDbType.VarChar, 50).Value = ESTADO_PAGO_PAGADO;
            cmd.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@Hasta", SqlDbType.DateTime).Value = hasta;

            using var rd = cmd.ExecuteReader();
            int ordId = rd.GetOrdinal("id_persona");
            int ordNom = rd.GetOrdinal("Inquilino");
            int ordTot = rd.GetOrdinal("TotalPagado");

            while (rd.Read())
            {
                lista.Add(new InquilinoIngreso
                {
                    IdPersona = rd.GetInt32(ordId),
                    Inquilino = rd.IsDBNull(ordNom) ? "" : rd.GetString(ordNom),
                    TotalPagado = rd.IsDBNull(ordTot) ? 0m : rd.GetDecimal(ordTot),
                    Desde = desde,
                    Hasta = hasta
                });
            }

            return lista;
        }

        // =======================================
        //  6) Cuotas vencidas e impagas (morosidad)
        //     Filtra por cu.fecha_vencimiento BETWEEN @Desde AND @Hasta
        //     (cu.estado = 'pendiente' y NO existe pago 'pagado' para esa cuota)
        // =======================================
        public List<CuotaVencida> ObtenerCuotasVencidasImpagas(DateTime desde, DateTime hasta)
        {
            var lista = new List<CuotaVencida>();

            const string sql = @"
                SELECT 
                    cu.id_contrato          AS IdContrato,
                    cu.nro_cuota            AS NroCuota,
                    cu.fecha_vencimiento    AS FechaVencimiento,
                    DATEDIFF(DAY, CAST(cu.fecha_vencimiento AS date), CAST(@Hasta AS date)) AS DiasAtraso,
                    per.apellido + ', ' + per.nombre AS Inquilino,
                    i.id_inmueble,
                    i.direccion             AS Inmueble,
                    cu.importe
                FROM cuota cu
                INNER JOIN contrato c   ON c.id_contrato   = cu.id_contrato
                INNER JOIN persona per  ON per.id_persona  = c.id_persona
                INNER JOIN inmueble i   ON i.id_inmueble   = c.id_inmueble
                LEFT JOIN pago p
                    ON  p.id_contrato = cu.id_contrato
                    AND p.nro_cuota   = cu.nro_cuota
                    AND p.estado      = @EstadoPagado
                WHERE 
                    c.estado = 1                                           -- ✅ solo contratos activos
                    AND @Hasta BETWEEN c.fecha_inicio AND c.fecha_fin      -- ✅ activo al corte
                    AND cu.estado = @CuotaPendiente
                    AND p.id_pago IS NULL                                  -- ✅ sin pago aplicado
                    AND DATEDIFF(DAY, CAST(cu.fecha_vencimiento AS date), CAST(@Hasta AS date)) > 0  -- ✅ vencidas (no incluye hoy)
                ORDER BY DiasAtraso DESC, cu.fecha_vencimiento;
            ";


            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@Hasta", SqlDbType.DateTime).Value = hasta;
            cmd.Parameters.Add("@EstadoPagado", SqlDbType.VarChar, 50).Value = ESTADO_PAGO_PAGADO;
            cmd.Parameters.Add("@CuotaPendiente", SqlDbType.VarChar, 50).Value = ESTADO_CUOTA_PENDIENTE;

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                lista.Add(new CuotaVencida
                {
                    IdContrato = rd.GetInt32(rd.GetOrdinal("IdContrato")),
                    NroCuota = rd.GetInt32(rd.GetOrdinal("NroCuota")),
                    FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento")),
                    DiasAtraso = rd.GetInt32(rd.GetOrdinal("DiasAtraso")),
                    Inquilino = rd.GetString(rd.GetOrdinal("Inquilino")),
                    IdInmueble = rd.GetInt32(rd.GetOrdinal("id_inmueble")),
                    Inmueble = rd.GetString(rd.GetOrdinal("Inmueble")),
                    Importe = rd.GetDecimal(rd.GetOrdinal("importe"))
                });
            }

            return lista;
        }

        // =======================================
        //  7) Contratos por vencer en ≤ N días (corte + ventana)
        //     corte = fecha base (ej: dtpHasta.Date)
        //     Devuelve sólo los contratos con fecha_fin dentro de (corte, corte + MaxDias]
        // =======================================
        public List<ContratoPorVencer> ObtenerContratosPorVencer(DateTime corte, int maxDias = 60)
        {
            var lista = new List<ContratoPorVencer>();

            const string sql = @"
        SELECT 
            c.id_contrato,
            c.fecha_fin,
            per.apellido + ', ' + per.nombre AS Inquilino,
            i.id_inmueble,
            i.direccion AS Inmueble,
            DATEDIFF(DAY, CAST(@Corte AS date), CAST(c.fecha_fin AS date)) AS DiasParaVencer
        FROM contrato c
        INNER JOIN persona per ON per.id_persona = c.id_persona
        INNER JOIN inmueble i  ON i.id_inmueble = c.id_inmueble
        WHERE c.estado = 1
          AND c.fecha_fin >  @Corte
          AND c.fecha_fin <= DATEADD(DAY, @MaxDias, @Corte)
        ORDER BY c.fecha_fin;";

            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@Corte", SqlDbType.DateTime).Value = corte.Date;
            cmd.Parameters.Add("@MaxDias", SqlDbType.Int).Value = maxDias;

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                lista.Add(new ContratoPorVencer
                {
                    IdContrato = rd.GetInt32(rd.GetOrdinal("id_contrato")),
                    FechaFin = rd.GetDateTime(rd.GetOrdinal("fecha_fin")),
                    Inquilino = rd.GetString(rd.GetOrdinal("Inquilino")),
                    IdInmueble = rd.GetInt32(rd.GetOrdinal("id_inmueble")),
                    Inmueble = rd.GetString(rd.GetOrdinal("Inmueble")),
                    DiasParaVencer = rd.GetInt32(rd.GetOrdinal("DiasParaVencer"))
                });
            }

            return lista;
        }


        // =======================================
        //  8) Proyección de ingresos (cuotas pendientes sin pago)
        //     Filtra por cu.fecha_vencimiento BETWEEN @Desde AND @Hasta
        //     (contrato.estado = 1, cuota.estado = 'pendiente', NO pago 'pagado')
        // =======================================
        public List<ProyeccionMensual> ObtenerProyeccionIngresos(DateTime desde, DateTime hasta)
        {
            var lista = new List<ProyeccionMensual>();

            const string sql = @"
                SELECT 
                    FORMAT(cu.fecha_vencimiento, 'yyyy-MM') AS Periodo,
                    ISNULL(SUM(cu.importe),0) AS MontoProyectado
                FROM cuota cu
                INNER JOIN contrato c ON c.id_contrato = cu.id_contrato AND c.estado = 1
                LEFT JOIN pago p
                   ON p.id_contrato = cu.id_contrato
                  AND p.nro_cuota   = cu.nro_cuota
                  AND p.estado = @EstadoPagado
                WHERE cu.estado = @CuotaPendiente
                  AND cu.fecha_vencimiento BETWEEN @Desde AND @Hasta
                  AND p.id_pago IS NULL
                GROUP BY FORMAT(cu.fecha_vencimiento, 'yyyy-MM')
                ORDER BY Periodo;";

            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@Hasta", SqlDbType.DateTime).Value = hasta;
            cmd.Parameters.Add("@EstadoPagado", SqlDbType.VarChar, 50).Value = ESTADO_PAGO_PAGADO;
            cmd.Parameters.Add("@CuotaPendiente", SqlDbType.VarChar, 50).Value = ESTADO_CUOTA_PENDIENTE;

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                lista.Add(new ProyeccionMensual
                {
                    Periodo = rd.GetString(rd.GetOrdinal("Periodo")),
                    MontoProyectado = rd.GetDecimal(rd.GetOrdinal("MontoProyectado")),
                    Desde = desde,
                    Hasta = hasta
                });
            }

            return lista;
        }

        // =======================================
        //  9) Contratos creados en el rango
        //     Cuenta por fecha_creacion BETWEEN @Desde AND @Hasta
        //     (opcional: sólo activos si soloActivos = true)
        // =======================================
        public int ContarContratosCreados(DateTime desde, DateTime hasta, bool? soloActivos = null)
        {
            using var cn = BDGeneral.GetConnection();

            var sql = @"
        SELECT COUNT(*) 
        FROM contrato c
        WHERE c.fecha_creacion BETWEEN @Desde AND @Hasta";

            if (soloActivos.HasValue)
                sql += " AND c.estado = @EstadoActivos";

            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@Hasta", SqlDbType.DateTime).Value = hasta;

            if (soloActivos.HasValue)
                cmd.Parameters.Add("@EstadoActivos", SqlDbType.Bit).Value = soloActivos.Value ? 1 : 0;

            var result = cmd.ExecuteScalar();
            return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
        }

        /// <summary>
        /// Lista inmuebles cuya columna [Condiciones] sea 'Disponible' o 'A estrenar'.
        /// NOTA: No devuelve 'amueblado' ni 'estado' (bit) para evitar columnas checkbox.
        /// </summary>
        public List<InmuebleCondicion> ObtenerInmueblesDisponiblesOAestrenar()
        {
            var lista = new List<InmuebleCondicion>();

            const string sql = @"
                SELECT
                    i.id_inmueble,
                    i.direccion         AS Inmueble,
                    i.tipo              AS Tipo,
                    i.nro_ambientes     AS Ambientes,
                    i.Condiciones       AS Condicion
                FROM dbo.inmueble i
                WHERE i.Condiciones IN (N'Disponible', N'A estrenar')
                ORDER BY i.direccion;";

            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);
            using var rd = cmd.ExecuteReader();

            int oId = rd.GetOrdinal("id_inmueble");
            int oDir = rd.GetOrdinal("Inmueble");
            int oTipo = rd.GetOrdinal("Tipo");
            int oAmb = rd.GetOrdinal("Ambientes");
            int oCond = rd.GetOrdinal("Condicion");

            while (rd.Read())
            {
                lista.Add(new InmuebleCondicion
                {
                    IdInmueble = rd.GetInt32(oId),
                    Inmueble = rd.IsDBNull(oDir) ? "" : rd.GetString(oDir),
                    Tipo = rd.IsDBNull(oTipo) ? "" : rd.GetString(oTipo),
                    Ambientes = rd.IsDBNull(oAmb) ? 0 : rd.GetInt32(oAmb),
                    Condicion = rd.IsDBNull(oCond) ? "" : rd.GetString(oCond)
                });
            }

            return lista;
        }

        // ReportePropietarioRepository.cs

        public List<OpcionCombo> ListarOperadores()
        {
            var lista = new List<OpcionCombo>();

            const string sql = @"
                SELECT u.dni AS Id, (u.apellido + ', ' + u.nombre) AS Nombre
                FROM usuario u
                WHERE u.id_rol = 2                       -- 👈 solo OPERADORES
                ORDER BY u.apellido, u.nombre;";

            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                lista.Add(new OpcionCombo
                {
                    Id = rd.GetInt32(rd.GetOrdinal("Id")),
                    Nombre = rd.IsDBNull(rd.GetOrdinal("Nombre")) ? "" : rd.GetString(rd.GetOrdinal("Nombre"))
                });
            }
            return lista;
        }

        public List<OpcionCombo> ListarMetodosPago()
        {
            var lista = new List<OpcionCombo>();

            // 👇 solo los métodos que mostraste en la captura (mismo texto)
            const string sql = @"
                SELECT mp.id_metodo_pago AS Id, mp.tipo_pago AS Nombre
                FROM metodo_pago mp
                WHERE mp.tipo_pago IN (
                    N'Efectivo',
                    N'Tarjeta de Crédito',
                    N'Tarjeta de Débito',
                    N'Transferencia Bancaria',
                    N'PayPal',
                    N'Cheque',
                    N'Criptomoneda',
                    N'Pago Móvil',
                    N'Depósito Bancario',
                    N'Vale o Cupón'
                )
                ORDER BY CASE mp.tipo_pago
                    WHEN N'Efectivo' THEN 1
                    WHEN N'Tarjeta de Crédito' THEN 2
                    WHEN N'Tarjeta de Débito' THEN 3
                    WHEN N'Transferencia Bancaria' THEN 4
                    WHEN N'PayPal' THEN 5
                    WHEN N'Cheque' THEN 6
                    WHEN N'Criptomoneda' THEN 7
                    WHEN N'Pago Móvil' THEN 8
                    WHEN N'Depósito Bancario' THEN 9
                    WHEN N'Vale o Cupón' THEN 10
                    ELSE 99 END;";

            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                lista.Add(new OpcionCombo
                {
                    Id = rd.GetInt32(rd.GetOrdinal("Id")),
                    Nombre = rd.GetString(rd.GetOrdinal("Nombre"))
                });
            }
            return lista;
        }


        // PAGOS REALIZADOS (con filtros) ===================

        /// <summary>
        /// Devuelve pagos en estado 'pagado' dentro del rango. 
        /// Filtros opcionales: idUsuario, idMetodoPago.
        /// </summary>
        public List<PagoRealizado> ObtenerPagosRealizados(
            DateTime desde, DateTime hasta, int? idUsuario = null, int? idMetodoPago = null)
        {
            var lista = new List<PagoRealizado>();

            const string sql = @"
                SELECT 
                    p.id_pago,
                    p.fecha_pago,
                    p.monto_total,
                    p.nro_cuota,
                    p.detalle,
                    p.id_usuario,
                    u.nombre + ' ' + u.apellido         AS Operador,
                    p.id_metodo_pago,
                    mp.tipo_pago                         AS MetodoPago,
                    c.id_contrato,
                    per.id_persona,
                    (per.apellido + ', ' + per.nombre)   AS Inquilino,
                    i.id_inmueble,
                    i.direccion                          AS Inmueble
                FROM pago p
                INNER JOIN usuario      u   ON u.dni = p.id_usuario
                INNER JOIN metodo_pago  mp  ON mp.id_metodo_pago = p.id_metodo_pago
                INNER JOIN contrato     c   ON c.id_contrato = p.id_contrato
                INNER JOIN persona      per ON per.id_persona = c.id_persona
                INNER JOIN inmueble     i   ON i.id_inmueble = c.id_inmueble
                WHERE p.estado = @EstadoPagado
                  AND p.fecha_pago BETWEEN @Desde AND @Hasta
                  AND (@IdUsuario   IS NULL OR p.id_usuario     = @IdUsuario)
                  AND (@IdMetodo    IS NULL OR p.id_metodo_pago = @IdMetodo)
                ORDER BY p.fecha_pago DESC, p.id_pago DESC;";

            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@EstadoPagado", SqlDbType.VarChar, 50).Value = ESTADO_PAGO_PAGADO;
            cmd.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@Hasta", SqlDbType.DateTime).Value = hasta;
            cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = (object?)idUsuario ?? DBNull.Value;
            cmd.Parameters.Add("@IdMetodo", SqlDbType.Int).Value = (object?)idMetodoPago ?? DBNull.Value;

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                lista.Add(new PagoRealizado
                {
                    IdPago = rd.GetInt32(rd.GetOrdinal("id_pago")),
                    FechaPago = rd.GetDateTime(rd.GetOrdinal("fecha_pago")),
                    MontoTotal = rd.GetDecimal(rd.GetOrdinal("monto_total")),
                    NroCuota = rd.GetInt32(rd.GetOrdinal("nro_cuota")),
                    Detalle = rd.IsDBNull(rd.GetOrdinal("detalle")) ? "" : rd.GetString(rd.GetOrdinal("detalle")),
                    IdUsuario = rd.GetInt32(rd.GetOrdinal("id_usuario")),
                    Operador = rd.GetString(rd.GetOrdinal("Operador")),
                    IdMetodoPago = rd.GetInt32(rd.GetOrdinal("id_metodo_pago")),
                    MetodoPago = rd.GetString(rd.GetOrdinal("MetodoPago")),
                    IdContrato = rd.GetInt32(rd.GetOrdinal("id_contrato")),
                    IdPersona = rd.GetInt32(rd.GetOrdinal("id_persona")),
                    Inquilino = rd.GetString(rd.GetOrdinal("Inquilino")),
                    IdInmueble = rd.GetInt32(rd.GetOrdinal("id_inmueble")),
                    Inmueble = rd.GetString(rd.GetOrdinal("Inmueble"))
                });
            }

            return lista;
        }

    }
}
