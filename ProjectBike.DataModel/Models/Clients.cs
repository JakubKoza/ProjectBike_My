using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.DataModel.Models
{
    public class Client : Person // Klasa dziedziczy po Person
    {
        private double _height;
        private double _weight;
        private string _riderPreference = string.Empty;

        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public double Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        public string RiderPreference
        {
            get { return _riderPreference; }
            set { _riderPreference = value; }
        }

        //public List<Rental> Rentals { get; set; } = new(); // asocjacja z rentals
    }
}