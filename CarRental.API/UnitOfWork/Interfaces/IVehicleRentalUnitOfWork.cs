using CarRental.API.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.UnitOfWork.Interfaces
{
    public interface IVehicleRentalUnitOfWork : IBaseUnitOfWork
    {
        IBillingTable BillingTable { get; }
        IReservationTable ReservationTable { get; }
    }
}
