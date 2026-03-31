using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.DataModel.Models
{
    public class BikePart // Części do roweru
    {
        private int _id;
        private int _bikeId;
        private int _partId;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int BikeId
        {
            get { return _bikeId; }
            set { _bikeId = value; }
        }
        public int PartId
        {
            get { return _partId; }
            set { _partId = value; }
        }
        //public Bike Bike { get; set; } relacje 
        //public Part Part { get; set; }
    }
}
