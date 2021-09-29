using CarRental.API.DAL.SQLManagment;
using CarRental.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CarRental.API.DAL
{
    public class ReservationTableTest
    {
        private DbContextOptionsBuilder<SQLContext> optionsBuilder
            = new DbContextOptionsBuilder<SQLContext>();

        [Fact]
        public async void GetById_Returns_proper_value_when_proper_id_is_given()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            ReservationTable bt = new ReservationTable(context);

            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation(){ ReservationId = 4, BookingNumber = "A0001", CarId = 7, CustomerId = 10, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 5, BookingNumber = "A0003", CarId = 8, CustomerId = 11, MilageWhenReturned = 900100, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 6, BookingNumber = "A0005", CarId = 9, CustomerId = 12, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(2)},
            };

            context.AddRange(reservations);
            context.SaveChanges();

            var reservation = await bt.GetById(4);

            Assert.NotNull(reservation);
        }

        [Fact]
        public async void GetById_Returns_nothing_id_over_range()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            ReservationTable bt = new ReservationTable(context);

            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation(){ ReservationId = 4, BookingNumber = "A0001", CarId = 7, CustomerId = 10, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 5, BookingNumber = "A0003", CarId = 8, CustomerId = 11, MilageWhenReturned = 900100, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 6, BookingNumber = "A0005", CarId = 9, CustomerId = 12, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(2)},
            };

            context.AddRange(reservations);
            context.SaveChanges();

            var reservation = await bt.GetById(9);

            Assert.Null(reservation);
        }

        [Fact]
        public async void GetById_Returns_nothing_id_negative()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            ReservationTable bt = new ReservationTable(context);

            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation(){ ReservationId = 4, BookingNumber = "A0001", CarId = 7, CustomerId = 10, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 5, BookingNumber = "A0003", CarId = 8, CustomerId = 11, MilageWhenReturned = 900100, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 6, BookingNumber = "A0005", CarId = 9, CustomerId = 12, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(2)},
            };

            context.AddRange(reservations);
            context.SaveChanges();

            var reservation = await bt.GetById(-1);

            Assert.Null(reservation);
        }

        [Fact]
        public async void GetByBookingNumber_Returns_proper_object_with_proper_value_given()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            ReservationTable bt = new ReservationTable(context);

            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation(){ ReservationId = 4, BookingNumber = "A0001", CarId = 7, CustomerId = 10, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 5, BookingNumber = "A0003", CarId = 8, CustomerId = 11, MilageWhenReturned = 900100, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 6, BookingNumber = "A0005", CarId = 9, CustomerId = 12, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(2)},
            };

            context.AddRange(reservations);
            context.SaveChanges();

            var billing = await bt.GetByBookingNumber("A0001");

            Assert.NotNull(billing);
        }

        [Fact]
        public async void GetByBookingNumber_Returns_nothing_when_booking_does_not_exists()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            ReservationTable bt = new ReservationTable(context);

            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation(){ ReservationId = 4, BookingNumber = "A0001", CarId = 7, CustomerId = 10, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 5, BookingNumber = "A0003", CarId = 8, CustomerId = 11, MilageWhenReturned = 900100, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 6, BookingNumber = "A0005", CarId = 9, CustomerId = 12, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(2)},
            };

            context.AddRange(reservations);
            context.SaveChanges();

            var billing = await bt.GetByBookingNumber("A0011");

            Assert.Null(billing);
        }

        [Fact]
        public async void GetByBookingNumber_Returns_nothing_when_booking_number_is_null()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            ReservationTable bt = new ReservationTable(context);

            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation(){ ReservationId = 4, BookingNumber = "A0001", CarId = 7, CustomerId = 10, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 5, BookingNumber = "A0003", CarId = 8, CustomerId = 11, MilageWhenReturned = 900100, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 6, BookingNumber = "A0005", CarId = 9, CustomerId = 12, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(2)},
            };

            context.AddRange(reservations);
            context.SaveChanges();

            var billing = await bt.GetByBookingNumber(null);

            Assert.Null(billing);
        }

        [Fact]
        public async void GetAllByCarId_Returns_proper_object_with_proper_value_given()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            ReservationTable bt = new ReservationTable(context);

            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation(){ ReservationId = 4, BookingNumber = "A0001", CarId = 7, CustomerId = 10, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 5, BookingNumber = "A0003", CarId = 8, CustomerId = 11, MilageWhenReturned = 900100, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 6, BookingNumber = "A0005", CarId = 9, CustomerId = 12, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(2)},
            };

            context.AddRange(reservations);
            context.SaveChanges();

            var billing = await bt.GetAllByCarId(7);

            Assert.True(billing.Any());
        }

        [Fact]
        public async void GetAllByCarId_Returns_nothing_when_car_does_not_exists()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            ReservationTable bt = new ReservationTable(context);

            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation(){ ReservationId = 4, BookingNumber = "A0001", CarId = 7, CustomerId = 10, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 5, BookingNumber = "A0003", CarId = 8, CustomerId = 11, MilageWhenReturned = 900100, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 6, BookingNumber = "A0005", CarId = 9, CustomerId = 12, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(2)},
            };

            context.AddRange(reservations);
            context.SaveChanges();

            var billing = await bt.GetAllByCarId(1);

            Assert.False(billing.Any());
        }

        [Fact]
        public async void GetAllByCarId_Returns_nothing_id_negative()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            ReservationTable bt = new ReservationTable(context);

            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation(){ ReservationId = 4, BookingNumber = "A0001", CarId = 7, CustomerId = 10, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 5, BookingNumber = "A0003", CarId = 8, CustomerId = 11, MilageWhenReturned = 900100, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 6, BookingNumber = "A0005", CarId = 9, CustomerId = 12, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(2)},
            };

            context.AddRange(reservations);
            context.SaveChanges();

            var billing = await bt.GetAllByCarId(-1);

            Assert.False(billing.Any());
        }

        [Fact]
        public async void CheckIfDateIsAvaliableFor_()
        {

        }
    }
}
