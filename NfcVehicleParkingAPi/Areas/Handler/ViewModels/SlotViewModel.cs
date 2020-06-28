using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.Handler.ViewModels
{
    public class SlotViewModel
    {
        public int SlotId { get; set; }
        public int SlotNo { get; set; }
        public string  Parking { get; set; }
        public bool IsReserved { get; set; }
    }
}
