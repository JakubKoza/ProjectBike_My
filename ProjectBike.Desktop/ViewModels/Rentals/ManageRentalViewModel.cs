using ProjectBike.Desktop.Infrastructure;
using ProjectBike.Desktop.Services;
using ProjectBike.Desktop.ViewModels.Clients;
using ProjectBike.ServiceAbstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectBike.Desktop.ViewModels.Rentals
{
    public class ManageRentalViewModel : BaseViewModel
    {
        private readonly IRentalService _rentalSvc;
        private readonly IBikeService _bikeSvc;
        private readonly INotificationService _notify;
        private readonly IBikeDialogService _dialogs;

        private int _clientId;
        private string _clientName = "";

        public string TitleText => $"Zarządzaj wypożyczeniami: {_clientName}";

        public ObservableCollection<RentalListItemVm> CurrentRentals { get; } = new();
        public ObservableCollection<AvailableBikeVm> AvailableBikes { get; } = new();

        private AvailableBikeVm? _selectedAvailableBike;
        public AvailableBikeVm? SelectedAvailableBike
        {
            get => _selectedAvailableBike;
            set { if (Set(ref _selectedAvailableBike, value)) AddRentalCommand.RaiseCanExecuteChanged(); }
        }

        public bool HasChanges { get; private set; }

        public RelayCommand LoadCommand { get; }
        public RelayCommand AddRentalCommand { get; }
        public RelayCommand<RentalListItemVm> ReturnBikeCommand { get; }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set { if (Set(ref _isBusy, value)) { LoadCommand.RaiseCanExecuteChanged(); AddRentalCommand.RaiseCanExecuteChanged(); } }
        }

        private int _rentalDays = 1;
        public int RentalDays
        {
            get => _rentalDays;
            set => Set(ref _rentalDays, value);
        }

        public ManageRentalViewModel(IRentalService rentalSvc, IBikeService bikeSvc, INotificationService notify, IBikeDialogService dialogs)
        {
            _rentalSvc = rentalSvc;
            _bikeSvc = bikeSvc;
            _notify = notify;
            _dialogs = dialogs;

            LoadCommand = new RelayCommand(Load, () => !IsBusy);
            AddRentalCommand = new RelayCommand(AddRental, () => !IsBusy && SelectedAvailableBike != null);
            ReturnBikeCommand = new RelayCommand<RentalListItemVm>(ReturnBike, r => !IsBusy && r != null);
        }

        public void SetClient(int id, string name)
        {
            _clientId = id;
            _clientName = name;
            OnPropertyChanged(nameof(TitleText));
            HasChanges = false;
        }

        private void Load()
        {
            IsBusy = true;
            CurrentRentals.Clear();
            var rentals = _rentalSvc.GetClientRentals(_clientId);
            foreach (var r in rentals)
            {
                var bike = _bikeSvc.Get(r.BikeId);
                CurrentRentals.Add(new RentalListItemVm
                {
                    RentalId = r.Id,
                    BikeId = r.BikeId,
                    BikeName = bike != null ? $"{bike.Brand} {bike.Model}" : "?",
                    EndDate = r.EndDate
                });
            }

            AvailableBikes.Clear();
            var free = _bikeSvc.GetAvailableBikes();
            foreach (var b in free)
            {
                AvailableBikes.Add(new AvailableBikeVm { Id = b.Id, Display = $"{b.Brand} {b.Model} ({b.Type})" });
            }
            SelectedAvailableBike = AvailableBikes.FirstOrDefault();
            IsBusy = false;
        }

        private void AddRental()
        {
            if (SelectedAvailableBike == null) return;
            try
            {
                IsBusy = true;
                _rentalSvc.CreateRental(_clientId, SelectedAvailableBike.Id, DateTime.Now, RentalDays);
                _notify.Info("Wypożyczono rower.", "Sukces");
                HasChanges = true;
                Load();
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message, "Błąd");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ReturnBike(RentalListItemVm? rental)
        {
            if (rental == null) return;
            if (!_dialogs.Confirm("Zwrócić ten rower?", "Potwierdź")) return;
            try
            {
                IsBusy = true;
                _rentalSvc.ReturnBike(rental.RentalId, "OK");
                _notify.Info("Zwrócono rower.", "Sukces");
                HasChanges = true;
                Load();
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message, "Błąd");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}