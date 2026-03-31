using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBike.Console.Helpers;
using ProjectBike.ServiceAbstractions;

namespace ProjectBike.Console.UIDictionary;

public class RentalsMenu : MenuBase
{
    private readonly IRentalService _rentalSvc;
    private readonly IBikeService _bikeSvc;
    private readonly IClientService _clientSvc;

    public RentalsMenu(IRentalService rentalSvc, IBikeService bikeSvc, IClientService clientSvc)
    {
        _rentalSvc = rentalSvc;
        _bikeSvc = bikeSvc;
        _clientSvc = clientSvc;
    }

    protected override string Title => "ZARZĄDZANIE WYPOŻYCZENIAMI";

    protected override Dictionary<char, MenuOption> Options => new()
    {
        ['1'] = new("Lista aktywnych wypożyczeń", ShowRentals),
        ['2'] = new("Nowe wypożyczenie", CreateRental),
        ['3'] = new("Szczegóły / Zwrot wypożyczenia", RentalDetailsFlow),
        ['0'] = new("Powrót", null),
    };

    private void ShowRentals()
    {
        ListPrinters.ShowRentals(_rentalSvc, _clientSvc, _bikeSvc);
        ConsoleHelpers.Pause();
    }

    private void CreateRental()
    {
        // 1. Wybór Klienta
        var clients = _clientSvc.GetAll().ToList();
        if (!clients.Any())
        {
            System.Console.WriteLine("Brak klientów. Dodaj najpierw klienta w menu Pracowników/Klientów.");
            ConsoleHelpers.Pause();
            return;
        }

        System.Console.WriteLine("--- Wybierz Klienta ---");
        for (int i = 0; i < clients.Count; i++)
            System.Console.WriteLine($"{i + 1}) {clients[i].Firstname} {clients[i].Lastname}");

        int clientIdx = ConsoleHelpers.ReadIndex("Numer klienta: ", clients.Count);
        if (clientIdx < 0) return;

        // 2. Wybór Roweru (tylko dostępne)
        var bikes = _bikeSvc.GetAvailableBikes().ToList();
        if (!bikes.Any())
        {
            System.Console.WriteLine("Brak dostępnych rowerów.");
            ConsoleHelpers.Pause();
            return;
        }

        System.Console.WriteLine("\n--- Wybierz Rower ---");
        for (int i = 0; i < bikes.Count; i++)
            System.Console.WriteLine($"{i + 1}) {bikes[i].Brand} {bikes[i].Model} ({bikes[i].Type})");

        int bikeIdx = ConsoleHelpers.ReadIndex("Numer roweru: ", bikes.Count);
        if (bikeIdx < 0) return;

        // 3. Czas
        int days = ConsoleHelpers.ReadInt("Na ile dni: ");

        try
        {
            _rentalSvc.CreateRental(clients[clientIdx].Id, bikes[bikeIdx].Id, DateTime.Now, days);
            System.Console.WriteLine("Wypożyczenie utworzone pomyślnie.");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Błąd: {ex.Message}");
        }
        ConsoleHelpers.Pause();
    }

    private void RentalDetailsFlow()
    {
        var rentals = _rentalSvc.GetActiveRentals().ToList();
        if (!rentals.Any())
        {
            System.Console.WriteLine("Brak aktywnych wypożyczeń.");
            ConsoleHelpers.Pause();
            return;
        }

        ListPrinters.ShowRentals(_rentalSvc, _clientSvc, _bikeSvc);

        int idx = ConsoleHelpers.ReadIndex("Wybierz wypożyczenie: ", rentals.Count);
        if (idx < 0) return;

        // Przejście do szczegółów konkretnego wypożyczenia
        new RentalDetailsMenu(rentals[idx].Id, _rentalSvc, _bikeSvc, _clientSvc).Run();
    }
}