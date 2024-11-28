using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Features;

namespace MTTKDotNetCore.PointOfSale.Api.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly ProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetProducts();
            return Execute(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _productService.GetProduct(id);
            return Execute(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(TblProduct product)
        {
            var result = await _productService.CreateProduct(product);
            return Execute(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, TblProduct product)
        {
            var result = await _productService.UpdateProduct(id, product);
            return Execute(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return Execute(result);
        }
    }
}
