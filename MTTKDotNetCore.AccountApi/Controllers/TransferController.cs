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
            var result2 = await _transferService.CreateTransfer(fromMobile, toMobile, amount, pin, notes);

            //if (result.Response.IsSuccess) 
            //    return Ok(result);

            //if (result.Response.RespType == EnumRespType.ValidationError)
            //    return BadRequest(result);

            //if (result.Response.RespType == EnumRespType.SystemError)
            //    return StatusCode(500, result);

            //return Ok(result);

            //return Execute(result);
            return Execute(result2);
        }
    }
}
