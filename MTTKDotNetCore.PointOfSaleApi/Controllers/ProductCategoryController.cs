using Microsoft.AspNetCore.Mvc;

namespace MTTKDotNetCore.PointOfSaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : Controller
    {
        [HttpGet]
        public IActionResult GetProductCategories()
        {
            return Ok();
        }
    }
}
