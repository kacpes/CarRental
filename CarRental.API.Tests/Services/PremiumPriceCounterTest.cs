using System;
using Xunit;
using CarRental.API.Services.Interfaces;
using CarRental.API.Exceptions;

namespace CarRental.API.Services
{
    public class PremiumPriceCounterTest
    {
        IPriceCounter _testObject { get; set; }

        public PremiumPriceCounterTest()
        {
            _testObject = new PremiumPriceCounter();
        }

        [Fact]
        public async void CalculatePrice_Calculating_with_value_zero()
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(10), milage + 100)
            {
                StartRent = startDate,
                StartKilometerCount = milage,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = 0,
                    KilometerPrice = 0
                }
            };

            double value = _testObject.CalculatePrice(car);

            Assert.Equal(0, value);
        }

        [Fact]
        public async void CalculatePrice_Throws_error_with_negative_value_base_price()
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(10), milage + 100)
            {
                StartRent = startDate,
                StartKilometerCount = milage,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = -1,
                    KilometerPrice = 100
                }
            };

            Assert.Throws<IncorrectValueException>(() =>
            {
                double value = _testObject.CalculatePrice(car);
            });
        }

        [Fact]
        public async void CalculatePrice_Throws_error_with_negative_value_kilometer_price()
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(10), milage + 100)
            {
                StartRent = startDate,
                StartKilometerCount = milage,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = 100,
                    KilometerPrice = -1
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
                StartKilometerCount = milage,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = 100,
                    KilometerPrice = 0
                }
            };

            double value = _testObject.CalculatePrice(car);

            Assert.Equal(1200, value);
        }

        [Fact]
        public async void CalculatePrice_Calculating_correct_value_one_hundred_days() //this will check month change
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(100), milage + 100)
            {
                StartRent = startDate,
                StartKilometerCount = milage,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = 100,
                    KilometerPrice = 0
                }
            };

            double value = _testObject.CalculatePrice(car);

            Assert.Equal(12000, value);
        }


        [Fact]
        public async void CalculatePrice_Calculating_correct_value_one_thousand_days() //this will check year change
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(1000), milage + 100)
            {
                StartRent = startDate,
                StartKilometerCount = milage,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = 100,
                    KilometerPrice = 0
                }
            };

            double value = _testObject.CalculatePrice(car);

            Assert.Equal(120000, value);
        }

        [Fact]
        public async void CalculatePrice_Calculating_correct_value_kilometer_price_above_zero() //this will check year change
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(10), milage + 100)
            {
                StartRent = startDate,
                StartKilometerCount = milage,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = 0,
                    KilometerPrice = 100
                }
            };

            double value = _testObject.CalculatePrice(car);

            Assert.Equal(10000, value);
        }

        [Fact]
        public async void CalculatePrice_Calculating_correct_both_values() //this will check year change
        {
            DateTime startDate = DateTime.Today;
            int milage = 90000;
            ViewModels.CarRentModel car = new ViewModels.CarReturnModel(startDate.AddDays(10), milage + 100)
            {
                StartRent = startDate,
                StartKilometerCount = milage,
                RentedCar = new ViewModels.Car()
                {
                    BasePrice = 100,
                    KilometerPrice = 100
                }
            };

            double value = _testObject.CalculatePrice(car);

            Assert.Equal(11200, value);
        }
    }
}
