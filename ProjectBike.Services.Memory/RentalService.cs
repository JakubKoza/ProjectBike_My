using ProjectBike.Abstractions;
using ProjectBike.DataModel.Models;
using ProjectBike.ServiceAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectBike.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentals;
        private readonly IBikeRepository _bikes;
        private readonly IClientRepository _clients;
        private readonly IUnitOfWork _uow;

        public RentalService(IRentalRepository rentals, IBikeRepository bikes, IClientRepository clients, IUnitOfWork uow)
        {
            _rentals = rentals;
            _bikes = bikes;
            _clients = clients;
            _uow = uow;
        }

        public int CreateRental(int clientId, int bikeId, DateTime startDate, int days)
        {
            var bike = _uow.Bikes.Get(bikeId);

            // Zmiana Exception na InvalidOperationException (wymagane przez testy)
            if (bike == null || !bike.IsAvailable)
                throw new InvalidOperationException("Rower niedostępny.");

            var rental = new Rental
            {
                ClientId = clientId,
                BikeId = bikeId,
                StartDate = startDate,
                EndDate = startDate.AddDays(days),
                TotalPrice = days * 40.0
            };

            _uow.Rentals.Add(rental);
            bike.IsAvailable = false;
            _uow.SaveChanges();

            return rental.Id; // Zwracamy ID
        }

        public bool ReturnBike(int rentalId, string condition)
        {
            var rental = _rentals.Get(rentalId);
            if (rental is null) return false;

            var bike = _bikes.Get(rental.BikeId);
            if (bike != null)
                bike.IsAvailable = true;

            _rentals.Remove(rental);
            _uow.SaveChanges();
            return true;
        }

        public IReadOnlyList<Rental> GetActiveRentals()
        {
            return _rentals.Query()
                .OrderBy(r => r.EndDate)
                .ToList();
        }

        public IReadOnlyList<Rental> GetClientRentals(int clientId)
        {
            return _rentals.Query()
                .Where(r => r.ClientId == clientId)
                .OrderByDescending(r => r.StartDate)
                .ToList();
        }

        public Rental? Get(int id) => _rentals.Get(id);
    }
}