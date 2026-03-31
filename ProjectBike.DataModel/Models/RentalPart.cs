using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.DataModel.Models
{
    public class RentalPart
    {
        private int _id;
        private int _rentalId;
        private int _partId;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int RentalId
        {
            get { return _rentalId; }
            set { _rentalId = value; }
        }
        public int PartId
        {
            get { return _partId; }
            set { _partId = value; }
        }
        // Relacje
        //public Rental Rental { get; set; }
        //public Part Part { get; set; }
    }
}
