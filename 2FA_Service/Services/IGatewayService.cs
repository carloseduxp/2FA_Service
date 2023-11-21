using _2FA_Service.Models;

namespace _2FA_Service.Services
{
    public interface IGatewayService
    {
        PhoneNumberCode GenerateTelephoneNumberCode(string telephoneNumber);
        List<PhoneNumberCode> GetTelephoneNumberCodes(string telephoneNumber);
    }
}
