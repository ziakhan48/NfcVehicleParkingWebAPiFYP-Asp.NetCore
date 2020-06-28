using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using NfcVehicleParkingAPi.ViewModels;

namespace NfcVehicleParkingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public SlotsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: api/Slots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slot>>> Getslots()
        {
            return await _context.slots.ToListAsync();
        }

        // GET: api/Slots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Parking>>> GetSlot(int id)
        {
            List<SlotListViewModel> listmodel = new List<SlotListViewModel>();
            SlotListViewModel model = null;
            var slots = await _context.slots.
                Where(p => p.Parking.ParkingId == id).
                Include(p => p.Parking).
                ToListAsync();

            if (slots == null)
            {
                return NotFound();
            }

            foreach (var slot in slots)
            {
                model = new SlotListViewModel()
                {
                    Id = slot.SlotId,
                    No = slot.No,
                    Reserved = slot.Reserved,
                    Parking = slot.Parking.Name,
                    Parkid=slot.Parking.ParkingId
                };

                listmodel.Add(model);
            }

            return new OkObjectResult(listmodel);


        }

        // PUT: api/Slots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlot(int id, Slot slot)
        {
            if (id != slot.SlotId)
            {
                return BadRequest();
            }

            _context.Entry(slot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Slots
        [HttpPost]
        public async Task<ActionResult<Slot>> PostSlot(Slot slot)
        {
            _context.slots.Add(slot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSlot", new { id = slot.SlotId }, slot);
        }

        // DELETE: api/Slots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Slot>> DeleteSlot(int id)
        {
            var slot = await _context.slots.FindAsync(id);
            if (slot == null)
            {
                return NotFound();
            }

            _context.slots.Remove(slot);
            await _context.SaveChangesAsync();

            return slot;
        }

        private bool SlotExists(int id)
        {
            return _context.slots.Any(e => e.SlotId == id);
        }
    }
}
