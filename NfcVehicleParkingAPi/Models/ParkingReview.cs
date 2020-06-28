using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Models
{
    [Table("ParkingReview")]
    public class ParkingReview
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParkingReviewId { get; set; }

        public string  Username { get; set; }

        public DateTime  ReviewDate { get; set; }
        public string  Review { get; set; }

        public int ReviewInNumbers { get; set; }
        public Parking  Parking { get; set; }
    }
}
