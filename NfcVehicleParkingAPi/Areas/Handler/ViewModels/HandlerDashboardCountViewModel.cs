using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.Handler.ViewModels
{ 
    public class HandlerDashboardCountViewModel
    {
        public int TotalSlotsCount  { get; set; }
        public int ReservedSlotCount { get; set; }
        public int NotReservedSlotCount { get; set; }
        public int SuccessfullPaymentCount { get; set; }
        public int TotalReservation { get; set; }


    }
}
