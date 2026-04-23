using ProjectBike.Abstractions;
using ProjectBike.DataModel;
using ProjectBike.DataModel.Models;
using ProjectBike.ServiceAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clients;
    private readonly IUnitOfWork _uow;

    public ClientService(IClientRepository clients, IUnitOfWork uow)
    {
        _clients = clients;
        _uow = uow;
    }

    public int CreateClient(string firstName, string lastName, int age, double height, double weight,
                            string street, string housenumber, string city, string state, string zipcode, string country)
    {
        var client = new Client
        {
            Firstname = firstName,
            Lastname = lastName,
            Age = age,
            Height = height,
            Weight = weight,

            Street = street,
            HouseNumber = housenumber,
            City = city,
            State = state,
            ZipCode = zipcode,
            Country = country

        };

        _clients.Add(client);
        _uow.SaveChanges();

        return client.Id;
    }

    public Client? Get(int id) => _clients.Get(id);

    public IReadOnlyList<Client> GetAll() => _clients.Query().ToList();

    public void UpdateClient(int id, string firstname, string lastname, int age, double height, double weight,
                        string street, string housenumber, string city, string state, string zipcode, string country)
    {
        var client = _clients.Get(id);
        if (client != null)
        {
            // Dane podstawowe
            client.Firstname = firstname;
            client.Lastname = lastname;
            client.Age = age;
            client.Height = height;
            client.Weight = weight;

            // Dane adresowe
            client.Street = street;
            client.HouseNumber = housenumber;
            client.City = city;
            client.State = state;
            client.ZipCode = zipcode;
            client.Country = country;

            // Zapisanie zmian w bazie
            _uow.SaveChanges();
        }
    }

    public bool DeleteClient(int clientId)
    {
        var client = _clients.Get(clientId);
        if (client == null) return false;

        _clients.Remove(client);
        return _uow.SaveChanges() > 0;
    }
}
