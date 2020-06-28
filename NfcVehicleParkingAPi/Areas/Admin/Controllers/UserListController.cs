using System.Collections.Generic;
using System.Linq;
using NfcVehicleParkingAPi.Areas.Admin.ViewModels;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NfcVehicleParkingAPi.Areas.Admin.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("Admin/api/[controller]")]
    [ApiController]
    public class UserListController : ControllerBase
    {
        private AuthDbContext _context;
        private UserManager<AppUser> _userManager;

        public UserListController(AuthDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("AllUsers")]
        public IActionResult GetAllUsers()
        {
            List<UserListViewModel> listmodel = new List<UserListViewModel>();
            UserListViewModel model = null;
            var users = _context.appUsers
                .Include(p => p.appRole);

            foreach (var user in users)
            {
                model = new UserListViewModel()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    phoneNo = user.PhoneNumber,
                    Address = user.Address,
                    Aboutyou = user.AboutYou,
                    City = user.City,
                    //Role = user.appRole.Name
                };
                listmodel.Add(model);
            }

            return new OkObjectResult(listmodel);

        }

        [HttpGet]
        [Route("Handler")]
        public IActionResult GetHandler()
        {
            List<UserListViewModel> listmodel = new List<UserListViewModel>();
            UserListViewModel model = null;
            var users = _context.appUsers
                .Include(p => p.appRole)
                .Where(p => p.appRole.Name == "Handler");

            foreach (var user in users)
            {
                model = new UserListViewModel()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    phoneNo = user.PhoneNumber,
                    Address = user.Address,
                    Aboutyou = user.AboutYou,
                    City = user.City,
                    Role = user.appRole.Name
                };
                listmodel.Add(model);
            }

            return new OkObjectResult(listmodel);

        }

        [HttpGet]
        [Route("Customer")]
        public IActionResult GetCustomer()
        {
            List<UserListViewModel> listmodel = new List<UserListViewModel>();
            UserListViewModel model = null;
            var users = _context.appUsers
                .Include(p => p.appRole)
                .Where(p => p.appRole.Name == "Customer");

            foreach (var user in users)
            {
                model = new UserListViewModel()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    phoneNo = user.PhoneNumber,
                    Address = user.Address,
                    Aboutyou = user.AboutYou,
                    City = user.City,
                    Role = user.appRole.Name
                };
                listmodel.Add(model);
            }

            return new OkObjectResult(listmodel);

        }

        [HttpGet("{id}")]
        [Route("UserProfile")]

        public IActionResult GetUserProfile(string id)
        {
            UserListViewModel model = null;

            var profile = _userManager.FindByIdAsync(id).Result;

            if (profile == null)
            {
                return NotFound();
            }

            model = new UserListViewModel()
            {
                UserId = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                phoneNo = profile.PhoneNumber,
                Address = profile.Address,
                Aboutyou = profile.AboutYou,
                City = profile.City
                //Role = profile.appRole.Name
            };
            return new OkObjectResult(model);
        }
    }
}