using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.PointOfSale.Domain.Models;

namespace MTTKDotNetCore.PointOfSale.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult Execute<T>(Result<T> model)
        {
            if (model.IsValidationError)
                return BadRequest(model);

            if (model.IsSystemError)
                return StatusCode(500, model);

            return Ok(model);
        }
    }
}
