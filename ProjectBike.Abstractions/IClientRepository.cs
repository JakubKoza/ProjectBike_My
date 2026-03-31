using ProjectBike.DataModel;
using ProjectBike.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.Abstractions;

public interface IClientRepository
{
    IQueryable<Client> Query();
    Client? Get(int id);
    void Add(Client entity);
    void Remove(Client entity);
}
