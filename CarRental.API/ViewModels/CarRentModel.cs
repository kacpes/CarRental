using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.ViewModels
{
    public class CarRentModel
    {

        public CarReturnModel CarReturn(DateTime endTime, int currentMilage)
        {
            return new CarReturnModel()
            {
                BookingNumber = this.BookingNumber,
                CustomerBirthDate = this.CustomerBirthDate,
                RentedCar = this.RentedCar,
                StartRent = this.StartRent,
                StartKilometerCount = this.StartKilometerCount,
                EndKilometerCount = currentMilage,
                EndRent = endTime
            };
        }

        public string BookingNumber { get; set; }
        public DateTime CustomerBirthDate { get; set; }

        public Car RentedCar { get; set; }
        public int? customerId { get; set; }

        public DateTime? StartRent { get; set; }
        public int StartKilometerCount { get; set; }
        public virtual DateTime? EndRent { get { return null; } set { } }
        public virtual int EndKilometerCount { get { return 0; } set { } }
        public virtual bool CarIsRented
        { 
            get
            {
                return (StartRent != null);
            } 
        }

        public virtual bool ClientCanBeBilled
        {
            get
            {
                return false;
            }
        }
    }
}
