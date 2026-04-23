using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.ServiceAbstractions;

public interface IDataSeeder
{
    SeedResult Seed();
}

public sealed class SeedResult
{
    //pracownicy
    public int Bike1Id {  get; set; }
    public int Bike2Id { get; set; }
    public int Bike3Id { get; set; }
    //klienci
    public int Client1Id { get; set; }
    public int Client2Id { get; set; }
    public int Client3Id { get; set; }
    //pracownicy
    public int Employee1Id {  get; set; }
    public int Employee2Id { get; set; }
}
