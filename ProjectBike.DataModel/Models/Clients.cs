using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.DataModel.Models
{
    public class Client : Person // Klasa dziedziczy po Person
    {
        // tutaj zastosowałem właściwości automatyczne
        public string Street { get; set; } // Tysiąclecie
        public string HouseNumber { get; set; } // 32A
        public string City { get; set; } // Częstochowa
        public string State { get; set; } // Śląsk
        public string ZipCode { get; set; } // 12-12
        public string Country { get; set; } // Polska
        public double Height { get; set; } // 180cm
        public double Weight { get; set; }  //70kg 
    }
}