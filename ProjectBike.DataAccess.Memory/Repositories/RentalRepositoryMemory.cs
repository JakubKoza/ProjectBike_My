using ProjectBike.Abstractions;
using ProjectBike.DataAccess.Memory;
using ProjectBike.DataModel.Models;
using System.Linq;

namespace ProjectBike.DataAccess.Memory.Repositories;

public class RentalRepositoryMemory : IRentalRepository
{
    private readonly MemoryDbContext _db;
    public RentalRepositoryMemory(MemoryDbContext db) => _db = db;

    public IQueryable<Rental> Query() => _db.Rentals.AsQueryable();

    public Rental? Get(int id) => _db.Rentals.FirstOrDefault(r => r.Id == id);

    public void Add(Rental entity)
    {
        // Generowanie ID
        entity.Id = _db.Rentals.Any() ? _db.Rentals.Max(r => r.Id) + 1 : 1;
        _db.Rentals.Add(entity);
    }

    public void Remove(Rental entity) => _db.Rentals.Remove(entity);
}