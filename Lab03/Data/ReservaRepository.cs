using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Lab03.Models;

namespace Lab03.Data
{
    public class ReservaRepository
    {
        // MODO DESCONECTADO: Usando SqlDataAdapter.Fill
        public DataTable ObtenerReservasDataTable()
        {
            DataTable dt = new DataTable();
            using SqlConnection conn = DbConnection.GetConnection();
            string query = @"
                SELECT r.ReservaId, a.Nombre AS Aula, u.NombreCompleto AS Usuario, 
                       r.Fecha, r.Hora, r.Motivo
                FROM Reservas r
                INNER JOIN Aulas a ON r.AulaId = a.AulaId
                INNER JOIN Usuarios u ON r.UsuarioId = u.UsuarioId";

            using SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dt);

            return dt;
        }

        // MODO CONECTADO: Usando SqlDataReader fila por fila
        public List<Reserva> ObtenerReservasLista()
        {
            List<Reserva> lista = new List<Reserva>();
            using SqlConnection conn = DbConnection.GetConnection();
            conn.Open();

            string query = @"
                SELECT r.ReservaId, r.AulaId, a.Nombre AS NombreAula, 
                       r.UsuarioId, u.NombreCompleto AS NombreUsuario, 
                       r.Fecha, r.Hora, r.Motivo
                FROM Reservas r
                INNER JOIN Aulas a ON r.AulaId = a.AulaId
                INNER JOIN Usuarios u ON r.UsuarioId = u.UsuarioId";

            using SqlCommand cmd = new SqlCommand(query, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Reserva
                {
                    ReservaId = reader.GetInt32(0),
                    AulaId = reader.GetInt32(1),
                    NombreAula = reader.GetString(2),
                    UsuarioId = reader.GetInt32(3),
                    NombreUsuario = reader.GetString(4),
                    Fecha = reader.GetDateTime(5),
                    Hora = reader.GetTimeSpan(6),
                    Motivo = reader.GetString(7)
                });
            }

            return lista;
        }

        // MODO CONECTADO: Búsqueda por Fecha
        public List<Reserva> BuscarReservasPorFecha(DateTime fecha)
        {
            List<Reserva> lista = new List<Reserva>();
            using SqlConnection conn = DbConnection.GetConnection();
            conn.Open();

            string query = @"
                SELECT r.ReservaId, r.AulaId, a.Nombre AS NombreAula, 
                       r.UsuarioId, u.NombreCompleto AS NombreUsuario, 
                       r.Fecha, r.Hora, r.Motivo
                FROM Reservas r
                INNER JOIN Aulas a ON r.AulaId = a.AulaId
                INNER JOIN Usuarios u ON r.UsuarioId = u.UsuarioId
                WHERE r.Fecha = @Fecha";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Reserva
                {
                    ReservaId = reader.GetInt32(0),
                    AulaId = reader.GetInt32(1),
                    NombreAula = reader.GetString(2),
                    UsuarioId = reader.GetInt32(3),
                    NombreUsuario = reader.GetString(4),
                    Fecha = reader.GetDateTime(5),
                    Hora = reader.GetTimeSpan(6),
                    Motivo = reader.GetString(7)
                });
            }

            return lista;
        }

        // MODO CONECTADO: Validar si existe reserva previa (Aula, Fecha, Hora)
        public bool ExisteReserva(int aulaId, DateTime fecha, TimeSpan hora)
        {
            using SqlConnection conn = DbConnection.GetConnection();
            conn.Open();

            string query = "SELECT COUNT(1) FROM Reservas WHERE AulaId = @AulaId AND Fecha = @Fecha AND Hora = @Hora";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@AulaId", aulaId);
            cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
            cmd.Parameters.AddWithValue("@Hora", hora);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        // MODO CONECTADO: Registrar reserva
        public bool InsertarReserva(Reserva reserva)
        {
            using SqlConnection conn = DbConnection.GetConnection();
            conn.Open();

            string query = "INSERT INTO Reservas (AulaId, UsuarioId, Fecha, Hora, Motivo) VALUES (@AulaId, @UsuarioId, @Fecha, @Hora, @Motivo)";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@AulaId", reserva.AulaId);
            cmd.Parameters.AddWithValue("@UsuarioId", reserva.UsuarioId);
            cmd.Parameters.AddWithValue("@Fecha", reserva.Fecha.Date);
            cmd.Parameters.AddWithValue("@Hora", reserva.Hora);
            cmd.Parameters.AddWithValue("@Motivo", reserva.Motivo);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}