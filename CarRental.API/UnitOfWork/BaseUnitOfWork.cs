using CarRental.API.DAL.SQLManagment;
using CarRental.API.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.UnitOfWork
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        protected SQLContext _context { get; }

        public BaseUnitOfWork(SQLContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
