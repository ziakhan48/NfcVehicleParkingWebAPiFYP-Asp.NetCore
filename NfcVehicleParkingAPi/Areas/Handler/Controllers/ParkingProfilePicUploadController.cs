using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NfcVehicleParkingAPi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NfcVehicleParkingAPi.Areas.Handler.Controllers
{
    [Route("Handler/api/[controller]")]
    [ApiController]
    public class ParkingProfilePicUploadController : ControllerBase
    {
        private AuthDbContext _context;

        public ParkingProfilePicUploadController(AuthDbContext context) 
        {
            _context = context;
        }


        // POST: api/ParkingProfilePicUpload
        [HttpPost("{id}")]
        public IActionResult Post([FromForm]IFormFile file, int id)
        {

            var park = _context.parkings.FirstOrDefault(p => p.ParkingId == id);



            if (park == null)
            {
                return NotFound();
            }
            using (var memoryStream = new MemoryStream())
            {
                file.CopyToAsync(memoryStream);
                park.ParkImage = memoryStream.ToArray();

            }

            var result = _context.Update(park);

            if (_context.SaveChanges() ==0)
            {
                return BadRequest();
            }



            return Ok();
        }
    }
}