using CarRental.API.DAL.SQLManagment;
using CarRental.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace CarRental.API.DAL
{
    public class BillingTableTest
    {

        private DbContextOptionsBuilder<SQLContext> optionsBuilder
            = new DbContextOptionsBuilder<SQLContext>();

        [Fact]
        public async void GetById_Returns_proper_object_when_id_is_given()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            BillingTable bt = new BillingTable(context);

            List<Billing> billings = new List<Billing>()
            {
                new Billing(){ BillingId = 1, KilometersDriven = 10, NumberOfDays = 1, PriceToPay = 100, ReservationId = 4 },
                new Billing(){ BillingId = 2, KilometersDriven = 100, NumberOfDays = 1, PriceToPay = 100, ReservationId = 5 },
                new Billing(){ BillingId = 3, KilometersDriven = 10, NumberOfDays = 2, PriceToPay = 200, ReservationId = 6 },
            };

            context.AddRange(billings);
            context.SaveChanges();

            var billing = await bt.GetById(1);

            Assert.NotNull(billing);
        }

        [Fact]
        public async void GetById_Returns_nothing_id_over_range_is_given()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            BillingTable bt = new BillingTable(context);

            List<Billing> billings = new List<Billing>()
            {
                new Billing(){ BillingId = 1, KilometersDriven = 10, NumberOfDays = 1, PriceToPay = 100, ReservationId = 4 },
                new Billing(){ BillingId = 2, KilometersDriven = 100, NumberOfDays = 1, PriceToPay = 100, ReservationId = 5 },
                new Billing(){ BillingId = 3, KilometersDriven = 10, NumberOfDays = 2, PriceToPay = 200, ReservationId = 6 },
            };

            context.AddRange(billings);
            context.SaveChanges();

            var billing = await bt.GetById(5);

            Assert.Null(billing);
        }

        [Fact]
        public async void GetById_Returns_nothing_id_negative_is_given()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            BillingTable bt = new BillingTable(context);

            List<Billing> billings = new List<Billing>()
            {
                new Billing(){ BillingId = 1, KilometersDriven = 10, NumberOfDays = 1, PriceToPay = 100, ReservationId = 4 },
                new Billing(){ BillingId = 2, KilometersDriven = 100, NumberOfDays = 1, PriceToPay = 100, ReservationId = 5 },
                new Billing(){ BillingId = 3, KilometersDriven = 10, NumberOfDays = 2, PriceToPay = 200, ReservationId = 6 },
            };

            context.AddRange(billings);
            context.SaveChanges();

            var billing = await bt.GetById(-1);

            Assert.Null(billing);
        }

        [Fact]
        public async void GetAllByReservationID_Returns_proper_value_when_existing_reservation_id_is_given()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            BillingTable bt = new BillingTable(context);

            List<Billing> billings = new List<Billing>()
            {
                new Billing(){ BillingId = 1, KilometersDriven = 10, NumberOfDays = 1, PriceToPay = 100, ReservationId = 4 },
                new Billing(){ BillingId = 2, KilometersDriven = 100, NumberOfDays = 1, PriceToPay = 100, ReservationId = 5 },
                new Billing(){ BillingId = 3, KilometersDriven = 10, NumberOfDays = 2, PriceToPay = 200, ReservationId = 6 },
            };

            context.AddRange(billings);
            context.SaveChanges();

            var billing = await bt.GetAllByReservationID(4);

            Assert.NotNull(billing);
        }

        [Fact]
        public async void GetAllByReservationID_Returns_nothing_id_over_range_is_given()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            BillingTable bt = new BillingTable(context);

            List<Billing> billings = new List<Billing>()
            {
                new Billing(){ BillingId = 1, KilometersDriven = 10, NumberOfDays = 1, PriceToPay = 100, ReservationId = 4 },
                new Billing(){ BillingId = 2, KilometersDriven = 100, NumberOfDays = 1, PriceToPay = 100, ReservationId = 5 },
                new Billing(){ BillingId = 3, KilometersDriven = 10, NumberOfDays = 2, PriceToPay = 200, ReservationId = 6 },
            };

            context.AddRange(billings);
            context.SaveChanges();

            var billing = await bt.GetAllByReservationID(7);

            Assert.Null(billing);
        }

        [Fact]
        public async void GetAllByReservationID_Returns_nothing_negative_id_is_given()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            BillingTable bt = new BillingTable(context);

            List<Billing> billings = new List<Billing>()
            {
                new Billing(){ BillingId = 1, KilometersDriven = 10, NumberOfDays = 1, PriceToPay = 100, ReservationId = 4 },
                new Billing(){ BillingId = 2, KilometersDriven = 100, NumberOfDays = 1, PriceToPay = 100, ReservationId = 5 },
                new Billing(){ BillingId = 3, KilometersDriven = 10, NumberOfDays = 2, PriceToPay = 200, ReservationId = 6 },
            };

            context.AddRange(billings);
            context.SaveChanges();

            var billing = await bt.GetAllByReservationID(-7);

            Assert.Null(billing);
        }

        [Fact]
        public async void GetByBookingNumber_Get_proper_value_when_existing_booking_number_is_given()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            BillingTable bt = new BillingTable(context);

            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation(){ ReservationId = 4, BookingNumber = "A0001", CarId = 7, CustomerId = 10, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 5, BookingNumber = "A0003", CarId = 8, CustomerId = 11, MilageWhenReturned = 900100, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 6, BookingNumber = "A0005", CarId = 9, CustomerId = 12, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(2)},
            };

            List<Billing> billings = new List<Billing>()
            {
                new Billing(){ BillingId = 1, KilometersDriven = 10, NumberOfDays = 1, PriceToPay = 100, ReservationId = 4 },
                new Billing(){ BillingId = 2, KilometersDriven = 100, NumberOfDays = 1, PriceToPay = 100, ReservationId = 5 },
                new Billing(){ BillingId = 3, KilometersDriven = 10, NumberOfDays = 2, PriceToPay = 200, ReservationId = 6 },
            };

            context.AddRange(reservations);
            context.AddRange(billings);
            context.SaveChanges();

            var billing = await bt.GetByBookingNumber("A0003");

            Assert.NotNull(billing);
        }

        [Fact]
        public async void GetByBookingNumber_Returns_nothing_when_booking_number_does_not_exists()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            BillingTable bt = new BillingTable(context);

            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation(){ ReservationId = 4, BookingNumber = "A0001", CarId = 7, CustomerId = 10, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 5, BookingNumber = "A0003", CarId = 8, CustomerId = 11, MilageWhenReturned = 900100, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 6, BookingNumber = "A0005", CarId = 9, CustomerId = 12, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(2)},
            };

            List<Billing> billings = new List<Billing>()
            {
                new Billing(){ BillingId = 1, KilometersDriven = 10, NumberOfDays = 1, PriceToPay = 100, ReservationId = 4 },
                new Billing(){ BillingId = 2, KilometersDriven = 100, NumberOfDays = 1, PriceToPay = 100, ReservationId = 5 },
                new Billing(){ BillingId = 3, KilometersDriven = 10, NumberOfDays = 2, PriceToPay = 200, ReservationId = 6 },
            };

            context.AddRange(reservations);
            context.AddRange(billings);
            context.SaveChanges();

            var billing = await bt.GetByBookingNumber("A0030");

            Assert.Null(billing);
        }

        [Fact]
        public async void GetByBookingNumber_Returns_nothing_when_booking_number_is_null()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            var context = new SQLContext(optionsBuilder.Options);
            BillingTable bt = new BillingTable(context);

            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation(){ ReservationId = 4, BookingNumber = "A0001", CarId = 7, CustomerId = 10, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 5, BookingNumber = "A0003", CarId = 8, CustomerId = 11, MilageWhenReturned = 900100, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(1)},
                new Reservation(){ ReservationId = 6, BookingNumber = "A0005", CarId = 9, CustomerId = 12, MilageWhenReturned = 900010, MilageWhenStarted = 900000, Started = DateTime.Now, Released = DateTime.Now.AddDays(2)},
            };

            List<Billing> billings = new List<Billing>()
            {
                new Billing(){ BillingId = 1, KilometersDriven = 10, NumberOfDays = 1, PriceToPay = 100, ReservationId = 4 },
                new Billing(){ BillingId = 2, KilometersDriven = 100, NumberOfDays = 1, PriceToPay = 100, ReservationId = 5 },
                new Billing(){ BillingId = 3, KilometersDriven = 10, NumberOfDays = 2, PriceToPay = 200, ReservationId = 6 },
            };

            context.AddRange(reservations);
            context.AddRange(billings);
            context.SaveChanges();

            var billing = await bt.GetByBookingNumber(null);

            Assert.Null(billing);
        }
    }
}
