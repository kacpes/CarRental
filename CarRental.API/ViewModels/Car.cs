using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.ViewModels
{
    public class Car
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }

        public double BasePrice { get; set; }
        public double KilometerPrice { get; set; }

        public string CarCategory { get; set; }
    }
}
