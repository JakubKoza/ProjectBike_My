using ProjectBike.DataModel.Models;
using ProjectBike.ServiceAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.Services;

public class DataSeeder : IDataSeeder
{
    private readonly IBikeService _bikeService;
    private readonly IClientService _clientService;
    private readonly IEmployeeService _employeeService;
    private readonly IRentalService _rentalService;

    public DataSeeder(IBikeService bikeService, IClientService clientService, IEmployeeService employeeService, IRentalService rentalService)
    {
        _bikeService = bikeService;
        _clientService = clientService;
        _employeeService = employeeService;
        _rentalService = rentalService;
    }

    public SeedResult Seed()
    {
        //rowery
        var bike1Id = _bikeService.CreateBike("Trek", "Slash 27.5", "Enduro", 2);
        var bike2Id = _bikeService.CreateBike("YT", "Tues 29", "Downhill", 2);
        var bike3Id = _bikeService.CreateBike("Canyon", "Spectral 29", "SlopeStyle", 2);

        //pracownicy

        var employee1Id = _employeeService.CreateEmployee("Anna", "Nowak", 30, "Manager");
        var employee2Id = _employeeService.CreateEmployee("Maciek", "Lewandowski", 21, "Pomocnik");

        //klienci

        var client1Id = _clientService.CreateClient("Jakub", "Kozłowski", 22, 178.5, 78.0, "Downhill");
        var client2Id = _clientService.CreateClient("Piotr", "Gała", 25, 181.5, 70.0, "Enduro");
        var client3Id = _clientService.CreateClient("Kuba", "Zatoń", 21, 169.5, 99.0, "FreeRide");

        //zlecenia

        _rentalService.CreateRental(client1Id, bike1Id, DateTime.Now, 7);


        return new SeedResult
        {
            Bike1Id = bike1Id,
            Bike2Id = bike2Id,
            Bike3Id = bike3Id,

        };
    }
}