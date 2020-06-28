using System.Linq;
using NfcVehicleParkingAPi.Areas.Handler.ViewModels;
using NfcVehicleParkingAPi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NfcVehicleParkingAPi.Areas.Handler.Controllers
{
    [Authorize(Roles = "Handler")]
    [Route("Handler/api/[controller]")]
    [ApiController]
    public class HandlerDasboardController : ControllerBase
    {
        private AuthDbContext _dbContext;

        public HandlerDasboardController(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public  IActionResult Gethandlerdashboard()
        {
            HandlerDashboardCountViewModel model = new HandlerDashboardCountViewModel();

            model.TotalSlotsCount = _dbContext.slots.Count();
            model.ReservedSlotCount = _dbContext.slots.Where(p => p.Reserved == true).Count();
            model.NotReservedSlotCount = _dbContext.slots.Where(p => p.Reserved == false).Count();
            model.SuccessfullPaymentCount = _dbContext.payments.Count();
            model.TotalReservation = _dbContext.slotReservations.Count();

            return new OkObjectResult(model);
        }
    }
}