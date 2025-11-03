using InmoTech.Data;
using InmoTech.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text; // Asegúrate de tener este using

namespace InmoTech.Repositories
{
    public class ReporteRepository
    {
        /// <summary>
        /// Obtiene los KPIs para el dashboard (Inmuebles, Inquilinos, Usuarios).
        /// </summary>
        public DashboardKpis ObtenerDashboardKpis(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var kpis = new DashboardKpis();
            using var cn = BDGeneral.GetConnection();

            // Total de propiedades (Inmuebles) creadas
            string sqlPropiedades = "SELECT COUNT(1) FROM dbo.inmueble WHERE fecha_creacion BETWEEN @FechaInicio AND @FechaFin;";
            using (var cmd = new SqlCommand(sqlPropiedades, cn))
            {
                cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio ?? DateTime.MinValue;
                cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = fechaFin ?? DateTime.MaxValue;
                kpis.TotalPropiedades = (int)cmd.ExecuteScalar();
            }

            // Total de inquilinos (Personas) creados
            string sqlInquilinos = "SELECT COUNT(1) FROM dbo.persona WHERE fecha_creacion BETWEEN @FechaInicio AND @FechaFin;";
            using (var cmd = new SqlCommand(sqlInquilinos, cn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio ?? DateTime.MinValue;
                cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = fechaFin ?? DateTime.MaxValue;
                kpis.TotalInquilinos = (int)cmd.ExecuteScalar();
            }

            // Total de Usuarios (del sistema) creados
            string sqlUsuarios = "SELECT COUNT(1) FROM dbo.usuario WHERE fecha_creacion BETWEEN @FechaInicio AND @FechaFin;";
            using (var cmd = new SqlCommand(sqlUsuarios, cn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio ?? DateTime.MinValue;
                cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = fechaFin ?? DateTime.MaxValue;
                kpis.TotalUsuarios = (int)cmd.ExecuteScalar();
            }

            return kpis;
        }

        /// <summary>
        /// Obtiene un reporte de todos los Inquilinos (Personas), con información del usuario creador.
        /// </summary>
        public List<InquilinoReporte> ObtenerReporteInquilinos(DateTime? fechaInicio, DateTime? fechaFin, bool? estado)
        {
            var list = new List<InquilinoReporte>();
            using var cn = BDGeneral.GetConnection();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT p.dni, p.nombre, p.apellido, p.telefono, p.email, p.direccion, p.fecha_nacimiento, p.estado, p.fecha_creacion, ");
            sql.AppendLine("       u.nombre + ' ' + u.apellido AS UsuarioCreador");
            sql.AppendLine("FROM dbo.persona p");
            sql.AppendLine("JOIN dbo.usuario u ON p.usuario_creador_dni = u.dni");
            sql.AppendLine("WHERE p.fecha_creacion BETWEEN @FechaInicio AND @FechaFin");
            if (estado.HasValue)
            {
                sql.AppendLine("  AND p.estado = @Estado");
            }
            sql.AppendLine("ORDER BY p.apellido, p.nombre;");

            using var cmd = new SqlCommand(sql.ToString(), cn);
            cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio ?? DateTime.MinValue;
            cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = fechaFin ?? DateTime.MaxValue;
            if (estado.HasValue)
            {
                cmd.Parameters.Add("@Estado", SqlDbType.TinyInt).Value = estado.Value ? 1 : 0;
            }

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new InquilinoReporte
                {
                    Dni = rd.GetInt32(0),
                    Nombre = rd.GetString(1),
                    Apellido = rd.GetString(2),
                    Telefono = rd.IsDBNull(3) ? "" : rd.GetString(3),
                    Email = rd.IsDBNull(4) ? "" : rd.GetString(4),
                    Direccion = rd.IsDBNull(5) ? "" : rd.GetString(5),
                    FechaNacimiento = rd.IsDBNull(6) ? DateTime.MinValue : rd.GetDateTime(6),
                    Estado = Convert.ToByte(rd["estado"]) == 1,
                    FechaCreacion = rd.GetDateTime(8),
                    UsuarioCreador = rd.GetString(9)
                });
            }
            return list;
        }

