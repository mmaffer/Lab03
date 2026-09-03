using Lab03.Models;
using System.Windows;
using Lab03.ViewModels;

namespace Lab03.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(Usuario usuario)
        {
            InitializeComponent();
            DataContext = new MainViewModel(usuario);
        }
    }
}