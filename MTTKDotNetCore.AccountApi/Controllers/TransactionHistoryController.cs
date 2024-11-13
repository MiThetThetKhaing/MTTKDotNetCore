using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.Domain.Features.Account;

namespace MTTKDotNetCore.AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : Controller
    {
        TransactionHistoryService _serviceHistory = new TransactionHistoryService();

        [HttpGet]
        public IActionResult GetAllHistories(string phoneNo)
        {
            var result = _serviceHistory.GetHistories(phoneNo);
            return Ok(result);
        }

        [HttpGet("{phoneNo}")]
        public IActionResult GetLastHistory(string phoneNo)
        {
            var result = _serviceHistory.GetHistory(phoneNo);
            return Ok(result);
        }
    }
}
