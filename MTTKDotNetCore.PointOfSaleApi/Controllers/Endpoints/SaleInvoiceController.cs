using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Features;
using System.Net.NetworkInformation;

namespace MTTKDotNetCore.PointOfSale.Api.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleInvoiceController : BaseController
    {
        private readonly SaleInvoiceService _saleInvoiceService;

        public SaleInvoiceController()
        {
            _saleInvoiceService = new SaleInvoiceService();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSaleInvoice(TblSaleInvoice saleInvoice)
        {
            var result = await _saleInvoiceService.CreateSaleInvoice(saleInvoice);
            return Execute(result);
        }
    }
}
