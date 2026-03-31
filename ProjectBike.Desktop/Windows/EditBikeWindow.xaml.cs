using ProjectBike.Desktop.ViewModels.Bikes;
using System.Windows;

namespace ProjectBike.Desktop.Windows
{
    public partial class EditBikeWindow : Window
    {
        public EditBikeWindow(EditBikeViewModel vm)
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