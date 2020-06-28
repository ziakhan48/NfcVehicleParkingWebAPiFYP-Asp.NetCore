using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Models
{
    [Table("ParkingGoogleMap")]
    public class ParkingGoogleMap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParkingGoogleMapId { get; set; }
        public string ParkingName { get; set; }
        public double ParkLat { get; set; }  
        public double ParkLang { get; set; }
        public CityMapLocation CityMapLocation { get; set; }
    }
}
