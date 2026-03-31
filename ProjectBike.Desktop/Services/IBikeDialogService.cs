using ProjectBike.Desktop.ViewModels.Bikes;
using ProjectBike.Desktop.Windows;
using System;
using System.Windows;

namespace ProjectBike.Desktop.Services
{
    public interface IBikeDialogService
    {
        (bool ok, string brand, string model, string type) PromptAddBike();
        (bool ok, string brand, string model, string type) PromptEditBike(string cBrand, string cModel, string cType);
        bool Confirm(string message, string title);
    }

    public class BikeDialogService : IBikeDialogService
    {
        private readonly Func<AddBikeWindow> _addFactory;
        private readonly Func<EditBikeWindow> _editFactory;

        public BikeDialogService(Func<AddBikeWindow> addFactory, Func<EditBikeWindow> editFactory)
        {
            _addFactory = addFactory;
            _editFactory = editFactory;
        }

        public (bool ok, string brand, string model, string type) PromptAddBike()
        {
            var win = _addFactory();
            var ok = win.ShowDialog() == true;
            if (!ok) return (false, "", "", "");
            var vm = (AddBikeViewModel)win.DataContext;
            return (true, vm.Brand, vm.Model, vm.Type);
        }

        public (bool ok, string brand, string model, string type) PromptEditBike(string cBrand, string cModel, string cType)
        {
            var win = _editFactory();
            var vm = (EditBikeViewModel)win.DataContext;
            vm.SetInitial(cBrand, cModel, cType);
            var ok = win.ShowDialog() == true;
            if (!ok) return (false, "", "", "");
            return (true, vm.Brand, vm.Model, vm.Type);
        }

        public bool Confirm(string message, string title)
            => MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
    }
}