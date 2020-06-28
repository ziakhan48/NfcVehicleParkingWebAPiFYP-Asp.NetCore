using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using NfcVehicleParkingAPi.Data;

using NfcVehicleParkingAPi.Models;
using NfcVehicleParkingAPi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NfcVehicleParkingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddReviewController : ControllerBase
    {
        private AuthDbContext _context;

        public AddReviewController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: api/AddReview/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ParkingReviewViewModel model = null;
            List<ParkingReviewViewModel> list = new List<ParkingReviewViewModel>();
            var parkingreview = _context.parkingReviews.Where(p => p.Parking.ParkingId == id)
                .Include(p => p.Parking)
                .ToList();


            if(parkingreview == null)
            {
                
            }

            for(int review=0;review < parkingreview.Count(); review++)
            {
                model = new ParkingReviewViewModel()
                {
                    Name =parkingreview[review].Username,
                    Review=parkingreview[review].Review,
                    Reviews=parkingreview[review].ReviewInNumbers,

                };

                list.Add(model);
            }

            return new ObjectResult(list);
        }

        // POST: api/AddReview
        [HttpPost]
        public IActionResult Post([FromForm] AddReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {

            }

            var Review = new ParkingReview()
            {
                Username = model.Name,
                Review = model.Review,
                ReviewDate = DateTime.Now,
                ReviewInNumbers = model.ReviewNo,
                Parking = _context.parkings.FirstOrDefault(p => p.Name == model.Parking)
            };

            _context.Add(Review);
            _context.SaveChanges();
            return Ok();
        }

    }
}
