using ProjectBike.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.ServiceAbstractions
{
    public interface IEmployeeService 
    {
        int CreateEmployee(string firstName, string lastName, int age, string position);
        Employee? Get(int id);
        IReadOnlyList<Employee> GetAll();
        IReadOnlyList<Employee> GetByPosition(string position);
        bool UpdateEmployeePosition(int employeeId, string newPosition);
        bool DeleteEmployee(int employeeId);
    }
}
