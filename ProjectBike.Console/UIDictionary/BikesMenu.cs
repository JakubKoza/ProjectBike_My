using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBike.Console.Helpers;
using ProjectBike.ServiceAbstractions;

namespace ProjectBike.Console.UIDictionary;

public class BikesMenu : MenuBase
{
    private readonly IBikeService _bikeSvc;
    private readonly IClientService _clientSvc;
    private readonly IRentalService _rentalSvc;

    public BikesMenu(IBikeService bikeSvc, IClientService clientSvc, IRentalService rentalSvc)
    {
        _bikeSvc = bikeSvc;
        _clientSvc = clientSvc;
        _rentalSvc = rentalSvc;
    }

    protected override string Title => "ROWERY";

    protected override Dictionary<char, MenuOption> Options => new()
    {
        ['1'] = new("Lista rowerów", ShowBikes),
        ['2'] = new("Dodaj rower", AddBike),
        ['3'] = new("Szczegóły wybranego roweru (Wypożycz/Zwróć/Edytuj)", BikeDetailsFlow),
        ['0'] = new("Powrót", null),
    };

    private void ShowBikes()
    {
        ListPrinters.ShowBikes(_bikeSvc);
        ConsoleHelpers.Pause();
    }

    private void AddBike()
    {
        var brand = ConsoleHelpers.ReadString("Marka: ");
        var model = ConsoleHelpers.ReadString("Model: ");
        var type = ConsoleHelpers.ReadString("Typ: ");
        var warehouseId = ConsoleHelpers.ReadInt("ID Magazynu: ");

        _bikeSvc.CreateBike(brand, model, type, warehouseId);
        System.Console.WriteLine("Dodano rower.");
        ConsoleHelpers.Pause();
    }

    private void BikeDetailsFlow()
    {
        var bikes = _bikeSvc.GetAll().ToList();
        if (!bikes.Any())
        {
            System.Console.WriteLine("Brak rowerów.");
            ConsoleHelpers.Pause();
            return;
        }

        ListPrinters.ShowBikes(_bikeSvc);

        int idx = ConsoleHelpers.ReadIndex("Wybierz rower (numer): ", bikes.Count);
        if (idx < 0) return;

        var bikeId = bikes[idx].Id;
        new BikeDetailsMenu(bikeId, _bikeSvc, _clientSvc, _rentalSvc).Run();
    }
}