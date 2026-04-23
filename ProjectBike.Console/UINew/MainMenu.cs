using System.Collections.Generic;
using ProjectBike.Console.UINew.Bikes;
using ProjectBike.Console.UINew.Clients;
using ProjectBike.Console.UINew.Core;
using ProjectBike.Console.UINew.Employes;
using ProjectBike.Console.UINew.Rentals;
using ProjectBike.ServiceAbstractions;

namespace ProjectBike.Console.UINew
{
    public class MainMenu : MenuBase
    {
        private readonly BikesMenu _bikesMenu;
        private readonly EmployeesMenu _employeesMenu;
        private readonly RentalsMenu _rentalsMenu;
        private readonly ClientMenu _clientsMenu;

        public MainMenu(IBikeService bikeSvc, IEmployeeService employeeSvc, IClientService clientSvc, IRentalService rentalSvc)
        {
            _bikesMenu = new BikesMenu(bikeSvc, clientSvc, rentalSvc);
            _clientsMenu = new ClientMenu(clientSvc);
            _employeesMenu = new EmployeesMenu(employeeSvc);
            _rentalsMenu = new RentalsMenu(rentalSvc, bikeSvc, clientSvc);
        }

        protected override string Title => "[] SYSTEM WYPOŻYCZALNI ROWERÓW []";

        protected override Dictionary<char, MenuOption> Options => new()
        {
            ['1'] = new("Klienci", () => _clientsMenu.Run()),
            ['2'] = new("Rowery", () => _bikesMenu.Run()),
            ['3'] = new("Pracownicy", () => _employeesMenu.Run()),
            ['4'] = new("Wypożyczenia", () => _rentalsMenu.Run()),
            ['0'] = new("Wyjście", null),
        };
    }
}