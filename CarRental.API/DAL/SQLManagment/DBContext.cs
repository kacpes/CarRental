using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.DAL.SQLManagment
{
    public class DBContext
    {
        protected SQLContext current { get; set; }

        public async Task SaveChanges()
        {
            await current.SaveChangesAsync();
        }
    }
}
