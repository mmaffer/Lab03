using Microsoft.Data.SqlClient;

namespace Lab03.Data
{
    public static class DbConnection
    {
        private static readonly string ConnectionString =
            @"Server=.\SQLEXPRESS;Database=ReservasDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}