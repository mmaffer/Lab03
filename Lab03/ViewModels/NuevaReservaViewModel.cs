using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Lab03.Data;
using Lab03.Models;

namespace Lab03.ViewModels
{
    public class NuevaReservaViewModel : ViewModelBase
    {
        private readonly AulaRepository _aulaRepo = new();
        private readonly ReservaRepository _reservaRepo = new();
        private readonly Usuario _usuarioLogueado;

        public List<Aula> ListaAulas { get; set; }
        public Aula? AulaSeleccionada { get; set; }
        public DateTime FechaSeleccionada { get; set; } = DateTime.Now;
        public string HoraTexto { get; set; } = "08:00"; // Formato HH:mm
        public string Motivo { get; set; } = string.Empty;

        public ICommand GuardarCommand { get; }

        public NuevaReservaViewModel(Usuario usuarioLogueado)
        {
            _usuarioLogueado = usuarioLogueado;
            ListaAulas = _aulaRepo.ObtenerAulasLista();

            GuardarCommand = new RelayCommand(_ => RegistrarReserva());
        }

        private void RegistrarReserva()
        {
            if (AulaSeleccionada == null)
            {
                MessageBox.Show("Por favor, seleccione un aula.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TimeSpan.TryParse(HoraTexto, out TimeSpan horaSpan))
            {
                MessageBox.Show("Ingrese una hora válida en formato HH:mm (Ejemplo: 09:30).", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Motivo))
            {
                MessageBox.Show("El motivo es obligatorio.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // MODO CONECTADO: Verificación de traslape previo
            bool existe = _reservaRepo.ExisteReserva(AulaSeleccionada.AulaId, FechaSeleccionada, horaSpan);
            if (existe)
            {
                MessageBox.Show("¡Atención! Ya existe una reserva para esta aula en la fecha y hora seleccionadas.", 
                                "Conflicto de Reserva", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Reserva nuevaReserva = new Reserva
            {
                AulaId = AulaSeleccionada.AulaId,
                UsuarioId = _usuarioLogueado.UsuarioId,
                Fecha = FechaSeleccionada,
                Hora = horaSpan,
                Motivo = Motivo
            };

            if (_reservaRepo.InsertarReserva(nuevaReserva))
            {
                MessageBox.Show("Reserva registrada con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                Motivo = string.Empty;
                OnPropertyChanged(nameof(Motivo));
            }
            else
            {
                MessageBox.Show("Ocurrió un error al registrar la reserva.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
