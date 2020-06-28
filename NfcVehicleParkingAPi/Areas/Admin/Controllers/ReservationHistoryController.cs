using System.Collections.Generic;
using System.Linq;
using NfcVehicleParkingAPi.Areas.Admin.ViewModels;
using NfcVehicleParkingAPi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NfcVehicleParkingAPi.Areas.Admin.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("Admin/api/[controller]")]
    [ApiController]
    public class ReservationHistoryController : ControllerBase
    {
        private AuthDbContext _context;

        public ReservationHistoryController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            SlotReservationViewModel model = new SlotReservationViewModel();
            List<SlotReservationViewModel> listmodel = new List<SlotReservationViewModel>();
            if (id == null)
            {
                return NotFound();
            }
            var reservationList = _context.slotReservations
                .Include(p => p.slot)
                .Where(p => p.slot.SlotId == id);

            foreach (var reservation in reservationList)
            {
                model.SlotId = reservation.slot.SlotId;
                model.CustomerName = reservation.CustomerName;
                model.CustomerEmail = reservation.CustomerEmail;
                model.CustomerPhoneNo = reservation.CustomerPhoneNo;
                model.CarNo = reservation.CarNo;
                model.SlotNo = reservation.slot.No;
                model.ReservationTime = reservation.ReservationTime;
                model.ReservationEndTime = reservation.ReservationEndTime;
                model.ReservationId = reservation.SlotReservationId;

                listmodel.Add(model);
            }

            return new OkObjectResult(listmodel);
        }

    }
}