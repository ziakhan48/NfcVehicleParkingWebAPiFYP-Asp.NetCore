using Microsoft.AspNetCore.Identity;

namespace NfcVehicleParkingAPi.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  FullName { get; set; }
        public long? FacebookId { get; set; }
        public string Address { get; set; }
        public byte[] PictureUrl { get; set; }
        public string City { get; set; }
        public string AboutYou { get; set; }
        public AppRole appRole { get; set; }
        public byte[] Avatarimage { get; set; }
    }
}
