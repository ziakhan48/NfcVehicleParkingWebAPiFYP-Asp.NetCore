using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Areas.Handler.ViewModels
{
    public class NfcReadWriteResponse
    {
        public string URL { get; set; }
        public string Tag { get; set; }
        public byte[] Byte_Array { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
    }
}
