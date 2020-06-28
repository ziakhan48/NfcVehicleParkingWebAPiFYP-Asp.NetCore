
using NfcVehicleParkingAPi.Areas.Admin.ViewModels;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace NfcVehicleParkingAPi.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin/api/[controller]")]
    public class ChangeUserNameController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private ClaimsPrincipal _caller;
        private AuthDbContext _context;

        public ChangeUserNameController(UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor, AuthDbContext context)
        {
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            ChangeUserNameViewModel model = new ChangeUserNameViewModel();
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser =  _userManager.FindByIdAsync(userId.Value).Result;

            model.UserName = OnlineUser.UserName;
            model.Id = OnlineUser.Id;

            return new OkObjectResult(model);
        }

        
        // PUT: api/ChangeUserNameViewModel/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ChangeUserNameViewModel model)
        {
            if(model == null)
            {
                return null;
            }

            var admin = _userManager.FindByIdAsync(id).Result;
            if(admin == null)
            {
                return NotFound();
            }

            admin.UserName = model.UserName;
            var result = _userManager.UpdateAsync(admin).Result;
            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

      
    }
}
