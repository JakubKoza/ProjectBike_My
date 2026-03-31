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

    public int CreateClient(string firstName, string lastName, int age, double height, double weight, string riderPreference)
    {
        var client = new Client
        {
            Firstname = firstName,
            Lastname = lastName,
            Age = age,
            Height = height,
            Weight = weight,
            RiderPreference = riderPreference
        };

        _clients.Add(client);
        _uow.SaveChanges();

        return client.Id;
    }

    public Client? Get(int id) => _clients.Get(id);

    public IReadOnlyList<Client> GetAll() => _clients.Query().ToList();

    public IReadOnlyList<Client> GetByRiderPreference(string preference) =>
        _clients.Query().Where(c => c.RiderPreference == preference).ToList();

    public void UpdateClient(int id, string fname, string lname)
    {
        var client = _clients.Get(id);
        if (client != null)
        {
            client.Firstname = fname;
            client.Lastname = lname;
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
