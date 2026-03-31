using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.Desktop.ViewModels.Bikes;
public class BikeListItemVm
{
    public int Id { get; set; }
    public string Description { get; set; } = ""; // Marka + Model
    public string Type { get; set; } = "";
    public bool IsAvailable { get; set; }
}