using ProjectBike.Console.UINew;
using ProjectBike.Console.UINew.Bikes;
using ProjectBike.Console.UINew.Core;
using ProjectBike.Console.UINew.Helpers;
using ProjectBike.DataModel.Models;
using ProjectBike.ServiceAbstractions;
using ProjectBike.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.Console.UINew.Clients;

public class ClientMenu : MenuBase // dziedziczy po oknie głównym
{
    private readonly IClientService _clientSvc;
    public ClientMenu (IClientService clientSvc) {
        _clientSvc = clientSvc;
    }
    protected override string Title => "KLIENCI"; // wyświetla tytuł menu

    protected override Dictionary<char, MenuOption> Options => new()
    {
        ['1'] = new("Lista klientow", ShowClient),
        ['2'] = new("Dodaj klienta", AddClient),
        ['3'] = new("Szczegóły wybranego klienta (Edytuj, Usuń)", ClientDetailFlow),
        ['0'] = new("Powrót", null),
    };
    private void ShowClient()
    {
        ListPrinters.ShowClients(_clientSvc);
        ConsoleHelpers.Pause();
    }
    private void AddClient()
    {
        var firstname = ConsoleHelpers.ReadString("Imie: ");
        var lastname = ConsoleHelpers.ReadString("Nazwisko: ");
        var age = ConsoleHelpers.ReadInt("Wiek: ");
        var height = ConsoleHelpers.ReadInt("Wysokość (cm): ");
        var weight = ConsoleHelpers.ReadInt("Waga (kg): ");
        var street = ConsoleHelpers.ReadString("Ulica: ");
        var housenumber = ConsoleHelpers.ReadString("Numer Domu: ");
        var city = ConsoleHelpers.ReadString("Miejscowość: ");
        var state = ConsoleHelpers.ReadString("Województwo: ");
        var zipcode = ConsoleHelpers.ReadString("Kod Pocztowy: ");
        var country = ConsoleHelpers.ReadString("kraj: ");

        _clientSvc.CreateClient(firstname, lastname, age, height, weight, street, housenumber, city, state, zipcode, country);
        System.Console.WriteLine("Zaktualizowano Klienta klienta.");
        ConsoleHelpers.Pause();
    }

    private void ClientDetailFlow()
    {
        var clients = _clientSvc.GetAll().ToList();
        if (!clients.Any())
        {
            System.Console.WriteLine("Brak klientów.");
            ConsoleHelpers.Pause();
            return;
        }
        ListPrinters.ShowClients(_clientSvc);

        int idx = ConsoleHelpers.ReadIndex("Wybierz klienta (numer): ", clients.Count);
        if (idx < 0) return;

        var clientId = clients[idx].Id;
        new ClientDetailMenu(clientId, _clientSvc).Run();

    }
}