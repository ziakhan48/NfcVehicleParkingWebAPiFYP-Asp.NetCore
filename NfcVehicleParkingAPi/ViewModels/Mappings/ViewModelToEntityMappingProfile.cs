using NfcVehicleParkingAPi.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.ViewModels.Mappings
{
    public class ViewModelToEntityMappingProfile:Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegisterationViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}
