using Lab03.Models;
using Lab03.ViewModels;
using System.Windows.Input;

namespace Lab03.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _vistaActual;
        public Usuario UsuarioSesion { get; }

        public ViewModelBase VistaActual
        {
            get => _vistaActual;
            set { _vistaActual = value; OnPropertyChanged(); }
        }

        public ICommand NavAulasDTCommand { get; }
        public ICommand NavAulasObjCommand { get; }
        public ICommand NavReservasDTCommand { get; }
        public ICommand NavReservasObjCommand { get; }
        public ICommand NavNuevaReservaCommand { get; }

        public MainViewModel(Usuario usuario)
        {
            UsuarioSesion = usuario;
            _vistaActual = new AulasDataTableViewModel(); // Vista por defecto

            NavAulasDTCommand = new RelayCommand(_ => VistaActual = new AulasDataTableViewModel());
            NavAulasObjCommand = new RelayCommand(_ => VistaActual = new AulasObjetosViewModel());
            NavReservasDTCommand = new RelayCommand(_ => VistaActual = new ReservasDataTableViewModel());
            NavReservasObjCommand = new RelayCommand(_ => VistaActual = new ReservasObjetosViewModel());
            NavNuevaReservaCommand = new RelayCommand(_ => VistaActual = new NuevaReservaViewModel(UsuarioSesion));
        }
    }
}