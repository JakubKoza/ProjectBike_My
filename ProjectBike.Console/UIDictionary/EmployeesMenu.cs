using System.Collections.Generic;
using System.Linq;
using ProjectBike.Console.Helpers;
using ProjectBike.ServiceAbstractions;

namespace ProjectBike.Console.UIDictionary;

public class EmployeesMenu : MenuBase
{
    private readonly IEmployeeService _employeeSvc;

    public EmployeesMenu(IEmployeeService employeeSvc)
    {
        _employeeSvc = employeeSvc;
    }

    protected override string Title => "PRACOWNICY";

    protected override Dictionary<char, MenuOption> Options => new()
    {
        ['1'] = new("Lista pracowników", ShowAll),
        ['2'] = new("Dodaj pracownika", AddEmployee),
        ['3'] = new("Szczegóły pracownika", DetailsFlow),
        ['0'] = new("Powrót", null),
    };

    private void ShowAll()
    {
        ListPrinters.ShowEmployees(_employeeSvc);
        ConsoleHelpers.Pause();
    }

    private void AddEmployee()
    {
        var f = ConsoleHelpers.ReadString("Imię: ");
        var l = ConsoleHelpers.ReadString("Nazwisko: ");
        var age = ConsoleHelpers.ReadInt("Wiek: ");
        var pos = ConsoleHelpers.ReadString("Stanowisko: ");

        _employeeSvc.CreateEmployee(f, l, age, pos);
        System.Console.WriteLine("Dodano pracownika.");
        ConsoleHelpers.Pause();
    }

    private void DetailsFlow()
    {
        var emps = _employeeSvc.GetAll().ToList();
        if (!emps.Any()) { System.Console.WriteLine("Brak."); ConsoleHelpers.Pause(); return; }

        ListPrinters.ShowEmployees(_employeeSvc);
        int idx = ConsoleHelpers.ReadIndex("Wybierz pracownika: ", emps.Count);
        if (idx < 0) return;

        new EmployeeDetailsMenu(emps[idx].Id, _employeeSvc).Run();
    }
}