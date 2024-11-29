using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Features;
using MTTKDotNetCore.PointOfSale.Domain.Models;

namespace MTTKDotNetCore.PointOfSale.Api.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleInvoiceDetailsController : BaseController
    {
        private readonly SaleInvoiceDetailsService _saleInvoiceDetailsService;
        public SaleInvoiceDetailsController()
        {
            _saleInvoiceDetailsService = new SaleInvoiceDetailsService();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSaleInvoiceDetail(TblSaleInvoiceDetail saleInvoiceDetail)
        {
            var result = await _saleInvoiceDetailsService.CreateSaleInvoiceDetail(saleInvoiceDetail);
            return Execute(result);
        }
    }
}
