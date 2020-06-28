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

namespace NfcVehicleParkingAPi.Areas.Admin.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("Admin/api/[controller]")]
    [ApiController]
    public class AdminChangePasswordController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private ClaimsPrincipal _caller;

        public AdminChangePasswordController(UserManager<AppUser> userManager
            , IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
        }


        // POST: api/AdminChangePassword
        [HttpPost]
        public async Task<IActionResult> Post(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser = await _userManager.FindByIdAsync(userId.Value);
            var result = await _userManager.ChangePasswordAsync(OnlineUser, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();

        }

    }

    
}