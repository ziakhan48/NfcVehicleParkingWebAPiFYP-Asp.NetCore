using System.ComponentModel.DataAnnotations.Schema;

namespace NfcVehicleParkingAPi.Models
{
    [Table("Parking")]
    public class Parking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParkingId { get; set; }

        public string Name { get; set; }

        public string City { get; set; }
        public string Address { get; set; }

        public string Description { get; set; }

        public int Slot { get; set; }

        public AppUser appUser { get; set; }
        public string image { get; set; }

        public byte[] ParkImage { get; set; }
    }
}
