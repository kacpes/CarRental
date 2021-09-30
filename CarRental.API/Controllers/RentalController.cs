using CarRental.API.Services.Interfaces;
using CarRental.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RentalController : Controller
    {
        private readonly IVehicleRental _vehicleRental;

        public RentalController(IVehicleRental vehicleRental)
        {
            _vehicleRental = vehicleRental;
        }

        [HttpGet("GetBasicCarVm")]
        public Car GetBasicCarVm()
        {
            return new Car();
        }

        [HttpPost("RentCar")]
        public async Task<CarRentModel> RentCar(CarRentModel vm)
        {
            return await _vehicleRental.RentVehicle(vm);
        }

        [HttpPost("ReturnCar")]
        public async Task<Bill> ReturnACar(CarReturnModel vm)
        {
            return await _vehicleRental.ReturnVehicle(vm);
        }
    }
}
