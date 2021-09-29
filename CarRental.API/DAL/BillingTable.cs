using CarRental.API.DAL.Interfaces;
using CarRental.API.DAL.SQLManagment;
using CarRental.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.DAL
{
    public class BillingTable : DBContext, IBillingTable
    {
        public BillingTable(SQLContext injectedContext)
        {
            current = injectedContext;
        }

        public async Task<Billing> Add(Billing item)
        {
            current.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await SaveChanges();
            return item;
        }

        public async Task<Billing> Update(Billing item)
        {
            current.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await SaveChanges();
            return item;
        }

        public async Task<Billing> GetAllByReservationID(int reservationId)
            => await current.Billings.FirstOrDefaultAsync(x=>x.ReservationId == reservationId);

        public async Task<Billing> GetByBookingNumber(string bookingNumber)
            => await current.Billings.FirstOrDefaultAsync(x => x.Reservation.BookingNumber == bookingNumber);
        

        public async Task<Billing> GetById(int billingId)
            => await current.Billings.FirstOrDefaultAsync(x => x.BillingId == billingId);
        
    }
}
