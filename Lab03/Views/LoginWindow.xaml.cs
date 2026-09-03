using Lab03.ViewModels;
using System.Windows;
using Lab03.ViewModels;

namespace Lab03.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(usuario =>
            {
                MainWindow main = new MainWindow(usuario);
                main.Show();
                this.Close();
            });
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Password = TxtPassword.Password;
            }
        }
    }
}