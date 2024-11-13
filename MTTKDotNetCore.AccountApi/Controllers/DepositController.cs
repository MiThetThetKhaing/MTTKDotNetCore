using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.Domain.Features.Account;

namespace MTTKDotNetCore.AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : Controller
    {
        private readonly DepositService _serviceDeposit = new DepositService();

        //[HttpGet]
        //public IActionResult GetDeposits()
        //{
            
        //}

        //[HttpGet]
        //public IActionResult GetDeposit()
        //{

        //}

        [HttpPost]
        public IActionResult Deposit(string mobileNo, decimal balance)
        {
            var result = _serviceDeposit.CreateDeposit(mobileNo, balance);

            return Ok(result);
        }
    }
}