        /// <summary>
        /// Obtiene un reporte de todos los Inmuebles, con información del usuario creador.
        /// </summary>
        public List<InmuebleReporte> ObtenerReporteInmuebles(DateTime? fechaInicio, DateTime? fechaFin, string? tipoInmueble)
        {
            var list = new List<InmuebleReporte>();
            using var cn = BDGeneral.GetConnection();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT i.id_inmueble, i.tipo, i.direccion, ");
            sql.AppendLine("       ISNULL(i.nro_ambientes, 0) AS NroAmbientes, i.amueblado, i.Condiciones, i.estado, "); // Columnas existentes
            sql.AppendLine("       i.fecha_creacion, u.nombre + ' ' + u.apellido AS UsuarioCreador");
            sql.AppendLine("FROM dbo.inmueble i");
            sql.AppendLine("JOIN dbo.usuario u ON i.usuario_creador_dni = u.dni");
            sql.AppendLine("WHERE i.fecha_creacion BETWEEN @FechaInicio AND @FechaFin");

            if (!string.IsNullOrWhiteSpace(tipoInmueble) && tipoInmueble != "Todos")
            {
                sql.AppendLine("  AND i.tipo = @TipoInmueble");
            }
            // Eliminado el filtro por 'disponible'
            sql.AppendLine("ORDER BY i.direccion;");

            using var cmd = new SqlCommand(sql.ToString(), cn);
            cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio ?? DateTime.MinValue;
            cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = fechaFin ?? DateTime.MaxValue;
            if (!string.IsNullOrWhiteSpace(tipoInmueble) && tipoInmueble != "Todos")
            {
                cmd.Parameters.Add("@TipoInmueble", SqlDbType.VarChar, 80).Value = tipoInmueble; // Ajustado tamaño
            }
            // Eliminado parámetro @Disponible

            using var rd = cmd.ExecuteReader();
            int ordId = rd.GetOrdinal("id_inmueble");
            int ordTipo = rd.GetOrdinal("tipo");
            int ordDir = rd.GetOrdinal("direccion");
            int ordAmb = rd.GetOrdinal("NroAmbientes");
            int ordAmue = rd.GetOrdinal("amueblado");
            int ordCond = rd.GetOrdinal("Condiciones");
            int ordEst = rd.GetOrdinal("estado");
            int ordFCrea = rd.GetOrdinal("fecha_creacion");
            int ordUCrea = rd.GetOrdinal("UsuarioCreador");

            while (rd.Read())
            {
                list.Add(new InmuebleReporte
                {
                    IdInmueble = rd.GetInt32(ordId),
                    Tipo = rd.GetString(ordTipo),
                    Direccion = rd.GetString(ordDir),
                    NroAmbientes = rd.GetInt32(ordAmb), // Ya tiene ISNULL en SQL
                    Amueblado = rd.GetBoolean(ordAmue),
                    Condiciones = rd.GetString(ordCond),
                    Estado = rd.GetBoolean(ordEst),
                    FechaCreacion = rd.GetDateTime(ordFCrea),
                    UsuarioCreador = rd.GetString(ordUCrea)
                    // Campos eliminados: Localidad, Provincia, PrecioAlquiler, Disponible
                });
            }
            return list;
        }

