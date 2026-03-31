using ProjectBike.Desktop.ViewModels;
using ProjectBike.DataModel.Models;
using ProjectBike.Desktop.Infrastructure;
using System;

namespace ProjectBike.Desktop.ViewModels.Bikes
{
    public class EditBikeViewModel : BaseViewModel
    {
        private string _brand = "";
        private string _model = "";
        private string _type = "";

        public string Brand { get => _brand; set => Set(ref _brand, value); }
        public string Model { get => _model; set => Set(ref _model, value); }
        public string Type { get => _type; set => Set(ref _type, value); }

        public RelayCommand ConfirmCommand { get; }
        public event EventHandler<bool>? CloseRequested;

        public EditBikeViewModel()
        {
            ConfirmCommand = new RelayCommand(() => CloseRequested?.Invoke(this, true));
        }
        public void SetInitial(string brand, string model, string type)
        {
            Brand = brand ?? "";
            Model = model ?? "";
            Type = type ?? "";
        }
    }
}