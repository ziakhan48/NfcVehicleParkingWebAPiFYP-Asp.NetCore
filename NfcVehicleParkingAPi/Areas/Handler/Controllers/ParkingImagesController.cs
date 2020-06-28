using NfcVehicleParkingAPi.Areas.Handler.ViewModels;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NfcVehicleParkingAPi.Areas.Handler.Controllers
{
    [Route("Handler/api/[controller]")]
    [ApiController]
    public class ParkingImagesController : ControllerBase
    {
        private AuthDbContext _context;

        public ParkingImagesController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            ParkingImagesListViewModel model = null;

            List<ParkingImagesListViewModel> list = new List<ParkingImagesListViewModel>();


            var parkingimages = _context.ParkingImages.Include(p => p.Parking).ToList();

            if(parkingimages == null)
            {
                return NotFound();
            }


            foreach(var parkingimage in parkingimages)
            {
                var base64 = Convert.ToBase64String(parkingimage.Image);
                var imgsrc = string.Format("data:image/gif;base64,{0}", base64);
                model = new ParkingImagesListViewModel()
                {
                    Id=parkingimage.ParkingImagesId,
                    Image=imgsrc,
                    ParkingId=parkingimage.Parking.ParkingId
                };

                list.Add(model);
            }
            
            return new  OkObjectResult(list);
        }
        [HttpPost("{id}")]
        public IActionResult Post([FromForm] IFormFile file ,int id)
        {
            if(file ==null && id == 0)
            {
                return null;
            }

            ParkingImages model = new ParkingImages();

            using (var memoryStream = new MemoryStream())
            {
                file.CopyToAsync(memoryStream);
                model.Image = memoryStream.ToArray();

            }

            model.Parking = _context.parkings.FirstOrDefault(p => p.ParkingId == id);

            var result = _context.Add(model);

            if(_context.SaveChanges() == 0)
            {
                return BadRequest();
            }

            return Ok();

        }
    }
}