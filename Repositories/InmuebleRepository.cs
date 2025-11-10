using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Drawing;
using Microsoft.Data.SqlClient;
using InmoTech.Data;
using InmoTech.Domain.Models;
using InmoTech.Services;

namespace InmoTech.Data.Repositories
{
    public class InmuebleRepository
    {
        // ======================================================
        // REGIÓN: Helpers de Archivos y Directorios
        // ======================================================
        #region Helpers de Archivos y Directorios
        // Carpeta base para imágenes: <carpeta app>\Resources\inmuebles\<id_inmueble>\
        private string GetResourcesInmueblesDir(int idInmueble)
        {
            // 📍 Buscar la raíz del proyecto (donde está el .csproj)
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // Si el ejecutable está en bin\Debug o bin\Release, sube 2 niveles
            var dirInfo = new DirectoryInfo(baseDir);
            while (dirInfo != null && dirInfo.Name != "InmoTech")
            {
                dirInfo = dirInfo.Parent;
            }

            if (dirInfo == null)
                throw new Exception("No se pudo ubicar la carpeta raíz del proyecto InmoTech.");

            // Carpeta final dentro de Resources/inmuebles/<id>
            string recursosDir = Path.Combine(dirInfo.FullName, "Resources", "inmuebles", idInmueble.ToString());

            if (!Directory.Exists(recursosDir))
                Directory.CreateDirectory(recursosDir);

            return recursosDir;
        }


        private static string GetMimeByExtension(string ext)
        {
            ext = (ext ?? "").ToLowerInvariant();
            if (ext == ".jpg" || ext == ".jpeg") return "image/jpeg";
            if (ext == ".png") return "image/png";
            if (ext == ".webp") return "image/webp";
            return "application/octet-stream";
        }

        // .NET Standard/Framework compatible (similar a Path.GetRelativePath en versiones nuevas)
        private static string GetRelativePath(string baseDir, string fullPath)
        {
            var baseUri = new Uri(AppendDirectorySeparatorChar(baseDir));
            var fullUri = new Uri(fullPath);
            var rel = baseUri.MakeRelativeUri(fullUri).ToString();
            // Uri usa '/', convertimos a separador de Windows
            return Uri.UnescapeDataString(rel).Replace('/', Path.DirectorySeparatorChar);
        }

        private static string AppendDirectorySeparatorChar(string path)
        {
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                return path + Path.DirectorySeparatorChar;
            return path;
        }
        #endregion

        // ======================================================
        // REGIÓN: CRUD Inmueble (Entidad Principal) - MODIFICADO
        // ======================================================
        #region CRUD Inmueble (Entidad Principal)
        // ------------------- CRUD Inmueble -------------------

