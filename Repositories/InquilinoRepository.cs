using InmoTech.Data;
using InmoTech.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace InmoTech.Repositories
{
    public class InquilinoRepository
    {
        // ======================================================
        //   REGIÓN: Operaciones de Creación y Actualización (CRUD)
        // ======================================================
        #region Operaciones de Creación y Actualización (CRUD)

        // MODIFICADO: Acepta dniUsuarioCreador
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

            return cmd.ExecuteNonQuery();
        }

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

            return cmd.ExecuteNonQuery();
        }

        public int ActualizarEstado(int dni, bool nuevoEstado)
        {
            using var cn = BDGeneral.GetConnection();
            const string sql = @"UPDATE dbo.persona SET estado=@estado WHERE dni=@dni;";
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@estado", SqlDbType.TinyInt).Value = nuevoEstado ? 1 : 0;
            cmd.Parameters.Add("@dni", SqlDbType.Int).Value = dni;
            return cmd.ExecuteNonQuery();
        }
        #endregion

        // ======================================================
        //  REGIÓN: Operaciones de Consulta (Read)
        // ======================================================
        #region Operaciones de Consulta (Read)
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

        /// <summary>Devuelve el id_persona (PK) para un DNI. Null si no existe.</summary>
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

        /// Búsqueda paginada por DNI/Nombre/Apellido/Email/Teléfono.
        /// estado: null=Todos, true=Activos, false=Inactivos

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