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
    int CreateClient(string firstName, string lastName, int age, double height, double weight, string riderPreference);
    Client? Get(int id);
    IReadOnlyList<Client> GetAll();
    IReadOnlyList<Client> GetByRiderPreference(string preference);
    void UpdateClient(int id, string fname, string lname);
    bool DeleteClient(int clientId);
}
