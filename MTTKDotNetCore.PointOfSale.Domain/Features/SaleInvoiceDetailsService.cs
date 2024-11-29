using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.PointOfSale.Domain.Features
{
    public class SaleInvoiceDetailsService
    {
        private readonly AppDbContext _db;

        public SaleInvoiceDetailsService()
        {
            _db = new AppDbContext();
        }

        public async Task<Result<SaleInvoiceDetailResponseModel>> CreateSaleInvoiceDetail(TblSaleInvoiceDetail saleInvoiceDetail)
        {
            try
            {
                var item = await _db.TblSaleInvoiceDetails.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.VoucherNo == saleInvoiceDetail.VoucherNo);
                var voucherNo = await _db.TblSaleInvoices.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.VoucherNo == saleInvoiceDetail.VoucherNo);
                var productCode = await _db.TblProducts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.ProductCode == saleInvoiceDetail.ProductCode);

                #region "Voucher Validation"
                if (item != null)
                    return Result<SaleInvoiceDetailResponseModel>.ValidationError("Voucher No. is already exist.");

                if (voucherNo is null)
                    return Result<SaleInvoiceDetailResponseModel>.ValidationError("Invalid Voucher.");

                if (saleInvoiceDetail.VoucherNo.Length > 17 || saleInvoiceDetail.VoucherNo.Length < 17)
                    return Result<SaleInvoiceDetailResponseModel>.ValidationError("Invalid Voucher length.");
                #endregion

                #region "Product Code Validation"
                if (productCode is null)
                    return Result<SaleInvoiceDetailResponseModel>.ValidationError("Invalid Product Code.");
                #endregion

                #region "Quantity Validation"
                if (saleInvoiceDetail.Quantity == 0)
                    return Result<SaleInvoiceDetailResponseModel>.ValidationError("Invalid Quantity.");
                #endregion

                #region "Calculation Validation"
                saleInvoiceDetail.Price = productCode.Price;
                saleInvoiceDetail.Amount = saleInvoiceDetail.Quantity * saleInvoiceDetail.Price;
                #endregion

                await _db.TblSaleInvoiceDetails.AddAsync(saleInvoiceDetail);
                await _db.SaveChangesAsync();

                var result = new SaleInvoiceDetailResponseModel()
                {
                    TblSaleInvoiceDetail = saleInvoiceDetail
                };

                return Result<SaleInvoiceDetailResponseModel>.Success(result, "Sale Invoice Detail is Successfully created.");
            }
            catch (Exception ex)
            {
                return Result<SaleInvoiceDetailResponseModel>.SystemError(ex.Message);
            }
        }
    }
}
