using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NfcVehicleParkingAPi.Models
{
    [Table("Payment")]
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int PaymentId { get; set; }
        public string  PaymentMethod { get; set; }
        public int  Amount { get; set; }
        public DateTime  dateTime { get; set; }
        public SlotReservation slotReservation { get; set; }
    }
}
