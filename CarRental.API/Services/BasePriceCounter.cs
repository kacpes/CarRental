using CarRental.API.Exceptions;
using CarRental.API.Services.Interfaces;
using CarRental.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Services
{
    public class BasePriceCounter : IPriceCounter
    {
        public double CalculatePrice(CarRentModel car)
        {
            int daysPassed = car.EndRent.Value.Subtract(car.StartRent.Value).Days;
            if (car.RentedCar.BasePrice < 0 || daysPassed < 0)
                throw new IncorrectValueException();

            return car.RentedCar.BasePrice * daysPassed;
        }
    }
}
