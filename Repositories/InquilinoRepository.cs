// ============================================================================
// InmoTech.Repositories.InquilinoRepository
// ----------------------------------------------------------------------------
// Repositorio para gestionar inquilinos (persona): altas, actualizaciones,
// cambios de estado, consultas y búsquedas paginadas.
// La documentación es breve y concisa; NO modifica el código original.
// ============================================================================

using InmoTech.Data;
using InmoTech.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using InmoTech.Services; // necesario para AppNotifier

namespace InmoTech.Repositories
{
    /// <summary>
    /// Provee operaciones CRUD, cambio de estado, consultas y búsqueda paginada
    /// para inquilinos. Notifica al dashboard cuando hay cambios relevantes.
    /// </summary>
    public class InquilinoRepository
    {
        // ======================================================
        //   REGIÓN: Operaciones de Creación y Actualización (CRUD)
        // ======================================================
        #region Operaciones de Creación y Actualización (CRUD)

        /// <summary>
        /// Inserta un nuevo inquilino y registra el DNI del usuario creador.
        /// </summary>
        /// <param name="i">Entidad <see cref="Inquilino"/> a insertar.</param>
        /// <param name="dniUsuarioCreador">DNI del usuario que realiza el alta.</param>
        /// <returns>Cantidad de filas afectadas.</returns>
        public int AgregarInquilino(Inquilino i, int dniUsuarioCreador)
        {
            using var cn = BDGeneral.GetConnection();
            // MODIFICADO: Se agrega usuario_creador_dni al INSERT
            const string sql = @"
            INSERT INTO dbo.persona (dni, nombre, apellido, telefono, email, direccion, fecha_nacimiento, estado, usuario_creador_dni)
            VALUES (@dni, @nombre, @apellido, @telefono, @email, @direccion, @fecha_nacimiento, 1, @usuario_creador_dni);";

            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@dni", SqlDbType.Int).Value = i.Dni;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 120).Value = i.Nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 120).Value = i.Apellido;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 30).Value = (object?)i.Telefono ?? DBNull.Value;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = (object?)i.Email ?? DBNull.Value;
            cmd.Parameters.Add("@direccion", SqlDbType.VarChar, 200).Value = (object?)i.Direccion ?? DBNull.Value;
            cmd.Parameters.Add("@fecha_nacimiento", SqlDbType.Date).Value =
              (i.FechaNacimiento == DateTime.MinValue ? (object)DBNull.Value : i.FechaNacimiento);

            // MODIFICADO: Se agrega el nuevo parámetro
            cmd.Parameters.Add("@usuario_creador_dni", SqlDbType.Int).Value = dniUsuarioCreador;

            int rowsAffected = cmd.ExecuteNonQuery();

            // INSERCIÓN DE NOTIFICACIÓN: Después de agregar un nuevo inquilino.
            if (rowsAffected > 0)
            {
                AppNotifier.NotifyDashboardChange();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Actualiza datos principales del inquilino identificado por su DNI.
        /// </summary>
        /// <param name="i">Entidad <see cref="Inquilino"/> con los valores a persistir.</param>
        /// <returns>Cantidad de filas afectadas.</returns>
        public int ActualizarInquilino(Inquilino i)
        {
            using var cn = BDGeneral.GetConnection();
            const string sql = @"
            UPDATE dbo.persona SET
                nombre=@nombre,
                apellido=@apellido,
                telefono=@telefono,
                email=@email,
                direccion=@direccion,
                fecha_nacimiento=@fecha_nacimiento
            WHERE dni=@dni;";

            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 120).Value = i.Nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 120).Value = i.Apellido;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 30).Value = (object?)i.Telefono ?? DBNull.Value;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = (object?)i.Email ?? DBNull.Value;
            cmd.Parameters.Add("@direccion", SqlDbType.VarChar, 200).Value = (object?)i.Direccion ?? DBNull.Value;
            cmd.Parameters.Add("@fecha_nacimiento", SqlDbType.Date).Value =
              (i.FechaNacimiento == DateTime.MinValue ? (object)DBNull.Value : i.FechaNacimiento);
            cmd.Parameters.Add("@dni", SqlDbType.Int).Value = i.Dni;

            int rowsAffected = cmd.ExecuteNonQuery();

