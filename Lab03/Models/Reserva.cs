using System;

namespace Lab03.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public int AulaId { get; set; }
        public string NombreAula { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Motivo { get; set; } = string.Empty;
    }
}