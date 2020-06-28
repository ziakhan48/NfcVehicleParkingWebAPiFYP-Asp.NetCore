namespace NfcVehicleParkingAPi.ViewModels
{
    public class SlotReservationViewModel
    {
        public int  Id { get; set; }
        public string  Name { get; set; }
        public string  Type { get; set; }
        public string  Phone { get; set; }
        public int   No { get; set; }

        public string  Email { get; set; }

        public string  City { get; set; }

        public string  ZipCode { get; set; }
        public int   NoOfHours { get; set; }
    }
}
