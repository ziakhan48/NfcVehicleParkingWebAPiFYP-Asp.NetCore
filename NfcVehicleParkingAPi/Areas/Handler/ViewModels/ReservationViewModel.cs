using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.Handler.ViewModels
{
    public class ReservationViewModel
    {
        public int  SlotId { get; set; }
        public DateTime ReservationTime { get; set; }
        public DateTime ReservationEndTime { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNo { get; set; }
        public int CarNo { get; set; }
    }
}
