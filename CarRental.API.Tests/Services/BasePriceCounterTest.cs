using System;
using Xunit;
using CarRental.API.Services.Interfaces;
using CarRental.API.Exceptions;

namespace CarRental.API.Services
{
    public class BasePriceCounterTest
    {
        IPriceCounter _testObject { get; set; }

        public BasePriceCounterTest()
        {
            _testObject = new BasePriceCounter();
        }

        [Fact]
        public async void CalculatePrice_Calculating_with_value_zero()
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(10),milage + 100)
            {
                StartRent = startDate,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = 0
                }
            };

            double value = _testObject.CalculatePrice(car);

            Assert.Equal(0, value);
        }

        [Fact]
        public async void CalculatePrice_Throws_error_with_negative_value()
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(10), milage + 100)
            {
                StartRent = startDate,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = -1
                }
            };

            Assert.Throws<IncorrectValueException>(() =>
            {
                double value = _testObject.CalculatePrice(car);
            });
        }

        [Fact]
        public async void CalculatePrice_Calculating_correct_value_ten_days()
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(10), milage + 100)
            {
                StartRent = startDate,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = 100
                }
            };

            double value = _testObject.CalculatePrice(car);

            Assert.Equal(1000, value);
        }

        [Fact]
        public async void CalculatePrice_Calculating_correct_value_one_hundred_days() //this will check month change
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(100), milage + 100)
            {
                StartRent = startDate,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = 100
                }
            };

            double value = _testObject.CalculatePrice(car);

            Assert.Equal(10000, value);
        }


        [Fact]
        public async void CalculatePrice_Calculating_correct_value_one_thousand_days() //this will check year change
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(1000), milage + 100)
            {
                StartRent = startDate,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = 100
                }
            };

            double value = _testObject.CalculatePrice(car);

            Assert.Equal(100000, value);
        }

        [Fact]
        public async void CalculatePrice_Calculating_correct_value_ten_days_price_per_km_does_not_change_a_thing() //this will check year change
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(10), milage + 100)
            {
                StartRent = startDate,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = 100,
                    KilometerPrice = 100
                },
            };

            double value = _testObject.CalculatePrice(car);

            Assert.Equal(1000, value);
        }
    }
}
