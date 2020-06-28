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
    public class HandlerDashbaordCountController : ControllerBase
    {
        private AuthDbContext _dbContext;

        public HandlerDashbaordCountController(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/HandlerDashbaordCount
        [Route("Count")]
        [HttpGet]
        public IActionResult Get()
        {
            HandlerDashboardCountViewModel model = new HandlerDashboardCountViewModel();

            model.TotalSlotsCount = _dbContext.slots.Count();
            model.ReservedSlotCount = _dbContext.slots.Where(p => p.Reserved == true).Count();
            model.NotReservedSlotCount = _dbContext.slots.Where(p => p.Reserved == false).Count();
            model.SuccessfullPaymentCount = _dbContext.payments.Count();
            model.TotalReservation = _dbContext.slotReservations.Count();

            return new OkObjectResult(model);
        }

        // GET: api/HandlerDashbaordCount/5
        [HttpGet("{id}", Name = "GetBydashId")]
        public string GetBydashId(int id)
        {
            return "value";
        }

        // POST: api/HandlerDashbaordCount
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/HandlerDashbaordCount/5
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
