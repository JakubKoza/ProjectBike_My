using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBike.DataModel.Models;

namespace ProjectBike.DataAccess.Memory;

public class MemoryDbContext
{
    public List<Bike> Bikes { get; set; } = new List<Bike>();
    public List<BikePart> BikeParts { get; set; } = new List<BikePart>();
    public List<Part> Parts { get; set; } = new List<Part>();
    public List<Client> Clients { get; set; } = new List<Client>();
    public List<Employee> Employees { get; set; } = new List<Employee>();
    public List<Rental> Rentals { get; set; } = new List<Rental>();
    public List<RentalPart> RentalParts { get; set; } = new List<RentalPart>();
    public List<Warehouse> Warehouses { get; set; } = new List<Warehouse>();

    public int SaveChanges() => 1;
}


