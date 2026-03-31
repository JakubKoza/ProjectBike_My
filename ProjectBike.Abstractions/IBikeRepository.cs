using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBike.DataModel.Models;

namespace ProjectBike.Abstractions
{
    public interface IBikeRepository
    {
        IQueryable<Bike> Query();
        Bike? Get(int id);
        void Add(Bike entity);
        void Remove(Bike entity);
    }
}
