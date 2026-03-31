using ProjectBike.Abstractions;
using ProjectBike.DataAccess.Memory;
using ProjectBike.DataModel.Models;
using System.Linq;

namespace ProjectBike.DataAccess.Memory.Repositories;

public class ClientRepositoryMemory : IClientRepository
{
    private readonly MemoryDbContext _db;
    public ClientRepositoryMemory(MemoryDbContext db) => _db = db;

    public IQueryable<Client> Query() => _db.Clients.AsQueryable();

    public Client? Get(int id) => _db.Clients.FirstOrDefault(c => c.Id == id);

    public void Add(Client entity)
    {
        // Generowanie ID
        entity.Id = _db.Clients.Any() ? _db.Clients.Max(c => c.Id) + 1 : 1;
        _db.Clients.Add(entity);
    }

    public void Remove(Client entity) => _db.Clients.Remove(entity);
}