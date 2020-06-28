using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using NfcVehicleParkingAPi.Areas.Handler.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace NfcVehicleParkingAPi.Areas.Handler.Controllers
{
    [Authorize(Roles = "Handler")]
    [Route("Handler/api/[controller]")]
    [ApiController]
    public class SlotReservationsController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public SlotReservationsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: api/SlotReservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SlotReservation>>> GetslotReservations()
        {
            return await _context.slotReservations.ToListAsync();
        }

        // GET: api/SlotReservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SlotReservation>> GetSlotReservation(int id)
        {
            var slotReservation = await _context.slotReservations.FindAsync(id);

            if (slotReservation == null)
            {
                return NotFound();
            }

            return slotReservation;
        }

        // PUT: api/SlotReservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlotReservation(int id, SlotReservation slotReservation)
        {
            if (id != slotReservation.SlotReservationId)
            {
                return BadRequest();
            }

            _context.Entry(slotReservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotReservationExists(id))
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

        // POST: api/SlotReservations
        [HttpPost]
        public async Task<ActionResult<SlotReservation>> PostSlotReservation(ReservationViewModel model)
        {
            var slot = _context.slots.FirstOrDefault(p => p.SlotId == model.SlotId);
            var slotmodel = new SlotReservation()
            {
                slot=slot,
                ReservationTime=DateTime.Now,
                ReservationEndTime=model.ReservationEndTime,
                CustomerEmail=model.CustomerEmail,
                CustomerName=model.CustomerName,
                CustomerPhoneNo=model.CustomerPhoneNo,
                CarNo=model.CarNo
            };
            slot.Reserved = true;
            _context.Update(slot);
            _context.Add(slotmodel);
            await _context.SaveChangesAsync();
            return Ok();
            //return CreatedAtAction("GetSlotReservation", new { id = slotReservation.SlotReservationId }, slotReservation);
        }

        // DELETE: api/SlotReservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SlotReservation>> DeleteSlotReservation(int id)
        {
            var slotReservation = await _context.slotReservations.FindAsync(id);
            if (slotReservation == null)
            {
                return NotFound();
            }

            _context.slotReservations.Remove(slotReservation);
            await _context.SaveChangesAsync();

            return slotReservation;
        }

        private bool SlotReservationExists(int id)
        {
            return _context.slotReservations.Any(e => e.SlotReservationId == id);
        }
    }
}
