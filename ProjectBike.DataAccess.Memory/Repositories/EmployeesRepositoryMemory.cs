using ProjectBike.Abstractions;
using ProjectBike.DataAccess.Memory;
using ProjectBike.DataModel.Models;
using System.Linq;

namespace ProjectBike.DataAccess.Memory.Repositories;

public class EmployeesRepositoryMemory : IEmployeesRepository
{
    private readonly MemoryDbContext _db;
    public EmployeesRepositoryMemory(MemoryDbContext db) => _db = db;

    public IQueryable<Employee> Query() => _db.Employees.AsQueryable();

    public Employee? Get(int id) => _db.Employees.FirstOrDefault(e => e.Id == id);

    public void Add(Employee entity)
    {
        // Generowanie ID
        entity.Id = _db.Employees.Any() ? _db.Employees.Max(e => e.Id) + 1 : 1;
        _db.Employees.Add(entity);
    }

    public void Remove(Employee entity) => _db.Employees.Remove(entity);
}