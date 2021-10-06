using CarRental.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.DAL.SQLManagment
{
    public static class Seed
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            SeedCategories(modelBuilder);
        }

        private static void SeedCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarCategory>().HasData(
                new CarCategory { CarCategoryId = 1, CarType = Enums.CarType.Compact.ToString() },
                new CarCategory { CarCategoryId = 2, CarType = Enums.CarType.Premium.ToString() },
                new CarCategory { CarCategoryId = 3, CarType = Enums.CarType.Minivan.ToString() }
                );
        }
    }
}
