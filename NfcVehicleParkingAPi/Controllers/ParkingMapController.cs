
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace NfcVehicleParkingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingMapController : ControllerBase
    {
        private AuthDbContext _context;

        public ParkingMapController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpGet("{city}")]
        public IActionResult Get(string city)
        {
            ParkingMapLocationViewModel model = null;
            List<ParkingMapLocationViewModel> list = new List<ParkingMapLocationViewModel>();
            if (city == null)
            {
                return null;
            }

            var CityLocation = _context.cityMapLocations.
                FirstOrDefault(p => p.CityName == city);
            if (CityLocation == null)
            {
                return NotFound();
            }

            var parkings = _context.parkings.Where(p => p.City == city).ToList();

            foreach (var parking in parkings)
            {
                var parkingLocation = _context.parkingGoogleMaps.
                    FirstOrDefault(p => p.ParkingName == parking.Name);

                model = new ParkingMapLocationViewModel()
                {
                    CityName = parking.City,
                    ParkingName = parking.Name,
                    ParkingLatitude = parkingLocation.ParkLat,
                    ParkingLongitude = parkingLocation.ParkLang,
                    CityLatitude = CityLocation.CityLAt,
                    CityLongitude = CityLocation.CityLan
                };

                list.Add(model);
            }

            return new OkObjectResult(list);
        }
    }
}