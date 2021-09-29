using CarRental.API.DAL;
using CarRental.API.DAL.Interfaces;
using CarRental.API.Helpers;
using CarRental.API.Models;
using CarRental.API.Services.Interfaces;
using CarRental.API.UnitOfWork.Interfaces;
using CarRental.API.ViewModels;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.API.Services
{
    public class MockUpPriceCounter : IPriceCounter
    {
        public double CalculatePrice(CarRentModel car)
            => 512;
    }


    public class VehicleRentalTest
    {
        private Mock<IBillingTable> _billingTable { get; set; }
        private Mock<IReservationTable> _reservationTable{ get; set; }

        private Mock<IVehicleRentalUnitOfWork> _unitOfWork { get; set; }

        private IVehicleRental _serviceTested { get; set; }

        public VehicleRentalTest()
        {
            _unitOfWork = new Mock<IVehicleRentalUnitOfWork>();
            _billingTable = new Mock<IBillingTable>();
            _reservationTable = new Mock<IReservationTable>();

            _serviceTested = new VehicleRental(_unitOfWork.Object);
        }

        [Fact]
        public async void RentVehicle_Car_will_be_reserved_time_period_is_avaliable_data_will_be_stored() 
        {
            DateTime timeReserved = DateTime.Now.AddDays(10);
            _unitOfWork.Setup(x => x.BillingTable).Returns(_billingTable.Object);
            _unitOfWork.Setup(x => x.ReservationTable).Returns(_reservationTable.Object);

            _reservationTable.Setup(x => x.Add(It.IsAny<Reservation>())).Returns<Reservation>(x => Task.FromResult(x));
            _reservationTable.Setup(x => x.GetByBookingNumber(It.Is<string>(x => x == "A00013"))).Returns(Task.FromResult(new Reservation() {
                 BookingNumber = "A00013",
                 CarId = 1,
            }));
            _reservationTable.Setup(x => x.CheckIfDateIsAvaliableFor(It.IsAny<Reservation>())).Returns<Reservation>(x =>
            {
                if (x.CarId != 1)
                    return Task.FromResult(true);

                return Task.FromResult(!x.Started.Between(timeReserved, timeReserved.AddDays(10)));
            });


        }

        [Fact]
        public async void RentVehicle_Car_will_bot_be_reserved_time_period_is_not_avaliable() { }

        [Fact]
        public async void RentVehicle_Car_will_bot_be_reserved_time_period_is_partially_avaliable() { }

        [Fact]
        public async void VehicleRental_Car_will_be_returned_billing_will_be_saved() { }

        [Fact]
        public async void VehicleRental_Car_cannot_be_returned_booking_does_not_exists() { }
    }
}
