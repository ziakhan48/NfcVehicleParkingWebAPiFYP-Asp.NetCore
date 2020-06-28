using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.ViewModels
{
    public class ParkingDetailViewModel
    {
        public string Parking { get; set; }
        public int Slots { get; set; }
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Review  { get; set; }
    }
}
