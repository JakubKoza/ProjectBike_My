using ProjectBike.ServiceAbstractions;
using System;
using Xunit;

namespace ProjectBike.Test
{
    public class ClientServiceTests : IClassFixture<InMemoryServicesFixture>
    {
        private readonly IClientService _clientSvc;
        public ClientServiceTests(InMemoryServicesFixture fx)
        {
            _clientSvc = fx.ClientService;
        }

        [Fact]
        public void CreateClient_ShouldAddClient()
        {
            var lastName = "Testowy" + Guid.NewGuid();
            var id = _clientSvc.CreateClient("Jan", lastName, 25, 180, 80, "Enduro");
            var client = _clientSvc.Get(id);

            Assert.NotEqual(0, id);
            Assert.NotNull(client);
            Assert.Equal(lastName, client!.Lastname);
        }

        [Fact]
        public void UpdateClientInfo_ShouldUpdateData()
        {
            // Metoda UpdateClient w Twoim serwisie obsługuje tylko zmianę Imienia i Nazwiska
            var id = _clientSvc.CreateClient("Anna", "Zmiana", 30, 160, 50, "City");

            _clientSvc.UpdateClient(id, "Anna", "NowaNazwa");

            var client = _clientSvc.Get(id);
            Assert.Equal("NowaNazwa", client!.Lastname);
        }

        [Fact]
        public void DeleteClient_ShouldRemoveClient()
        {
            var id = _clientSvc.CreateClient("Marek", "Usuwany", 40, 170, 70, "None");
            var result = _clientSvc.DeleteClient(id);
            var clientAfter = _clientSvc.Get(id);

            Assert.True(result);
            Assert.Null(clientAfter);
        }
    }
}