using ProjectBike.Desktop.Windows;
using System;
using System.Windows;

namespace ProjectBike.Desktop.Services
{
    //public interface IManageRentalWindowService
    //{
    //    bool Show(int clientId, string clientName);
    //}

    //public class ManageRentalWindowService : IManageRentalWindowService
    //{
    //    private readonly Func<ManageRentalWindow> _factory;
    //    public ManageRentalWindowService(Func<ManageRentalWindow> factory) => _factory = factory;

    //    public bool Show(int clientId, string clientName)
    //    {
    //        var win = _factory();
    //        win.Owner = Application.Current.MainWindow;
    //        win.SetClient(clientId, clientName);
    //        return win.ShowDialog() == true;
    //    }
    //}

    public interface IManageRentalWindowService
    {
        bool Show(int clientId, string clientName);
    }

    public class ManageRentalWindowService : IManageRentalWindowService
    {
        public bool Show(int clientId, string clientName)
        {
            MessageBox.Show($"Zarządzanie wypożyczeniami dla: {clientName} w przebudowie...", "Nawigacja");
            return false;
        }
    }
}