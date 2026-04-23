using ProjectBike.DataModel;
using ProjectBike.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.ServiceAbstractions;

public interface IClientService
{
    int CreateClient(string firstName, string lastName, int age, double height, double weight,
                     string street, string housenumber, string city, string state, string zipcode, string country);
    Client? Get(int id);
    IReadOnlyList<Client> GetAll();
    void UpdateClient(int Id, string firstName, string lastName, int age, double height, double weight,
                     string street, string housenumber, string city, string state, string zipcode, string country);
    bool DeleteClient(int clientId);
}
