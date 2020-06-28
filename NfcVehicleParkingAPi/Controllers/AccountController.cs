using System;
using System.Threading.Tasks;
using NfcVehicleParkingAPi.Data;
using NfcVehicleParkingAPi.Helpers;
using NfcVehicleParkingAPi.Models;
using NfcVehicleParkingAPi.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NfcVehicleParkingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AuthDbContext _appDbContext;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, 
            IMapper mapper, AuthDbContext appDbContext,
            RoleManager<AppRole> roleManager)

        {
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
            _roleManager = roleManager;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            //var approle = new AppRole()
            //{
            //    Name = "Customer",
            //    Description = "End User",
            //    Created = DateTime.Now
            //};

            //var result2 = await _roleManager.CreateAsync(approle);

            //if (!result2.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result2, ModelState));
            var roles = _roleManager.Roles;
            int count = 0;
            foreach (var role in roles)
            {
                var userrole = _userManager.IsInRoleAsync(userIdentity, role.Name).Result;
                if (userrole == true)
                {
                    count++;
                }

            }
            if (count == 0)
            {

                var result1 = await _userManager.AddToRoleAsync(userIdentity, "User");
                await _userManager.AddClaimAsync(userIdentity, new System.Security.Claims.Claim("role", "User"));
            }
            await _appDbContext.users.AddAsync(new User { IdentityId = userIdentity.Id, Location = "Islamabad" });
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }

        [HttpGet]
        public string Get()
        {
            return "my profile";
        }

    }
}