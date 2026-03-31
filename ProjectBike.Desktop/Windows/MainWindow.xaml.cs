using ProjectBike.Desktop.ViewModels.Clients;
using System.Windows;

namespace ProjectBike.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}