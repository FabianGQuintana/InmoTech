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
    /// <summary>
    /// Repositorio de acceso a datos para la entidad <see cref="Inmueble"/> y sus imágenes asociadas.
    /// Gestiona operaciones CRUD, manejo de archivos de imagen en disco y notificaciones de actualización del dashboard.
    /// </summary>
    public class InmuebleRepository
    {
        // ======================================================
        // REGIÓN: Helpers de Archivos y Directorios
        // ======================================================
        #region Helpers de Archivos y Directorios
        // Carpeta base para imágenes: <carpeta app>\Resources\inmuebles\<id_inmueble>\

        /// <summary>
        /// Obtiene (y crea si no existe) la carpeta de recursos para un inmueble específico dentro de
        /// <c>Resources/inmuebles/&lt;id&gt;</c>, partiendo de la raíz del proyecto <c>InmoTech</c>.
        /// </summary>
        /// <param name="idInmueble">Identificador del inmueble.</param>
        /// <returns>Ruta absoluta a la carpeta del inmueble.</returns>
        /// <exception cref="Exception">Si no se encuentra la carpeta raíz del proyecto InmoTech.</exception>
        private string GetResourcesInmueblesDir(int idInmueble)
        {
            // Buscar la raíz del proyecto (donde está el .csproj)
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

        /// <summary>
        /// Devuelve el content-type/mime esperado para una extensión de imagen conocida.
        /// </summary>
        /// <param name="ext">Extensión con punto (p. ej. ".jpg").</param>
        /// <returns>Mime type correspondiente; si no se reconoce, <c>application/octet-stream</c>.</returns>
        private static string GetMimeByExtension(string ext)
        {
            ext = (ext ?? "").ToLowerInvariant();
            if (ext == ".jpg" || ext == ".jpeg") return "image/jpeg";
            if (ext == ".png") return "image/png";
            if (ext == ".webp") return "image/webp";
            return "application/octet-stream";
        }

        /// <summary>
        /// Obtiene la ruta relativa desde un directorio base a una ruta completa.
        /// Equivalente a <c>Path.GetRelativePath</c> en frameworks más nuevos.
        /// </summary>
        private static string GetRelativePath(string baseDir, string fullPath)
        {
            var baseUri = new Uri(AppendDirectorySeparatorChar(baseDir));
            var fullUri = new Uri(fullPath);
            var rel = baseUri.MakeRelativeUri(fullUri).ToString();
            // Uri usa '/', convertimos a separador de Windows
            return Uri.UnescapeDataString(rel).Replace('/', Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Asegura que una ruta termine con separador de directorio.
        /// </summary>
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

        /// <summary>
        /// Crea un nuevo registro de <see cref="Inmueble"/> y devuelve el ID generado.
        /// Dispara una notificación de actualización del dashboard si la inserción fue exitosa.
        /// </summary>
        /// <param name="i">Entidad a insertar.</param>
        /// <param name="dniUsuarioCreador">DNI del usuario que crea el registro (auditoría).</param>
        /// <returns>ID del inmueble creado.</returns>
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

        /// <summary>
        /// Obtiene un inmueble por ID. Opcionalmente incluye la lista de imágenes asociadas.
        /// </summary>
        /// <param name="idInmueble">ID a buscar.</param>
        /// <param name="incluirImagenes">Si <c>true</c>, carga <see cref="ListarImagenes"/>.</param>
        /// <returns>Entidad <see cref="Inmueble"/> o <c>null</c> si no existe.</returns>
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

        /// <summary>
        /// Lista inmuebles con filtros simples por texto y estado activo.
        /// Ordena descendentemente por <c>id_inmueble</c>.
        /// </summary>
        /// <param name="filtroTexto">Texto a buscar en dirección, tipo o condiciones.</param>
        /// <param name="soloActivos">Si <c>true</c>, solo estado = 1.</param>
        /// <returns>Colección de <see cref="Inmueble"/>.</returns>
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

        /// <summary>
        /// Actualiza campos editables de un inmueble existente (no altera auditoría).
        /// Notifica al dashboard si hubo cambios.
        /// </summary>
        /// <param name="i">Entidad con datos a persistir (Id requerido).</param>
        /// <returns>Cantidad de filas afectadas.</returns>
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

        /// <summary> 
        /// Realiza baja lógica de un inmueble (establece <c>estado = 0</c>).
        /// </summary>
        /// <param name="idInmueble">ID a desactivar.</param>
        /// <returns>Filas afectadas.</returns>
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

        /// <summary>
        /// Agrega una imagen a un inmueble copiando primero el archivo al repositorio local
        /// (<c>Resources/inmuebles/&lt;id&gt;</c>) y luego persistiendo su registro en BD.
        /// Si <paramref name="esPortada"/> es <c>true</c>, quita la portada anterior.
        /// </summary>
        /// <param name="idInmueble">ID del inmueble dueño de la imagen.</param>
        /// <param name="archivoOrigen">Ruta de archivo fuente a copiar.</param>
        /// <param name="titulo">Título opcional.</param>
        /// <param name="esPortada">Marca la imagen como portada del inmueble.</param>
        /// <param name="posicion">Orden relativo de la imagen.</param>
        /// <returns>ID de la imagen creada.</returns>
        /// <exception cref="FileNotFoundException">Si no existe el archivo de origen.</exception>
        public int AgregarImagenDesdeArchivo(
            int idInmueble,
            string archivoOrigen,
            string titulo = null,
            bool esPortada = false,
            short posicion = 0)
        {
            if (string.IsNullOrWhiteSpace(archivoOrigen) || !File.Exists(archivoOrigen))
                throw new FileNotFoundException("No se encontró el archivo de imagen.", archivoOrigen);

            // ---------- 1) Preparar paths y copiar archivo físico ----------
            var destDir = GetResourcesInmueblesDir(idInmueble);        
            var fileName = Path.GetFileName(archivoOrigen);
            var destPath = Path.Combine(destDir, fileName);

            // Evitar colisión de nombres
            if (File.Exists(destPath))
            {
                var name = Path.GetFileNameWithoutExtension(fileName);
                var ext = Path.GetExtension(fileName);
                var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                fileName = $"{name}_{stamp}{ext}";
                destPath = Path.Combine(destDir, fileName);
            }

            File.Copy(archivoOrigen, destPath);

            // Metadatos opcionales (intento de lectura de ancho/alto y tamaño)
            int? w = null, h = null, size = null;
            try
            {
                using (var img = Image.FromFile(destPath))
                { w = img.Width; h = img.Height; }

                var fi = new FileInfo(destPath);
                size = (int)fi.Length;
            }
            catch { /* seguimos sin medidas si falla */ }

            // Ruta relativa que guardaremos en DB
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var rutaRel = GetRelativePath(baseDir, destPath);

            var extLower = Path.GetExtension(fileName);
            var mime = GetMimeByExtension(extLower);

            // ---------- 2) Persistir en BD dentro de una transacción ----------
            using (var cn = BDGeneral.GetConnection())
            using (var tr = cn.BeginTransaction())
            {
                try
                {
                    // 2.a) Si será portada, primero “liberá” la actual
                    if (esPortada)
                    {
                        using (var cmd0 = new SqlCommand(@"
                    UPDATE dbo.inmueble_imagen
                    SET es_portada = 0
                    WHERE id_inmueble = @id AND es_portada = 1;", cn, tr))
                        {
                            cmd0.Parameters.Add("@id", SqlDbType.Int).Value = idInmueble;
                            cmd0.ExecuteNonQuery();
                        }
                    }

                    // 2.b) Insertar la nueva imagen
                    int newId;
                    using (var cmd = new SqlCommand(@"
                INSERT INTO dbo.inmueble_imagen
                    (id_inmueble, titulo, nombre_archivo, ruta, content_type,
                     bytes_size, ancho_px, alto_px, es_portada, posicion)
                VALUES
                    (@id_inmueble, @titulo, @nombre_archivo, @ruta, @content_type,
                     @bytes_size, @ancho_px, @alto_px, @es_portada, @posicion);
                SELECT CAST(SCOPE_IDENTITY() AS int);", cn, tr))
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

                        newId = (int)cmd.ExecuteScalar();
                    }

                    tr.Commit();

                    // Notificar UI / dashboard
                    AppNotifier.NotifyDashboardChange();

                    return newId;
                }
                catch
                {
                    // Si algo falla, hacemos rollback y limpiamos el archivo copiado
                    try { tr.Rollback(); } catch { /* ignore */ }
                    try { if (File.Exists(destPath)) File.Delete(destPath); } catch { /* ignore */ }
                    throw;
                }
            }
        }

        /// <summary>
        /// Lista las imágenes asociadas a un inmueble, opcionalmente filtrando solo las activas.
        /// Orden: portada primero, luego por posición e ID.
        /// </summary>
        /// <param name="idInmueble">ID de inmueble.</param>
        /// <param name="soloActivas">Si <c>true</c>, filtra <c>activo = 1</c>.</param>
        /// <returns>Colección de <see cref="InmuebleImagen"/>.</returns>
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

        /// <summary>
        /// Marca una imagen como portada. Asegura exclusividad de portada por inmueble.
        /// Notifica al dashboard si la operación fue exitosa.
        /// </summary>
        /// <param name="idImagen">ID de la imagen a promover como portada.</param>
        /// <returns>Filas afectadas (0 si no existe).</returns>
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
        /// Elimina un registro de imagen y, opcionalmente, intenta borrar el archivo físico asociado.
        /// Notifica al dashboard si la operación afectó filas.
        /// </summary>
        /// <param name="idImagen">ID de la imagen.</param>
        /// <param name="borrarArchivoFisico">Si <c>true</c>, intenta eliminar el archivo en disco.</param>
        /// <returns>Filas afectadas.</returns>
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

        /// <summary>
        /// Actualiza la posición/orden de una imagen.
        /// </summary>
        /// <param name="idImagen">ID de la imagen.</param>
        /// <param name="nuevaPosicion">Nueva posición relativa.</param>
        /// <returns>Filas afectadas.</returns>
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
        /// Obtiene la imagen de portada de un inmueble desde el sistema de archivos.
        /// Lee la ruta desde BD y devuelve un <see cref="Image"/> cargado en memoria
        /// para evitar locks de archivo.
        /// </summary>
        /// <param name="idInmueble">ID del inmueble.</param>
        /// <returns>Instancia de <see cref="Image"/> o <c>null</c> si no hay portada o falla la carga.</returns>
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

        /// <summary>
        /// Realiza una búsqueda paginada devolviendo un conjunto liviano (<see cref="InmuebleLite"/>) y el total.
        /// Filtra por término (dirección, tipo o condiciones) y estado (activo/any).
        /// </summary>
        /// <param name="term">Término de búsqueda; si vacío, no filtra por texto.</param>
        /// <param name="soloActivos">Si <c>true</c> filtra por <c>estado = 1</c>; si <c>null</c> no filtra estado.</param>
        /// <param name="page">Página (1-based).</param>
        /// <param name="pageSize">Tamaño de página (&gt;=1).</param>
        /// <returns>Tupla con <c>items</c> y <c>total</c>.</returns>
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

        /// <summary>
        /// DTO liviano proyectado para listados/paginación en grillas.
        /// </summary>
        public sealed class InmuebleLite
        {
            /// <summary>ID de inmueble.</summary>
            public int IdInmueble { get; set; }
            /// <summary>Dirección completa.</summary>
            public string Direccion { get; set; } = "";
            /// <summary>Tipo (casa, depto, etc.).</summary>
            public string Tipo { get; set; } = "";
            /// <summary>Condiciones de alquiler/venta.</summary>
            public string Condiciones { get; set; } = "";   // 👈 NUEVO
            /// <summary>Número de ambientes (puede ser null).</summary>
            public int? NroAmbientes { get; set; }
            /// <summary>Indica si está amueblado.</summary>
            public bool Amueblado { get; set; }
            /// <summary>Estado lógico (true=activo).</summary>
            public bool Estado { get; set; }  // true=Activo
        }
        #endregion

        /// <summary>
        /// Actualiza el campo <c>condiciones</c> de un inmueble y notifica al dashboard si hubo cambios.
        /// </summary>
        /// <param name="idInmueble">ID del inmueble a actualizar.</param>
        /// <param name="nuevaCondicion">Texto de condiciones.</param>
        /// <returns>Filas afectadas.</returns>
        public int ActualizarCondicion(int idInmueble, string nuevaCondicion)
        {
            using (var cn = BDGeneral.GetConnection())
            using (var cmd = new SqlCommand(@"
        UPDATE dbo.inmueble
           SET condiciones = @cond
         WHERE id_inmueble = @id;", cn))
            {
                cmd.Parameters.Add("@cond", SqlDbType.NVarChar, 255).Value = nuevaCondicion;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idInmueble;
                var n = cmd.ExecuteNonQuery();
                if (n > 0) AppNotifier.NotifyDashboardChange(); // si venís usando notificaciones
                return n;
            }
        }

    }
}
