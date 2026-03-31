using ProjectBike.Desktop.ViewModels.Rentals;
using System.ComponentModel;
using System.Windows;

namespace ProjectBike.Desktop.Windows
{
    public partial class ManageRentalWindow : Window
    {
        private readonly ManageRentalViewModel _vm;
        public ManageRentalWindow(ManageRentalViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
            Loaded += (s, e) => { if (_vm.LoadCommand.CanExecute(null)) _vm.LoadCommand.Execute(null); };
            Closing += (s, e) => { DialogResult = _vm.HasChanges; };
        }
        public void SetClient(int id, string name) => _vm.SetClient(id, name);
    }
}