using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.UnitOfWork.Interfaces
{
    public interface IBaseUnitOfWork
    {
        Task CompleteAsync();
    }
}
