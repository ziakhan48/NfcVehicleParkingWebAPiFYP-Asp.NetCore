using NfcVehicleParkingAPi.Areas.Admin.ViewModels;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace NfcVehicleParkingAPi.Areas.Admin.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("Admin/api/[controller]")]
    [ApiController]
    public class AddParkingController : ControllerBase
    {
        private AuthDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public AddParkingController(AuthDbContext context, 
            UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        //[Route("AddParking")]
        public IActionResult Post(AddParkingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var appuser = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,

            };
            var result = _userManager.CreateAsync(appuser, model.Password).Result;
           
            
           
            if (result.Succeeded)
            {
               

               
                    var result1 = _userManager.AddToRoleAsync(appuser, "Handler").Result;

                var roleByName = _roleManager.FindByNameAsync("Handler").Result;


                var UserbyId = _userManager.FindByEmailAsync(model.Email).Result;

                   UserbyId.appRole = roleByName;
                   _userManager.UpdateAsync(UserbyId);
               
                    var parking = new Parking()
                    {
                        Name = model.ParkingName,
                        Slot = model.NoOfSlots,
                        City = model.City,
                        appUser = appuser
                    };

                    _context.Add(parking);

                    _context.SaveChanges();
             
               
            }
            return Ok();
        }
    }
}