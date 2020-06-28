using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.User.ViewModels
{
    public class ReservationHistoryViewModel
    {
        public int Id { get; set; }
        public int No { get; set; }
        public string  Parking { get; set; }
        public string City  { get; set; }
        public string  ReservationTime { get; set; }
        public string  EndTime { get; set; }

    }
}
