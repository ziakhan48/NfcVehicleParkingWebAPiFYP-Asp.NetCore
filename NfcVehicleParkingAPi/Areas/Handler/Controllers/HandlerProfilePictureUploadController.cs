using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NfcVehicleParkingAPi.Areas.Handler.Controllers
{
    [Route("Handler/api/[controller]")]
    [ApiController]
    public class HandlerProfilePictureUploadController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private ClaimsPrincipal _caller;

        public HandlerProfilePictureUploadController(UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
        }

        // POST: api/HandlerProfilePictureUpload
        [HttpPost("{id}")]
        public IActionResult Post([FromForm]IFormFile file, string id)
        {

            var adminuser = _userManager.FindByIdAsync(id).Result;



            if (adminuser == null)
            {
                return NotFound();
            }
            using (var memoryStream = new MemoryStream())
            {
                file.CopyToAsync(memoryStream);
                adminuser.Avatarimage = memoryStream.ToArray();

            }

            var result = _userManager.UpdateAsync(adminuser).Result;

            if (!result.Succeeded)
            {
                return BadRequest();
            }



            return Ok();
        }
    }
}