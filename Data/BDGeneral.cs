using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace InmoTech.Data
{
    public class BDGeneral
    {
        public static SqlConnection GetConnection()
        {
            var cs =
                "Data Source=localhost\\MSSQLSERVER04;" +
                "Initial Catalog=InmoTechDB;" +
                "Integrated Security=SSPI;" +
                "Encrypt=True;" +
                "TrustServerCertificate=True;" +
                "Persist Security Info=False;";

            var cn = new SqlConnection(cs);
            cn.Open();
            return cn;
        }
    }
}
//inmotech
//Ivan\\SQLEXPRESS;
