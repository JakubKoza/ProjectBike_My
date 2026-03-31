using ProjectBike.Desktop.ViewModels;
using ProjectBike.Desktop.Infrastructure;

namespace ProjectBike.Desktop.ViewModels.Bikes
{
    public class AddBikeViewModel : BaseViewModel
    {
        private string _brand = "";
        private string _model = "";
        private string _type = "";
        private string _validationMessage = "";

        public string Brand { get => _brand; set { if (Set(ref _brand, value)) Validate(); } }
        public string Model { get => _model; set { if (Set(ref _model, value)) Validate(); } }
        public string Type { get => _type; set { if (Set(ref _type, value)) Validate(); } }

        public string ValidationMessage
        {
            get => _validationMessage;
            private set => Set(ref _validationMessage, value);
        }

        public RelayCommand ConfirmCommand { get; }
        public RelayCommand CancelCommand { get; }
        public event EventHandler<bool>? CloseRequested;

        public AddBikeViewModel()
        {
            ConfirmCommand = new RelayCommand(OnConfirm);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private void Validate() => ValidationMessage = "";

        private void OnConfirm()
        {
            if (string.IsNullOrWhiteSpace(Brand) || string.IsNullOrWhiteSpace(Model) || string.IsNullOrWhiteSpace(Type))
            {
                ValidationMessage = "Wszystkie pola (Marka, Model, Typ) są wymagane.";
                return;
            }
            CloseRequested?.Invoke(this, true);
        }

        private void OnCancel() => CloseRequested?.Invoke(this, false);
    }
}