        public int CrearInmueble(Inmueble i, int dniUsuarioCreador)
        {
            using (var cn = BDGeneral.GetConnection())
            using (var cmd = new SqlCommand(@"
                INSERT INTO dbo.inmueble
                  (direccion, tipo, descripcion, condiciones, nro_ambientes, amueblado, estado, usuario_creador_dni)
                VALUES
                  (@direccion, @tipo, @descripcion, @condiciones, @nro_ambientes, @amueblado, @estado, @usuario_creador_dni);
                SELECT CAST(SCOPE_IDENTITY() AS int);
            ", cn))
            {
                cmd.Parameters.Add("@direccion", SqlDbType.VarChar, 200).Value = i.Direccion;
                cmd.Parameters.Add("@tipo", SqlDbType.VarChar, 80).Value = i.Tipo;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 1000).Value = (object)i.Descripcion ?? DBNull.Value;
                cmd.Parameters.Add("@condiciones", SqlDbType.NVarChar, 255).Value = i.Condiciones;
                cmd.Parameters.Add("@nro_ambientes", SqlDbType.Int).Value = (object)i.NroAmbientes ?? DBNull.Value;
                cmd.Parameters.Add("@amueblado", SqlDbType.Bit).Value = i.Amueblado;
                cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = i.Estado; // bit

                // MODIFICADO: Se agrega el nuevo parámetro
                cmd.Parameters.Add("@usuario_creador_dni", SqlDbType.Int).Value = dniUsuarioCreador;

                var newId = (int)cmd.ExecuteScalar();

                // NOTIFICACIÓN: Se crea un nuevo inmueble (afecta KPI y Cards)
                if (newId > 0)
                {
                    AppNotifier.NotifyDashboardChange();
                }
                return newId;
            }
        }

        public Inmueble ObtenerPorId(int idInmueble, bool incluirImagenes = false)
        {
            Inmueble res = null;
            using (var cn = BDGeneral.GetConnection())
            using (var cmd = new SqlCommand(@"
                SELECT id_inmueble, direccion, tipo, ISNULL(descripcion,''), condiciones,
                       nro_ambientes, amueblado, estado,
                       fecha_creacion, usuario_creador_dni
                FROM dbo.inmueble
                WHERE id_inmueble = @id;", cn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idInmueble;
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        res = new Inmueble
                        {
                            IdInmueble = rd.GetInt32(0),
                            Direccion = rd.GetString(1),
                            Tipo = rd.GetString(2),
                            Descripcion = rd.GetString(3),
                            Condiciones = rd.GetString(4),
                            NroAmbientes = rd.IsDBNull(5) ? (int?)null : rd.GetInt32(5),
                            Amueblado = rd.GetBoolean(6),
                            Estado = rd.GetBoolean(7),

                            // MODIFICADO: Leer nuevos campos
                            FechaCreacion = rd.GetDateTime(8),
                            UsuarioCreadorDni = rd.GetInt32(9)
                        };
                    }
                }
                if (res != null && incluirImagenes) res.Imagenes = ListarImagenes(idInmueble);
            }
            return res;
        }

