using System;
using System.Linq;
using NfcVehicleParkingAPi.Areas.User.ViewModels;
using NfcVehicleParkingAPi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace NfcVehicleParkingAPi.Areas.User.Controllers
{
    [Route("User/api/[controller]")]
    [ApiController]
    public class UnreservedSlotController : ControllerBase
    {
        private AuthDbContext _context;

        public UnreservedSlotController(AuthDbContext context)
        {
            _context = context;
        }
       

        // GET: api/UnreservedSlot/5
        [HttpGet("{id}")]
        public IActionResult GetUnreservedSlot(int id)
        {
            ReservationHistoryViewModel model = new ReservationHistoryViewModel();
            if(id == 0)
            {

            }

            var slotreservation = _context.slotReservations.Include(p => p.slot).Include(p => p.slot.Parking)
                .FirstOrDefault(p => p.SlotReservationId == id && p.slot.Reserved ==true); 

            if(slotreservation != null)
            {
                slotreservation.ReservationEndTime = DateTime.Now;
              
                _context.Update(slotreservation);
                _context.SaveChanges();
            }

          

            var slot = _context.slots.FirstOrDefault(p => p.SlotId == slotreservation.slot.SlotId);

            if(slot != null)
            {
                slot.Reserved = false;
                _context.Update(slot);
                _context.SaveChanges();
            }


            model.Id = slotreservation.SlotReservationId;
            model.No = slotreservation.slot.SlotId;
            model.Parking = slotreservation.slot.Parking.Name;
            model.ReservationTime = slotreservation.ReservationTime.ToString();
            model.EndTime = slotreservation.ReservationEndTime.ToString();
            model.City = slotreservation.slot.Parking.City;


            return new OkObjectResult(model);
        }

    }
}
