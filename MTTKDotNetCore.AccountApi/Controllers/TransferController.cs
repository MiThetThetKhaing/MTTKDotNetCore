using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.Database.Models;
using MTTKDotNetCore.Domain.Features.Account;
using MTTKDotNetCore.Domain.Models;
using MTTKDotNetCore.MiniKpayApi.Controllers;

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
            var result = await _transferService.CreateTransfer(fromMobile, toMobile, amount, pin, notes);

            return Execute(result);
        }
    }
}
