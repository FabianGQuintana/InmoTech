// ============================================================================
// InmoTech.Repositories.UsuarioRepository
// ----------------------------------------------------------------------------
// Repositorio para operaciones CRUD y autenticación sobre la entidad Usuario.
// La documentación es breve y concisa, y NO modifica el código original.
// ============================================================================

using InmoTech.Data;
using InmoTech.Models;
using InmoTech.Security;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace InmoTech.Repositories
{
    /// <summary>
    /// Provee métodos para crear, actualizar, consultar y administrar estado de usuarios,
    /// así como verificar credenciales de acceso.
    /// </summary>
    public class UsuarioRepository
    {
        // ======================================================
        //  REGIÓN: Operaciones de Creación (Insert)
        // ======================================================
        #region Operaciones de Creación (Insert)
        // ================
        // INSERT
        // ================
        /// <summary>
        /// Inserta un nuevo usuario aplicando hash a la contraseña.
        /// </summary>
        /// <param name="usuario">Datos del usuario a crear.</param>
        /// <param name="dniUsuarioCreador">DNI del usuario que crea el registro.</param>
        /// <returns>Número de filas afectadas.</returns>
        public int AgregarUsuario(Usuario usuario, int dniUsuarioCreador)
        {
            using var conexion = BDGeneral.GetConnection();

            //  Hashear contraseña ANTES de guardarla
            string hashedPassword = PasswordHasher.Hash(usuario.Password);

            // MODIFICADO: Se agrega usuario_creador_dni al INSERT.
            // (fecha_creacion se inserta sola por el DEFAULT GETDATE() de la DB)
            const string sql = @"
            INSERT INTO usuario (dni, nombre, apellido, email, password, telefono, fecha_nacimiento, id_rol, usuario_creador_dni)
            VALUES (@Dni, @Nombre, @Apellido, @Email, @Password, @Telefono, @FechaNacimiento, @IdRol, @UsuarioCreadorDni);";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Dni", SqlDbType.Int).Value = usuario.Dni;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = usuario.Nombre;
            cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 200).Value = usuario.Apellido;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = usuario.Email;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 600).Value = hashedPassword;
            cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 100).Value = usuario.Telefono;
            cmd.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = usuario.FechaNacimiento.Date;
            cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = usuario.IdRol;

            // MODIFICADO: Se agrega el nuevo parámetro
            cmd.Parameters.Add("@UsuarioCreadorDni", SqlDbType.Int).Value = dniUsuarioCreador;

            return cmd.ExecuteNonQuery();
        }
        #endregion

        // ... (La región de UPDATE no se toca, esos campos no deben actualizarse) ...
        #region Operaciones de Actualización (Update)
        // (Sin cambios aquí)
        /// <summary>
        /// Actualiza datos del usuario. Opcionalmente actualiza y re-hashea la contraseña.
        /// </summary>
        /// <param name="usuario">Entidad con los nuevos valores.</param>
        /// <param name="actualizarPassword">Si es <c>true</c>, actualiza la contraseña.</param>
        /// <returns>Número de filas afectadas.</returns>
        public int ActualizarUsuario(Usuario usuario, bool actualizarPassword)
        {
            // ... (código existente sin modificar) ...
            // OMITIDO POR BREVEDAD
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
                //  Hashear contraseña nueva
                string hashedPassword = PasswordHasher.Hash(usuario.Password);
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 600).Value = hashedPassword;
            }

            return cmd.ExecuteNonQuery();
        }
        #endregion


        // ======================================================
        //  REGIÓN: Operaciones de Consulta (Read)
        // ======================================================
        #region Operaciones de Consulta (Read)
        // ================
        // SELECT ALL
        // ================
        /// <summary>
        /// Obtiene la lista completa de usuarios, incluyendo metadatos de creación.
        /// </summary>
        /// <returns>Lista de <see cref="Usuario"/>.</returns>
        public List<Usuario> ObtenerUsuarios()
        {
            using var connection = BDGeneral.GetConnection();

            // MODIFICADO: Se agregan las nuevas columnas al SELECT
            const string sqlQuery = @"
            SELECT dni, nombre, apellido, email, telefono, estado, fecha_nacimiento, id_rol,
                   fecha_creacion, usuario_creador_dni
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

            // MODIFICADO: Se obtienen los índices de las nuevas columnas
            int ordFechaCreacion = reader.GetOrdinal("fecha_creacion");
            int ordUsuarioCreadorDni = reader.GetOrdinal("usuario_creador_dni");

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
                    Password = string.Empty, //  nunca exponemos el hash

                    // MODIFICADO: Se cargan las nuevas propiedades
                    FechaCreacion = reader.GetDateTime(ordFechaCreacion),
                    UsuarioCreadorDni = reader.GetInt32(ordUsuarioCreadorDni)
                });
            }

            return users;
        }

        // ================
        // OBTENER POR EMAIL
        // ================
        /// <summary>
        /// Obtiene un usuario por su email (incluye el hash de contraseña para validación).
        /// </summary>
        /// <param name="email">Correo electrónico a buscar.</param>
        /// <returns><see cref="Usuario"/> o <c>null</c> si no existe.</returns>
        public Usuario? ObtenerPorEmail(string email)
        {
            using var conexion = BDGeneral.GetConnection();

            // MODIFICADO: Se agregan las nuevas columnas al SELECT
            const string sql = @"
            SELECT dni, nombre, apellido, email, password, telefono, estado, fecha_nacimiento, id_rol,
                   fecha_creacion, usuario_creador_dni
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
                IdRol = reader.GetInt32(reader.GetOrdinal("id_rol")),

                // MODIFICADO: Se cargan las nuevas propiedades
                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("fecha_creacion")),
                UsuarioCreadorDni = reader.GetInt32(reader.GetOrdinal("usuario_creador_dni"))
            };
        }
        #endregion

        // ======================================================
        //  REGIÓN: Operaciones de Estado y Baja (Update/Delete)
        // ======================================================
        #region Operaciones de Estado y Baja (Update/Delete)
        // ================
        // ESTADO
        // ================
        /// <summary>
        /// Actualiza el estado (activo/inactivo) de un usuario.
        /// </summary>
        /// <param name="dni">DNI del usuario.</param>
        /// <param name="estado">Nuevo estado (true = activo).</param>
        /// <returns>Número de filas afectadas.</returns>
        public int ActualizarEstado(int dni, bool estado)
        {
            using var conexion = BDGeneral.GetConnection();
            const string sql = @"UPDATE usuario SET estado = @Estado WHERE dni = @Dni;";

            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = estado;
            cmd.Parameters.Add("@Dni", SqlDbType.Int).Value = dni;

            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Marca al usuario como inactivo (baja lógica).
        /// </summary>
        /// <param name="dni">DNI del usuario.</param>
        /// <returns>Número de filas afectadas.</returns>
        public int DarDeBajaUsuario(int dni) => ActualizarEstado(dni, false);
        #endregion

        // ======================================================
        //  REGIÓN: Logica de Autenticación (Login)
        // ======================================================
        #region Lógica de Autenticación (Login)
        // ================
        // LOGIN
        // ================
        /// <summary>
        /// Valida credenciales comparando el password plano con el hash almacenado y verifica estado activo.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="passwordPlano">Contraseña en texto plano a verificar.</param>
        /// <returns>Usuario autenticado sin hash en memoria, o <c>null</c> si falla.</returns>
        public Usuario? ValidarCredenciales(string email, string passwordPlano)
        {
            var user = ObtenerPorEmail(email);
            if (user is null) return null;

            //  Verificar hash + estado
            bool ok = PasswordHasher.Verify(passwordPlano, user.Password);
            if (!ok || !user.Estado) return null;

            user.Password = string.Empty; //  nunca exponer hash
            return user;
        }
        #endregion
    }
}
