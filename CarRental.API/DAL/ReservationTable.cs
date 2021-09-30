using CarRental.API.DAL.Interfaces;
using CarRental.API.DAL.SQLManagment;
using CarRental.API.Models;
using Microsoft.EntityFrameworkCore;
using CarRental.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.DAL
{
    public class ReservationTable : DBContext, IReservationTable
    {
        public ReservationTable(SQLContext injectedContext)
        {
            current = injectedContext;
        }

        public async Task<Reservation> Add(Reservation item)
        {
            current.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await SaveChanges();
            return item;
        }

        public async Task<Reservation> Update(Reservation item)
        {
            current.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await SaveChanges();
            return item;
        }

        public async Task<List<Reservation>> GetAllByCarId(int carId)
            => await current.Reservations.Where(x => x.CarId == carId).ToListAsync();

        public async Task<Reservation> GetByBookingNumber(string bookingNumber)
            => await current.Reservations
            .Include(x => x.ChosenCar)
            .ThenInclude(x => x.CarCategory)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.BookingNumber == bookingNumber);


        public async Task<Reservation> GetById(int reservationId)
            => await current.Reservations.FirstOrDefaultAsync(x => x.ReservationId == reservationId);

        public async Task<bool> CheckIfDateIsAvaliableFor(Reservation reservation)
        {
            var assignedToCar = await this.GetAllByCarId(reservation.CarId ?? 0);

            bool carIsReservedInGivenTime = 
                assignedToCar.Any(x => 
                    !x.Started.Between(reservation.Started, reservation.Released) || 
                    !x.Released.Between(reservation.Started, reservation.Released));

            return await Task.FromResult(carIsReservedInGivenTime);
        }
    }
}
