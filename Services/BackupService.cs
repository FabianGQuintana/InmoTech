using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;  // usen siempre el mismo provider
using InmoTech.Data;             // para BDGeneral.GetConnection()

namespace InmoTech.Services
{
    public sealed class BackupService
    {
        private readonly string _databaseName;

        public BackupService()
        {
            // Tomo la DB del connection string de BDGeneral
            using var cn = BDGeneral.GetConnection(); // ya viene abierto
            _databaseName = cn.Database;              // "inmotech" en su caso
        }

        /// <summary>
        /// Ejecuta BACKUP DATABASE a un archivo .bak local en el servidor SQL.
        /// </summary>
        public async Task CreateBackupAsync(
            string fullPathBak,
            bool overwrite,
            string compressionLevel,  // "Ninguna" | "Baja" | "Media" | "Alta"
            bool verify,
            IProgress<string>? progress = null,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(fullPathBak))
                throw new ArgumentException("Ruta de backup inválida.", nameof(fullPathBak));

            // 1) Armo opciones de BACKUP
            //    (COMPRESSION requiere ediciones/versions que lo soporten; si no, SQL lo ignora)
            var opts = overwrite ? "INIT" : "NOINIT";
            var compression = compressionLevel?.Equals("Ninguna", StringComparison.OrdinalIgnoreCase) == true
                ? "NO_COMPRESSION"
                : "COMPRESSION"; // no hay grados en T-SQL, solo ON/OFF

            var sqlBackup = $@"
BACKUP DATABASE [{_databaseName}]
TO DISK = @p0
WITH {opts}, {compression}, STATS = 10, CHECKSUM;";

            // 2) Ejecuto BACKUP
            progress?.Report("Iniciando backup…");
            using (var cn = BDGeneral.GetConnection())
            using (var cmd = new SqlCommand(sqlBackup, cn))
            {
                cmd.Parameters.Add(new SqlParameter("@p0", SqlDbType.NVarChar, 4000) { Value = fullPathBak });
                cmd.CommandTimeout = 0; // backups grandes
                await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
            }
            progress?.Report("Backup finalizado.");

            // 3) Verificación opcional (RESTORE VERIFYONLY)
            if (verify)
            {
                progress?.Report("Verificando backup…");
                var sqlVerify = @"RESTORE VERIFYONLY FROM DISK = @p0 WITH STATS = 5;";
                using var cn = BDGeneral.GetConnection();
                using var cmd = new SqlCommand(sqlVerify, cn);
                cmd.Parameters.Add(new SqlParameter("@p0", SqlDbType.NVarChar, 4000) { Value = fullPathBak });
                cmd.CommandTimeout = 0;
                await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
                progress?.Report("Verificación OK.");
            }
        }

        /// <summary>
        /// Utilidad simple para construir el nombre final (agregando timestamp si hace falta).
        /// </summary>
        public static string BuildFinalPath(string folder, string filename, bool addDate)
        {
            var name = filename;
            var ext = Path.GetExtension(name);
            if (string.IsNullOrWhiteSpace(ext)) ext = ".bak";

            if (addDate)
            {
                var baseName = Path.GetFileNameWithoutExtension(name);
                name = $"{baseName}_{DateTime.Now:yyyyMMdd_HHmm}{ext}";
            }
            return Path.Combine(folder, name);
        }
    }
}
