using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.MiniKpay.Api.Endpoints;
using MTTKDotNetCore.MiniKpay.Domain.Features;

namespace MTTKDotNetCore.AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawController : BaseController
    {
        private readonly WithdrawService _withdrawService = new WithdrawService();

        [HttpPost]
        public IActionResult Withdraw(string mobileNo, decimal balance)
        {
            var result = _withdrawService.CreateWithdraw(mobileNo, balance);
            return Execute(result);
        }
    }
}
