using CarRental.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.DAL.Interfaces
{
    public interface IBillingTable
    {
        Task<Billing> GetById(int reservationId);
        Task<Billing> GetByBookingNumber(string bookingNumber);
        Task<Billing> GetAllByReservationID(int reservationId);

        Task<Billing> Add(Billing item);
        Task<Billing> Update(Billing Item);
    }
}
