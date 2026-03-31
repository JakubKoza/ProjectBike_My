using ProjectBike.Desktop.ViewModels.Clients;
using System.Windows;

namespace ProjectBike.Desktop.Windows 
{
    public partial class FinalMainWindow : Window 
    {
        public FinalMainWindow(MainViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}