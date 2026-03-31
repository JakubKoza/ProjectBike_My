using ProjectBike.Console.Helpers;
using ProjectBike.ServiceAbstractions;
using System;
using System.Linq;

namespace ProjectBike.Console.UI
{
    public class BikeMenu
    {
        private readonly IBikeService _bikeSvc;

        public BikeMenu(IBikeService bikeSvc)
        {
            _bikeSvc = bikeSvc;
        }

        public void Run()
        {
            while (true)
            {
                System.Console.Clear();
                System.Console.WriteLine("=== ROWERY (UI Switch) ===");

                // Wyświetlanie skróconej listy (jak u profesora w TeamsMenu)
                var bikes = _bikeSvc.GetAll();
                if (!bikes.Any())
                {
                    System.Console.WriteLine("Brak rowerów w bazie.");
                }
                else
                {
                    // Wyświetlamy max 5 dla podglądu, żeby nie zapychać ekranu menu
                    foreach (var b in bikes.Take(5))
                    {
                        string status = b.IsAvailable ? "Dostępny" : "Wypożyczony";
                        System.Console.WriteLine($" - {b.Brand} {b.Model} ({status})");
                    }
                    if (bikes.Count > 5) System.Console.WriteLine("   ...");
                }

                System.Console.WriteLine("\n1) Pokaż wszystkie rowery");
                System.Console.WriteLine("2) Dodaj nowy rower");
                System.Console.WriteLine("3) Usuń rower");
                System.Console.WriteLine("0) Powrót");
                System.Console.Write("Wybierz opcję: ");

                var key = System.Console.ReadKey();
                System.Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        ShowAllBikes();
                        ConsoleHelpers.Pause();
                        break;
                    case '2':
                        AddBike();
                        ConsoleHelpers.Pause();
                        break;
                    case '3':
                        DeleteBike();
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

        private void ShowAllBikes()
        {
            var bikes = _bikeSvc.GetAll();
            if (!bikes.Any())
            {
                System.Console.WriteLine("Brak rowerów.");
                return;
            }

            System.Console.WriteLine("\n--- Lista Rowerów ---");
            int i = 1;
            foreach (var b in bikes)
            {
                string status = b.IsAvailable ? "Dostępny" : "Wypożyczony";
                System.Console.WriteLine($"{i++}) {b.Brand} {b.Model} [{b.Type}] - {status}");
            }
        }

        private void AddBike()
        {
            System.Console.WriteLine("--- Dodawanie Roweru ---");
            string brand = ConsoleHelpers.ReadString("Marka: ");
            string model = ConsoleHelpers.ReadString("Model: ");
            string type = ConsoleHelpers.ReadString("Typ: ");
            int warehouseId = ConsoleHelpers.ReadInt("ID Magazynu: ");

            try
            {
                int id = _bikeSvc.CreateBike(brand, model, type, warehouseId);
                System.Console.WriteLine($"Dodano rower. ID: {id}");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Błąd: {ex.Message}");
            }
        }

        private void DeleteBike()
        {
            int id = ConsoleHelpers.ReadInt("Podaj ID roweru do usunięcia: ");
            if (_bikeSvc.DeleteBike(id))
                System.Console.WriteLine("Usunięto pomyślnie.");
            else
                System.Console.WriteLine("Nie znaleziono roweru.");
        }
    }
}