using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.DataModel.Models
{
    public class Person //Klasa bazowa dla clients i Employess
    {
        public int Id { get; set; }
        public int Age { get; set; }

        private string _firstname = string.Empty;
        private string _lastname = string.Empty;
        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }
        public string Lastname
        {
            get { return _lastname; }
            set { _lastname = value; }
        }
    }    
}