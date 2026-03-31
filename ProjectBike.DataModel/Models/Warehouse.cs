using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.DataModel.Models
{
    public class Warehouse // klasa garażu z rowerami itp
    {
        private int _id;
        private string _name = string.Empty;
        private string _location = string.Empty;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        //public List<Bike> Bikes { get; set; } = new(); // Agregacja – magazyn zawiera rowery i części
        //public List<Part> Parts { get; set; } = new();
    }
}
