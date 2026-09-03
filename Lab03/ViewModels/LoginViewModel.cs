using System;
using System.Windows.Input;
using Lab03.Data;
using Lab03.Models;

namespace Lab03.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _mensajeError = string.Empty;
        private readonly Action<Usuario>? _onLoginSuccess;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string MensajeError
        {
            get => _mensajeError;
            set { _mensajeError = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public Usuario? UsuarioAutenticado { get; private set; }

        // Constructor por defecto
        public LoginViewModel() : this(null) { }

        // Constructor que acepta la acción de navegación/éxito
        public LoginViewModel(Action<Usuario>? onLoginSuccess)
        {
            _onLoginSuccess = onLoginSuccess;
            LoginCommand = new RelayCommand(EjecutarLogin);
        }

        private void EjecutarLogin(object? parameter)
        {
            MensajeError = string.Empty;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MensajeError = "Por favor ingrese usuario y contraseña.";
                return;
            }

            var repo = new UsuarioRepository();
            var usuario = repo.ValidarLogin(Username, Password);

            if (usuario != null)
            {
                UsuarioAutenticado = usuario;
                _onLoginSuccess?.Invoke(usuario); 
            }
            else
            {
                MensajeError = "Usuario o contraseña incorrectos.";
            }
        }
    }
}