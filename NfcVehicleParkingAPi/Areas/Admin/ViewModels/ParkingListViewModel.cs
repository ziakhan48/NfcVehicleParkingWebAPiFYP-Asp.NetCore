using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.Admin.ViewModels
{
    public class ParkingListViewModel
    {
        public int ParkingId { get; set; }
        public string  Name { get; set; }
        public string  Handlername { get; set; }
        public int NoOfSlot { get; set; }
        public string  Address { get; set; }
        public string  City { get; set; }
        public string  Description { get; set; }
        public string  PhoneNo { get; set; }
        public string  Email { get; set; }
    }
}
