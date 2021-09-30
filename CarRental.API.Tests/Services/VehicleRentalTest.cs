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
            _reservationTable.Setup(x => x.Update(It.IsAny<Reservation>()))
                .Returns<Reservation>(x => Task.FromResult(x));
            _reservationTable.Setup(x => x.GetByBookingNumber(It.Is<string>(x => x == "A00013"))).Returns(Task.FromResult(new Reservation() {
                 BookingNumber = "A00013",
                 CarId = 1,
                 ChosenCar = new Models.Car(),
                 Customer = new Models.Customer()
            }));
            _reservationTable.Setup(x => x.CheckIfDateIsAvaliableFor(It.IsAny<Reservation>())).Returns<Reservation>(x =>
            {
                if (x.CarId != 1)
                    return Task.FromResult(true);

                return Task.FromResult(!x.Started.Between(timeReserved, timeReserved.AddDays(10)));
            });

            CarRentModel testModel = new CarRentModel()
            {
                BookingNumber = "A00013",
                StartRent = DateTime.Parse("2020-08-17"),
                EndRent = DateTime.Parse("2020-08-27"),
            };

            var returnedModel = await _serviceTested.RentVehicle(testModel);

            Assert.Equal(testModel.BookingNumber, returnedModel.BookingNumber);
        }

        [Fact]
        public async void RentVehicle_Car_will_bot_be_reserved_time_period_is_not_avaliable() 
        {
            DateTime timeReserved = DateTime.Now.AddDays(10);
            _unitOfWork.Setup(x => x.BillingTable).Returns(_billingTable.Object);
            _unitOfWork.Setup(x => x.ReservationTable).Returns(_reservationTable.Object);

            _reservationTable.Setup(x => x.Add(It.IsAny<Reservation>())).Returns<Reservation>(x => Task.FromResult(x));
            _reservationTable.Setup(x => x.Update(It.IsAny<Reservation>()))
                .Returns<Reservation>(x => Task.FromResult(x));
            _reservationTable.Setup(x => x.GetByBookingNumber(It.Is<string>(x => x == "A00013"))).Returns(Task.FromResult(new Reservation()
            {
                BookingNumber = "A00013",
                CarId = 1,
                ChosenCar = new Models.Car(),
                Customer = new Models.Customer()
            }));
            _reservationTable.Setup(x => x.CheckIfDateIsAvaliableFor(It.IsAny<Reservation>())).Returns<Reservation>(x =>
            {
                if (x.CarId != 1)
                    return Task.FromResult(true);

                return Task.FromResult(x.Started.Between(timeReserved, timeReserved.AddDays(10)));
            });

            CarRentModel testModel = new CarRentModel()
            {
                BookingNumber = "A00013",
                StartRent = timeReserved.AddDays(-1),
                EndRent = timeReserved.AddDays(11),
            };

            var returnedModel = await _serviceTested.RentVehicle(testModel);

            Assert.NotEqual(testModel.BookingNumber, returnedModel.BookingNumber);
        }

        [Fact]
        public async void RentVehicle_Car_will_bot_be_reserved_time_period_is_partially_avaliable()
        {
            DateTime timeReserved = DateTime.Now.AddDays(10);
            _unitOfWork.Setup(x => x.BillingTable).Returns(_billingTable.Object);
            _unitOfWork.Setup(x => x.ReservationTable).Returns(_reservationTable.Object);

            _reservationTable.Setup(x => x.Add(It.IsAny<Reservation>())).Returns<Reservation>(x => Task.FromResult(x));
            _reservationTable.Setup(x => x.Update(It.IsAny<Reservation>()))
                .Returns<Reservation>(x => Task.FromResult(x));
            _reservationTable.Setup(x => x.GetByBookingNumber(It.Is<string>(x => x == "A00013"))).Returns(Task.FromResult(new Reservation()
            {
                BookingNumber = "A00013",
                CarId = 1,
                ChosenCar = new Models.Car(),
                Customer = new Models.Customer()
            }));
            _reservationTable.Setup(x => x.CheckIfDateIsAvaliableFor(It.IsAny<Reservation>())).Returns<Reservation>(x =>
            {
                if (x.CarId != 1)
                    return Task.FromResult(true);

                return Task.FromResult(!x.Started.Between(timeReserved, timeReserved.AddDays(10)));
            });

            CarRentModel testModel = new CarRentModel()
            {
                BookingNumber = "A00013",
                StartRent = timeReserved.AddDays(-1),
                EndRent = timeReserved.AddDays(3),
            };

            var returnedModel = await _serviceTested.RentVehicle(testModel);

            Assert.NotEqual(testModel.BookingNumber, returnedModel.BookingNumber);
        }

        [Fact]
        public async void ReturnVehicle_Car_will_be_returned_billing_will_be_saved() 
        {
            DateTime timeReserved = DateTime.Now.AddDays(10);
            _unitOfWork.Setup(x => x.BillingTable).Returns(_billingTable.Object);
            _unitOfWork.Setup(x => x.ReservationTable).Returns(_reservationTable.Object);

            _reservationTable.Setup(x => x.Update(It.IsAny<Reservation>()))
                .Returns<Reservation>(x => Task.FromResult(x));
            _reservationTable.Setup(x => x.GetByBookingNumber(It.Is<string>(x => x == "A00013"))).Returns(Task.FromResult(new Reservation()
            {
                BookingNumber = "A00013",
                CarId = 1,
                RentStarted = true,
                ChosenCar = new Models.Car() { CarCategory = new CarCategory() { CarType = "Base" } },
                Customer = new Models.Customer()
            }));
            _reservationTable.Setup(x => x.CheckIfDateIsAvaliableFor(It.IsAny<Reservation>())).Returns<Reservation>(x =>
            {
                if (x.CarId != 1)
                    return Task.FromResult(true);

                return Task.FromResult(!x.Started.Between(timeReserved, timeReserved.AddDays(10)));
            });
            _billingTable.Setup(x => x.Add(It.IsAny<Billing>()))
                .Returns<Billing>(x => Task.FromResult(x));

            CarReturnModel testModel = new CarReturnModel()
            {
                BookingNumber = "A00013",
                StartRent = timeReserved.AddDays(-1),
                EndRent = timeReserved.AddDays(3),
                RentedCar = new ViewModels.Car() { BasePrice = 100 }
            };

            var returnedModel = await _serviceTested.ReturnVehicle(testModel);

            Assert.NotNull(returnedModel);
        }

        [Fact]
        public async void ReturnVehicle_Car_cannot_be_returned_booking_does_not_exists() 
        {

            DateTime timeReserved = DateTime.Now.AddDays(10);
            _unitOfWork.Setup(x => x.BillingTable).Returns(_billingTable.Object);
            _unitOfWork.Setup(x => x.ReservationTable).Returns(_reservationTable.Object);


           _reservationTable.Setup(x => x.GetByBookingNumber(It.Is<string>(x => x == "A00013"))).Returns(Task.FromResult(new Reservation()
            {
                BookingNumber = "A00013",
                CarId = 1,
                RentStarted = true,
                ChosenCar = new Models.Car() { CarCategory = new CarCategory() { CarType = "Base" } },
                Customer = new Models.Customer()
            }));
            _reservationTable.Setup(x => x.CheckIfDateIsAvaliableFor(It.IsAny<Reservation>())).Returns<Reservation>(x =>
            {
                if (x.CarId != 1)
                    return Task.FromResult(true);

                return Task.FromResult(!x.Started.Between(timeReserved, timeReserved.AddDays(10)));
            });

            CarReturnModel testModel = new CarReturnModel()
            {
                BookingNumber = "A00000",
                StartRent = timeReserved.AddDays(-1),
                EndRent = timeReserved.AddDays(3),
                RentedCar = new ViewModels.Car() { BasePrice = 100 }
            };

            await Assert.ThrowsAsync<Exception>(async () => { await _serviceTested.ReturnVehicle(testModel); });
        }
    }
}
