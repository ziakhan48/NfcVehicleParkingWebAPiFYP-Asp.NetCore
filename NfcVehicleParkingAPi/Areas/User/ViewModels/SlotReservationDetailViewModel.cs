using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.User.ViewModels
{
    public class SlotReservationDetailViewModel
    {
        public string  Parking { get; set; }
        public int No { get; set; }
        public int Id { get; set; }
        public string ReservationTime { get; set; }
        public string  EndTime { get; set; }
        public string City { get; set; }
        public string  Address { get; set; }
        public string  Description { get; set; }
        public bool Reserved { get; set; }
    }
}
