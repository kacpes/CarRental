using CarRental.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.DAL.Interfaces
{
    public interface IReservationTable
    {
        Task<Reservation> GetById(int reservationId);
        Task<Reservation> GetByBookingNumber(string bookingNumber);
        Task<List<Reservation>> GetAllByCarId(int carId);
        Task<bool> CheckIfDateIsAvaliableFor(Reservation reservation);

        Task<Reservation> Add(Reservation item);
        Task<Reservation> Update(Reservation Item);

    }
}
