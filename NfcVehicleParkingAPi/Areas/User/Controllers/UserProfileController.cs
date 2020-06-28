using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NfcVehicleParkingAPi.Areas.User.ViewModels;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NfcVehicleParkingAPi.Areas.User.Controllers
{
    //[Authorize(Policy = "ApiUser")]
    //[Authorize(Roles ="User)
    //[Authorize()]
    [Route("User/api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private ClaimsPrincipal _caller;
        public UserProfileController(UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
        }
        // GET: api/UserProfile
        [HttpGet]
       
        public async Task<IActionResult> Get()
        {

            UserProfileViewModel model = new UserProfileViewModel();
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser = await _userManager.FindByIdAsync(userId.Value);
            //var OnlineUser = await _userManager.FindByEmailAsync("waqarahmad117429@gmail.com");
            string password = null;
            model.UserId = OnlineUser.Id;
            model.Name = OnlineUser.FullName;
            model.Email = OnlineUser.Email;
            model.Address = OnlineUser.Address;
            model.phone = OnlineUser.PhoneNumber;
            model.City = OnlineUser.City;
            password = OnlineUser.PasswordHash;
            return new OkObjectResult(model);
        }



        // POST: api/UserProfile
        [HttpPost]
        public  async Task<IActionResult> Post([FromBody]UserProfileUpdateViewModel model)
        {
            
            if (model == null)
            {
                return null;
            }

            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser = await _userManager.FindByIdAsync(userId.Value);
            
            if (OnlineUser == null)
            {
                return BadRequest();
            }
           
                OnlineUser.FullName = model.FullName;
                OnlineUser.Address = model.Address;
                OnlineUser.Email = model.Email;
                OnlineUser.PhoneNumber = model.Phone;
                OnlineUser.City = model.City;

            var result = await _userManager.UpdateAsync(OnlineUser);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/UserProfile/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
