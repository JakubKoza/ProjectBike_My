using System.Linq;
using ProjectBike.ServiceAbstractions;

namespace ProjectBike.Console.Helpers;

public static class ListPrinters
{
    public static void ShowBikes(IBikeService bikeSvc)
    {
        var bikes = bikeSvc.GetAll();
        if (!bikes.Any())
        {
            System.Console.WriteLine("Brak rowerów.");
            return;
        }

        System.Console.WriteLine("\n--- [] Lista Rowerów [] ---");
        int i = 1;
        foreach (var b in bikes)
        {
            string status = b.IsAvailable ? "Dostępny" : "Wypożyczony";
            System.Console.WriteLine($"{i++}) {b.Brand} {b.Model} [{b.Type}] - {status}");
        }
    }

    public static void ShowEmployees(IEmployeeService empSvc)
    {
        var emps = empSvc.GetAll();
        if (!emps.Any())
        {
            System.Console.WriteLine("Brak pracowników.");
            return;
        }

        System.Console.WriteLine("\n--- [] Lista Pracowników [] ---");
        int i = 1;
        foreach (var e in emps)
        {
            System.Console.WriteLine($"{i++}) {e.Firstname} {e.Lastname} ({e.Position})");
        }
    }
    public static void ShowRentals(IRentalService rentalSvc, IClientService clientSvc, IBikeService bikeSvc)
    {
        var rentals = rentalSvc.GetActiveRentals();
        if (!rentals.Any())
        {
            System.Console.WriteLine("Brak aktywnych wypożyczeń.");
            return;
        }

        System.Console.WriteLine("\n--- [] Aktywne Wypożyczenia [] ---");
        int i = 1;
        foreach (var r in rentals)
        {
            var client = clientSvc.Get(r.ClientId);
            var bike = bikeSvc.Get(r.BikeId);

            string clientName = client != null ? $"{client.Firstname} {client.Lastname}" : $"Klient {r.ClientId}";
            string bikeName = bike != null ? $"{bike.Brand} {bike.Model}" : $"Rower {r.BikeId}";

            System.Console.WriteLine($"{i++}) [ID:{r.Id}] {clientName} wypożyczył/a {bikeName} (do: {r.EndDate:yyyy-MM-dd})");
        }
    }
}