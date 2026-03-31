using System.Collections.Generic;
using ProjectBike.ServiceAbstractions;

namespace ProjectBike.Console.UIDictionary
{
    public class MainMenu : MenuBase
    {
        private readonly BikesMenu _bikesMenu;
        private readonly EmployeesMenu _employeesMenu;
        private readonly RentalsMenu _rentalsMenu;

        public MainMenu(IBikeService bikeSvc, IEmployeeService employeeSvc, IClientService clientSvc, IRentalService rentalSvc)
        {
            _bikesMenu = new BikesMenu(bikeSvc, clientSvc, rentalSvc);
            _employeesMenu = new EmployeesMenu(employeeSvc);
            _rentalsMenu = new RentalsMenu(rentalSvc, bikeSvc, clientSvc);
        }

        protected override string Title => "[] SYSTEM WYPOŻYCZALNI ROWERÓW []";

        protected override Dictionary<char, MenuOption> Options => new()
        {
            ['1'] = new("Rowery", () => _bikesMenu.Run()),
            ['2'] = new("Pracownicy", () => _employeesMenu.Run()),
            ['3'] = new("Wypożyczenia", () => _rentalsMenu.Run()),
            ['0'] = new("Wyjście", null),
        };
    }
}