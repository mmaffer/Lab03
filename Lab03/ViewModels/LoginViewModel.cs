using System.Windows;
using System.Windows.Input;
using Lab03.Data;
using Lab03.Models;

namespace Lab03.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly UsuarioRepository _usuarioRepo = new();

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public ICommand IngresarCommand { get; }

        public LoginViewModel(Action<Usuario> onLoginSuccess)
        {
            IngresarCommand = new RelayCommand(_ =>
            {
                var usuario = _usuarioRepo.ValidarLogin(Username, Password);
                if (usuario != null)
                {
                    onLoginSuccess(usuario);
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
    }
}