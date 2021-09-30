using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.ViewModels
{
    public class CarReturnModel : CarRentModel
    {
        public CarReturnModel()
        {

        }

        public CarReturnModel(DateTime endTime, int currentMilage)
        {
            this.EndRent = endTime;
            this.EndKilometerCount = currentMilage;

        }

        public override DateTime? EndRent { get; set; }
        public override int EndKilometerCount { get; set; }
        public override bool CarIsRented
        {
            get { return false; }
        }

        public override bool ClientCanBeBilled
        {
            get
            {
                return true;
            }
        }

    }
}
