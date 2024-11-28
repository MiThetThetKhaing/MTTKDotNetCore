using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.PointOfSale.Api;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Features;
using MTTKDotNetCore.PointOfSale.Domain.Models;

namespace MTTKDotNetCore.PointOfSaleApi.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : BaseController
    {
        private readonly ProductCategoryService _service;

        public ProductCategoryController()
        {
            _service = new ProductCategoryService();
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _service.GetProductCategories();
            return Execute(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetProductCategory(id);
            return Execute(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TblProductCategory productCategory)
        {
            var result = await _service.CreateProductCategories(productCategory);
            return Execute(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, TblProductCategory productCategory)
        {
            var result = await _service.UpdateProductCategory(id, productCategory);
            return Execute(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteProductCategory(id);
            return Execute(result);
        }
    }
}
