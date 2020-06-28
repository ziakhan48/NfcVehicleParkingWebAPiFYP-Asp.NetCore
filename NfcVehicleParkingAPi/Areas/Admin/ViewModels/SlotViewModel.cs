namespace NfcVehicleParkingAPi.Areas.Admin.ViewModels
{
    public class SlotViewModel
    {
        public int SlotId { get; set; }
        public int SlotNo { get; set; }
        public int ParkingId { get; set; }
        public string ParkingName { get; set; }
        public bool IsReserved { get; set; }
    }
}