        // ... (ObtenerReporteUsuarios - Asegúrate que esté la versión corregida de antes) ...
        // Copio de nuevo la versión corregida de ObtenerReporteUsuarios para asegurar
        public List<UsuarioReporte> ObtenerReporteUsuarios(DateTime? fechaInicio, DateTime? fechaFin, string? rol, bool? activo)
        {
            var list = new List<UsuarioReporte>();
            using var cn = BDGeneral.GetConnection();

            var sql = new StringBuilder();
            sql.AppendLine("SELECT u.dni, u.nombre, u.apellido, u.email, ");
            // Ajustar CASE a los id_rol que realmente usas (1=Admin, 2=Operador, 3=Propietario?)
            sql.AppendLine("       CASE u.id_rol WHEN 1 THEN 'Administrador' WHEN 2 THEN 'Operador' WHEN 3 THEN 'Propietario' ELSE 'Rol ' + CAST(u.id_rol AS VARCHAR) END AS Rol, ");
            sql.AppendLine("       u.estado AS Activo, u.fecha_creacion, ");
            sql.AppendLine("       ISNULL(uc.nombre + ' ' + uc.apellido, 'Sistema') AS UsuarioCreador"); // Alias para el usuario que creó y manejo de NULL
            sql.AppendLine("FROM dbo.usuario u");
            sql.AppendLine("LEFT JOIN dbo.usuario uc ON u.usuario_creador_dni = uc.dni"); // LEFT JOIN por si el primer admin no tiene creador
            sql.AppendLine("WHERE u.fecha_creacion BETWEEN @FechaInicio AND @FechaFin");

            // Variables para parámetros dinámicos
            int? idRolParam = null;

            if (!string.IsNullOrWhiteSpace(rol) && rol != "Todos")
            {
                sql.AppendLine("  AND u.id_rol = @IdRol");
                // Mapear rol de texto a id_rol
                if (rol == "Administrador") idRolParam = 1;
                else if (rol == "Operador") idRolParam = 2;
                else if (rol == "Propietario") idRolParam = 3;
                // else idRolParam = ...; // Manejar otros roles si existen
            }
            if (activo.HasValue)
            {
                sql.AppendLine("  AND u.estado = @Activo");
            }
            sql.AppendLine("ORDER BY u.apellido, u.nombre;");

            using var cmd = new SqlCommand(sql.ToString(), cn);

            // Añadir parámetros estándar
            cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio ?? DateTime.MinValue;
            cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = fechaFin ?? DateTime.MaxValue;

            // Añadir parámetros dinámicos si aplican
            if (idRolParam.HasValue)
            {
                cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = idRolParam.Value;
            }
            if (activo.HasValue)
            {
                cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = activo.Value;
            }

            using var rd = cmd.ExecuteReader();
            int ordDni = rd.GetOrdinal("dni");
            int ordNom = rd.GetOrdinal("nombre");
            int ordApe = rd.GetOrdinal("apellido");
            int ordEma = rd.GetOrdinal("email");
            int ordRol = rd.GetOrdinal("Rol");
            int ordAct = rd.GetOrdinal("Activo");
            int ordFCrea = rd.GetOrdinal("fecha_creacion");
            int ordUCrea = rd.GetOrdinal("UsuarioCreador");

            while (rd.Read())
            {
                list.Add(new UsuarioReporte
                {
                    Dni = rd.GetInt32(ordDni),
                    Nombre = rd.GetString(ordNom),
                    Apellido = rd.GetString(ordApe),
                    Email = rd.GetString(ordEma),
                    Rol = rd.GetString(ordRol),
                    Activo = rd.GetBoolean(ordAct),
                    FechaCreacion = rd.GetDateTime(ordFCrea),
                    UsuarioCreador = rd.GetString(ordUCrea) // Ya se maneja el NULL en el SQL con ISNULL
                });
            }
            return list;
        }

        public OperadorDashboardKpis ObtenerKpisOperador(int dniUsuarioOperador, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var kpis = new OperadorDashboardKpis();
            using var cn = BDGeneral.GetConnection();

            // Suma de montos de pagos registrados por este usuario en el período
            string sqlIngresos = @"
                SELECT ISNULL(SUM(monto_total), 0)
                FROM dbo.pago
                WHERE id_usuario = @DniUsuario
                  AND fecha_registro BETWEEN @FechaInicio AND @FechaFin;"; // Usamos fecha_registro del pago
            using (var cmd = new SqlCommand(sqlIngresos, cn))
            {
                cmd.Parameters.Add("@DniUsuario", SqlDbType.Int).Value = dniUsuarioOperador;
                cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime2).Value = (object?)fechaInicio ?? DateTime.MinValue;
                cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime2).Value = (object?)fechaFin ?? DateTime.MaxValue;
                kpis.IngresosGenerados = (decimal)cmd.ExecuteScalar();
            }

