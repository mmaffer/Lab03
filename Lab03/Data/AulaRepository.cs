using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Lab03.Models;

namespace Lab03.Data
{
    public class AulaRepository
    {
        public DataTable ObtenerAulasDataTable()
        {
            DataTable dt = new DataTable();
            using SqlConnection conn = DbConnection.GetConnection();
            string query = "SELECT AulaId, Nombre, Capacidad FROM Aulas";

            using SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dt); // SqlDataAdapter abre, llena y CIERRA la conexión automáticamente

            return dt;
        }

        public List<Aula> ObtenerAulasLista()
        {
            List<Aula> lista = new List<Aula>();
            using SqlConnection conn = DbConnection.GetConnection();
            conn.Open();

            string query = "SELECT AulaId, Nombre, Capacidad FROM Aulas";
            using SqlCommand cmd = new SqlCommand(query, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Aula
                {
                    AulaId = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Capacidad = reader.GetInt32(2)
                });
            }

            return lista;
        }

        public List<Aula> BuscarAulasPorNombre(string nombre)
        {
            List<Aula> lista = new List<Aula>();
            using SqlConnection conn = DbConnection.GetConnection();
            conn.Open();

            string query = "SELECT AulaId, Nombre, Capacidad FROM Aulas WHERE Nombre LIKE @Nombre";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Aula
                {
                    AulaId = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Capacidad = reader.GetInt32(2)
                });
            }

            return lista;
        }
    }
}