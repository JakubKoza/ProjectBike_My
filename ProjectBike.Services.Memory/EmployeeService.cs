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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeesRepository _employees;
        private readonly IUnitOfWork _uow;

        public EmployeeService(IEmployeesRepository employees, IUnitOfWork uow)
        {
            _employees = employees;
            _uow = uow;
        }

        public int CreateEmployee(string firstName, string lastName, int age, string position)
        {
            var employee = new Employee
            {
                Firstname = firstName,
                Lastname = lastName,
                Age = age,
                Position = position,
            };

            _employees.Add(employee);
            _uow.SaveChanges();

            return employee.Id;
        }

        public Employee? Get(int id) => _employees.Get(id);

        public IReadOnlyList<Employee> GetAll() => _employees.Query().ToList();

        public IReadOnlyList<Employee> GetByPosition(string position) =>
            _employees.Query().Where(e => e.Position == position).ToList();

        public bool UpdateEmployeePosition(int employeeId, string newPosition)
        {
            var employee = _employees.Get(employeeId);
            if (employee == null) return false;

            employee.Position = newPosition;
            return _uow.SaveChanges() > 0;
        }

        public bool DeleteEmployee(int employeeId)
        {
            var employee = _employees.Get(employeeId);
            if (employee == null) return false;

            _employees.Remove(employee);
            return _uow.SaveChanges() > 0;
        }
    }
}
