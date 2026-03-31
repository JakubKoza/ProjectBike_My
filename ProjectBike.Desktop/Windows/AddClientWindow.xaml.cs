using ProjectBike.Desktop.ViewModels.Clients;
using System.Windows;

namespace ProjectBike.Desktop.Windows
{
    public partial class AddClientWindow : Window
    {
        public AddClientWindow(AddClientViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            vm.CloseRequested += (s, result) =>
            {
                DialogResult = result;
                Close();
            };
        }
    }
}