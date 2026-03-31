using ProjectBike.Abstractions;
using ProjectBike.DataAccess.Memory;
using ProjectBike.DataModel.Models;
using System.Linq;

namespace ProjectBike.DataAccess.Memory.Repositories;

public class BikeRepositoryMemory : IBikeRepository
{
    private readonly MemoryDbContext _db;
    public BikeRepositoryMemory(MemoryDbContext db) => _db = db;

    public IQueryable<Bike> Query() => _db.Bikes.AsQueryable();

    public Bike? Get(int id) => _db.Bikes.FirstOrDefault(b => b.Id == id);

    public void Add(Bike entity)
    {
        // Generowanie ID
        entity.Id = _db.Bikes.Any() ? _db.Bikes.Max(b => b.Id) + 1 : 1;
        _db.Bikes.Add(entity);
    }

    public void Remove(Bike entity) => _db.Bikes.Remove(entity);
}