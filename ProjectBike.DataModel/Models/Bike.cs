using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.DataModel.Models
{
    public class Bike // Rowery
    {
        private int _id;
        private string _brand = string.Empty;
        private string _model = string.Empty;
        private string _type = string.Empty;
        private bool _isAvailable;
        private int _warehouseId;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { _isAvailable = value; }
        }
        public int WarehouseId
        {
            get { return _warehouseId; }
            set { _warehouseId = value; }
        }

        //public Warehouse Warehouse { get; set; } // relacja z garażem
        //public List<BikePart> BikeParts { get; set; } = new(); // Kompozycja. Rower ma swoje części
    }
}
