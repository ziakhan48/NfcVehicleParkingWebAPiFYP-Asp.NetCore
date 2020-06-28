using System;
using System.Threading.Tasks;
using NfcVehicleParkingAPi.Helpers;
using NfcVehicleParkingAPi.Models;
using NfcVehicleParkingAPi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NfcVehicleParkingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppRoleController : ControllerBase
    {
        private RoleManager<AppRole> _roleManager;

        public AppRoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async  Task<IActionResult> Post([FromBody]AppRoleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    var result = await _roleManager.CreateAsync(new AppRole()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Created = DateTime.Now

                    });

                    if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
                }
            }
            return new OkObjectResult("Role created");
        }
    }
}