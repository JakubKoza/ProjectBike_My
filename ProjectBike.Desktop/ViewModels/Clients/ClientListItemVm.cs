using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.Desktop.ViewModels.Clients;
public class ClientListItemVm
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = ""; // Imię + Nazwisko
    public int RentalsCount { get; set; }
}