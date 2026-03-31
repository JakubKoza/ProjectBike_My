using ProjectBike.Console.Helpers;
using ProjectBike.ServiceAbstractions;
using System;
using System.Linq;

namespace ProjectBike.Console.UI
{
    public class RentalMenu
    {
        private readonly IBikeService _bikeSvc;
        private readonly IClientService _clientSvc;
        private readonly IRentalService _rentalSvc;
        public RentalMenu(IBikeService bikeSvc, IClientService clientSvc, IRentalService rentalSvc)
        {
            _bikeSvc = bikeSvc;
            _clientSvc = clientSvc;
            _rentalSvc = rentalSvc;
        }

        public void Run()
        {
            while (true)
            {
                System.Console.Clear();
                System.Console.WriteLine("=== WYPOŻYCZENIA (UI Switch) ===");
                System.Console.WriteLine("1) Pokaż aktywne wypożyczenia");
                System.Console.WriteLine("2) Nowe wypożyczenie");
                System.Console.WriteLine("0) Powrót");
                System.Console.Write("Wybierz opcję: ");

                var key = System.Console.ReadKey();
                System.Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        ShowRentals();
                        ConsoleHelpers.Pause();
                        break;
                    case '2':
                        CreateRental();
                        ConsoleHelpers.Pause();
                        break;
                    case '0':
                        return;
                    default:
                        System.Console.WriteLine("Nieznana opcja.");
                        ConsoleHelpers.Pause();
                        break;
                }
            }
        }

        private void ShowRentals()
        {
            var rentals = _rentalSvc.GetActiveRentals();
            if (!rentals.Any())
            {
                System.Console.WriteLine("Brak aktywnych wypożyczeń.");
                return;
            }

            foreach (var r in rentals)
            {
                System.Console.WriteLine($"Rental #{r.Id} | Klient ID: {r.ClientId} | Rower ID: {r.BikeId} | Do: {r.EndDate:yyyy-MM-dd}");
            }
        }

        private void CreateRental()
        {
            // 1. Wyświetl i wybierz klienta
            var clients = _clientSvc.GetAll().ToList();
            if (!clients.Any())
            {
                System.Console.WriteLine("Brak klientów. Dodaj klienta w menu Pracowników/Klientów.");
                return;
            }

            System.Console.WriteLine("\n-- Klienci --");
            for (int i = 0; i < clients.Count; i++)
                System.Console.WriteLine($"{i + 1}) {clients[i].Firstname} {clients[i].Lastname}");

            int clientIdx = ConsoleHelpers.ReadIndex("Wybierz klienta (nr): ", clients.Count);
            if (clientIdx == -1) return;

            // 2. Wyświetl i wybierz rower
            var bikes = _bikeSvc.GetAvailableBikes().ToList();
            if (!bikes.Any())
            {
                System.Console.WriteLine("Brak dostępnych rowerów.");
                return;
            }

            System.Console.WriteLine("\n-- Dostępne Rowery --");
            for (int i = 0; i < bikes.Count; i++)
                System.Console.WriteLine($"{i + 1}) {bikes[i].Brand} {bikes[i].Model}");

            int bikeIdx = ConsoleHelpers.ReadIndex("Wybierz rower (nr): ", bikes.Count);
            if (bikeIdx == -1) return;

            // 3. Czas
            int days = ConsoleHelpers.ReadInt("Na ile dni: ");

            try
            {
                _rentalSvc.CreateRental(clients[clientIdx].Id, bikes[bikeIdx].Id, DateTime.Now, days);
                System.Console.WriteLine("Wypożyczenie utworzone.");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Błąd: {ex.Message}");
            }
        }
    }
}