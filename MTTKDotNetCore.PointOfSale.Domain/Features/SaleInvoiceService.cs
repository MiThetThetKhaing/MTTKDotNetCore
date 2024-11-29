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
    public class SaleInvoiceService
    {
        private readonly AppDbContext _db;

        public SaleInvoiceService()
        {
            _db = new AppDbContext();
        }

        public async Task<Result<SaleInvoiceResponseModel>> CreateSaleInvoice(TblSaleInvoice saleInvoice)
        {
            try
            {
                var item = await _db.TblSaleInvoices.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.VoucherNo == saleInvoice.VoucherNo);

                #region "Voucher Validation"
                if (item != null)
                    return Result<SaleInvoiceResponseModel>.ValidationError("Voucher No. already exist.");

                if (saleInvoice.VoucherNo.Length > 17 || saleInvoice.VoucherNo.Length < 17)
                {
                    return Result<SaleInvoiceResponseModel>.ValidationError("Invalid Voucher No. length.");
                }
                #endregion

                #region "Staff Code Validation"
                if (saleInvoice.StaffCode.Length > 8 || saleInvoice.StaffCode.Length < 8)
                {
                    return Result<SaleInvoiceResponseModel>.ValidationError("Invalid StaffCode Length");
                }
                #endregion

                #region "Payment Validation"
                if (saleInvoice.PaymentType!.ToLower() == Payment.Kpay.ToString().ToLower())
                {
                    saleInvoice.PaymentType = Payment.Kpay.ToString();
                }
                else if (saleInvoice.PaymentType!.ToLower() == Payment.Card.ToString().ToLower())
                {
                    saleInvoice.PaymentType = Payment.Card.ToString();
                }
                else if (saleInvoice.PaymentType!.ToLower() == Payment.Cash.ToString().ToLower())
                {
                    saleInvoice.PaymentType = Payment.Cash.ToString();
                }
                else
                {
                    return Result<SaleInvoiceResponseModel>.ValidationError("Payment type needed.");
                }
                #endregion

                #region "Payment Amount calculation"
                saleInvoice.PaymentAmount = saleInvoice.TotalAmount - saleInvoice.Discount;

                if (saleInvoice.PaymentAmount > saleInvoice.ReceiveAmount)
                {
                    return Result<SaleInvoiceResponseModel>.ValidationError("Insufficient balance.");
                }
                saleInvoice.Change = saleInvoice.ReceiveAmount - saleInvoice.PaymentAmount;
                #endregion

                await _db.TblSaleInvoices.AddAsync(saleInvoice);
                await _db.SaveChangesAsync();

                var result = new SaleInvoiceResponseModel
                {
                    TblSaleInvoice = saleInvoice
                };
                return Result<SaleInvoiceResponseModel>.Success(result, "SaleInvoice is successfully created.");
            }
            catch (Exception ex)
            {
                return Result<SaleInvoiceResponseModel>.SystemError(ex.Message);
            }
        }
    }

    public enum Payment
    {
        None,
        Kpay,
        Card,
        Cash
    }
}
