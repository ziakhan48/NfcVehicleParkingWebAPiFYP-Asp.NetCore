
using NfcVehicleParkingAPi.Areas.Handler.ViewModels;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace NfcVehicleParkingAPi.Areas.Handler.Controllers
{
    [Authorize(Roles = "Handler")]
    [Route("Handler/api/[controller]")]
    [ApiController]
    public class UserNameUpdateController : ControllerBase
    {
        private AuthDbContext _context;
        private UserManager<AppUser> _userManager;
        private ClaimsPrincipal _caller;

        public UserNameUpdateController(AuthDbContext context , 
            UserManager<AppUser> userManager , IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
        }


        [HttpGet]
        public IActionResult Get()
        {
            UpdateUserNameViewModel model = new UpdateUserNameViewModel();
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser = _userManager.FindByIdAsync(userId.Value).Result;

            model.UserName = OnlineUser.UserName;
            model.Id = OnlineUser.Id;

            return new OkObjectResult(model);
        }

        // PUT: api/UserNameUpdate/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] UpdateUserNameViewModel model)
        {
            if (model == null)
            {
                return null;
            }

            var admin = _userManager.FindByIdAsync(id).Result;
            if (admin == null)
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