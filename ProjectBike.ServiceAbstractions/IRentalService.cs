using ProjectBike.DataModel;
using ProjectBike.DataModel.Models;
using System;
using System.Collections.Generic;

namespace ProjectBike.ServiceAbstractions
{
    public interface IRentalService
    {
        int CreateRental(int clientId, int bikeId, DateTime startDate, int days);
        bool ReturnBike(int rentalId, string condition);
        IReadOnlyList<Rental> GetActiveRentals();
        IReadOnlyList<Rental> GetClientRentals(int clientId);
        Rental? Get(int id);
    }
}