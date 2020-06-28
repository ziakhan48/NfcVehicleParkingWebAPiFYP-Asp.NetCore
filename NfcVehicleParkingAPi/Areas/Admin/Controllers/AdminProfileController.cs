using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NfcVehicleParkingAPi.Areas.Admin.ViewModels;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NfcVehicleParkingAPi.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("Admin/api/[controller]")]
    [ApiController]
    public class AdminProfileController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private ClaimsPrincipal _caller;
        public AdminProfileController(UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
        }
        // GET: api/AdminProfile
        [HttpGet]
        [Route("Profile")]
        public async Task<IActionResult> GetProfile()
        {

            AdminProfileViewModel model = new AdminProfileViewModel();
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser = await _userManager.FindByIdAsync(userId.Value);

            var base64 = Convert.ToBase64String(OnlineUser.Avatarimage);
            var imgsrc = string.Format("data:image/gif;base64,{0}", base64);
            model.Id = OnlineUser.Id;
            model.FirstName = OnlineUser.FirstName;
            model.LastName = OnlineUser.LastName;
            model.Email = OnlineUser.Email;
            model.Address = OnlineUser.Address;
            model.AboutYou = OnlineUser.AboutYou;
            model.PhoneNo = OnlineUser.PhoneNumber;
            model.City = OnlineUser.City;
            model.Image = imgsrc;
            
            return new ObjectResult(model);
        }

     

        // POST: api/AdminProfile
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/AdminProfile/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] AdminProfileViewModel model)
        {
            var admin = _userManager.FindByIdAsync(id).Result;
            if(admin == null)
            {
                return NotFound();
            }

            admin.FirstName = model.FirstName;
            admin.LastName = model.LastName;
            admin.City = model.City;
            admin.Address = model.Address;
            admin.Email = model.Email;
            admin.PhoneNumber = model.PhoneNo;
            admin.AboutYou = model.AboutYou;

            var result = _userManager.UpdateAsync(admin).Result;

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
