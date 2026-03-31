using ProjectBike.Abstractions;
using ProjectBike.DataAccess.Memory.Repositories;
using ProjectBike.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.DataAccess.Memory
{
    public class UnitOfWorkMemory : IUnitOfWork
    {
        private readonly MemoryDbContext _db;

        public UnitOfWorkMemory(MemoryDbContext db)
        {
            _db = db;
            Bikes = new BikeRepositoryMemory(_db);
            Employees = new EmployeesRepositoryMemory(_db);
            Rentals = new RentalRepositoryMemory(_db);        // Moje
            Clients = new ClientRepositoryMemory(_db);
        }

        // TE WŁAŚCIWOŚCI SĄ WYMAGANE PRZEZ INTERFEJS
        public IBikeRepository Bikes { get; }
        public IEmployeesRepository Employees { get; }
        public IRentalRepository Rentals { get; }             // moje
        public IClientRepository Clients { get; }             ///Moje
        public int SaveChanges() => _db.SaveChanges();

    }
}
