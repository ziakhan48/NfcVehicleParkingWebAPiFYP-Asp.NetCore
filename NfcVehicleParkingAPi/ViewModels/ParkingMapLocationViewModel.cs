using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.ViewModels
{
    public class ParkingMapLocationViewModel
    {
        public string CityName { get; set; }
        public string ParkingName { get; set; }
        public double CityLatitude { get; set; }
        public double CityLongitude { get; set; }
        public double ParkingLatitude { get; set; }
        public double ParkingLongitude { get; set; }
    }
}
