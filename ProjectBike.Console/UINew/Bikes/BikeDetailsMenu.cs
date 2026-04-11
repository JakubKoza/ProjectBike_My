using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBike.Console.UINew.Core;
using ProjectBike.Console.UINew.Helpers;
using ProjectBike.ServiceAbstractions;

namespace ProjectBike.Console.UINew.Bikes;

public class BikeDetailsMenu : MenuBase
{
    private readonly int _bikeId;
    private readonly IBikeService _bikeSvc;
    private readonly IClientService _clientSvc;
    private readonly IRentalService _rentalSvc;

    public BikeDetailsMenu(int bikeId, IBikeService bikeSvc, IClientService clientSvc, IRentalService rentalSvc)
    {
        _bikeId = bikeId;
        _bikeSvc = bikeSvc;
        _clientSvc = clientSvc;
        _rentalSvc = rentalSvc;
    }

    // Dynamiczny tytuł menu
    protected override string Title => $"SZCZEGÓŁY ROWERU: {_bikeSvc.Get(_bikeId)?.Model ?? "Nieznany"}";

    protected override Dictionary<char, MenuOption> Options => new()
    {
        ['1'] = new("Wypożycz ten rower", RentBike),
        ['2'] = new("Zmień status (Dostępny/Niedostępny)", ToggleStatus),
        ['3'] = new("Usuń rower", DeleteBike),
        ['4'] = new("Edytuj dane roweru", EditBike),
        ['0'] = new("Powrót", null),
    };

    private void RentBike()
    {
        var bike = _bikeSvc.Get(_bikeId);
        if (bike != null && !bike.IsAvailable)
        {
            System.Console.WriteLine("Ten rower jest już wypożyczony.");
            ConsoleHelpers.Pause();
            return;
        }

        // Wybór klienta
        var clients = _clientSvc.GetAll().ToList();
        if (!clients.Any())
        {
            System.Console.WriteLine("Brak klientów w bazie. Dodaj klienta najpierw.");
            ConsoleHelpers.Pause();
            return;
        }

        System.Console.WriteLine("Wybierz klienta:");
        for (int i = 0; i < clients.Count; i++)
            System.Console.WriteLine($"{i + 1}) {clients[i].Firstname} {clients[i].Lastname}");

        int idx = ConsoleHelpers.ReadIndex("Numer klienta: ", clients.Count);
        if (idx < 0) return;

        int days = ConsoleHelpers.ReadInt("Na ile dni: ");

        try
        {
            _rentalSvc.CreateRental(clients[idx].Id, _bikeId, DateTime.Now, days);
            System.Console.WriteLine("Rower wypożyczony pomyślnie.");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Błąd: {ex.Message}");
        }
        ConsoleHelpers.Pause();
    }
    private void EditBike()
    {
        var bike = _bikeSvc.Get(_bikeId);
        if (bike == null) return;

        System.Console.WriteLine($"Edycja: {bike.Brand} {bike.Model}");
        var brand = ConsoleHelpers.ReadString($"Nowa marka ({bike.Brand}): ");
        var model = ConsoleHelpers.ReadString($"Nowy model ({bike.Model}): ");
        var type = ConsoleHelpers.ReadString($"Nowy typ ({bike.Type}): ");
        var warehouse = ConsoleHelpers.ReadInt($"Nowe ID Magazynu ({bike.WarehouseId}): ");

        if (_bikeSvc.UpdateBike(_bikeId, brand, model, type, warehouse))
            System.Console.WriteLine("Zaktualizowano dane.");

        ConsoleHelpers.Pause();
    }

    private void ToggleStatus()
    {
        var bike = _bikeSvc.Get(_bikeId);
        if (bike == null) return;

        bool newState = !bike.IsAvailable;
        _bikeSvc.UpdateBikeStatus(_bikeId, newState);
        System.Console.WriteLine($"Status zmieniony na: {(newState ? "Dostępny" : "Niedostępny")}");
        ConsoleHelpers.Pause();
    }

    private void DeleteBike()
    {
        System.Console.WriteLine("Czy na pewno? (t/n)");
        var key = System.Console.ReadLine();
        if (key?.ToLower() == "t")
        {
            _bikeSvc.DeleteBike(_bikeId);
            System.Console.WriteLine("Usunięto.");
            throw new Exception("Rower usunięty, powrót do menu.");
        }
    }
}