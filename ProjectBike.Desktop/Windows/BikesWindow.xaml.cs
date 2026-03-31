using ProjectBike.Desktop.ViewModels.Bikes;
using System.Windows;

namespace ProjectBike.Desktop.Windows
{
    public partial class BikesWindow : Window
    {
        private readonly BikesViewModel _vm;
        public BikesWindow(BikesViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
            Loaded += (s, e) => { if (_vm.LoadCommand.CanExecute(null)) _vm.LoadCommand.Execute(null); };
            Closing += (s, e) => { DialogResult = _vm.HasChanges; };
        }
    }
}