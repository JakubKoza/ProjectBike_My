using ProjectBike.Desktop.ViewModels;
using ProjectBike.Desktop.Infrastructure;
using ProjectBike.Desktop.Services;
using ProjectBike.ServiceAbstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectBike.Desktop.ViewModels.Bikes
{
    public class BikesViewModel : BaseViewModel
    {
        private readonly IBikeService _bikeSvc;
        private readonly IBikeDialogService _dialogs;
        private readonly INotificationService _notify;

        public ObservableCollection<BikeListItemVm> Bikes { get; } = new();

        private BikeListItemVm? _selectedBike;
        public BikeListItemVm? SelectedBike
        {
            get => _selectedBike;
            set
            {
                if (Set(ref _selectedBike, value))
                {
                    OnPropertyChanged(nameof(SelectedBikeName));
                    EditCommand.RaiseCanExecuteChanged();
                    DeleteCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string SelectedBikeName => SelectedBike?.Description ?? "brak";

        public RelayCommand LoadCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public bool HasChanges { get; private set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set { if (Set(ref _isBusy, value)) { LoadCommand.RaiseCanExecuteChanged(); AddCommand.RaiseCanExecuteChanged(); } }
        }

        public BikesViewModel(IBikeService bikeSvc, IBikeDialogService dialogs, INotificationService notify)
        {
            _bikeSvc = bikeSvc;
            _dialogs = dialogs;
            _notify = notify;

            LoadCommand = new RelayCommand(LoadBikes, () => !IsBusy);
            AddCommand = new RelayCommand(AddBike, () => !IsBusy);
            EditCommand = new RelayCommand(EditBike, () => SelectedBike != null && !IsBusy);
            DeleteCommand = new RelayCommand(DeleteBike, () => SelectedBike != null && !IsBusy);
        }

        private void LoadBikes()
        {
            IsBusy = true;
            Bikes.Clear();
            foreach (var b in _bikeSvc.GetAll())
            {
                Bikes.Add(new BikeListItemVm
                {
                    Id = b.Id,
                    Description = $"{b.Brand} {b.Model}",
                    Type = b.Type,
                    IsAvailable = b.IsAvailable
                });
            }
            IsBusy = false;
        }

        private void AddBike()
        {
            var res = _dialogs.PromptAddBike();
            if (!res.ok) return;
            try
            {
                // WarehouseId = 1 (domyślny)
                _bikeSvc.CreateBike(res.brand, res.model, res.type, 1);
                _notify.Info("Dodano rower.", "Sukces");
                LoadBikes();
                HasChanges = true;
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message, "Błąd");
            }
        }

        private void EditBike()
        {
            if (SelectedBike == null) return;

            var fullBike = _bikeSvc.Get(SelectedBike.Id);
            if (fullBike == null)
            {
                _notify.Error("Nie znaleziono roweru w bazie (może został usunięty?).", "Błąd");
                LoadBikes(); 
                return;
            }

            var result = _dialogs.PromptEditBike(fullBike.Brand, fullBike.Model, fullBike.Type);

            if (!result.ok) return;

            try
            {
                bool success = _bikeSvc.UpdateBike(fullBike.Id, result.brand, result.model, result.type, fullBike.WarehouseId);

                if (success)
                {
                    _notify.Info("Zaktualizowano dane roweru.", "Sukces");
                    LoadBikes(); // Odświeżamy listę, żeby zobaczyć zmiany
                    HasChanges = true;
                }
                else
                {
                    _notify.Error("Nie udało się zapisać zmian.", "Błąd");
                }
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message, "Błąd");
            }
        }

        private void DeleteBike()
        {
            if (SelectedBike == null) return;
            if (!_dialogs.Confirm("Usunąć rower?", "Potwierdź")) return;
            try
            {
                _bikeSvc.DeleteBike(SelectedBike.Id);
                LoadBikes();
                HasChanges = true;
            }
            catch (Exception ex) { _notify.Error(ex.Message, "Błąd"); }
        }
    }
}