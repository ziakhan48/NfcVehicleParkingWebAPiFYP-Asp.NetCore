using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Models
{
    [Table("SlotReservation")]
    public class SlotReservation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SlotReservationId { get; set; }
        public Slot  slot { get; set; }
        public DateTime  ReservationTime { get; set; }
        public DateTime ReservationEndTime { get; set; }
        public string  CustomerEmail { get; set; }
        public string  CustomerName { get; set; }
        public string  CustomerPhoneNo { get; set; }
        public int CarNo { get; set; }
        public string  CarType { get; set; }
        public string  NoOfHours { get; set; }
        public string  City { get; set; }
        public string  ZIpCode { get; set; }
        public int HoursInNumner { get; set; }
        public bool SuccessfulPayment { get; set; }

    }
}
