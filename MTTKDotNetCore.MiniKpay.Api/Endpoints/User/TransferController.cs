using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.MiniKpay.Domain.Features;
using MTTKDotNetCore.MiniKpay.Api.Endpoints;

namespace MTTKDotNetCore.AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : BaseController
    {
        private readonly TransferService _transferService = new TransferService();

        [HttpPost]
        public async Task<IActionResult> Transfer (string fromMobile, string toMobile, decimal amount, string pin, string notes)
        {
            var result = await _transferService.Transfer(fromMobile, toMobile, amount, pin, notes);
            return Execute(result);
        }

        [HttpGet]
        public IActionResult GetAllHistories(string phoneNo)
        {
            var result = _transferService.GetHistories(phoneNo);
            return Execute(result);
        }

        [HttpGet("{phoneNo}")]
        public IActionResult GetLastHistory(string phoneNo)
        {
            var result = _transferService.GetHistory(phoneNo);
            return Execute(result);
        }
    }
}
