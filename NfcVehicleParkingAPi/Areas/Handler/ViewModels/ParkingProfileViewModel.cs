namespace NfcVehicleParkingAPi.Areas.Handler.ViewModels
{
    public class ParkingProfileViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }
        public string Address { get; set; }

        public string Description { get; set; }

        public int NoOfSlot { get; set; }
        public string PhoneNo { get; set; }
        public string  Email { get; set; }
        public string  Image { get; set; }
        public bool GuestRoom { get; set; }
        public bool ServiceStation { get; set; }
        public bool OnlinePayment { get; set; }

    }
}
