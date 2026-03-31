using ProjectBike.Desktop.ViewModels.Clients;
using ProjectBike.Desktop.Windows;
using System;
using System.Windows;

namespace ProjectBike.Desktop.Services
{
    public interface IClientDialogService
    {
        bool Confirm(string message, string title);
        (string fname, string lname)? PromptAddClient();
        (string fname, string lname)? PromptEditClient(string currentFname, string currentLname);
    }

    public sealed class ClientDialogService : IClientDialogService
    {
        private readonly Func<AddClientWindow> _addFactory;
        private readonly Func<EditClientWindow> _editFactory;

        public ClientDialogService(Func<AddClientWindow> addFactory, Func<EditClientWindow> editFactory)
        {
            _addFactory = addFactory;
            _editFactory = editFactory;
        }

        public bool Confirm(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }

        public (string fname, string lname)? PromptAddClient()
        {
            var dlg = _addFactory();
            if (dlg.ShowDialog() != true) return null;
            var vm = (AddClientViewModel)dlg.DataContext!;
            return (vm.FirstName, vm.LastName);
        }

        public (string fname, string lname)? PromptEditClient(string currentFname, string currentLname)
        {
            var dlg = _editFactory();
            var vm = (EditClientViewModel)dlg.DataContext!;
            vm.SetInitial(currentFname, currentLname);

            if (dlg.ShowDialog() != true) return null;
            return (vm.FirstName, vm.LastName);
        }
    }
}