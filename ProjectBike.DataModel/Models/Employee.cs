using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.DataModel.Models
{
    public class Employee : Person // Klasa dziedziczy po Person
    {

        private string _position = string.Empty;
 
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }
    }
}
