using ProjectBike.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.ServiceAbstractions
{
    public interface IBikeService
    {
        int CreateBike(string brand, string model, string type, int warehouseId);
        Bike? Get(int id);
        IReadOnlyList<Bike> GetAll();
        IReadOnlyList<Bike> GetAvailableBikes();

        IReadOnlyList<Bike> Search(string query);

        bool UpdateBike(int id, string brand, string model, string type, int warehouseId);
        bool UpdateBikeStatus(int id, bool isAvailable);

        bool DeleteBike(int bikeId);
    }
}