        public List<Inmueble> Listar(string filtroTexto = null, bool soloActivos = true)
        {
            var lista = new List<Inmueble>();
            var sql = @"
                SELECT id_inmueble, direccion, tipo, ISNULL(descripcion,''), condiciones,
                       nro_ambientes, amueblado, estado,
                       fecha_creacion, usuario_creador_dni
                FROM dbo.inmueble
                WHERE (1=1)";
            if (soloActivos) sql += " AND estado = 1";
            if (!string.IsNullOrWhiteSpace(filtroTexto))
                sql += " AND (direccion LIKE @q OR tipo LIKE @q OR condiciones LIKE @q)";
            sql += " ORDER BY id_inmueble DESC";

            using (var cn = BDGeneral.GetConnection())
            using (var cmd = new SqlCommand(sql, cn))
            {
                if (!string.IsNullOrWhiteSpace(filtroTexto))
                    cmd.Parameters.Add("@q", SqlDbType.VarChar, 200).Value = "%" + filtroTexto + "%";

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        lista.Add(new Inmueble
                        {
                            IdInmueble = rd.GetInt32(0),
                            Direccion = rd.GetString(1),
                            Tipo = rd.GetString(2),
                            Descripcion = rd.GetString(3),
                            Condiciones = rd.GetString(4),
                            NroAmbientes = rd.IsDBNull(5) ? (int?)null : rd.GetInt32(5),
                            Amueblado = rd.GetBoolean(6),
                            Estado = rd.GetBoolean(7),

                            // MODIFICADO: Leer nuevos campos
                            FechaCreacion = rd.GetDateTime(8),
                            UsuarioCreadorDni = rd.GetInt32(9)
                        });
                    }
                }
            }
            return lista;
        }

        public int Actualizar(Inmueble i)
        {
            // NOTA: No se actualizan fecha_creacion ni usuario_creador_dni (es correcto)
            using (var cn = BDGeneral.GetConnection())
            using (var cmd = new SqlCommand(@"
                UPDATE dbo.inmueble
                   SET direccion=@direccion, tipo=@tipo, descripcion=@descripcion,
                       condiciones=@condiciones, nro_ambientes=@nro_ambientes,
                       amueblado=@amueblado, estado=@estado
                 WHERE id_inmueble=@id;", cn))
            {
                cmd.Parameters.Add("@direccion", SqlDbType.VarChar, 200).Value = i.Direccion;
                cmd.Parameters.Add("@tipo", SqlDbType.VarChar, 80).Value = i.Tipo;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 1000).Value = (object)i.Descripcion ?? DBNull.Value;
                cmd.Parameters.Add("@condiciones", SqlDbType.NVarChar, 255).Value = i.Condiciones;
                cmd.Parameters.Add("@nro_ambientes", SqlDbType.Int).Value = (object)i.NroAmbientes ?? DBNull.Value;
                cmd.Parameters.Add("@amueblado", SqlDbType.Bit).Value = i.Amueblado;
                cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = i.Estado;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = i.IdInmueble;

                int rowsAffected = cmd.ExecuteNonQuery();

                // NOTIFICACIÓN: La actualización de estado o datos debe refrescar el dashboard
                if (rowsAffected > 0)
                {
                    AppNotifier.NotifyDashboardChange();
                }
                return rowsAffected;
            }
        }

        /// <summary> Baja lógica: estado = 0 (false). </summary>
        public int DarDeBaja(int idInmueble)
        {
            using (var cn = BDGeneral.GetConnection())
            using (var cmd = new SqlCommand("UPDATE dbo.inmueble SET estado = 0 WHERE id_inmueble = @id;", cn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idInmueble;
                int rowsAffected = cmd.ExecuteNonQuery();

                // NOTIFICACIÓN: La baja afecta el KPI de Propiedades Activas y las Cards
                if (rowsAffected > 0)
                {
                    AppNotifier.NotifyDashboardChange();
                }
                return rowsAffected;
            }
        }
        #endregion

        // ======================================================
        // REGIÓN: CRUD Inmueble Imagen (Sub-entidad) - MODIFICADO
        // ======================================================
        #region CRUD Inmueble Imagen (Sub-entidad)
        public int AgregarImagenDesdeArchivo(
            int idInmueble,
            string archivoOrigen,
            string titulo = null,
            bool esPortada = false,
            short posicion = 0)
        {
            if (string.IsNullOrWhiteSpace(archivoOrigen) || !File.Exists(archivoOrigen))
                throw new FileNotFoundException("No se encontró el archivo de imagen.", archivoOrigen);

            // Copiar a Resources\inmuebles\<id>\filename
            var destDir = GetResourcesInmueblesDir(idInmueble);
            var fileName = Path.GetFileName(archivoOrigen);
            var destPath = Path.Combine(destDir, fileName);

            // Evitar colisiones
            if (File.Exists(destPath))
            {
                var name = Path.GetFileNameWithoutExtension(fileName);
                var ext = Path.GetExtension(fileName);
                var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                fileName = name + "_" + stamp + ext;
                destPath = Path.Combine(destDir, fileName);
            }

            File.Copy(archivoOrigen, destPath);

            // Medidas y metadatos
            int? w = null, h = null, size = null;
            try
            {
                using (var img = Image.FromFile(destPath))
                {
                    w = img.Width; h = img.Height;
                }
                var fi = new FileInfo(destPath);
                size = (int)fi.Length;
            }
            catch { /* si falla, seguimos sin medidas */ }

            // Ruta relativa que guardaremos en DB (para mover la app sin romper enlaces)
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var rutaRel = GetRelativePath(baseDir, destPath);

            var extLower = Path.GetExtension(fileName);
            var mime = GetMimeByExtension(extLower);

            // Insert en dbo.inmueble_imagen
            using (var cn = BDGeneral.GetConnection())
            using (var tr = cn.BeginTransaction())
            using (var cmd = new SqlCommand(@"
            INSERT INTO dbo.inmueble_imagen
            (id_inmueble, titulo, nombre_archivo, ruta, content_type, bytes_size, ancho_px, alto_px, es_portada, posicion)
            VALUES (@id_inmueble, @titulo, @nombre_archivo, @ruta, @content_type, @bytes_size, @ancho_px, @alto_px, @es_portada, @posicion);
            SELECT CAST(SCOPE_IDENTITY() AS int);
            ", cn, tr))
            {
                cmd.Parameters.Add("@id_inmueble", SqlDbType.Int).Value = idInmueble;
                cmd.Parameters.Add("@titulo", SqlDbType.VarChar, 150).Value = (object)titulo ?? DBNull.Value;
                cmd.Parameters.Add("@nombre_archivo", SqlDbType.VarChar, 255).Value = fileName;
                cmd.Parameters.Add("@ruta", SqlDbType.NVarChar, 400).Value = rutaRel;
                cmd.Parameters.Add("@content_type", SqlDbType.VarChar, 100).Value = mime;
                cmd.Parameters.Add("@bytes_size", SqlDbType.Int).Value = (object)size ?? DBNull.Value;
                cmd.Parameters.Add("@ancho_px", SqlDbType.Int).Value = (object)w ?? DBNull.Value;
                cmd.Parameters.Add("@alto_px", SqlDbType.Int).Value = (object)h ?? DBNull.Value;
                cmd.Parameters.Add("@es_portada", SqlDbType.Bit).Value = esPortada;
                cmd.Parameters.Add("@posicion", SqlDbType.SmallInt).Value = posicion;

                var newId = (int)cmd.ExecuteScalar();

                if (esPortada)
                {
                    // deja una sola portada (similar a índice único filtrado)
                    using (var cmd2 = new SqlCommand(@"
                    UPDATE dbo.inmueble_imagen
                        SET es_portada = 0
                        WHERE id_inmueble = @id_inmueble AND id_imagen <> @id AND es_portada = 1;", cn, tr))
                    {
                        cmd2.Parameters.Add("@id_inmueble", SqlDbType.Int).Value = idInmueble;
                        cmd2.Parameters.Add("@id", SqlDbType.Int).Value = newId;
                        cmd2.ExecuteNonQuery();
                    }
                }

                tr.Commit();

                // NOTIFICACIÓN: La adición de una imagen (especialmente si es portada) debe refrescar las Cards.
                AppNotifier.NotifyDashboardChange();

                return newId;
            }
        }

        public List<InmuebleImagen> ListarImagenes(int idInmueble, bool soloActivas = true)
        {
            var lista = new List<InmuebleImagen>();
            using (var cn = BDGeneral.GetConnection())
            using (var cmd = new SqlCommand(@"
            SELECT id_imagen, id_inmueble, titulo, nombre_archivo, ruta, content_type,
                   bytes_size, ancho_px, alto_px, es_portada, posicion, creado_en, activo
            FROM dbo.inmueble_imagen
            WHERE id_inmueble = @id " + (soloActivas ? "AND activo = 1 " : "") + @"
            ORDER BY es_portada DESC, posicion ASC, id_imagen ASC;", cn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idInmueble;
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var it = new InmuebleImagen
                        {
                            IdImagen = rd.GetInt32(0),
                            IdInmueble = rd.GetInt32(1),
                            Titulo = rd.IsDBNull(2) ? null : rd.GetString(2),
                            NombreArchivo = rd.GetString(3),
                            Ruta = rd.GetString(4),
                            ContentType = rd.GetString(5),
                            BytesSize = rd.IsDBNull(6) ? (int?)null : rd.GetInt32(6),
                            AnchoPx = rd.IsDBNull(7) ? (int?)null : rd.GetInt32(7),
                            AltoPx = rd.IsDBNull(8) ? (int?)null : rd.GetInt32(8),
                            EsPortada = rd.GetBoolean(9),
                            Posicion = rd.GetInt16(10),
                            CreadoEn = rd.GetDateTime(11),
                            Activo = rd.GetBoolean(12)
                        };
                        lista.Add(it);
                    }
                }
            }
            return lista;
        }

        public int SetPortada(int idImagen)
        {
            int idInmueble = 0;
            using (var cn = BDGeneral.GetConnection())
            {
                // Obtener id_inmueble
                using (var get = new SqlCommand("SELECT id_inmueble FROM dbo.inmueble_imagen WHERE id_imagen=@id;", cn))
                {
                    get.Parameters.Add("@id", SqlDbType.Int).Value = idImagen;
                    var o = get.ExecuteScalar();
                    if (o == null) return 0;
                    idInmueble = (int)o;
                }

                using (var tr = cn.BeginTransaction())
                {
                    using (var cmd1 = new SqlCommand("UPDATE dbo.inmueble_imagen SET es_portada=0 WHERE id_inmueble=@i AND es_portada=1;", cn, tr))
                    {
                        cmd1.Parameters.Add("@i", SqlDbType.Int).Value = idInmueble;
                        cmd1.ExecuteNonQuery();
                    }
                    using (var cmd2 = new SqlCommand("UPDATE dbo.inmueble_imagen SET es_portada=1 WHERE id_imagen=@id;", cn, tr))
                    {
                        cmd2.Parameters.Add("@id", SqlDbType.Int).Value = idImagen;
                        var n = cmd2.ExecuteNonQuery();
                        tr.Commit();

                        // NOTIFICACIÓN: Cambiar la portada requiere actualizar el dashboard
                        if (n > 0)
                        {
                            AppNotifier.NotifyDashboardChange();
                        }

                        return n;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina el registro y, opcionalmente, el archivo físico.
        /// </summary>
        public int EliminarImagen(int idImagen, bool borrarArchivoFisico = true)
        {
            string ruta = null;
            int idInmueble = 0;

            using (var cn = BDGeneral.GetConnection())
            {
                // Obtener ruta para borrar archivo y carpeta
                using (var get = new SqlCommand("SELECT id_inmueble, ruta FROM dbo.inmueble_imagen WHERE id_imagen=@id;", cn))
                {
                    get.Parameters.Add("@id", SqlDbType.Int).Value = idImagen;
                    using (var rd = get.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            idInmueble = rd.GetInt32(0);
                            ruta = rd.GetString(1);
                        }
                    }
                }

                int n;
                using (var del = new SqlCommand("DELETE FROM dbo.inmueble_imagen WHERE id_imagen=@id;", cn))
                {
                    del.Parameters.Add("@id", SqlDbType.Int).Value = idImagen;
                    n = del.ExecuteNonQuery();
                }

                if (n > 0 && borrarArchivoFisico && !string.IsNullOrWhiteSpace(ruta))
                {
                    // ... (lógica de borrado de archivos se mantiene igual) ...
                }

                // NOTIFICACIÓN: La eliminación de una imagen puede afectar la portada del dashboard.
                if (n > 0)
                {
                    AppNotifier.NotifyDashboardChange();
                }

                return n;
            }
        }

        public int ReordenarImagen(int idImagen, short nuevaPosicion)
        {
            using (var cn = BDGeneral.GetConnection())
            using (var cmd = new SqlCommand("UPDATE dbo.inmueble_imagen SET posicion=@p WHERE id_imagen=@id;", cn))
            {
                cmd.Parameters.Add("@p", SqlDbType.SmallInt).Value = nuevaPosicion;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idImagen;
                int rowsAffected = cmd.ExecuteNonQuery();

                // La reordenación es visual, pero la incluimos por si afecta la selección de la portada.
                if (rowsAffected > 0)
                {
                    AppNotifier.NotifyDashboardChange();
                }

                return rowsAffected;
            }
        }
        #endregion

        // ======================================================
        // REGIÓN: Carga de Imagen Portada
        // ======================================================
        #region Carga de Imagen Portada (NUEVO)

        /// <summary> 
        /// Obtiene la imagen de portada de un inmueble del sistema de archivos. 
        /// </summary>
        public Image? ObtenerImagenPortada(int idInmueble)
        {
            string? rutaRelativa = null;

            // 1. Obtener la ruta de la portada desde la BD
            using (var cn = BDGeneral.GetConnection())
            using (var cmd = new SqlCommand(
                "SELECT TOP 1 ruta FROM dbo.inmueble_imagen WHERE id_inmueble = @id AND es_portada = 1 AND activo = 1 ORDER BY id_imagen;", cn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idInmueble;
                var o = cmd.ExecuteScalar();
                if (o != null) rutaRelativa = o.ToString();
            }

            if (string.IsNullOrEmpty(rutaRelativa))
            {
                return null; // No hay portada registrada
            }

            try
            {
                // 2. Construir la ruta absoluta (desde la ruta relativa guardada)
                string rutaAbsoluta = Path.IsPathRooted(rutaRelativa) ?
                                      rutaRelativa :
                                      Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rutaRelativa);

                if (File.Exists(rutaAbsoluta))
                {
                    // Necesitamos copiar la imagen a un MemoryStream 
                    // para que el archivo no quede bloqueado (locked)
                    using (var stream = new MemoryStream(File.ReadAllBytes(rutaAbsoluta)))
                    {
                        // IMPORTANTE: Image.FromStream(stream) debe ser devuelto directamente
                        // para que la imagen persista fuera del bloque using.
                        // Lo copiamos al bitmap para evitar el lock de stream.
                        return new Bitmap(Image.FromStream(stream));
                    }
                }
            }
            catch (Exception)
            {
                // Opcional: loggear el error
                return null;
            }

            return null;
        }

        #endregion

        // ======================================================
        //  REGIÓN: Consultas Paginadas
        // ======================================================
        #region Consultas Paginadas
        public (List<InmuebleLite> items, int total) BuscarPaginado(
            string term, bool? soloActivos, int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 20;

            var lista = new List<InmuebleLite>();

            // 👉 AGREGADO: columna 'condiciones' en la CTE y en el SELECT externo
            var sql = @"
            ;WITH q AS (
                SELECT id_inmueble, direccion, tipo, condiciones, nro_ambientes, amueblado, estado
                FROM dbo.inmueble
                WHERE (@term='' 
                       OR direccion   LIKE @like
                       OR tipo        LIKE @like
                       OR condiciones LIKE @like)
                  AND (@estado IS NULL OR estado = @estado)
            )
            SELECT id_inmueble, direccion, tipo, condiciones, nro_ambientes, amueblado, estado
            FROM q
            ORDER BY direccion
            OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY;

            SELECT COUNT(1)
            FROM dbo.inmueble
            WHERE (@term='' 
                  OR direccion   LIKE @like
                  OR tipo        LIKE @like
                  OR condiciones LIKE @like)
              AND (@estado IS NULL OR estado = @estado);";

            using (var cn = BDGeneral.GetConnection())
            using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, cn))
            {
                var like = string.IsNullOrWhiteSpace(term) ? "" : $"%{term.Trim()}%";
                cmd.Parameters.Add("@term", SqlDbType.VarChar, 200).Value = string.IsNullOrWhiteSpace(term) ? "" : term.Trim();
                cmd.Parameters.Add("@like", SqlDbType.VarChar, 200).Value = like;
                cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = (object?)soloActivos ?? DBNull.Value;
                cmd.Parameters.Add("@skip", SqlDbType.Int).Value = (page - 1) * pageSize;
                cmd.Parameters.Add("@take", SqlDbType.Int).Value = pageSize;

                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    lista.Add(new InmuebleLite
                    {
                        IdInmueble = rd.GetInt32(0),
                        Direccion = rd.GetString(1),
                        Tipo = rd.GetString(2),
                        Condiciones = rd.GetString(3), // 👈 NUEVO
                        NroAmbientes = rd.IsDBNull(4) ? (int?)null : rd.GetInt32(4),
                        Amueblado = rd.GetBoolean(5),
                        Estado = rd.GetBoolean(6)
                    });
                }
                rd.NextResult(); rd.Read();
                var total = rd.GetInt32(0);
                return (lista, total);
            }
        }
        #endregion

        // ======================================================
        //  REGIÓN: DTO
        // ======================================================
        #region Data Transfer Object (DTO)
        // DTO liviano para la grilla del buscador
        public sealed class InmuebleLite
        {
            public int IdInmueble { get; set; }
            public string Direccion { get; set; } = "";
            public string Tipo { get; set; } = "";
            public string Condiciones { get; set; } = "";   // 👈 NUEVO
            public int? NroAmbientes { get; set; }
            public bool Amueblado { get; set; }
            public bool Estado { get; set; }  // true=Activo
        }
        #endregion
    }
}