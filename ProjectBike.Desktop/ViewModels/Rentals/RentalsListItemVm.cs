using System;

namespace ProjectBike.Desktop.ViewModels.Rentals
{
    public class RentalListItemVm
    {
        public int RentalId { get; set; }
        public int BikeId { get; set; }
        public string BikeName { get; set; } = "";
        public DateTime EndDate { get; set; }
    }
}