using System;
using System.Collections.Generic;
using System.Windows.Input;
using Lab03.Data;
using Lab03.Models;

namespace Lab03.ViewModels
{
    public class ReservasObjetosViewModel : ViewModelBase
    {
        private readonly ReservaRepository _repo = new();
        private List<Reserva> _reservas = new();

        public List<Reserva> Reservas
        {
            get => _reservas;
            set { _reservas = value; OnPropertyChanged(); }
        }

        public DateTime? FechaBusqueda { get; set; } = DateTime.Now;
        public ICommand BuscarCommand { get; }

        public ReservasObjetosViewModel()
        {
            Reservas = _repo.ObtenerReservasLista();
            BuscarCommand = new RelayCommand(_ =>
            {
                Reservas = FechaBusqueda.HasValue 
                    ? _repo.BuscarReservasPorFecha(FechaBusqueda.Value) 
                    : _repo.ObtenerReservasLista();
            });
        }
    }
}
