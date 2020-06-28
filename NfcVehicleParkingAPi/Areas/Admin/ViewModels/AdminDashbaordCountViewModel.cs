using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.Admin.ViewModels
{
    public class AdminDashbaordCountViewModel
    {
        public int UserCount { get; set; }
        public int HandlerCount { get; set; }
        public int ParkingCount { get; set; }
        public int RoleCount { get; set; }
        public int CustomerCount { get; set; }
    }
}
