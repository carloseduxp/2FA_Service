using _2FA_Service.Helpers;
using _2FA_Service.Models;

namespace _2FA_Service.Services
{
    public class SQLDatabaseGateway : IGatewayService
    {
        private readonly _2FA_ServiceContext _context;
        private const int fixedCodeLength = 20;
        public SQLDatabaseGateway(_2FA_ServiceContext context)
        {
            _context = context;
        }
        public PhoneNumberCode GenerateTelephoneNumberCode(string telephoneNumber)
        {
            var code = RandomGenerator.RandomString(fixedCodeLength);

            PhoneNumberCode phoneNumberCode = new()
            {
                Code = code,
                PhoneNumber = telephoneNumber,
                CreatedDate = DateTime.Now
            };

            _context.PhoneNumberCodes.Add(phoneNumberCode);
            _context.SaveChanges();
            return phoneNumberCode;
        }

        public List<PhoneNumberCode> GetTelephoneNumberCodes(string telephoneNumber)
        {
            var codes = _context.PhoneNumberCodes.Where(x => x.PhoneNumber == telephoneNumber).ToList();
            return codes;
        }
    }
}
