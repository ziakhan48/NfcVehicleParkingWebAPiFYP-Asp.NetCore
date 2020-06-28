using System.Linq;
using NfcVehicleParkingAPi.Areas.User.ViewModels;
using NfcVehicleParkingAPi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NfcVehicleParkingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotReservationDetailHistoryController : ControllerBase
    {
        private AuthDbContext _context;

        public SlotReservationDetailHistoryController(AuthDbContext context)
        {
            _context = context;
        }
        

        // GET: api/SlotReservationDetailHistory/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            SlotReservationDetailViewModel model = new SlotReservationDetailViewModel();

            var reservationslot =
                _context.slotReservations.Include(p => p.slot).Include(p => p.slot.Parking)
                .FirstOrDefault(p => p.SlotReservationId == id);

            model.Parking = reservationslot.slot.Parking.Name;
            model.No = reservationslot.slot.No;
            model.ReservationTime = reservationslot.ReservationTime.ToString();
            model.EndTime = reservationslot.ReservationEndTime.ToString();
            model.Id = reservationslot.SlotReservationId;
            model.City = reservationslot.slot.Parking.City;
            model.Address = reservationslot.slot.Parking.Address;
            model.Description = reservationslot.slot.Parking.Description;
            model.Reserved = reservationslot.slot.Reserved;
            return new OkObjectResult(model);
        }

        // POST: api/SlotReservationDetailHistory
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/SlotReservationDetailHistory/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
