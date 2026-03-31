using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBike.DataModel.Models;

namespace ProjectBike.Abstractions
{
    public interface IEmployeesRepository
    {
        IQueryable<Employee> Query();
        Employee? Get(int id);
        void Add(Employee entity);
        void Remove(Employee entity);
    }
}
