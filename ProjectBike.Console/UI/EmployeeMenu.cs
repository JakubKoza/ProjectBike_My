using ProjectBike.Console.Helpers;
using ProjectBike.ServiceAbstractions;
using System;
using System.Linq;

namespace ProjectBike.Console.UI
{
    public class EmployeeMenu
    {
        private readonly IEmployeeService _employeeSvc;

        public EmployeeMenu(IEmployeeService employeeSvc)
        {
            _employeeSvc = employeeSvc;
        }

        public void Run()
        {
            while (true)
            {
                System.Console.Clear();
                System.Console.WriteLine("=== PRACOWNICY (UI Switch) ===");
                System.Console.WriteLine("1) Lista pracowników");
                System.Console.WriteLine("2) Dodaj pracownika");
                System.Console.WriteLine("3) Zmień stanowisko");
                System.Console.WriteLine("0) Powrót");
                System.Console.Write("Wybierz opcję: ");

                var key = System.Console.ReadKey();
                System.Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        ShowEmployees();
                        ConsoleHelpers.Pause();
                        break;
                    case '2':
                        AddEmployee();
                        ConsoleHelpers.Pause();
                        break;
                    case '3':
                        ChangePosition();
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

        private void ShowEmployees()
        {
            var employees = _employeeSvc.GetAll();
            if (!employees.Any())
            {
                System.Console.WriteLine("Brak pracowników.");
                return;
            }

            System.Console.WriteLine("\n--- Lista Pracowników ---");
            foreach (var e in employees)
            {
                System.Console.WriteLine($"ID: {e.Id} | {e.Firstname} {e.Lastname} ({e.Position})");
            }
        }

        private void AddEmployee()
        {
            string fname = ConsoleHelpers.ReadString("Imię: ");
            string lname = ConsoleHelpers.ReadString("Nazwisko: ");
            int age = ConsoleHelpers.ReadInt("Wiek: ");
            string pos = ConsoleHelpers.ReadString("Stanowisko: ");

            int id = _employeeSvc.CreateEmployee(fname, lname, age, pos);
            System.Console.WriteLine($"Dodano pracownika. ID: {id}");
        }

        private void ChangePosition()
        {
            int id = ConsoleHelpers.ReadInt("Podaj ID pracownika: ");
            string newPos = ConsoleHelpers.ReadString("Nowe stanowisko: ");

            if (_employeeSvc.UpdateEmployeePosition(id, newPos))
                System.Console.WriteLine("Zaktualizowano stanowisko.");
            else
                System.Console.WriteLine("Błąd (nie znaleziono pracownika).");
        }
    }
}