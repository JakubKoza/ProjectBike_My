using ProjectBike.ServiceAbstractions;
using System;
using Xunit;

namespace ProjectBike.Test
{
    public class RentalServiceTests : IClassFixture<InMemoryServicesFixture>
    {
        private readonly IRentalService _rentalSvc;
        private readonly IBikeService _bikeSvc;
        private readonly IClientService _clientSvc;

        public RentalServiceTests(InMemoryServicesFixture fx)
        {
            _rentalSvc = fx.RentalService;
            _bikeSvc = fx.BikeService;
            _clientSvc = fx.ClientService;
        }

        [Fact]
        public void CreateRental_ShouldRentBike_And_MakeItUnavailable()
        {
            // 1. Przygotowanie danych
            var bikeId = _bikeSvc.CreateBike("Rental", "TestBike", "City", 1);
            var clientId = _clientSvc.CreateClient("Rent", "User", 20, 180, 75, "All");

            // 2. Akcja: Wypożyczamy
            var rentalId = _rentalSvc.CreateRental(clientId, bikeId, DateTime.Now, 3);

            // 3. Sprawdzenie
            var rental = _rentalSvc.Get(rentalId);
            var bike = _bikeSvc.Get(bikeId);

            Assert.NotEqual(0, rentalId);
            Assert.NotNull(rental);

            // Rower musi być oznaczony jako niedostępny!
            Assert.False(bike!.IsAvailable, "Rower powinien być niedostępny po wypożyczeniu.");
        }

        [Fact]
        public void CreateRental_ShouldThrow_WhenBikeIsAlreadyRented()
        {
            // 1. Przygotowanie - Rower i pierwsze wypożyczenie
            var bikeId = _bikeSvc.CreateBike("Busy", "Bike", "MTB", 1);
            var clientId = _clientSvc.CreateClient("User", "One", 30, 180, 80, "MTB");

            _rentalSvc.CreateRental(clientId, bikeId, DateTime.Now, 5);

            Assert.Throws<InvalidOperationException>(() =>
            {
                _rentalSvc.CreateRental(clientId, bikeId, DateTime.Now, 2);
            });
        }

        [Fact]
        public void ReturnBike_ShouldMakeBikeAvailableAgain()
        {
            // 1. Wypożyczamy rower
            var bikeId = _bikeSvc.CreateBike("Return", "Me", "Road", 1);
            var clientId = _clientSvc.CreateClient("User", "Returner", 25, 175, 70, "Road");
            var rentalId = _rentalSvc.CreateRental(clientId, bikeId, DateTime.Now, 2);

            // 2. Zwracamy rower
            var result = _rentalSvc.ReturnBike(rentalId, "OK");
            var bikeAfter = _bikeSvc.Get(bikeId);

            Assert.True(result);
            Assert.True(bikeAfter!.IsAvailable, "Rower powinien być znów dostępny po zwrocie.");
        }
    }
}