
using NfcVehicleParkingAPi.ViewModels.Validations;
using ServiceStack.FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.ViewModels
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
       
       
            public string UserName { get; set; }
            public string Password { get; set; }
        
    }
}

