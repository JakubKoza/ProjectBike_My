using ProjectBike.Abstractions;
using ProjectBike.ServiceAbstractions;
using ProjectBike.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.Services
{
    public class BikeService : IBikeService
    {
        private readonly IBikeRepository _bikes;
        private readonly IUnitOfWork _uow;

        public BikeService(IBikeRepository bikes, IUnitOfWork uow)
        {
            _bikes = bikes;
            _uow = uow;
        }
        public int CreateBike(string brand, string model, string type, int warehouseId)
        {
            var bike = new Bike
            {
                Brand = brand,
                Model = model,
                Type = type,
                WarehouseId = warehouseId,
                IsAvailable = true
            };

            _bikes.Add(bike);
            _uow.SaveChanges();

            return bike.Id;
        }

        public Bike? Get(int id) => _bikes.Get(id);

        public IReadOnlyList<Bike> GetAll() => _bikes.Query().ToList();

        public IReadOnlyList<Bike> GetAvailableBikes() =>
            _bikes.Query().Where(b => b.IsAvailable).ToList();

        public IReadOnlyList<Bike> Search(string query)
        {
            string normalizedQuery = query.ToLower();

            return _bikes.Query()
                .Where(b => b.Brand.ToLower().Contains(normalizedQuery) ||
                            b.Model.ToLower().Contains(normalizedQuery))
                .ToList();
        }

        public bool UpdateBike(int id, string brand, string model, string type, int warehouseId)
        {
            var bike = _uow.Bikes.Get(id);
            if (bike == null) return false; 

            bike.Brand = brand;
            bike.Model = model;
            bike.Type = type;
            bike.WarehouseId = warehouseId;
            _uow.SaveChanges();

            return true; 
        }
        public bool UpdateBikeStatus(int id, bool isAvailable)
        {
            var bike = _uow.Bikes.Get(id);
            if (bike == null) return false;
            bike.IsAvailable = isAvailable;
            _uow.SaveChanges();
            return true;
        }

        public bool DeleteBike(int bikeId)
        {
            var bike = _bikes.Get(bikeId);
            if (bike == null) return false;

            _bikes.Remove(bike);
            return _uow.SaveChanges() > 0;
        }

        
    }
}
