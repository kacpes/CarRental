using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Models
{
    public class Billing
    {
        public int BillingId { get; set; }

        public int KilometersDriven { get; set; }
        public int NumberOfDays { get; set; }

        public double PriceToPay { get; set; }

        public int? ReservationId { get; set; }
        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }
    }
}
