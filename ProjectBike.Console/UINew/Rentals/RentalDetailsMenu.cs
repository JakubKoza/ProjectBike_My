using System;
using System.Collections.Generic;
using ProjectBike.Console.UINew.Core;
using ProjectBike.Console.UINew.Helpers;
using ProjectBike.ServiceAbstractions;

namespace ProjectBike.Console.UINew.Rentals;

public class RentalDetailsMenu : MenuBase
{
    private readonly int _rentalId;
    private readonly IRentalService _rentalSvc;
    private readonly IBikeService _bikeSvc;
    private readonly IClientService _clientSvc;

    public RentalDetailsMenu(int rentalId, IRentalService rentalSvc, IBikeService bikeSvc, IClientService clientSvc)
    {
        _rentalId = rentalId;
        _rentalSvc = rentalSvc;
        _bikeSvc = bikeSvc;
        _clientSvc = clientSvc;
    }

    protected override string Title
    {
        get
        {
            var r = _rentalSvc.Get(_rentalId);
            if (r == null) return "WYPOŻYCZENIE (Nieznane)";
            var bike = _bikeSvc.Get(r.BikeId);
            return $"WYPOŻYCZENIE: Rower {bike?.Model ?? "?"}";
        }
    }

    protected override Dictionary<char, MenuOption> Options => new()
    {
        ['1'] = new("Zwróć rower (Zakończ wypożyczenie)", ReturnBike),
        ['0'] = new("Powrót", null),
    };

    private void ReturnBike()
    {
        System.Console.WriteLine("Podaj stan roweru przy zwrocie (np. OK, Uszkodzony): ");
        var condition = System.Console.ReadLine() ?? "OK";

        bool success = _rentalSvc.ReturnBike(_rentalId, condition);

        if (success)
        {
            System.Console.WriteLine("Rower zwrócony pomyślnie. Wypożyczenie zakończone.");
            ConsoleHelpers.Pause();
            throw new ExitMenuException();
        }
        else
        {
            System.Console.WriteLine("Błąd podczas zwracania roweru.");
            ConsoleHelpers.Pause();
        }
    }
}