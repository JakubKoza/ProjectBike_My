using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.DataModel.Models
{
    public class Rental // klasa służy do zarządzania wypożyczaniem aut
    {
        private int _id;
        private DateTime _startDate;
        private DateTime _endDate;
        private double _totalPrice;
        private int _clientId;
        private int _bikeId;
        private int _employeeId;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        public double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        public int ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }
        public int BikeId
        {
            get { return _bikeId; }
            set { _bikeId = value; }
        }
        public int EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }
        }
        // Relacje
        //public Client Client { get; set; }
        //public Bike Bike { get; set; }
        //public Employee Employee { get; set; }
        //public List<RentalPart> RentalParts { get; set; } = new();// Kompozycja – wypożyczenie zawiera konkretne części
    }
}
