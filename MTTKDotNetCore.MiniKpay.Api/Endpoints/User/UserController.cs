using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.MiniKpay.Database.Models;
using MTTKDotNetCore.MiniKpay.Domain.Features;

namespace MTTKDotNetCore.MiniKpay.Api.Endpoints.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        [HttpGet("{id}")]
        public IActionResult GetAccount(int id)
        {
            var item = _userService.GetAccount(id);
            return Execute(item);
        }

        [HttpPost]
        public IActionResult CreateUser(TblAccount account)
        {
            var result = _userService.CreateUserAccount(account);
            return Execute(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, TblAccount account)
        {
            var item = _userService.UpdateUserAccount(id, account);
            return Execute(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchAccount(int id, TblAccount account)
        {
            var item = _userService.PatchUserAccount(id, account);
            return Execute(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            var item = _userService.DeleteAccount(id);
            return Execute(item);
        }
    }
}