            // INSERCIÓN DE NOTIFICACIÓN: Después de actualizar los datos del inquilino.
            if (rowsAffected > 0)
            {
                AppNotifier.NotifyDashboardChange();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Cambia el estado (activo/inactivo) de un inquilino por DNI.
        /// </summary>
        /// <param name="dni">DNI del inquilino.</param>
        /// <param name="nuevoEstado"><c>true</c> activo; <c>false</c> inactivo.</param>
        /// <returns>Cantidad de filas afectadas.</returns>
        public int ActualizarEstado(int dni, bool nuevoEstado)
        {
            using var cn = BDGeneral.GetConnection();
            const string sql = @"UPDATE dbo.persona SET estado=@estado WHERE dni=@dni;";
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@estado", SqlDbType.TinyInt).Value = nuevoEstado ? 1 : 0;
            cmd.Parameters.Add("@dni", SqlDbType.Int).Value = dni;

            int rowsAffected = cmd.ExecuteNonQuery();

            // INSERCIÓN DE NOTIFICACIÓN: Cambiar el estado afecta directamente el KPI de Inquilinos.
            if (rowsAffected > 0)
            {
                AppNotifier.NotifyDashboardChange();
            }

            return rowsAffected;
        }
        #endregion

        // ======================================================
        //  REGIÓN: Operaciones de Consulta (Read)
        // ======================================================
        #region Operaciones de Consulta (Read)

        /// <summary>
        /// Obtiene la lista de inquilinos con datos de auditoría (fecha_creacion y creador).
        /// </summary>
        /// <returns>Lista de <see cref="Inquilino"/> ordenada por Apellido, Nombre.</returns>
        public List<Inquilino> ObtenerInquilinos()
        {
            var list = new List<Inquilino>();
            using var cn = BDGeneral.GetConnection();
            // MODIFICADO: Se agregan las columnas de auditoría
            const string sql = @"
            SELECT  dni, nombre, apellido, telefono, email, direccion, fecha_nacimiento, estado,
                    fecha_creacion, usuario_creador_dni
            FROM    dbo.persona
            ORDER BY apellido, nombre;";

            using var cmd = new SqlCommand(sql, cn);
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new Inquilino
                {
                    Dni = rd.GetInt32(0),
                    Nombre = rd.GetString(1),
                    Apellido = rd.GetString(2),
                    Telefono = rd.IsDBNull(3) ? "" : rd.GetString(3),
                    Email = rd.IsDBNull(4) ? "" : rd.GetString(4),
                    Direccion = rd.IsDBNull(5) ? "" : rd.GetString(5),
                    FechaNacimiento = rd.IsDBNull(6) ? DateTime.MinValue : rd.GetDateTime(6),
                    Estado = Convert.ToByte(rd["estado"]) == 1,

                    // MODIFICADO: Se leen los nuevos campos
                    FechaCreacion = rd.GetDateTime(8),
                    UsuarioCreadorDni = rd.GetInt32(9)
                });
            }
            return list;
        }

        /// <summary>
        /// Devuelve el <c>id_persona</c> (PK) asociado a un DNI, o <c>null</c> si no existe.
        /// </summary>
        /// <param name="dni">DNI a buscar.</param>
        /// <returns>Entero PK de persona o <c>null</c>.</returns>
        public int? ObtenerIdPersonaPorDni(int dni)
        {
            using var cn = BDGeneral.GetConnection();
            const string sql = @"SELECT id_persona FROM dbo.persona WHERE dni = @dni;";
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@dni", SqlDbType.Int).Value = dni;
            var obj = cmd.ExecuteScalar();
            return obj == null ? (int?)null : Convert.ToInt32(obj);
        }
        #endregion

        // ======================================================
        //  REGIÓN: Busquedas Paginadas (Para Selectores y Grillas)
        // ======================================================
        #region Búsqueda Paginada (Para Selectores y Grillas)

        /// <summary>
        /// Búsqueda paginada por DNI/Nombre/Apellido/Email/Teléfono con filtro de estado.
        /// </summary>
        /// <param name="term">Texto de búsqueda (vacío para todos).</param>
        /// <param name="estado">Filtro de estado: <c>null</c>=todos, <c>true</c>=activos, <c>false</c>=inactivos.</param>
        /// <param name="page">Página (>= 1).</param>
        /// <param name="pageSize">Tamaño de página (>= 1).</param>
        /// <returns>Tupla con <c>items</c> (lista de <see cref="InquilinoLite"/>) y <c>total</c> (registros).</returns>
        public (List<InquilinoLite> items, int total) BuscarPaginado(
      string term, bool? estado, int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 20;

            var items = new List<InquilinoLite>();

            // Esta consulta no necesita cambios ya que InquilinoLite no incluye los campos de auditoría
            const string sql = @"
            ;WITH q AS (
                SELECT p.dni, p.nombre, p.apellido, p.telefono, p.email, p.estado
                FROM dbo.persona p
                WHERE (@term='' OR
                      CAST(p.dni AS varchar(20)) LIKE @like OR
                      p.nombre   LIKE @like OR
                      p.apellido LIKE @like OR
                      p.email    LIKE @like OR
                      p.telefono LIKE @like)
                  AND (@estado IS NULL OR p.estado = @estado)
            )
            SELECT dni, nombre, apellido, telefono, email, estado
            FROM q
            ORDER BY apellido, nombre
            OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY;

            SELECT COUNT(1)
            FROM dbo.persona p
            WHERE (@term='' OR
                  CAST(p.dni AS varchar(20)) LIKE @like OR
                  p.nombre   LIKE @like OR
                  p.apellido LIKE @like OR
                  p.email    LIKE @like OR
                  p.telefono LIKE @like)
              AND (@estado IS NULL OR p.estado = @estado);";

            using var cn = BDGeneral.GetConnection();
            using var cmd = new SqlCommand(sql, cn);

            var like = string.IsNullOrWhiteSpace(term) ? "" : $"%{term.Trim()}%";
            cmd.Parameters.Add("@term", SqlDbType.VarChar, 200).Value = string.IsNullOrWhiteSpace(term) ? "" : term.Trim();
            cmd.Parameters.Add("@like", SqlDbType.VarChar, 200).Value = like;
            cmd.Parameters.Add("@estado", SqlDbType.TinyInt).Value = (object?)(estado.HasValue ? (estado.Value ? 1 : 0) : null) ?? DBNull.Value;
            cmd.Parameters.Add("@skip", SqlDbType.Int).Value = (page - 1) * pageSize;
            cmd.Parameters.Add("@take", SqlDbType.Int).Value = pageSize;

            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                items.Add(new InquilinoLite
                {
                    Dni = rd.GetInt32(0),
                    Nombre = rd.GetString(1),
                    Apellido = rd.GetString(2),
                    Telefono = rd.IsDBNull(3) ? "" : rd.GetString(3),
                    Email = rd.IsDBNull(4) ? "" : rd.GetString(4),
                    Estado = Convert.ToByte(rd["estado"]) == 1
                });
            }

            rd.NextResult();
            rd.Read();
            var total = rd.GetInt32(0);

            return (items, total);
        }
        #endregion
    }
}
