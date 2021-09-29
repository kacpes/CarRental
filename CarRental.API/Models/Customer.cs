using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastNAme { get; set; }
        public string IdNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
