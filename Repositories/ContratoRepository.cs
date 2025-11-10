// ============================================================================
// InmoTech.Repositories.ContratoRepository
// ----------------------------------------------------------------------------
// Repositorio para operaciones CRUD y de verificación sobre la entidad Contrato.
// Incluye lógica transaccional para alta de contratos con generación de cuotas,
// validaciones atómicas y actualización de estado del inmueble.
// NOTA: La documentación es breve y no modifica el código original.
// ============================================================================

using InmoTech.Data;
using InmoTech.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using InmoTech.Services;

namespace InmoTech.Repositories
{
    /// <summary>
    /// Provee métodos para crear, leer, actualizar y verificar contratos.
    /// Maneja transacciones al dar de alta un contrato (incluye cuotas e impacto en inmueble).
    /// </summary>
    public class ContratoRepository
    {
        // ======================================================
        //  REGIÓN: Operaciones de Creación y Lógica Transaccional
        // ======================================================
        #region Operaciones de Creación y Lógica Transaccional

        /// <summary>
        /// Crea un contrato junto con sus cuotas mensuales y marca el inmueble como <c>Ocupado</c>.
        /// Realiza validaciones atómicas sobre inmueble e inquilino y confirma en una única transacción.
        /// </summary>
        /// <param name="c">Entidad <see cref="Contrato"/> a persistir (con fechas, monto, FK, usuario, estado).</param>
        /// <returns>Siempre <c>1</c> si la operación completa se confirma.</returns>
        /// <exception cref="Exception">
        /// Se lanza si el inmueble o la persona no existen/están inactivos,
        /// si el inmueble no está <c>Disponible</c>/<c>A estrenar</c>,
        /// o si las fechas son inválidas.
        /// </exception>
        public int AgregarContrato(Contrato c)
        {
            using var conexion = BDGeneral.GetConnection();
            using var transaccion = conexion.BeginTransaction();

            try
            {
                // ----------------------------------------------
                // 0) VALIDACIONES ATÓMICAS (mismo TRX + locks)
                //    - Inmueble: activo y condiciones válidas
                //    - Inquilino(persona): activo
                // ----------------------------------------------

                // Bloqueo de fila del inmueble para evitar carreras de "doble contratación"
                const string sqlCheckInmueble = @"
                    SELECT estado, condiciones
                    FROM dbo.inmueble WITH (UPDLOCK, ROWLOCK)
                    WHERE id_inmueble = @IdInmueble;";

                bool inmuebleActivo;
                string condicionesInmueble = "";

                using (var cmdCheckI = new SqlCommand(sqlCheckInmueble, conexion, transaccion))
                {
                    cmdCheckI.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = c.IdInmueble;
                    using var rd = cmdCheckI.ExecuteReader();
                    if (!rd.Read())
                        throw new Exception("El inmueble seleccionado no existe.");
                    inmuebleActivo = rd.GetBoolean(0);
                    condicionesInmueble = rd.GetString(1);
                }

                if (!inmuebleActivo)
                    throw new Exception("El inmueble no está activo.");

                var condUpper = (condicionesInmueble ?? "").Trim().ToUpperInvariant();
                if (condUpper != "DISPONIBLE" && condUpper != "A ESTRENAR" && condUpper != "A ESTRENAR.")
                    throw new Exception("El inmueble no está en condición 'Disponible' o 'A estrenar'.");

                // Validación del inquilino (persona) activo
                const string sqlCheckPersona = @"
                    SELECT estado
                    FROM dbo.persona WITH (UPDLOCK, ROWLOCK)
                    WHERE id_persona = @IdPersona;";

                bool personaActiva;

                using (var cmdCheckP = new SqlCommand(sqlCheckPersona, conexion, transaccion))
                {
                    cmdCheckP.Parameters.Add("@IdPersona", SqlDbType.Int).Value = c.IdPersona;
                    var o = cmdCheckP.ExecuteScalar();
                    if (o == null)
                        throw new Exception("El inquilino seleccionado no existe.");
                    personaActiva = Convert.ToByte(o) == 1; // en tu esquema persona.estado es bit
                }

                if (!personaActiva)
                    throw new Exception("El inquilino está inactivo.");

                // ----------------------------------------------
                // 1) INSERTAR CONTRATO
                // ----------------------------------------------
                const string sqlContrato = @"
                    INSERT INTO dbo.contrato
                        (fecha_inicio, fecha_fin, monto, id_inmueble, id_persona, fecha_creacion, dni_usuario, estado)
                    OUTPUT INSERTED.id_contrato
                    VALUES
                        (@Inicio, @Fin, @Monto, @IdInmueble, @IdPersona, @FechaCreacion, @DniUsuario, @Estado);";

                using var cmdContrato = new SqlCommand(sqlContrato, conexion, transaccion);

                cmdContrato.Parameters.Add("@Inicio", SqlDbType.Date).Value = c.FechaInicio.Date;
                cmdContrato.Parameters.Add("@Fin", SqlDbType.Date).Value = c.FechaFin.Date;

                var pMonto = cmdContrato.Parameters.Add("@Monto", SqlDbType.Decimal);
                pMonto.Precision = 12;
                pMonto.Scale = 2;
                pMonto.Value = c.Monto;

                cmdContrato.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = c.IdInmueble;
                cmdContrato.Parameters.Add("@IdPersona", SqlDbType.Int).Value = c.IdPersona;
                cmdContrato.Parameters.Add("@FechaCreacion", SqlDbType.DateTime).Value = c.FechaCreacion;
                cmdContrato.Parameters.Add("@DniUsuario", SqlDbType.Int).Value = c.DniUsuario;
                cmdContrato.Parameters.Add("@Estado", SqlDbType.Bit).Value = c.Estado;

                int nuevoIdContrato = Convert.ToInt32(cmdContrato.ExecuteScalar());

                // ----------------------------------------------
                // 2) GENERAR CUOTAS (una por mes)
                // ----------------------------------------------
                int totalMeses = ((c.FechaFin.Year - c.FechaInicio.Year) * 12) + (c.FechaFin.Month - c.FechaInicio.Month);
                if (totalMeses == 0 && c.FechaFin >= c.FechaInicio) totalMeses = 1;

                if (totalMeses <= 0)
                    throw new Exception("La fecha de fin debe ser igual o posterior a la fecha de inicio del contrato.");

                DateTime primerVencimiento = c.FechaInicio.Day <= 10
                    ? new DateTime(c.FechaInicio.Year, c.FechaInicio.Month, 10)
                    : new DateTime(c.FechaInicio.Year, c.FechaInicio.Month, 1).AddMonths(1).AddDays(9);

                const string sqlCuota = @"
                    INSERT INTO dbo.cuota (id_contrato, nro_cuota, fecha_vencimiento, importe, estado, id_pago)
                    VALUES (@IdContrato, @NroCuota, @FechaVencimiento, @Importe, @Estado, NULL);";

                for (int i = 1; i <= totalMeses; i++)
                {
                    DateTime fechaVencimiento = primerVencimiento.AddMonths(i - 1);

                    using var cmdCuota = new SqlCommand(sqlCuota, conexion, transaccion);
                    cmdCuota.Parameters.Add("@IdContrato", SqlDbType.Int).Value = nuevoIdContrato;
                    cmdCuota.Parameters.Add("@NroCuota", SqlDbType.Int).Value = i;
                    cmdCuota.Parameters.Add("@FechaVencimiento", SqlDbType.Date).Value = fechaVencimiento;

                    var pImporteCuota = cmdCuota.Parameters.Add("@Importe", SqlDbType.Decimal);
                    pImporteCuota.Precision = 12;
                    pImporteCuota.Scale = 2;
                    pImporteCuota.Value = c.Monto;

                    cmdCuota.Parameters.Add("@Estado", SqlDbType.VarChar, 50).Value = "Pendiente";

                    cmdCuota.ExecuteNonQuery();
                }

                // ----------------------------------------------
                // 3) CAMBIAR CONDICIÓN DEL INMUEBLE A 'Ocupado'
                // ----------------------------------------------
                const string sqlOcuparInmueble = @"
                    UPDATE dbo.inmueble
                       SET condiciones = N'Ocupado'
                     WHERE id_inmueble = @IdInmueble;";

                using (var cmdUpd = new SqlCommand(sqlOcuparInmueble, conexion, transaccion))
                {
                    cmdUpd.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = c.IdInmueble;
                    cmdUpd.ExecuteNonQuery();
                }

                // ----------------------------------------------
                // 4) CONFIRMAR TODO
                // ----------------------------------------------
                transaccion.Commit();

                AppNotifier.NotifyDashboardChange();

                return 1;
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Operaciones de Consulta (Read)
        // ======================================================
        #region Operaciones de Consulta (Read)

        /// <summary>
        /// Obtiene una lista de contratos, opcionalmente filtrada por DNI de usuario creador.
        /// Incluye datos enriquecidos (inquilino, inmueble y usuario).
        /// </summary>
        /// <param name="dniUsuario">DNI del usuario para filtrar (o <c>null</c> para todos).</param>
        /// <returns>Lista de <see cref="Contrato"/> ordenada por fecha de inicio descendente.</returns>
        public List<Contrato> ObtenerContratos(int? dniUsuario = null)
        {
            using var conexion = BDGeneral.GetConnection();

            const string sql = @"
            SELECT  c.id_contrato, c.fecha_inicio, c.fecha_fin, c.monto,
                    c.id_inmueble, c.id_persona, c.fecha_creacion, c.dni_usuario, c.estado,
                    (p.apellido + ' ' + p.nombre) AS NombreInquilino,
                    i.direccion AS DireccionInmueble,
                    (u.apellido + ' ' + u.nombre) AS NombreUsuario
            FROM contrato c
            INNER JOIN persona  p ON p.id_persona  = c.id_persona
            INNER JOIN inmueble i ON i.id_inmueble = c.id_inmueble
            INNER JOIN usuario  u ON u.dni = c.dni_usuario
            WHERE (@DniUsuario IS NULL OR c.dni_usuario = @DniUsuario)
            ORDER BY c.fecha_inicio DESC;";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@DniUsuario", SqlDbType.Int).Value = (object?)dniUsuario ?? DBNull.Value;

            using var reader = cmd.ExecuteReader();
            var lista = new List<Contrato>();

            int ordIdContrato = reader.GetOrdinal("id_contrato");
            int ordInicio = reader.GetOrdinal("fecha_inicio");
            int ordFin = reader.GetOrdinal("fecha_fin");
            int ordMonto = reader.GetOrdinal("monto");
            int ordIdInm = reader.GetOrdinal("id_inmueble");
            int ordIdPer = reader.GetOrdinal("id_persona");
            int ordCreacion = reader.GetOrdinal("fecha_creacion");
            int ordDniUser = reader.GetOrdinal("dni_usuario");
            int ordEstado = reader.GetOrdinal("estado");
            int ordNomInq = reader.GetOrdinal("NombreInquilino");
            int ordDirInm = reader.GetOrdinal("DireccionInmueble");
            int ordNomUsr = reader.GetOrdinal("NombreUsuario");

            while (reader.Read())
            {
                lista.Add(new Contrato
                {
                    IdContrato = reader.GetInt32(ordIdContrato),
                    FechaInicio = reader.GetDateTime(ordInicio),
                    FechaFin = reader.GetDateTime(ordFin),
                    Monto = reader.GetDecimal(ordMonto),
                    IdInmueble = reader.GetInt32(ordIdInm),
                    IdPersona = reader.GetInt32(ordIdPer),
                    FechaCreacion = reader.GetDateTime(ordCreacion),
                    DniUsuario = reader.GetInt32(ordDniUser),
                    Estado = reader.GetBoolean(ordEstado),
                    NombreInquilino = reader.GetString(ordNomInq),
                    DireccionInmueble = reader.GetString(ordDirInm),
                    NombreUsuario = reader.GetString(ordNomUsr)
                });
            }
            return lista;
        }

        /// <summary>
        /// Obtiene un contrato por su identificador primario.
        /// </summary>
        /// <param name="idContrato">ID del contrato.</param>
        /// <returns>Instancia de <see cref="Contrato"/> o <c>null</c> si no existe.</returns>
        public Contrato? ObtenerPorId(int idContrato)
        {
            using var conexion = BDGeneral.GetConnection();
            const string sql = @"
                SELECT id_contrato, fecha_inicio, fecha_fin, monto,
                       id_inmueble, id_persona, fecha_creacion, dni_usuario, estado
                FROM contrato WHERE id_contrato = @Id;";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = idContrato;

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            int ordIdContrato = reader.GetOrdinal("id_contrato");
            int ordInicio = reader.GetOrdinal("fecha_inicio");
            int ordFin = reader.GetOrdinal("fecha_fin");
            int ordMonto = reader.GetOrdinal("monto");
            int ordIdInm = reader.GetOrdinal("id_inmueble");
            int ordIdPer = reader.GetOrdinal("id_persona");
            int ordCreacion = reader.GetOrdinal("fecha_creacion");
            int ordDniUser = reader.GetOrdinal("dni_usuario");
            int ordEstado = reader.GetOrdinal("estado");

            return new Contrato
            {
                IdContrato = reader.GetInt32(ordIdContrato),
                FechaInicio = reader.GetDateTime(ordInicio),
                FechaFin = reader.GetDateTime(ordFin),
                Monto = reader.GetDecimal(ordMonto),
                IdInmueble = reader.GetInt32(ordIdInm),
                IdPersona = reader.GetInt32(ordIdPer),
                FechaCreacion = reader.GetDateTime(ordCreacion),
                DniUsuario = reader.GetInt32(ordDniUser),
                Estado = reader.GetBoolean(ordEstado)
            };
        }
        #endregion

        // ======================================================
        //  REGIÓN: Operaciones de Actualización (Update/Estado)
        // ======================================================
        #region Operaciones de Actualización (Update/Estado)

        /// <summary>
        /// Actualiza el campo <c>estado</c> del contrato.
        /// </summary>
        /// <param name="idContrato">ID del contrato.</param>
        /// <param name="estado">Nuevo estado (true=activo, false=inactivo).</param>
        /// <returns>Cantidad de filas afectadas.</returns>
        public int ActualizarEstado(int idContrato, bool estado)
        {
            using var conexion = BDGeneral.GetConnection();
            const string sql = @"UPDATE contrato SET estado = @Estado WHERE id_contrato = @Id;";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = estado;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = idContrato;

            int rowsAffected = cmd.ExecuteNonQuery();

            // NOTIFICACIÓN: Cambiar el estado del contrato afecta Inquilinos y Contratos por Vencer.
            if (rowsAffected > 0)
            {
                AppNotifier.NotifyDashboardChange();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Actualiza datos primarios del contrato (fechas, monto, FKs, usuario y estado).
        /// </summary>
        /// <param name="c">Entidad <see cref="Contrato"/> con los valores a persistir.</param>
        /// <returns>Cantidad de filas afectadas.</returns>
        public int ActualizarContrato(Contrato c)
        {
            using var conexion = BDGeneral.GetConnection();

            const string sql = @"
            UPDATE contrato SET
                fecha_inicio = @Inicio,
                fecha_fin    = @Fin,
                monto        = @Monto,
                id_inmueble  = @IdInmueble,
                id_persona   = @IdPersona,
                dni_usuario  = @DniUsuario,
                estado       = @Estado
            WHERE id_contrato = @IdContrato;";

            using var cmd = new SqlCommand(sql, conexion);

            cmd.Parameters.Add("@Inicio", SqlDbType.Date).Value = c.FechaInicio.Date;
            cmd.Parameters.Add("@Fin", SqlDbType.Date).Value = c.FechaFin.Date;

            var pMonto = cmd.Parameters.Add("@Monto", SqlDbType.Decimal);
            pMonto.Precision = 12; pMonto.Scale = 2; pMonto.Value = c.Monto;

            cmd.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = c.IdInmueble;
            cmd.Parameters.Add("@IdPersona", SqlDbType.Int).Value = c.IdPersona;
            cmd.Parameters.Add("@DniUsuario", SqlDbType.Int).Value = c.DniUsuario;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = c.Estado;
            cmd.Parameters.Add("@IdContrato", SqlDbType.Int).Value = c.IdContrato;

            int rowsAffected = cmd.ExecuteNonQuery();

            // NOTIFICACIÓN: Actualizar el contrato afecta la grilla y potencialmente Ingresos.
            if (rowsAffected > 0)
            {
                AppNotifier.NotifyDashboardChange();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Marca un contrato como inactivo (baja lógica).
        /// </summary>
        /// <param name="idContrato">ID del contrato.</param>
        /// <returns>Filas afectadas.</returns>
        public int DarDeBajaContrato(int idContrato) => ActualizarEstado(idContrato, false);

        /// <summary>
        /// Restaura un contrato a activo.
        /// </summary>
        /// <param name="idContrato">ID del contrato.</param>
        /// <returns>Filas afectadas.</returns>
        public int RestaurarContrato(int idContrato) => ActualizarEstado(idContrato, true);
        #endregion

        // ======================================================
        //  REGIÓN: Verificaciones
        // ======================================================
        #region Verificaciones

        /// <summary>
        /// Verifica si existe al menos un contrato activo para un inmueble.
        /// </summary>
        /// <param name="idInmueble">ID del inmueble.</param>
        /// <returns><c>true</c> si existe contrato activo; en caso contrario, <c>false</c>.</returns>
        public bool ExisteContratoActivoPorInmueble(int idInmueble)
        {
            using var cn = BDGeneral.GetConnection();
            const string sql = @"SELECT TOP (1) 1 FROM contrato WHERE id_inmueble = @id AND estado = 1;";
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = idInmueble;
            var o = cmd.ExecuteScalar();
            return o != null;
        }

        /// <summary>
        /// Verifica si una persona (inquilino) posee al menos un contrato activo.
        /// </summary>
        /// <param name="idPersona">ID de la persona.</param>
        /// <returns><c>true</c> si tiene contrato activo; en caso contrario, <c>false</c>.</returns>
        public bool ExisteContratoActivoPorPersona(int idPersona)
        {
            using var cn = InmoTech.Data.BDGeneral.GetConnection();
            const string sql = @"IF EXISTS (SELECT 1 FROM dbo.contrato WHERE id_persona = @id AND estado = 1)
                         SELECT 1 ELSE SELECT 0;";
            using var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, cn);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = idPersona;
            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        #endregion
    }
}
