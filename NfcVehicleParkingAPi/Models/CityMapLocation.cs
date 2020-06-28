using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Models
{

    [Table("CityMapLocation")]
    public class CityMapLocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityMapLocationId { get; set; }
        public string CityName { get; set; }

        public double CityLAt { get; set; }
        public double CityLan { get; set; }
    

    }
}
