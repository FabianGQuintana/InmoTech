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
        public List<Inquilino> ObtenerInquilinos()
        {
            var list = new List<Inquilino>();
            using var cn = BDGeneral.GetConnection();
            const string sql = @"
SELECT  dni, nombre, apellido, telefono, email, direccion, fecha_nacimiento, estado
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
                    Estado = Convert.ToByte(rd["estado"]) == 1
                });
            }
            return list;
        }

        public int AgregarInquilino(Inquilino i)
        {
            using var cn = BDGeneral.GetConnection();
            const string sql = @"
INSERT INTO dbo.persona (dni, nombre, apellido, telefono, email, direccion, fecha_nacimiento, estado)
VALUES (@dni, @nombre, @apellido, @telefono, @email, @direccion, @fecha_nacimiento, 1);";

            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@dni", SqlDbType.Int).Value = i.Dni;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 120).Value = i.Nombre;
            cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 120).Value = i.Apellido;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 30).Value = (object?)i.Telefono ?? DBNull.Value;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = (object?)i.Email ?? DBNull.Value;
            cmd.Parameters.Add("@direccion", SqlDbType.VarChar, 200).Value = (object?)i.Direccion ?? DBNull.Value;
            cmd.Parameters.Add("@fecha_nacimiento", SqlDbType.Date).Value =
                (i.FechaNacimiento == DateTime.MinValue ? (object)DBNull.Value : i.FechaNacimiento);

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
    }
}

/*
 POSIBLES PARCHES PARA EL SQL.
-- 1) Agregar columnas que usa la UI
ALTER TABLE dbo.persona
ADD apellido VARCHAR(120) NOT NULL CONSTRAINT DF_persona_apellido DEFAULT(''),
    fecha_nacimiento DATE NULL;

-- 2) Unificar tipo de DNI a INT (como en Usuarios)
--    *Si tenés datos no numéricos, corregilos antes: SELECT * FROM persona WHERE TRY_CONVERT(INT, dni) IS NULL;
ALTER TABLE dbo.persona DROP CONSTRAINT UQ_persona_dni;
ALTER TABLE dbo.persona ALTER COLUMN dni INT NOT NULL;
ALTER TABLE dbo.persona ADD CONSTRAINT UQ_persona_dni UNIQUE (dni);

-- (Opcional) Dejar estado como TINYINT o pasarlo a BIT
-- ALTER TABLE dbo.persona ADD CONSTRAINT CK_persona_estado CHECK (estado IN (0,1));

 
 
 */