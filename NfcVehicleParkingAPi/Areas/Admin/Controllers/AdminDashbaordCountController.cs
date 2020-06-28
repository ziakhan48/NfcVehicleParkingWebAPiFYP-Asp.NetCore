using System.Linq;
using NfcVehicleParkingAPi.Areas.Admin.ViewModels;
using NfcVehicleParkingAPi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NfcVehicleParkingAPi.Areas.Admin.Controllers
{
    //[Authorize(Policy = "ApiUser")]
    [Authorize(Roles ="Admin")]
    [Route("Admin/api/[controller]")]
    [ApiController]
    public class AdminDashbaordCountController : ControllerBase
    {
        private AuthDbContext _dbContext;

        public AdminDashbaordCountController(AuthDbContext dbContext)
        {

            _dbContext = dbContext;
        }
        // GET: api/HandlerDashbaordCount
        [HttpGet]
        [Route("AdminDCount")]
        public IActionResult GetCount()
        {
            AdminDashbaordCountViewModel model = new AdminDashbaordCountViewModel();
            model.UserCount = _dbContext.appUsers.Count();
            model.CustomerCount = _dbContext.appUsers.Where(p => p.appRole.Name == "Customer").Count();
            model.HandlerCount = _dbContext.appUsers.Where(p => p.appRole.Name == "Handler").Count();
            model.ParkingCount = _dbContext.parkings.Count();
            model.RoleCount = _dbContext.appRoles.Count();

            return new  OkObjectResult(model);
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
