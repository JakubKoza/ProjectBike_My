using ProjectBike.DataModel;
using ProjectBike.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.Abstractions;

public interface IRentalRepository
{
    IQueryable<Rental> Query();
    Rental? Get(int id);
    void Add(Rental entity);
    void Remove(Rental entity);
}
