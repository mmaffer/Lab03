using Lab03.Data;
using Lab03.Models;
using Lab03.ViewModels;
using System.Collections.Generic;
using System.Windows.Input;

namespace Lab03.ViewModels
{
    public class AulasObjetosViewModel : ViewModelBase
    {
        private readonly AulaRepository _repo = new();
        private List<Aula> _aulas = new();

        public List<Aula> Aulas
        {
            get => _aulas;
            set { _aulas = value; OnPropertyChanged(); }
        }

        public string TextoBusqueda { get; set; } = string.Empty;

        public ICommand BuscarCommand { get; }

        public AulasObjetosViewModel()
        {
            Aulas = _repo.ObtenerAulasLista();
            BuscarCommand = new RelayCommand(_ =>
            {
                Aulas = string.IsNullOrWhiteSpace(TextoBusqueda)
                    ? _repo.ObtenerAulasLista()
                    : _repo.BuscarAulasPorNombre(TextoBusqueda);
            });
        }
    }
}