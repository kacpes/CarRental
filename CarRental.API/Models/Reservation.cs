using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string BookingNumber { get; set; }


        public int? CarId { get; set; }
        [ForeignKey("CarId")]
        public Car ChosenCar { get; set; }

        public DateTime Started { get; set; }
        public DateTime Released { get; set; }

        public bool RentStarted { get; set; }

        //We can reserve car when other reservations are made
        //We cannot expect milage untill vehicle will be ready to this specyfic rental
        public int? MilageWhenStarted { get; set; }
        public int? MilageWhenReturned { get; set; }


        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
