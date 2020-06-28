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
    [Authorize()]
    [Route("User/api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private ClaimsPrincipal _caller;
        public ProfileController(UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
        }
        // GET: api/Profile
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ProfileViewModel model = new ProfileViewModel();
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser = await _userManager.FindByIdAsync(userId.Value);
          
            model.Name = OnlineUser.FullName;
            model.Email = OnlineUser.Email;

            return new ObjectResult(model);
        }

      

        // POST: api/Profile
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Profile/5
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
