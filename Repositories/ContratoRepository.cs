using InmoTech.Data;
using InmoTech.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace InmoTech.Repositories
{
    public class ContratoRepository
    {
        // ======================================================
        //  REGIÓN: Operaciones de Creación y Lógica Transaccional
        // ======================================================
        #region Operaciones de Creación y Lógica Transaccional
        public int AgregarContrato(Contrato c)
        {
            using var conexion = BDGeneral.GetConnection();
            using var transaccion = conexion.BeginTransaction();

            try
            {
                // =====================
                // 1️⃣ INSERTAR CONTRATO (Sin 'condiciones')
                // =====================
                const string sqlContrato = @"
                    INSERT INTO contrato
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

                // Parámetro @Condiciones eliminado

                cmdContrato.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = c.IdInmueble;
                cmdContrato.Parameters.Add("@IdPersona", SqlDbType.Int).Value = c.IdPersona;
                cmdContrato.Parameters.Add("@FechaCreacion", SqlDbType.DateTime).Value = c.FechaCreacion; // Considerar DEFAULT GETDATE() en DB
                cmdContrato.Parameters.Add("@DniUsuario", SqlDbType.Int).Value = c.DniUsuario;
                cmdContrato.Parameters.Add("@Estado", SqlDbType.Bit).Value = c.Estado;

                int nuevoIdContrato = Convert.ToInt32(cmdContrato.ExecuteScalar());

                // =====================
                // 2️⃣ GENERAR CUOTAS
                // =====================
                int totalMeses = ((c.FechaFin.Year - c.FechaInicio.Year) * 12) + (c.FechaFin.Month - c.FechaInicio.Month);
                // Si termina el mismo mes que empieza pero días después, es al menos 1 mes/cuota
                if (totalMeses == 0 && c.FechaFin >= c.FechaInicio) totalMeses = 1;


                if (totalMeses <= 0)
                    throw new Exception("La fecha de fin debe ser igual o posterior a la fecha de inicio del contrato.");

                DateTime primerVencimiento = c.FechaInicio.Day <= 10
                    ? new DateTime(c.FechaInicio.Year, c.FechaInicio.Month, 10)
                    : new DateTime(c.FechaInicio.Year, c.FechaInicio.Month, 1).AddMonths(1).AddDays(9);

                const string sqlCuota = @"
                    INSERT INTO cuota (id_contrato, nro_cuota, fecha_vencimiento, importe, estado, id_pago)
                    VALUES (@IdContrato, @NroCuota, @FechaVencimiento, @Importe, @Estado, NULL);";

                // =====================
                // 3️⃣ INSERTAR UNA CUOTA POR MES
                // =====================
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

                // =====================
                // 4️⃣ CONFIRMAR TODO
                // =====================
                transaccion.Commit();
                return 1; // Indica éxito (se insertó 1 contrato y sus cuotas)
            }
            catch
            {
                transaccion.Rollback();
                throw; // Re-lanza la excepción para que sea manejada más arriba
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: Operaciones de Consulta (Read)
        // ======================================================
        #region Operaciones de Consulta (Read)
        public List<Contrato> ObtenerContratos(int? dniUsuario = null)
        {
            using var conexion = BDGeneral.GetConnection();

            // Quitado 'c.condiciones' del SELECT
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

            // Obtener ordinales una vez
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
                    // Condiciones eliminada
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

        public Contrato? ObtenerPorId(int idContrato)
        {
            using var conexion = BDGeneral.GetConnection();
            // Seleccionar columnas explícitamente sin 'condiciones'
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
                // Condiciones eliminada
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
        public int ActualizarEstado(int idContrato, bool estado)
        {
            using var conexion = BDGeneral.GetConnection();
            const string sql = @"UPDATE contrato SET estado = @Estado WHERE id_contrato = @Id;";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = estado;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = idContrato;

            return cmd.ExecuteNonQuery();
        }

        public int ActualizarContrato(Contrato c)
        {
            using var conexion = BDGeneral.GetConnection();

            // Quitado 'condiciones = @Condiciones' del UPDATE
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

            // Parámetro @Condiciones eliminado

            cmd.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = c.IdInmueble;
            cmd.Parameters.Add("@IdPersona", SqlDbType.Int).Value = c.IdPersona;
            cmd.Parameters.Add("@DniUsuario", SqlDbType.Int).Value = c.DniUsuario;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = c.Estado;
            cmd.Parameters.Add("@IdContrato", SqlDbType.Int).Value = c.IdContrato;

            return cmd.ExecuteNonQuery();
        }

        public int DarDeBajaContrato(int idContrato) => ActualizarEstado(idContrato, false);
        public int RestaurarContrato(int idContrato) => ActualizarEstado(idContrato, true);
        #endregion
    }
}