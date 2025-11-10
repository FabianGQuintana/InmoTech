using System.Data;
using System.Data.SqlClient; // Asegúrate de tener el paquete System.Data.SqlClient NuGet instalado si no lo tienes.
using System.Threading.Tasks;

namespace InmoTech.Data
{
    public abstract class BaseRepository
    {
        // ⚠️ IMPORTANTE: Aquí deberás poner tu cadena de conexión a la base de datos.
        // Lo ideal sería cargarla desde un App.config o .env también.
        // Por ahora, la ponemos aquí como placeholder para que funcione.
        private readonly string _connectionString = "Data Source=Ivan\\SQLEXPRESS;" +
                "Initial Catalog=inmotech;" +
                "Integrated Security=SSPI;" +
                "Encrypt=True;" +
                "TrustServerCertificate=True;" +
                "Persist Security Info=False;";
        // Ejemplo: "Data Source=.;Initial Catalog=InmoTechDB;Integrated Security=True"; para SQL Server local
        // Ejemplo: "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";

        protected async Task<T> ExecuteReaderAsync<T>(string query, Func<SqlDataReader, T> mapFunction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return mapFunction(reader);
                    }
                }
            }
        }
    }
}