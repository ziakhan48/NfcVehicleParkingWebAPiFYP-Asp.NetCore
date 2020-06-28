using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Models
{
    [Table("Slot")]
    public class Slot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SlotId { get; set; }
        public int No { get; set; }
        public Parking  Parking { get; set; }
        public bool Reserved { get; set; }
        public int RsPerHours { get; set; }
    }
}
