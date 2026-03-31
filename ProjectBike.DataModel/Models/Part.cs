using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.DataModel.Models
{
    public class Part // Części rowerowe (wszystkie)
    {
        private int _id;
        private string _name = string.Empty;
        private string _type = string.Empty;
        private int _warehouseId;

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
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public int WarehouseId
        {
            get { return _warehouseId; }
            set { _warehouseId = value; }
        }

        //public Warehouse Warehouse { get; set; } Relacja z garażem

    }
}