            // Cantidad de pagos registrados por este usuario en el período
            string sqlCantPagos = @"
                SELECT COUNT(id_pago)
                FROM dbo.pago
                WHERE id_usuario = @DniUsuario
                  AND fecha_registro BETWEEN @FechaInicio AND @FechaFin;";
            using (var cmd = new SqlCommand(sqlCantPagos, cn))
            {
                cmd.Parameters.Add("@DniUsuario", SqlDbType.Int).Value = dniUsuarioOperador;
                cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime2).Value = (object?)fechaInicio ?? DateTime.MinValue;
                cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime2).Value = (object?)fechaFin ?? DateTime.MaxValue;
                kpis.PagosRegistrados = (int)cmd.ExecuteScalar();
            }


            // Cantidad de contratos creados por este usuario en el período
            string sqlContratos = @"
                SELECT COUNT(id_contrato)
                FROM dbo.contrato
                WHERE dni_usuario = @DniUsuario
                  AND fecha_creacion BETWEEN @FechaInicio AND @FechaFin;"; // Usamos fecha_creacion del contrato
            using (var cmd = new SqlCommand(sqlContratos, cn))
            {
                cmd.Parameters.Clear(); // Limpiar parámetros anteriores si reutilizas cmd
                cmd.Parameters.Add("@DniUsuario", SqlDbType.Int).Value = dniUsuarioOperador;
                cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = (object?)fechaInicio ?? DateTime.MinValue;
                cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = (object?)fechaFin ?? DateTime.MaxValue;
                kpis.ContratosCreados = (int)cmd.ExecuteScalar();
            }

            return kpis;
        }

        /// <summary>
        /// Obtiene una lista de inquilinos con cuotas vencidas en contratos creados por el operador especificado.
        /// Se considera vencida si está Pendiente, sin pago y la fecha de vencimiento es anterior a hoy.
        /// </summary>
        public List<ClienteMorosoViewModel> ObtenerClientesMorosos(int dniUsuarioOperador, DateTime? fechaInicioFiltro, DateTime? fechaFinFiltro)
        {
            // OJO: Los filtros de fecha aquí aplican a la FECHA DE CREACIÓN del contrato,
            // no necesariamente a la fecha de vencimiento de la cuota morosa. Ajustar si es necesario.
            var list = new List<ClienteMorosoViewModel>();
            using var cn = BDGeneral.GetConnection();
            DateTime hoy = DateTime.Today;

            // 1. Obtener todas las cuotas vencidas de contratos creados por el operador
            // 2. Agrupar por inquilino/contrato
            string sql = @"
                SELECT
                    p.dni AS DniInquilino,
                    p.nombre + ' ' + p.apellido AS NombreCompletoInquilino,
                    ISNULL(p.telefono, '') AS TelefonoInquilino,
                    c.id_contrato AS IdContrato,
                    i.direccion AS DireccionInmueble,
                    cu.nro_cuota AS NroCuotaVencida,
                    cu.fecha_vencimiento AS FechaVencimientoCuota
                FROM dbo.cuota cu
                JOIN dbo.contrato c ON cu.id_contrato = c.id_contrato
                JOIN dbo.persona p ON c.id_persona = p.id_persona
                JOIN dbo.inmueble i ON c.id_inmueble = i.id_inmueble
                WHERE
                    c.dni_usuario = @DniUsuarioOperador   -- Contratos creados por el operador
                    AND cu.estado = 'Pendiente'          -- Cuotas pendientes
                    AND cu.id_pago IS NULL               -- Sin pago asociado
                    AND cu.fecha_vencimiento < @Hoy      -- Vencidas (fecha anterior a hoy)
                    -- Opcional: Filtrar contratos creados en el rango de fechas del reporte
                    AND c.fecha_creacion BETWEEN @FechaInicioFiltro AND @FechaFinFiltro
                ORDER BY
                    p.apellido, p.nombre, c.id_contrato, cu.fecha_vencimiento ASC; -- Importante ordenar para agrupar
            ";

            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@DniUsuarioOperador", SqlDbType.Int).Value = dniUsuarioOperador;
            cmd.Parameters.Add("@Hoy", SqlDbType.Date).Value = hoy;
            cmd.Parameters.Add("@FechaInicioFiltro", SqlDbType.DateTime).Value = (object?)fechaInicioFiltro ?? DateTime.MinValue;
            cmd.Parameters.Add("@FechaFinFiltro", SqlDbType.DateTime).Value = (object?)fechaFinFiltro ?? DateTime.MaxValue;

            // Estructura temporal para facilitar el agrupamiento
            var cuotasVencidas = new List<(int Dni, string Nombre, string Tel, int IdContrato, string DirInm, DateTime Vencimiento)>();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    cuotasVencidas.Add((
                        rd.GetInt32(rd.GetOrdinal("DniInquilino")),
                        rd.GetString(rd.GetOrdinal("NombreCompletoInquilino")),
                        rd.GetString(rd.GetOrdinal("TelefonoInquilino")),
                        rd.GetInt32(rd.GetOrdinal("IdContrato")),
                        rd.GetString(rd.GetOrdinal("DireccionInmueble")),
                        rd.GetDateTime(rd.GetOrdinal("FechaVencimientoCuota"))
                    ));
                }
            }

            // Agrupar en C#
            var grouped = cuotasVencidas
                .GroupBy(c => new { c.Dni, c.Nombre, c.Tel, c.IdContrato, c.DirInm })
                .Select(g => new ClienteMorosoViewModel
                {
                    DniInquilino = g.Key.Dni,
                    NombreCompletoInquilino = g.Key.Nombre,
                    TelefonoInquilino = g.Key.Tel,
                    IdContrato = g.Key.IdContrato,
                    DireccionInmueble = g.Key.DirInm,
                    CuotasVencidas = g.Count(), // Contar cuántas cuotas vencidas tiene este contrato/inquilino
                    ProximoVencimiento = g.Min(c => c.Vencimiento) // Obtener la fecha de vencimiento más antigua
                })
                .OrderBy(vm => vm.NombreCompletoInquilino).ThenBy(vm => vm.ProximoVencimiento) // Ordenar resultado final
                .ToList();

            return grouped;
        }

        /// <summary>
        /// Obtiene un reporte de Pagos registrados por un Operador específico en un rango de fechas.
        /// </summary>
        public List<PagoOperadorReporte> ObtenerPagosPorOperador(int dniUsuarioOperador, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var list = new List<PagoOperadorReporte>();
            using var cn = BDGeneral.GetConnection();
            const string sql = @"
                SELECT
                    p.id_pago, p.fecha_pago, p.monto_total, mp.tipo_pago AS MetodoPago,
                    p.id_contrato, p.nro_cuota,
                    per.nombre + ' ' + per.apellido AS Inquilino,
                    p.fecha_registro
                FROM dbo.pago p
                JOIN dbo.metodo_pago mp ON p.id_metodo_pago = mp.id_metodo_pago
                JOIN dbo.persona per ON p.id_persona = per.id_persona -- Unir con persona para nombre inquilino
                WHERE p.id_usuario = @DniUsuario
                  AND p.fecha_registro BETWEEN @FechaInicio AND @FechaFin
                ORDER BY p.fecha_registro DESC;";

            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@DniUsuario", SqlDbType.Int).Value = dniUsuarioOperador;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime2).Value = (object?)fechaInicio ?? DateTime.MinValue;
            cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime2).Value = (object?)fechaFin ?? DateTime.MaxValue;

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new PagoOperadorReporte
                {
                    IdPago = rd.GetInt32(rd.GetOrdinal("id_pago")),
                    FechaPago = rd.GetDateTime(rd.GetOrdinal("fecha_pago")),
                    MontoTotal = rd.GetDecimal(rd.GetOrdinal("monto_total")),
                    MetodoPago = rd.GetString(rd.GetOrdinal("MetodoPago")),
                    IdContrato = rd.GetInt32(rd.GetOrdinal("id_contrato")),
                    NroCuota = rd.GetInt32(rd.GetOrdinal("nro_cuota")),
                    Inquilino = rd.GetString(rd.GetOrdinal("Inquilino")),
                    FechaRegistro = rd.GetDateTime(rd.GetOrdinal("fecha_registro"))
                });
            }
            return list;
        }

        /// <summary>
        /// Obtiene un reporte de Contratos creados por un Operador específico en un rango de fechas.
        /// </summary>
        public List<ContratoOperadorReporte> ObtenerContratosPorOperador(int dniUsuarioOperador, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var list = new List<ContratoOperadorReporte>();
            using var cn = BDGeneral.GetConnection();
            const string sql = @"
                SELECT
                    c.id_contrato, c.fecha_inicio, c.fecha_fin, c.monto AS MontoMensual,
                    p.nombre + ' ' + p.apellido AS Inquilino,
                    i.direccion AS Inmueble,
                    c.estado, c.fecha_creacion
                FROM dbo.contrato c
                JOIN dbo.persona p ON c.id_persona = p.id_persona
                JOIN dbo.inmueble i ON c.id_inmueble = i.id_inmueble
                WHERE c.dni_usuario = @DniUsuario
                  AND c.fecha_creacion BETWEEN @FechaInicio AND @FechaFin
                ORDER BY c.fecha_creacion DESC;";

            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@DniUsuario", SqlDbType.Int).Value = dniUsuarioOperador;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = (object?)fechaInicio ?? DateTime.MinValue;
            cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = (object?)fechaFin ?? DateTime.MaxValue;

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new ContratoOperadorReporte
                {
                    IdContrato = rd.GetInt32(rd.GetOrdinal("id_contrato")),
                    FechaInicio = rd.GetDateTime(rd.GetOrdinal("fecha_inicio")),
                    FechaFin = rd.GetDateTime(rd.GetOrdinal("fecha_fin")),
                    MontoMensual = rd.GetDecimal(rd.GetOrdinal("MontoMensual")),
                    Inquilino = rd.GetString(rd.GetOrdinal("Inquilino")),
                    Inmueble = rd.GetString(rd.GetOrdinal("Inmueble")),
                    Estado = rd.GetBoolean(rd.GetOrdinal("estado")),
                    FechaCreacion = rd.GetDateTime(rd.GetOrdinal("fecha_creacion"))
                });
            }
            return list;
        }

        public List<ContratoPorVencerViewModel> ObtenerContratosPorVencer(int dniOperador, int dias)
        {
            using var conexion = InmoTech.Data.BDGeneral.GetConnection();

            const string sql = @"
                SELECT
                    c.id_contrato                               AS IdContrato,
                    (p.apellido + ' ' + p.nombre)               AS Inquilino,
                    i.direccion                                 AS Inmueble,
                    c.fecha_fin                                 AS FechaFin,
                    DATEDIFF(DAY, CAST(GETDATE() AS date), c.fecha_fin) AS DiasRestantes
                FROM contrato c
                INNER JOIN persona  p ON p.id_persona  = c.id_persona
                INNER JOIN inmueble i ON i.id_inmueble = c.id_inmueble
                WHERE
                    c.estado = 1
                    AND c.dni_usuario = @Dni
                    AND c.fecha_fin >= CAST(GETDATE() AS date)
                    AND c.fecha_fin <= DATEADD(DAY, @Dias, CAST(GETDATE() AS date))
                ORDER BY c.fecha_fin ASC;";

            using var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Dni", System.Data.SqlDbType.Int).Value = dniOperador;
            cmd.Parameters.Add("@Dias", System.Data.SqlDbType.Int).Value = dias;

            var lista = new List<ContratoPorVencerViewModel>();
            using var rd = cmd.ExecuteReader();

            int ordId = rd.GetOrdinal("IdContrato");
            int ordInq = rd.GetOrdinal("Inquilino");
            int ordInm = rd.GetOrdinal("Inmueble");
            int ordFin = rd.GetOrdinal("FechaFin");
            int ordDias = rd.GetOrdinal("DiasRestantes");

            while (rd.Read())
            {
                lista.Add(new ContratoPorVencerViewModel
                {
                    IdContrato = rd.GetInt32(ordId),
                    Inquilino = rd.GetString(ordInq),
                    Inmueble = rd.GetString(ordInm),
                    FechaFin = rd.GetDateTime(ordFin),
                    DiasRestantes = rd.GetInt32(ordDias)
                });
            }

            return lista;
        }

    }
}
