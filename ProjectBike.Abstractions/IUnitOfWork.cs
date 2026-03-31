using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.Abstractions
{
    public interface IUnitOfWork
    {
        IBikeRepository Bikes { get; }
        IClientRepository Clients { get; }
        IRentalRepository Rentals { get; }
        IEmployeesRepository Employees { get; }
        int SaveChanges();

    }
}
