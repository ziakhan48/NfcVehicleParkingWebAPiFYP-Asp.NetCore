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
    public class ParkingsController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public ParkingsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: api/Parkings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parking>>> Getparkings()
        {
            var parking = await _context.parkings.ToListAsync();
            return new OkObjectResult(parking);
        }

        [HttpGet("{city}")]
        //[Route("parkingByName")]
        public async Task<ActionResult<IEnumerable<Parking>>> GetParking(string city)
        {
            List<ParkingListViewModel> listmodel = new List<ParkingListViewModel>();
            ParkingListViewModel model = null;
            var result = await _context.parkings.
                Where(p => p.City == city).
                ToListAsync();

            foreach (var park in result)
            {
                model = new ParkingListViewModel()
                {
                    Id = park.ParkingId,
                    Name = park.Name,
                    City = park.City,
                    Slot = park.Slot,
                    Image=park.image
                };

                listmodel.Add(model);
            }

            return new OkObjectResult(listmodel);
        }

        //// GET: api/Parkings/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Parking>> GetParking(int id)
        //{
        //    var parking = await _context.parkings.FindAsync(id);

        //    if (parking == null)
        //    {
        //        return NotFound();
        //    }

        //    return parking;
        //}

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
