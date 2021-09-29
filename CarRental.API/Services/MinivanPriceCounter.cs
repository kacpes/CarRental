using CarRental.API.Exceptions;
using CarRental.API.Services.Interfaces;
using CarRental.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Services
{
    public class MinivanPriceCounter : IPriceCounter
    {
        public double CalculatePrice(CarRentModel car)
        {
            int daysPassed = car.EndRent.Value.Subtract(car.StartRent.Value).Days;
            int kmDriven = car.EndKilometerCount - car.StartKilometerCount;
            if (car.RentedCar.BasePrice < 0 || daysPassed < 0 || kmDriven < 0 || car.RentedCar.KilometerPrice < 0)
                throw new IncorrectValueException();

            double priceForTime = daysPassed * car.RentedCar.BasePrice * 1.7;
            double priceForDistance = kmDriven * car.RentedCar.KilometerPrice * 1.5;

            return priceForDistance + priceForTime;

        }
    }
}
