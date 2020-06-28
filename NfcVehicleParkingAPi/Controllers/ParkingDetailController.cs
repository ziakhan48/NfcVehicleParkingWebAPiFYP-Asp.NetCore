using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using NfcVehicleParkingAPi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NfcVehicleParkingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingDetailController : ControllerBase
    {

        private AuthDbContext _context;

        public ParkingDetailController(AuthDbContext context)
        {
            _context = context;
        }


        // GET: api/ParkingDetail/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ParkingDetailViewModel model = new ParkingDetailViewModel();

            var Parking = _context.parkings.FirstOrDefault(p => p.ParkingId == id);
            if(Parking == null)
            {

            }
            var slotCount = _context.slots.Where(p => p.Parking.ParkingId == Parking.ParkingId).Count();

            if(slotCount ==0)
            {

            }
            int review = 0;
            var Parkingreview = _context.parkingReviews.Where(p => p.Parking.ParkingId == Parking.ParkingId).ToList();
           
            if(Parkingreview == null)
            {

            }
            for(int Preview=0; Preview < Parkingreview.Count(); Preview++)
            {
                review = review + Parkingreview[Preview].ReviewInNumbers;
            }

            model.Review = review;
            model.Parking = Parking.Name;
            model.Id = Parking.ParkingId;
            model.Slots = slotCount;
            model.Description =Parking.Description;
            model.City = Parking.City;
            model.Address = Parking.Address;
           
            return new OkObjectResult(model);
        }

       
    }
}
