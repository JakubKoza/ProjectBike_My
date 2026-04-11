using System;
using ProjectBike.Console.UIDictionary;
using ProjectBike.ServiceAbstractions;
namespace ProjectBike.Console.UI
{
    public class MainMenu
    {
        private readonly BikeMenu _bikeMenu;
        private readonly EmployeeMenu _employeesMenu;
        private readonly RentalMenu _rentalMenu;

        public MainMenu(IBikeService bikeSvc, IClientService clientSvc, IEmployeeService employeeSvc, IRentalService rentalSvc)
        {
            _bikeMenu = new BikeMenu(bikeSvc);
            _employeesMenu = new EmployeeMenu(employeeSvc);
            _rentalMenu = new RentalMenu(bikeSvc, clientSvc, rentalSvc);
        }

        public void Run()
        {
            while (true)
            {
                System.Console.Clear();
                System.Console.WriteLine("=== STARE MENU (SWITCH) ===");
                System.Console.WriteLine("1) Klienci");
                System.Console.WriteLine("2) Rowery");
                System.Console.WriteLine("3) Pracownicy");
                System.Console.WriteLine("4) Wypożyczenia");
                System.Console.WriteLine("0) Wyjście");
                System.Console.Write("Wybierz opcję: ");

                var key = System.Console.ReadKey();
                System.Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '2':
                        _bikeMenu.Run();
                        break;
                    case '3':
                        _employeesMenu.Run();
                        break;
                    case '4':
                        _rentalMenu.Run();
                        break;
                    case '0':
                        return;
                    default:
                        System.Console.WriteLine("Nieznana opcja.");
                        break;
                }
            }
        }
    }
}