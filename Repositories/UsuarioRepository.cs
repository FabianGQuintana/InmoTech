using InmoTech.Data;
using InmoTech.Models;
using InmoTech.Security; 
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace InmoTech.Repositories
{
    public class UsuarioRepository
    {
        // ================
        // INSERT
        // ================
        public int AgregarUsuario(Usuario usuario)
        {
            using var conexion = BDGeneral.GetConnection();

            // 🔐 Hashear contraseña ANTES de guardarla
            string hashedPassword = PasswordHasher.Hash(usuario.Password);

            const string sql = @"
            INSERT INTO usuario (dni, nombre, apellido, email, password, telefono, fecha_nacimiento, id_rol)
            VALUES (@Dni, @Nombre, @Apellido, @Email, @Password, @Telefono, @FechaNacimiento, @IdRol);";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Dni", SqlDbType.Int).Value = usuario.Dni;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = usuario.Nombre;
            cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 200).Value = usuario.Apellido;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = usuario.Email;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 600).Value = hashedPassword;
            cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 100).Value = usuario.Telefono;
            cmd.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = usuario.FechaNacimiento.Date;
            cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = usuario.IdRol;

            return cmd.ExecuteNonQuery();
        }

        // ================
        // UPDATE
        // ================
        public int ActualizarUsuario(Usuario usuario, bool actualizarPassword)
        {
            using var conexion = BDGeneral.GetConnection();

            var sql = actualizarPassword
                ? @"
            UPDATE usuario
               SET nombre = @Nombre,
                   apellido = @Apellido,
                   email = @Email,
                   password = @Password,
                   telefono = @Telefono,
                   estado = @Estado,
                   fecha_nacimiento = @FechaNacimiento,
                   id_rol = @IdRol
             WHERE dni = @Dni;"
                : @"
            UPDATE usuario
               SET nombre = @Nombre,
                   apellido = @Apellido,
                   email = @Email,
                   telefono = @Telefono,
                   estado = @Estado,
                   fecha_nacimiento = @FechaNacimiento,
                   id_rol = @IdRol
             WHERE dni = @Dni;";

            using var cmd = new SqlCommand(sql, conexion);

            cmd.Parameters.Add("@Dni", SqlDbType.Int).Value = usuario.Dni;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = usuario.Nombre;
            cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 200).Value = usuario.Apellido;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = usuario.Email;
            cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 100).Value = usuario.Telefono;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = usuario.Estado;
            cmd.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = usuario.FechaNacimiento.Date;
            cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = usuario.IdRol;

            if (actualizarPassword)
            {
                // 🔐 Hashear contraseña nueva
                string hashedPassword = PasswordHasher.Hash(usuario.Password);
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 600).Value = hashedPassword;
            }

            return cmd.ExecuteNonQuery();
        }

        // ================
        // SELECT ALL
        // ================
        public List<Usuario> ObtenerUsuarios()
        {
            using var connection = BDGeneral.GetConnection();

            const string sqlQuery = @"
            SELECT dni, nombre, apellido, email, telefono, estado, fecha_nacimiento, id_rol
            FROM usuario
            ORDER BY apellido, nombre;";

            using var command = new SqlCommand(sqlQuery, connection);
            using var reader = command.ExecuteReader();

            var users = new List<Usuario>();

            int ordDni = reader.GetOrdinal("dni");
            int ordNombre = reader.GetOrdinal("nombre");
            int ordApellido = reader.GetOrdinal("apellido");
            int ordEmail = reader.GetOrdinal("email");
            int ordTelefono = reader.GetOrdinal("telefono");
            int ordEstado = reader.GetOrdinal("estado");
            int ordFechaNacimiento = reader.GetOrdinal("fecha_nacimiento");
            int ordIdRol = reader.GetOrdinal("id_rol");

            while (reader.Read())
            {
                users.Add(new Usuario
                {
                    Dni = reader.GetInt32(ordDni),
                    Nombre = reader.GetString(ordNombre),
                    Apellido = reader.GetString(ordApellido),
                    Email = reader.GetString(ordEmail),
                    Telefono = reader.GetString(ordTelefono),
                    Estado = reader.GetBoolean(ordEstado),
                    FechaNacimiento = reader.GetDateTime(ordFechaNacimiento),
                    IdRol = reader.GetInt32(ordIdRol),
                    Password = string.Empty // 🚫 nunca exponemos el hash
                });
            }

            return users;
        }

        // ================
        // ESTADO
        // ================
        public int ActualizarEstado(int dni, bool estado)
        {
            using var conexion = BDGeneral.GetConnection();
            const string sql = @"UPDATE usuario SET estado = @Estado WHERE dni = @Dni;";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = estado;
            cmd.Parameters.Add("@Dni", SqlDbType.Int).Value = dni;

            return cmd.ExecuteNonQuery();
        }

        public int DarDeBajaUsuario(int dni) => ActualizarEstado(dni, false);

        // ================
        // OBTENER POR EMAIL
        // ================
        public Usuario? ObtenerPorEmail(string email)
        {
            using var conexion = BDGeneral.GetConnection();

            const string sql = @"
            SELECT dni, nombre, apellido, email, password, telefono, estado, fecha_nacimiento, id_rol
            FROM usuario
            WHERE email = @Email;";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = email;

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new Usuario
            {
                Dni = reader.GetInt32(reader.GetOrdinal("dni")),
                Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                Email = reader.GetString(reader.GetOrdinal("email")),
                Password = reader.GetString(reader.GetOrdinal("password")), // hash para validación
                Telefono = reader.GetString(reader.GetOrdinal("telefono")),
                Estado = reader.GetBoolean(reader.GetOrdinal("estado")),
                FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento")),
                IdRol = reader.GetInt32(reader.GetOrdinal("id_rol"))
            };
        }

        // ================
        // LOGIN
        // ================
        public Usuario? ValidarCredenciales(string email, string passwordPlano)
        {
            var user = ObtenerPorEmail(email);
            if (user is null) return null;

            // 🔐 Verificar hash + estado
            bool ok = PasswordHasher.Verify(passwordPlano, user.Password);
            if (!ok || !user.Estado) return null;

            user.Password = string.Empty; // 🚫 nunca exponer hash
            return user;
        }
    }
}
