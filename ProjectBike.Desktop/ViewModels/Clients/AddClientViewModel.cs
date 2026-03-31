using ProjectBike.Desktop.ViewModels;
using ProjectBike.Desktop.Infrastructure;
using System;

namespace ProjectBike.Desktop.ViewModels.Clients
{
    public class AddClientViewModel : BaseViewModel
    {
        private string _firstName = "";
        private string _lastName = "";
        private string _validationMessage = "";

        public string FirstName { get => _firstName; set { if (Set(ref _firstName, value)) ValidationMessage = ""; } }
        public string LastName { get => _lastName; set { if (Set(ref _lastName, value)) ValidationMessage = ""; } }
        public string ValidationMessage { get => _validationMessage; private set => Set(ref _validationMessage, value); }

        public RelayCommand ConfirmCommand { get; }
        public RelayCommand CancelCommand { get; }
        public event EventHandler<bool>? CloseRequested;

        public AddClientViewModel()
        {
            ConfirmCommand = new RelayCommand(OnConfirm);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private void OnConfirm()
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                ValidationMessage = "Imię i nazwisko są wymagane.";
                return;
            }
            CloseRequested?.Invoke(this, true);
        }
        private void OnCancel() => CloseRequested?.Invoke(this, false);
    }
}