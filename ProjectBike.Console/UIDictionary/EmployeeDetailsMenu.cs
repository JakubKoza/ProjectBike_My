using System.Collections.Generic;
using ProjectBike.Console.Helpers;
using ProjectBike.ServiceAbstractions;

namespace ProjectBike.Console.UIDictionary;

public class EmployeeDetailsMenu : MenuBase
{
    private readonly int _empId;
    private readonly IEmployeeService _employeeSvc;

    public EmployeeDetailsMenu(int empId, IEmployeeService employeeSvc)
    {
        _empId = empId;
        _employeeSvc = employeeSvc;
    }

    protected override string Title => $"PRACOWNIK: {_employeeSvc.Get(_empId)?.Lastname}";

    protected override Dictionary<char, MenuOption> Options => new()
    {
        ['1'] = new("Zmień stanowisko", ChangePos),
        ['2'] = new("Zwolnij (Usuń)", DeleteEmp),
        ['0'] = new("Powrót", null),
    };

    private void ChangePos()
    {
        var pos = ConsoleHelpers.ReadString("Nowe stanowisko: ");
        _employeeSvc.UpdateEmployeePosition(_empId, pos);
        System.Console.WriteLine("Zmieniono.");
        ConsoleHelpers.Pause();
    }

    private void DeleteEmp()
    {
        _employeeSvc.DeleteEmployee(_empId);
        System.Console.WriteLine("Usunięto.");
        throw new System.Exception("Pracownik usunięty.");
    }
}