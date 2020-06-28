using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NfcVehicleParkingAPi.Areas.User.ViewModels;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NfcVehicleParkingAPi.Areas.User.Controllers
{
    [Route("User/api/[controller]")]
    [ApiController]
    public class SlotReservationHistoryController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private ClaimsPrincipal _caller;
        private AuthDbContext _context;

        public SlotReservationHistoryController(UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor, AuthDbContext context)
        {
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
            _context = context;
        }
        // GET: api/SlotReservationHistory
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<SlotReservationListviewModel> list = new List<SlotReservationListviewModel>();
            SlotReservationListviewModel model = null;
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser = await _userManager.FindByIdAsync(userId.Value);
            //var OnlineUser = await _userManager.FindByEmailAsync("waqarahmad117429@gmail.com");
            if (OnlineUser == null)
            {

            }

            var slotreservations = _context.slotReservations.Include(p=>p.slot).Include(p=>p.slot.Parking).Include(p=>p.slot.Parking.appUser)
                .Where(p => p.CustomerName == OnlineUser.FullName || p.CustomerPhoneNo == OnlineUser.PhoneNumber)
                .Where(p => p.slot.Reserved == false)
                .ToList();

            foreach (var slot in slotreservations)
            {
                model = new SlotReservationListviewModel()
                {
                    Id = slot.SlotReservationId,
                    Reserved = slot.slot.Reserved,
                    Parking = slot.slot.Parking.Name,
                    No = slot.slot.No,
                    Parkid=slot.slot.Parking.ParkingId

                };

                list.Add(model);
            }

            return new OkObjectResult(list);
        }

        

        // POST: api/SlotHistory
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/SlotHistory/5
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
