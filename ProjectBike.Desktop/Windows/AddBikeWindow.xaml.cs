using ProjectBike.Desktop.ViewModels.Bikes;
using System.Windows;

namespace ProjectBike.Desktop.Windows
{
    public partial class AddBikeWindow : Window
    {
        public AddBikeWindow(AddBikeViewModel vm)
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