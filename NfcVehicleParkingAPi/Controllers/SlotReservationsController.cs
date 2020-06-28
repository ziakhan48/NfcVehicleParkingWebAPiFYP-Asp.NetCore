using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using NfcVehicleParkingAPi.Services;
using NfcVehicleParkingAPi.Models;

namespace NfcVehicleParkingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotReservationsController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        public SlotReservationsController(AuthDbContext context,
            IEmailSender emailSender,
            ISmsSender smsSender)
        {
            _context = context;
            _emailSender = emailSender;
            _smsSender = smsSender;
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
        public async Task<ActionResult<SlotReservation>> PostSlotReservation([FromForm]SlotReservationViewModel model)
        {
           if(model == null)
            {
                return NotFound();
            }
            var Searchslot = _context.slots.FirstOrDefault(p => p.SlotId == model.Id);
            var Slotreservation = new SlotReservation()
            {
                CustomerName = model.Name,
                CustomerPhoneNo = model.Phone,
                CarNo = model.No,
                CarType = model.Type,
                slot = Searchslot,
                ReservationTime = DateTime.Now,
                CustomerEmail=model.Email,
                City=model.City,
                ZIpCode=model.ZipCode,
         

            };

         
            var result=_context.Add(Slotreservation);

            if (result != null)
            {
                _context.SaveChanges();

                Searchslot.Reserved = true;

                _context.Update(Searchslot);

              if(_context.SaveChanges() > 0)
                {
                    var res = _context.slotReservations.
                        Include(p=>p.slot)
                        .FirstOrDefault(p => p.CustomerEmail == model.Email 
                        && p.CustomerPhoneNo == model.Phone);

                    var payement = new Payment()
                    {
                        PaymentMethod="Debit card",
                        Amount=res.HoursInNumner * res.slot.RsPerHours,
                        slotReservation=res,
                        dateTime=DateTime.Now

                    };

                    _context.Add(payement);

                    if(_context.SaveChanges() > 0)
                    {
                        res.SuccessfulPayment = true;

                        _context.Update(res);
                        _context.SaveChanges();
                    }
                    
                        
                   await  _emailSender.SendEmailAsync(model.Email, $"(From Parkit.Com)",
                        $"[{model.Name}] Your Reservation Completed Successfuly for Slot::  {Searchslot.No}  ...For {model.NoOfHours} hourse ... Now Proccessed further with Payment");

                    await _smsSender.SendSmsAsync(model.Phone, $"(From Parkit.Com)" +
                       $"[{model.Name}] Your Reservation Completed Successfuly for Slot::  {Searchslot.No}  ...For {model.NoOfHours} hourse ... Now Proccessed further with Payment");
                }
              

            }
            
          
           

            return Ok();
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
