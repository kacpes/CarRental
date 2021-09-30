using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Helpers
{
    public static class DateTimeExtentions
    {
        public static bool Between(this DateTime input, DateTime date1, DateTime? date2)
        {
            return (input > date1 || (date2 != null && input < date2));
        }
    }
}
