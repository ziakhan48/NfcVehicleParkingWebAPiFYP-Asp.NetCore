using System.Collections.Generic;
using System.Linq;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using NfcVehicleParkingAPi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace NfcVehicleParkingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecomendationController : ControllerBase
    {
        private AuthDbContext _context;

        public RecomendationController(AuthDbContext context)
        {
            _context = context;
        }
        // GET: api/Recomendation/city
        [HttpGet("{city}")]
        public IActionResult Get(string city)
        {
            RecomendationViewModel model = null;
            List<RecomendationViewModel> temp = new List<RecomendationViewModel>();
            List<RecomendationViewModel> recomendationList = new List<RecomendationViewModel>();
            List<string> imageList = new List<string>();
            int reviewcount = 0;
            int Averagereview = 0;
            List<Parking> parkings = null;

            var image1 = "lib/assets/images/g5.jfif";
            var image2 = "lib/assets/images/g9.jpg";
            var image3 = "lib/assets/images/g3.jfif";
            var image4 = "lib/assets/images/g6.jpeg";

            imageList.Add(image1);
            imageList.Add(image2);
            imageList.Add(image3);
            imageList.Add(image4);

            if (!(city.Equals("null")))
            {
                parkings = _context.parkings.Where(p => p.City == city)
                    .ToList();
            }
            else
            {
                parkings = _context.parkings.ToList();
            }

            foreach (var parking in parkings)
            {
                var reviews = _context.parkingReviews.
                    Where(p => p.Parking.ParkingId == parking.ParkingId).ToList();

                for (int reviewloop = 0; reviewloop < reviews.Count; reviewloop++)
                {
                    reviewcount = reviewcount + reviews[reviewloop].ReviewInNumbers;

                    if (reviewloop == (reviews.Count) - 1)
                    {
                        Averagereview = reviewcount / reviews.Count;
                    }

                    model = new RecomendationViewModel()
                    {
                        Parking = parking.Name,
                        Id = parking.ParkingId,
                        AverageReview = Averagereview

                    };
                }
                temp.Add(model);
                reviewcount = 0;
                Averagereview = 0;
            }

            var finalresult = temp.OrderByDescending(p => p.AverageReview).ToList();
            var image = imageList.ToList();

            for (int recomndedloop = 0; recomndedloop < finalresult.Count(); recomndedloop++)
            {
                model = new RecomendationViewModel()
                {
                    Parking = finalresult[recomndedloop].Parking,
                    Id = finalresult[recomndedloop].Id,
                    AverageReview = finalresult[recomndedloop].AverageReview,
                    Image=imageList[recomndedloop]
                };

                recomendationList.Add(model);
            }

            return new OkObjectResult(recomendationList);
        }


    }
}
