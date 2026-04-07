using ProjectBike.Desktop.Windows;
using System;
using System.Windows;

namespace ProjectBike.Desktop.Services
{
    //public interface IBikesWindowService
    //{
    //    bool ShowBikesWindow();
    //}

    //public class BikesWindowService : IBikesWindowService
    //{
    //    private readonly Func<BikesWindow> _factory;
    //    public BikesWindowService(Func<BikesWindow> factory) => _factory = factory;

    //    public bool ShowBikesWindow()
    //    {
    //        var win = _factory();
    //        win.Owner = Application.Current.MainWindow;
    //        return win.ShowDialog() == true;
    //    }
    //}
    public interface IBikesWindowService
    {
        bool ShowBikesWindow();
    }

    public class BikesWindowService : IBikesWindowService
    {
        public bool ShowBikesWindow()
        {
            MessageBox.Show("Nawigacja do Bazy Rowerów w przebudowie...", "Nawigacja");
            return false;
        }
    }
}