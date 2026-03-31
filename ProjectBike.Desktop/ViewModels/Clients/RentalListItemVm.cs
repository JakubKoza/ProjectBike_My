using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.Desktop.ViewModels.Clients;
public class RentalListItemVm
{
    public int RentalId { get; set; } // ID Wypożyczenia
    public int BikeId { get; set; }
    public string BikeName { get; set; } = "";
    public DateTime EndDate { get; set; }
}