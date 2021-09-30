using CarRental.API.Exceptions;
using CarRental.API.Helpers;
using CarRental.API.Services.Interfaces;
using CarRental.API.UnitOfWork.Interfaces;
using CarRental.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Services
{
    public class VehicleRental : IVehicleRental
    {
        private readonly IVehicleRentalUnitOfWork _vehicleUnitOfWork;

        public VehicleRental(IVehicleRentalUnitOfWork vehicleUnitOfWork)
        {
            _vehicleUnitOfWork = vehicleUnitOfWork;
        }

        // This method needs to be devided because it breakes single responsibility 
        public async Task<Bill> ReturnVehicle(CarReturnModel carReturnModel)
        {
            var reservation = await _vehicleUnitOfWork.ReservationTable.GetByBookingNumber(carReturnModel.BookingNumber);
            if (reservation == null || !reservation.RentStarted)
                throw new Exception("User is trying to return vehicle, by booking number that does not exists");

            IPriceCounter priceCounter = new ObjectFromStringFactory<IPriceCounter>().GetInstance((reservation.ChosenCar.CarCategory.CarType ?? "Base") + "PriceCounter", "CarRental.API.Services");

            reservation.Released = carReturnModel.EndRent ?? throw new IncorrectValueException("User tries to return car without enddate");
            reservation.MilageWhenReturned = carReturnModel.EndKilometerCount;
            reservation = await _vehicleUnitOfWork.ReservationTable.Update(reservation);
            var billing = await _vehicleUnitOfWork.BillingTable.Add(new Models.Billing() { 
                KilometersDriven = reservation.MilageWhenReturned ?? 0 - reservation.MilageWhenStarted ?? 0,
                NumberOfDays = (reservation.Released - reservation.Started).Days,
                ReservationId = reservation.ReservationId,
                PriceToPay = priceCounter.CalculatePrice(carReturnModel)
            });


            //I need to use mapper if it is not seen here i've probably done it after code was cloned
            return await Task.FromResult(new Bill()
            {
                KilometersDriven = billing.KilometersDriven,
                NumberOfDays = billing.NumberOfDays,
                PriceToPay = billing.PriceToPay
            });
        }

        // This method needs to be devided because it breakes single responsibility 
        public async Task<CarRentModel> RentVehicle(CarRentModel carRentModel)
        {

            var reservation =  await _vehicleUnitOfWork.ReservationTable.GetByBookingNumber(carRentModel.BookingNumber);
            if (reservation == null)
            {
                reservation = await _vehicleUnitOfWork.ReservationTable.Add(new Models.Reservation()
                {
                    // In both comments below, there could be easy fix to fir it strictly to requirements
                    // just add this fields to reservation table
                    // but in relational table data is already there
                    BookingNumber = carRentModel.BookingNumber,
                    CarId = carRentModel.RentedCar.CarId, //when car exists it is assigned to category there should be no need to save category
                    CustomerId = carRentModel.customerId, //when customer exists it has filled birthdate there should be no need to save it
                });
            }

            if (!await _vehicleUnitOfWork.ReservationTable.CheckIfDateIsAvaliableFor(reservation))
                return await Task.FromResult(new CarRentModel()
                {
                    BookingNumber = "Car is booked in this time"
                });

            reservation.MilageWhenStarted = carRentModel.StartKilometerCount;
            reservation.Started = carRentModel.StartRent ?? throw new IncorrectValueException();
            reservation.RentStarted = true;
            reservation = await _vehicleUnitOfWork.ReservationTable.Update(reservation);

            reservation = await _vehicleUnitOfWork.ReservationTable.GetByBookingNumber(reservation.BookingNumber);

            //I need to use mapper if it is not seen here i've probably done it after code was cloned
            return await Task.FromResult(new CarRentModel()
            {
                BookingNumber = reservation.BookingNumber,
                CustomerBirthDate = reservation.Customer.BirthDate,
                customerId = reservation.CustomerId,
                StartKilometerCount = reservation.MilageWhenStarted ?? 0,
                StartRent = reservation.Started,
                RentedCar = new Car()
                {
                    BasePrice = reservation.ChosenCar.BasePrice,
                    KilometerPrice = reservation.ChosenCar.KilometerPrice,
                    Color = reservation.ChosenCar.Color,
                    Make = reservation.ChosenCar.Make,
                    Model = reservation.ChosenCar.Model
                }
            });
        }
    }
}
