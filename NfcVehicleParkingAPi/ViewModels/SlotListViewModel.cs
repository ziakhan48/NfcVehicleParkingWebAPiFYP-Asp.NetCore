using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.ViewModels
{
    public class SlotListViewModel
    {
        public int Id { get; set; }
        public string  Parking { get; set; }
        public int   No { get; set; }
        public bool Reserved { get; set; }
        public int Parkid { get; set; }
    }
}
