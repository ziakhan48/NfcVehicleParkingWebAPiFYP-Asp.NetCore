using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.Admin.ViewModels
{
    public class AddParkingViewModel
    {
        public string  ParkingName { get; set; }
        public int  NoOfSlots { get; set; }
        public string  City { get; set; }
        public string Email { get; set; }
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public string  Password { get; set; }

    }
}
