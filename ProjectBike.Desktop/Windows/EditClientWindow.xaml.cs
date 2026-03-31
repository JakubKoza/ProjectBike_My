using ProjectBike.Desktop.ViewModels.Clients;
using System.Windows;

namespace ProjectBike.Desktop.Windows
{
    public partial class EditClientWindow : Window
    {
        public EditClientWindow(EditClientViewModel vm)
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