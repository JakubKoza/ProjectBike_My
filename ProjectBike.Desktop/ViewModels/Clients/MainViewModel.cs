using ProjectBike.Desktop.Infrastructure;
using ProjectBike.Desktop.Services;
using ProjectBike.Desktop.ViewModels.Rentals; // Dodany using dla RentalListItemVm
using ProjectBike.ServiceAbstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectBike.Desktop.ViewModels.Clients
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IClientService _clientSvc;
        private readonly IRentalService _rentalSvc;
        private readonly IBikeService _bikeSvc;
        private readonly IClientDialogService _dialogs;
        private readonly INotificationService _notify;

        private readonly IBikesWindowService _bikesWindow;
        private readonly IManageRentalWindowService _manageRentalWindow;

        public ObservableCollection<ClientListItemVm> Clients { get; } = new();
        public ObservableCollection<RentalListItemVm> ClientRentals { get; } = new();

        private ClientListItemVm? _selectedClient;
        public ClientListItemVm? SelectedClient
        {
            get => _selectedClient;
            set
            {
                if (Set(ref _selectedClient, value))
                {
                    OnPropertyChanged(nameof(SelectedClientName));
                    DeleteCommand.RaiseCanExecuteChanged();
                    EditCommand.RaiseCanExecuteChanged();
                    ManageRentalsCommand.RaiseCanExecuteChanged();
                    LoadRentals();
                }
            }
        }

        public string SelectedClientName => SelectedClient?.DisplayName ?? "brak";

        public RelayCommand LoadCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand OpenBikesCommand { get; }
        public RelayCommand ManageRentalsCommand { get; }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (Set(ref _isBusy, value))
                {
                    LoadCommand.RaiseCanExecuteChanged();
                    AddCommand.RaiseCanExecuteChanged();
                    EditCommand.RaiseCanExecuteChanged();
                    DeleteCommand.RaiseCanExecuteChanged();
                    OpenBikesCommand.RaiseCanExecuteChanged();
                    ManageRentalsCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public MainViewModel(IClientService clientSvc, IRentalService rentalSvc, IBikeService bikeSvc,
            IClientDialogService dialogs, INotificationService notify,
            IBikesWindowService bikesWindow, IManageRentalWindowService manageRentalWindow)
        {
            _clientSvc = clientSvc;
            _rentalSvc = rentalSvc;
            _bikeSvc = bikeSvc;
            _dialogs = dialogs;
            _notify = notify;
            _bikesWindow = bikesWindow;
            _manageRentalWindow = manageRentalWindow;

            LoadCommand = new RelayCommand(LoadClients, () => !IsBusy);
            AddCommand = new RelayCommand(AddClient, () => !IsBusy);
            EditCommand = new RelayCommand(EditSelectedClient, () => SelectedClient != null && !IsBusy);
            DeleteCommand = new RelayCommand(DeleteSelectedClient, () => SelectedClient != null && !IsBusy);
            OpenBikesCommand = new RelayCommand(ManageBikes, () => !IsBusy);
            ManageRentalsCommand = new RelayCommand(ManageRentals, () => SelectedClient != null && !IsBusy);
        }

        private void LoadClients()
        {
            Clients.Clear();
            foreach (var c in _clientSvc.GetAll())
            {
                var count = _rentalSvc.GetClientRentals(c.Id).Count();
                Clients.Add(new ClientListItemVm
                {
                    Id = c.Id,
                    DisplayName = $"{c.Firstname} {c.Lastname}",
                    RentalsCount = count
                });
            }
        }

        private void LoadRentals()
        {
            ClientRentals.Clear();
            if (SelectedClient is null) return;

            var rentals = _rentalSvc.GetClientRentals(SelectedClient.Id);
            foreach (var r in rentals)
            {
                var bike = _bikeSvc.Get(r.BikeId);
                ClientRentals.Add(new RentalListItemVm
                {
                    RentalId = r.Id,
                    BikeId = r.BikeId,
                    BikeName = bike != null ? $"{bike.Brand} {bike.Model}" : $"Rower {r.BikeId}",
                    EndDate = r.EndDate
                });
            }
        }

        private void AddClient()
        {
            var result = _dialogs.PromptAddClient();
            if (result == null) return;

            try
            {
                IsBusy = true;
                var (fname, lname) = result.Value;
                var id = _clientSvc.CreateClient(fname, lname, 20, 170, 70, "Brak");
                LoadClients();
                SelectedClient = Clients.FirstOrDefault(c => c.Id == id);
                _notify.Info($"Dodano klienta \"{fname} {lname}\".", "Sukces");
            }
            catch (Exception ex)
            {
                _notify.Error("Błąd dodawania: " + ex.Message, "Błąd");
            }
            finally { IsBusy = false; }
        }

        private void EditSelectedClient()
        {
            if (SelectedClient is null) return;
            var existing = _clientSvc.Get(SelectedClient.Id);
            if (existing == null) return;

            var result = _dialogs.PromptEditClient(existing.Firstname, existing.Lastname);
            if (result == null) return;

            try
            {
                IsBusy = true;
                var (fname, lname) = result.Value;
                // IMPLEMENTACJA EDYCJI
                _clientSvc.UpdateClient(SelectedClient.Id, fname, lname);
                LoadClients();
                // Przywróć zaznaczenie
                SelectedClient = Clients.FirstOrDefault(c => c.Id == existing.Id);
                _notify.Info("Zaktualizowano dane klienta.", "Sukces");
            }
            catch (Exception ex)
            {
                _notify.Error("Błąd edycji: " + ex.Message, "Błąd");
            }
            finally { IsBusy = false; }
        }

        private void DeleteSelectedClient()
        {
            if (SelectedClient is null) return;
            if (!_dialogs.Confirm($"Czy usunąć klienta {SelectedClient.DisplayName}?", "Potwierdzenie")) return;

            try
            {
                IsBusy = true;
                _clientSvc.DeleteClient(SelectedClient.Id);
                Clients.Remove(SelectedClient);
                SelectedClient = null;
                ClientRentals.Clear();
                _notify.Info("Usunięto klienta.", "Sukces");
            }
            catch (Exception ex) { _notify.Error(ex.Message, "Błąd"); }
            finally { IsBusy = false; }
        }

        private void ManageRentals()
        {
            if (SelectedClient is null) return;
            int currentId = SelectedClient.Id;

            var changed = _manageRentalWindow.Show(currentId, SelectedClient.DisplayName);

            if (changed)
            {
                LoadClients();
                SelectedClient = Clients.FirstOrDefault(c => c.Id == currentId);
                LoadRentals();
            }
        }

        private void ManageBikes()
        {
            var changed = _bikesWindow.ShowBikesWindow();
            if (changed)
            {
                LoadRentals();
            }
        }
    }
}