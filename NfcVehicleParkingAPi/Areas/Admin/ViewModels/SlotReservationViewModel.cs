using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.Admin.ViewModels
{
    public class SlotReservationViewModel
    {
        public int ReservationId { get; set; }
        public int SlotId { get; set; }
        public DateTime ReservationTime { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime ReservationEndTime { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNo { get; set; }
        public int CarNo { get; set; }
        public int SlotNo { get; set; }
    }
}
