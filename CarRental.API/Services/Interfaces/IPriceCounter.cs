using CarRental.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Services.Interfaces
{
    public interface IPriceCounter
    {
        double CalculatePrice(CarRentModel car);
    }
}
