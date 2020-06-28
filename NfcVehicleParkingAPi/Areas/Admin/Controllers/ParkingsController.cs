using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using NfcVehicleParkingAPi.Areas.Admin.ViewModels;

namespace NfcVehicleParkingAPi.Areas.Admin.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("Admin/api/[controller]")]
    [ApiController]
    public class ParkingsController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public ParkingsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: api/Parkings
        [HttpGet]
        public ActionResult Getparkings()
        {
            List<ParkingListViewModel> Listmodel = new List<ParkingListViewModel>();
            ParkingListViewModel model = new ParkingListViewModel();
            var parkings = _context.parkings.Include(p => p.appUser);

            foreach (var park in parkings)
            {
                model = new ParkingListViewModel()
                {
                    ParkingId = park.ParkingId,
                    City = park.City,
                    Handlername = park.appUser.FirstName + park.appUser.LastName,
                    Name = park.Name,
                    NoOfSlot = park.Slot
                };


                Listmodel.Add(model);
            }

            return new OkObjectResult(Listmodel);
        }

        // GET: api/Parkings/5
        [HttpGet("{id}")]
        public IActionResult GetParking(int id)
        {
            ParkingListViewModel model = new ParkingListViewModel();
            var parking = _context.parkings.Include(p => p.appUser)
                .FirstOrDefault(p => p.ParkingId == id);
            model.ParkingId = parking.ParkingId;
            model.PhoneNo = parking.appUser.PhoneNumber;
            model.NoOfSlot = parking.Slot;
            model.Address = parking.Address;
            model.City = parking.City;
            model.Email = parking.appUser.Email;
            model.Handlername = parking.appUser.FirstName + parking.appUser.LastName;
            model.Description = parking.Description;
            model.Name = parking.Name;

            if (parking == null)
            {
                return NotFound();
            }

            return new OkObjectResult(model);
        }

        // PUT: api/Parkings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParking(int id, Parking parking)
        {
            if (id != parking.ParkingId)
            {
                return BadRequest();
            }

            _context.Entry(parking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingExists(id))
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

        // POST: api/Parkings
        [HttpPost]
        public async Task<ActionResult<Parking>> PostParking(Parking parking)
        {
            _context.parkings.Add(parking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParking", new { id = parking.ParkingId }, parking);
        }

        // DELETE: api/Parkings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Parking>> DeleteParking(int id)
        {
            var parking = await _context.parkings.FindAsync(id);
            if (parking == null)
            {
                return NotFound();
            }

            _context.parkings.Remove(parking);
            await _context.SaveChangesAsync();

            return parking;
        }

        private bool ParkingExists(int id)
        {
            return _context.parkings.Any(e => e.ParkingId == id);
        }
    }
}
