using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.ViewModels
{
    public class ParkingListViewModel
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public int  Slot { get; set; }
        public string City  { get; set; }
        public string  Image { get; set; }
    }
}
