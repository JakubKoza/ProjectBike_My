using ProjectBike.Console.UINew.Core;
using ProjectBike.Console.UINew.Helpers;
using ProjectBike.ServiceAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.Console.UINew.Clients;

public class ClientDetailMenu : MenuBase
{
    private int _clientId;
    private readonly IClientService _clientSvc;

    public ClientDetailMenu(int clientId, IClientService clientSvc)
    {
        _clientId = clientId;
        _clientSvc = clientSvc;
    }

    protected override string Title => $"SZCZEGÓŁY KLIENTA: {_clientSvc.Get(_clientId)?.Firstname ?? "Nieznany"}";

    protected override Dictionary<char, MenuOption> Options => new()
    {
        ['1'] = new ("Edytuj dane o kliencie", UpdateClient),
        ['2'] = new ("Usuń klienta", DeleteClient),
        ['0'] = new("Powrót", null),
    };

    private void UpdateClient()
    {
        var client = _clientSvc.Get(_clientId);
        if (client == null) return;

        System.Console.WriteLine($"Edytowanie {client.Firstname} {client.Lastname}");
        var firstname = ConsoleHelpers.ReadString($"[Nowe] Imie: {client.Firstname}");
        var lastname = ConsoleHelpers.ReadString($"[Nowe] Nazwisko: {client.Lastname}");
        var age = ConsoleHelpers.ReadInt($"[Nowe] Wiek: {client.Age}");
        var height = ConsoleHelpers.ReadDouble($"[Nowe] Wysokość (cm): {client.Height}");
        var weight = ConsoleHelpers.ReadDouble($"[Nowe] Waga (kg): {client.Weight}");
        var street = ConsoleHelpers.ReadString($"[Nowe] Ulica: {client.Street}");
        var housenumber = ConsoleHelpers.ReadString($"[Nowe] Numer Domu: {client.HouseNumber}");
        var city = ConsoleHelpers.ReadString($"[Nowe] Miejscowość: {client.City}");
        var state = ConsoleHelpers.ReadString($"[Nowe] Województwo: {client.State}");
        var zipcode = ConsoleHelpers.ReadString($"[Nowe] Kod Pocztowy: {client.ZipCode}");
        var country = ConsoleHelpers.ReadString($"[Nowe] kraj: {client.Country}");

        _clientSvc.UpdateClient(_clientId, firstname, lastname, age, height, weight, street, housenumber, city, state, zipcode, country);

         System.Console.WriteLine("Zaktualizowano klienta.");

        ConsoleHelpers.Pause();
    }

    private void DeleteClient()
    {
        System.Console.WriteLine("Czy na pewno? (t/n)");
        var key = System.Console.ReadLine();
        if (key?.ToLower() == "t")
        {
            _clientSvc.DeleteClient(_clientId);
            System.Console.WriteLine("Usunięto.");
            throw new Exception("Klient usunięty, powrót do menu.");
        }
    }
}