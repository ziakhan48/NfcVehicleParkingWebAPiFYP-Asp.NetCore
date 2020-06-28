using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using NfcVehicleParkingAPi.Areas.Handler.ViewModels;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NfcVehicleParkingAPi.Areas.Handler.Controllers
{
    //[Area("Handler")]
    //[Authorize(Policy = "ApiUser")]
    [Authorize(Roles ="Handler")]
    [Route("Handler/api/[controller]")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ClaimsPrincipal _caller;

        public SlotsController(AuthDbContext context, UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
        }

        // GET: api/Slots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slot>>> Getslots()
        {
            SlotViewModel model = null;
            List<SlotViewModel> list = new List<SlotViewModel>();
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser = await _userManager.FindByIdAsync(userId.Value);
            var parking = _context.parkings.Include(p=>p.appUser)
                .FirstOrDefault(p => p.appUser.Id == OnlineUser.Id);

            var slots = await _context.slots.Include(p => p.Parking).
                Where(p => p.Parking.ParkingId == parking.ParkingId)
                .ToListAsync();

            foreach(var slot in slots)
            {
                model = new SlotViewModel()
                {
                    SlotNo=slot.No,
                    SlotId=slot.SlotId,
                    Parking=slot.Parking.Name,
                    IsReserved=slot.Reserved
                };

                list.Add(model);
            }

            return new OkObjectResult(list);

           
        }

        // GET: api/Slots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slot>> GetSlot(int id)
        {
            var slot = await _context.slots.FindAsync(id);

            if (slot == null)
            {
                return NotFound();
            }

            return slot;
        }

        // PUT: api/Slots/5
        [HttpPut("{slotId}")]
        public async Task<IActionResult> PutSlot(int slotId, SlotCreateViewModel model)
        {
            if(slotId == 0)
            {
                return null;
            }

            var sLot = _context.slots.FirstOrDefault(p => p.SlotId == slotId);

            if(sLot == null)
            {
                return NotFound();
            }

            sLot.No = model.SlotNo;

            _context.Update(sLot);

            if(_context.SaveChanges() == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Slots
        [HttpPost]
        public async Task<ActionResult<Slot>> PostSlot(SlotCreateViewModel model)
        {
            if(model == null)
            {

            }
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var OnlineUser = await _userManager.FindByIdAsync(userId.Value);

            var slot = new Slot()
            {
                No=model.SlotNo,
                Parking= _context.parkings
                .FirstOrDefault(p => p.appUser.Id == OnlineUser.Id),
                Reserved=false
            };
            _context.slots.Add(slot);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Slots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Slot>> DeleteSlot(int id)
        {
            var slot = await _context.slots.FindAsync(id);
            if (slot == null)
            {
                return NotFound();
            }

            _context.slots.Remove(slot);
            await _context.SaveChangesAsync();

            return slot;
        }

        private bool SlotExists(int id)
        {
            return _context.slots.Any(e => e.SlotId == id);
        }
    }
}
