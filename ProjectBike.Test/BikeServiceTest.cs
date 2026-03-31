using ProjectBike.ServiceAbstractions;
using System;
using System.Linq;
using Xunit;

namespace ProjectBike.Test
{
    public class BikeServiceTests : IClassFixture<InMemoryServicesFixture>
    {
        private readonly IBikeService _bikeSvc;

        public BikeServiceTests(InMemoryServicesFixture fx)
        {
            _bikeSvc = fx.BikeService;
        }

        [Fact]
        public void CreateBike_ShouldAddNewBike_WhenDataIsValid()
        {
            var brand = "Kross";
            var model = "Level " + Guid.NewGuid(); // Unikalna nazwa dla testu

            var id = _bikeSvc.CreateBike(brand, model, "MTB", 1);
            var bike = _bikeSvc.Get(id);

            Assert.NotEqual(0, id);
            Assert.NotNull(bike);
            Assert.Equal(model, bike!.Model);
            Assert.True(bike.IsAvailable);
        }

        [Fact]
        public void Search_ShouldFindBike_ByBrandFragment()
        {
            // Zakładamy, że Seeder dodał rower marki "Trek"
            var results = _bikeSvc.Search("Trek");

            Assert.NotEmpty(results);
            Assert.Contains(results, b => b.Brand.Contains("Trek"));
        }

        [Fact]
        public void UpdateBikeStatus_ShouldChangeAvailability()
        {
            var id = _bikeSvc.CreateBike("Test", "StatusChange", "City", 1);

            // Zmieniamy na niedostępny
            var result = _bikeSvc.UpdateBikeStatus(id, false);
            var bike = _bikeSvc.Get(id);

            Assert.True(result);
            Assert.False(bike!.IsAvailable);
        }

        [Fact]
        public void DeleteBike_ShouldRemoveBike()
        {
            var id = _bikeSvc.CreateBike("Test", "ToDelete", "BMX", 1);

            var result = _bikeSvc.DeleteBike(id);
            var bikeAfter = _bikeSvc.Get(id);

            Assert.True(result);
            Assert.Null(bikeAfter);
        }
    }
}