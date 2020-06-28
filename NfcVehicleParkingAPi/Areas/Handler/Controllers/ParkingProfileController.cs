using NfcVehicleParkingAPi.Areas.Handler.ViewModels;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace NfcVehicleParkingAPi.Areas.Handler.Controllers
{
    [Authorize(Roles = "Handler")]
    [Route("Handler/api/[controller]")]
    [ApiController]
    public class ParkingProfileController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private AuthDbContext _dbContext;
        private ClaimsPrincipal _caller;

        public ParkingProfileController(UserManager<AppUser> userManager,
            AuthDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _caller = httpContextAccessor.HttpContext.User;

        }
        // GET: api/ParkingProfile
        [Route("Profile")]
        [HttpGet]
        public IActionResult Get()
        {
            string imgsrc = null;
            ParkingProfileViewModel model = new ParkingProfileViewModel();
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser = _userManager.FindByIdAsync(userId.Value).Result;

            var parking = _dbContext.parkings.FirstOrDefault(p => p.appUser.Id == OnlineUser.Id);
            var slotCount = _dbContext.slots.Where(p => p.Parking.ParkingId == parking.ParkingId).Count();
            var faicilities = _dbContext.ParkingFacilities.Include(p => p.Parking)
                .FirstOrDefault(p => p.Parking.ParkingId == parking.ParkingId);
            if (parking.ParkImage != null)
            {
                var base64 = Convert.ToBase64String(parking.ParkImage);
                imgsrc = string.Format("data:image/gif;base64,{0}", base64);
            }


            if (faicilities != null)
            {
                model.GuestRoom = faicilities.GuestRoom;
                model.OnlinePayment = faicilities.OnlinePayment;
                model.ServiceStation = faicilities.ServiceStation;
            }


            model.Id = parking.ParkingId;
            model.PhoneNo = OnlineUser.PhoneNumber;
            model.NoOfSlot = slotCount;
            model.Email = OnlineUser.Email;
            model.Description = parking.Description;
            model.City = parking.City;
            model.Address = parking.Address;
            model.Name = parking.Name;

            if (imgsrc != null)
            {
                model.Image = imgsrc;
            }

            return new OkObjectResult(model);
        }




        // PUT: api/ParkingProfile/5

        [HttpPut("{id}")]

        public IActionResult Put(int id, ParkingProfileViewModel model)
        {

            if (id == 0 && model == null)
            {
                return null;
            }

            var parking = _dbContext.parkings
                .FirstOrDefault(p => p.ParkingId == id);
            var facilities = _dbContext.ParkingFacilities
                .FirstOrDefault(p => p.Parking.ParkingId == parking.ParkingId);
            if (parking == null)
            {
                return NotFound();
            }



            parking.Name = model.Name;
            parking.Description = model.Description;
            parking.City = model.City;
            parking.Address = model.Address;

            var result = _dbContext.Update(parking);

            if (_dbContext.SaveChanges() > 0)
            {
                if (facilities == null)
                {
                    var facility = new ParkingFacilities()
                    {
                        Parking = parking,
                        GuestRoom = model.GuestRoom,
                        OnlinePayment = model.OnlinePayment,
                        ServiceStation = model.ServiceStation
                    };

                    _dbContext.Add(facilities);
                    _dbContext.SaveChanges();
                }
                else
                {
                    facilities.ServiceStation = model.ServiceStation;
                    facilities.OnlinePayment = model.OnlinePayment;
                    facilities.GuestRoom = model.GuestRoom;
                    _dbContext.Update(facilities);
                    _dbContext.SaveChanges();
                }
            }


            return Ok();
        }

    }
}
