using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NfcVehicleParkingAPi.Areas.Admin.ViewModels;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NfcVehicleParkingAPi.Areas.User.Controllers
{
    //[Authorize()]
    [Route("User/api/[controller]")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private ClaimsPrincipal _caller;
        public ChangePasswordController(UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
        }
        // GET: api/ChangePassword
       

     

        // POST: api/ChangePassword
        [HttpPost]
        public async Task<IActionResult>  Post([FromBody] ChangePasswordViewModel model)
        {
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser = await _userManager.FindByIdAsync(userId.Value);
            var result = await _userManager.ChangePasswordAsync(OnlineUser, model.CurrentPassword, model.NewPassword);
            return Ok(result);  
        }

        // PUT: api/ChangePassword/5
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
