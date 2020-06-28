using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Models
{
    [Table("ParkingImages")]
    public class ParkingImages
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParkingImagesId { get; set; }

        public byte[] Image { get; set; }

        public Parking  Parking { get; set; }
    }
}
