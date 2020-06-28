using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
