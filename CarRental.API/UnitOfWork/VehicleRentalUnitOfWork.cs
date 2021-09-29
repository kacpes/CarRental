using CarRental.API.DAL.Interfaces;
using CarRental.API.DAL.SQLManagment;
using CarRental.API.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.UnitOfWork
{
    public class VehicleRentalUnitOfWork : BaseUnitOfWork, IVehicleRentalUnitOfWork
    {
        public IBillingTable BillingTable { get; private set; }

        public IReservationTable ReservationTable { get; private set; }

        public VehicleRentalUnitOfWork(SQLContext context, IBillingTable billingTable,
            IReservationTable reservationTable) : base(context)
        {
            BillingTable = billingTable;
            ReservationTable = reservationTable;
        }
    }
}
