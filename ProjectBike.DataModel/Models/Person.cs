using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.DataModel.Models
{
    public class Person //Klasa bazowa dla clients i Employess
    {
        private int _id;
        private string _firstname = string.Empty;
        private string _lastname = string.Empty;
        private int _age;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
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
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
    }    
}