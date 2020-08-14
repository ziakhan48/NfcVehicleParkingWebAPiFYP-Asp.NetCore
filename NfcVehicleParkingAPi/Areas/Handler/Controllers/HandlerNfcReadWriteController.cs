using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NfcVehicleParkingAPi.Areas.Handler.ViewModels;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;

namespace NfcVehicleParkingAPi.Areas.Handler.Controllers
{
    [Authorize(Roles = "Handler")]
    [Route("Handler/api/[controller]")]
    [ApiController]
    public class HandlerNfcReadWriteController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ClaimsPrincipal _caller;

        public HandlerNfcReadWriteController(AuthDbContext context, UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NfcReadWriteResponse model)
        {
            if (model == null)
                return BadRequest();


            return Ok();

        }
    }
}
