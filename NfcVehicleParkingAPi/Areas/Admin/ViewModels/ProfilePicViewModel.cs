using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.Admin.ViewModels
{
    public class ProfilePicViewModel
    {
        //public byte[] AvatarImages { get; set; }
        public IFormFile avatarImages { get; set; }
    }
}
