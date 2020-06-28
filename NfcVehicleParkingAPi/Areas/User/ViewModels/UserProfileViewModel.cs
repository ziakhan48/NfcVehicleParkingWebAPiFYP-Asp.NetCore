using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.User.ViewModels
{
    public class UserProfileViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string  Address { get; set; }
        public string Email { get; set; }
        public string  phone { get; set; }
        public string City { get; set; }
    }
}
