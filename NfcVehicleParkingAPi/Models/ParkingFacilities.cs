using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Models
{
    [Table("ParkingFacilities")]
    public class ParkingFacilities
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParkingFacilitiesId { get; set; }
        public Parking  Parking { get; set; }
        public bool GuestRoom { get; set; }
        public bool ServiceStation { get; set; }
        public bool OnlinePayment { get; set; }
    }
}
