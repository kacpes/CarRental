using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }

        public double BasePrice { get; set; }
        public double KilometerPrice { get; set; }

        public int? CarCategoryId { get; set; }
        [ForeignKey("CarCategoryId")]
        public CarCategory CarCategory { get; set; }
    }
}
