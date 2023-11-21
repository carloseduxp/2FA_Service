using _2FA_Service.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _2FA_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class _2FAController : Controller
    {
        private readonly IGatewayService _gatewayService;
        private readonly IConfiguration _configuration;

        public _2FAController(IGatewayService gatewayService, IConfiguration configuration)
        {
            _gatewayService = gatewayService;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Index([FromBody]string phoneNumber)
        {
            var concurrentCodesPerPhone = _configuration.GetValue<int>("ConcurrentCodesPerPhone");
            var codeLifetime = _configuration.GetValue<int>("CodeLifetime"); //in seconds
            var codes = _gatewayService.GetTelephoneNumberCodes(phoneNumber);

            var availableCodes = codes.Where(x => (DateTime.Now - x.CreatedDate).TotalSeconds <= codeLifetime).ToList();
            if (availableCodes.Count < concurrentCodesPerPhone)
            {
               var phoneNumberCode = _gatewayService.GenerateTelephoneNumberCode(phoneNumber);
                return Content(JsonConvert.SerializeObject(phoneNumberCode), "application/json");
            }
            else
            {
                return Content($"Max of {concurrentCodesPerPhone} ConcurrentCodesPerPhone available reached.");
            }
        }

        [HttpGet]
        public IActionResult VerifyCode(string phoneNumber, string phoneCode)
        {
            var codeLifetime = _configuration.GetValue<int>("CodeLifetime");

            if (!string.IsNullOrEmpty(phoneNumber) && !string.IsNullOrEmpty(phoneCode))
            {
                var availableCodes = _gatewayService.GetTelephoneNumberCodes(phoneNumber);
                var code2 = availableCodes.FirstOrDefault(x => x.Code == phoneCode);
                if (code2 != null)
                {
                    if ((DateTime.Now - code2.CreatedDate).TotalSeconds <= codeLifetime)
                    {
                        return Ok("Valid.");
                    }
                    else
                    {
                        return Unauthorized("Code expired.");
                    }
                }
                else
                {
                    return Unauthorized("The phone Code is not vaid.");
                }
            }
            else
            {
                return BadRequest("phoneNumber and phoneCode must be send.");
            }
        }
    }
}
