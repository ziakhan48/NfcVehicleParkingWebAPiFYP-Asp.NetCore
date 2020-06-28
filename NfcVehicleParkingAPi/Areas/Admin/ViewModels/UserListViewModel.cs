using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.Admin.ViewModels
{
    public class UserListViewModel
    {
        public string UserId { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string  Email { get; set; }
        public string  phoneNo { get; set; }
        public string  Address { get; set; }
        public string  Aboutyou { get; set; }
        public string City { get; set; }
        public string  Role { get; set; }
    }
}
