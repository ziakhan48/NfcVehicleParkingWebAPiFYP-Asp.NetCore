using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NfcVehicleParkingAPi.Areas.Admin.ViewModels;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NfcVehicleParkingAPi.Areas.Admin.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("Admin/api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private AuthDbContext _context;

        public SlotController(AuthDbContext context)
        {
            _context = context;
        }
        // GET: api/Slot
        [HttpGet]
        public IEnumerable<string> GetSlots()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Slot/5
        [HttpGet("{id}")]
        public IActionResult GetSlot(int id)
        {
            List<SlotViewModel> Listmodel = new List<SlotViewModel>();
            SlotViewModel model = null;

            var slots = _context.slots
                .Include(p => p.Parking)
                .Where(p => p.Parking.ParkingId == id);
             foreach(var slot in slots)
            {
                model = new SlotViewModel()
                {
                    SlotId=slot.SlotId,
                    SlotNo=slot.No,
                    ParkingId=slot.Parking.ParkingId,
                    ParkingName=slot.Parking.Name,
                    IsReserved=slot.Reserved
                };

                Listmodel.Add(model);
            }

            return new OkObjectResult(Listmodel);
        }

        // POST: api/Slot
        [HttpPost]
        [Route("SlotReservation")]
        public async  Task<IActionResult> Post(SlotReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var slot = _context.slots.FirstOrDefault(p => p.SlotId == model.SlotId);
            var slotmodel = new SlotReservation()
            {
                slot = slot,
                ReservationTime = DateTime.Now,
                CustomerEmail = model.CustomerEmail,
                CustomerName = model.CustomerName,
                CustomerPhoneNo = model.CustomerPhoneNo,
                CarNo = model.CarNo
            };
            slot.Reserved = true;
            _context.Update(slot);
            _context.Add(slotmodel);
            await _context.SaveChangesAsync();
            return Ok();
        }


        
        // PUT: api/Slot/5
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
