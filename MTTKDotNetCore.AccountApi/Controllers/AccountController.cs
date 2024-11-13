using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.Database.Models;
using MTTKDotNetCore.Domain.Features.Account;

namespace MTTKDotNetCore.AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController()
        {
            _accountService = new AccountService();
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            var lst = _accountService.GetAccounts();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetAccount(int id)
        {
            var item = _accountService.GetAccount(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateAccount(TblAccount account)
        {
            var result = _accountService.CreateAccount(account);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, TblAccount account)
        {
            var item = _accountService.UpdateAccount(id, account);
            if (item is null)
            {
                return NotFound("Your input balance is Incorrect or Your id is not found!");
            }
            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchAccount(int id, TblAccount account)
        {
            var item = _accountService.PatchAccount(id, account);
            if (item is null)
            {
                return NotFound("Your input balance is Incorrect or Your id is not found!");
            }
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            var item = _accountService.DeleteAccount(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok("Account is successfully deleted!");
        }
    }
}
