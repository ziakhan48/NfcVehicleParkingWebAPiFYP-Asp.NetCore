using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NfcVehicleParkingAPi.Areas.Handler.ViewModels;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NfcVehicleParkingAPi.Areas.Handler.Controllers
{
    [Authorize(Roles = "Handler")]
    [Route("Handler/api/[controller]")]
    public class HandlerProfileController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private ClaimsPrincipal _caller;

        public HandlerProfileController(UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
        }
        // GET: api/HandlerProfile
        [HttpGet]
        [Route("getProfile")]
        public async Task<IActionResult> GetHandlerProfile()
        {
            HandlerProfileViewModel model = new HandlerProfileViewModel();
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser = await _userManager.FindByIdAsync(userId.Value);
            string imgsrc = null;
            if (OnlineUser.Avatarimage != null)
            {
                var base64 = Convert.ToBase64String(OnlineUser.Avatarimage);
                imgsrc = string.Format("data:image/gif;base64,{0}", base64);
            }

            model.Id = OnlineUser.Id;
            model.FirstName = OnlineUser.FirstName;
            model.LastName = OnlineUser.LastName;
            model.Email = OnlineUser.Email;
            model.Address = OnlineUser.Address;
            model.AboutYou = OnlineUser.AboutYou;
            model.Phone = OnlineUser.PhoneNumber;
            model.City = OnlineUser.City;

            if (imgsrc != null)
            {
                model.Image = imgsrc;
            }

            return new ObjectResult(model);
        }

        // GET: api/HandlerProfile/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/HandlerProfile
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/HandlerProfile/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Puthandleredit([FromBody]HandlerProfileViewModel model , string id)
        {

            var OnlineUser = await _userManager.FindByIdAsync(id);

            if(OnlineUser == null)
            {
                return NotFound();
            }
            OnlineUser.FirstName = model.FirstName;
            OnlineUser.LastName = model.LastName;
            OnlineUser.Email = model.Email;
            OnlineUser.PhoneNumber = model.Phone;
            OnlineUser.Address = model.Address;
            OnlineUser.AboutYou = model.AboutYou;
            OnlineUser.City = model.City;

            var result = await _userManager.UpdateAsync(OnlineUser);
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
