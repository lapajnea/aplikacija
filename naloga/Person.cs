using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace naloga
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int VatNumber { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
        public string IdAndNameAndSurname
        {
            get { return Id + ": "+ FirstName + " " + LastName; }
        }
        public string AllData 
        { 
            get
            {
                return $"{FirstName} {LastName}, {VatNumber}, {Adress}, {PostCode} {City}";
            }
        }
    }

   
}
