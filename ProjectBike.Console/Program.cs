using ProjectBike.Abstractions;
using ProjectBike.DataAccess.Memory;
using ProjectBike.DataAccess.Memory.Repositories;
using ProjectBike.ServiceAbstractions;
using ProjectBike.Services;
using ProjectBike.Console.UIDictionary;

namespace Program;
class Program
{
    static void Main()
    {
        // 1. Baza
        var db = new MemoryDbContext();

        // 2. Repozytoria
        IBikeRepository bikeRepo = new BikeRepositoryMemory(db);
        IClientRepository clientRepo = new ClientRepositoryMemory(db);
        IEmployeesRepository employeeRepo = new EmployeesRepositoryMemory(db);
        IRentalRepository rentalRepo = new RentalRepositoryMemory(db);
        IUnitOfWork uow = new UnitOfWorkMemory(db);

        // 3. Serwisy
        IBikeService bikeSvc = new BikeService(bikeRepo, uow);
        IClientService clientSvc = new ClientService(clientRepo, uow);
        IEmployeeService employeeSvc = new EmployeeService(employeeRepo, uow);
        IRentalService rentalSvc = new RentalService(rentalRepo, bikeRepo, clientRepo, uow);

        // 4. Seed
        DataSeeder seeder = new DataSeeder(bikeSvc, clientSvc, employeeSvc, rentalSvc);
        seeder.Seed();
        System.Console.WriteLine("Dane załadowane.");

        // 5. Uruchomienie Menu 
        var mainMenu = new MainMenu(bikeSvc, employeeSvc, clientSvc, rentalSvc);
        mainMenu.Run();
    }
}