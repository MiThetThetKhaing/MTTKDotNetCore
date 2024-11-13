using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.Domain.Features.Account;

namespace MTTKDotNetCore.AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawController : Controller
    {
        private readonly WithdrawService _withdrawService = new WithdrawService();

        [HttpPost]
        public IActionResult Withdraw(string mobileNo, decimal balance)
        {
            var result = _withdrawService.CreateWithdraw(mobileNo, balance);

            return Ok(result);
        }
    }
}
