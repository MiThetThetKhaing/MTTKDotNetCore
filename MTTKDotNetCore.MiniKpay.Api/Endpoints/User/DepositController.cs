using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.MiniKpay.Api.Endpoints;
using MTTKDotNetCore.MiniKpay.Domain.Features;

namespace MTTKDotNetCore.AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : BaseController
    {
        private readonly DepositService _serviceDeposit = new DepositService();

        [HttpPost]
        public IActionResult Deposit(string mobileNo, decimal balance)
        {
            var result = _serviceDeposit.CreateDeposit(mobileNo, balance);
            return Execute(result);
        }
    }
}
