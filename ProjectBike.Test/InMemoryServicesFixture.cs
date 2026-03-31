using ProjectBike.Abstractions;
using ProjectBike.DataAccess.Memory;
using ProjectBike.DataAccess.Memory.Repositories;
using ProjectBike.ServiceAbstractions;
using ProjectBike.Services;
using System;

namespace ProjectBike.Test
{
    public class InMemoryServicesFixture
    {
        public IBikeService BikeService { get; }
        public IClientService ClientService { get; }
        public IEmployeeService EmployeeService { get; }
        public IRentalService RentalService { get; }

        public InMemoryServicesFixture()
        {
            // 1. Baza danych
            var db = new MemoryDbContext();

            // 2. Repozytoria
            IBikeRepository bikeRepo = new BikeRepositoryMemory(db);
            IEmployeesRepository employeeRepo = new EmployeesRepositoryMemory(db);
            IClientRepository clientRepo = new ClientRepositoryMemory(db);
            IRentalRepository rentalRepo = new RentalRepositoryMemory(db);
            IUnitOfWork uow = new UnitOfWorkMemory(db);

            // 3. Serwisy
            BikeService = new BikeService(bikeRepo, uow);
            EmployeeService = new EmployeeService(employeeRepo, uow);
            ClientService = new ClientService(clientRepo, uow);
            RentalService = new RentalService(rentalRepo, bikeRepo, clientRepo, uow);

            // 4. Seed (dane startowe - opcjonalne w testach, ale profesor często to ma)
            DataSeeder seeder = new DataSeeder(BikeService, ClientService, EmployeeService, RentalService);
            seeder.Seed();
        }
    }
}