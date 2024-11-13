using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.Database.Models;
using MTTKDotNetCore.Domain.Features.Account;

namespace MTTKDotNetCore.AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : Controller
    {
        private readonly TransferService _transferService = new TransferService();

        [HttpPost]
        public IActionResult Transfer (string fromMobile, string toMobile, decimal amount, string pin, string notes)
        {
            var result = _transferService.CreateTransfer(fromMobile, toMobile, amount, pin, notes);

            return Ok(result);
        }
    }
}
