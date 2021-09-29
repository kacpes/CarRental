using CarRental.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Services.Interfaces
{
    public interface IVehicleRental
    {
        Task<CarRentModel> RentVehicle(CarRentModel carRentModel);
        Task<Bill> ReturnVehicle(CarReturnModel carReturnModel);
    }
}
