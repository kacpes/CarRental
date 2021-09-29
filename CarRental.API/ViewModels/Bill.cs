using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.ViewModels
{
    public class Bill
    {
        public int KilometersDriven { get; set; }
        public int NumberOfDays { get; set; }

        public double PriceToPay { get; set; }
    }
}
