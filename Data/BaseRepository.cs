using System.Data;
using System.Data.SqlClient; 
using System.Threading.Tasks;

namespace InmoTech.Data
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString = "Data Source=localhost\\MSSQLSERVER04;" +
                                                    "Initial Catalog=inmotech;" +
                                                    "Integrated Security=SSPI;" +
                                                    "Encrypt=True;" +
                                                    "TrustServerCertificate=True;" +
                                                    "Persist Security Info=False;";

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