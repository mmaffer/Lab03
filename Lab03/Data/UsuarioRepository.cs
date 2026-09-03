using Microsoft.Data.SqlClient;
using Lab03.Models;

namespace Lab03.Data
{
    public class UsuarioRepository
    {
        public Usuario? ValidarLogin(string username, string password)
        {
            using SqlConnection conn = DbConnection.GetConnection();
            conn.Open(); // Conexión abierta durante la operación

            string query = "SELECT UsuarioId, Username, NombreCompleto FROM Usuarios WHERE Username = @User AND Password = @Pass";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@User", username);
            cmd.Parameters.AddWithValue("@Pass", password);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                {
                    UsuarioId = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    NombreCompleto = reader.GetString(2)
                };
            }

            return null;
        }
    }
}