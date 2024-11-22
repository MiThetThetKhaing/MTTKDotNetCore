using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.MiniKpay.Database.Models;
using MTTKDotNetCore.MiniKpay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.MiniKpay.Domain.Features
{
    public class TransferService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public Result<ResultTransferResponseModel> GetHistories(string phone)
        {
            try
            {
                var result = _db.TblTransactionHistories
                .AsNoTracking()
                .Where(x => x.DeleteFlag == false)
                .Where(x => x.FromMobileNo == phone)
                .OrderByDescending(x => x.TranDate)
                .ToList();
                if (result.Count == 0)
                {
                    return Result<ResultTransferResponseModel>.ValidationError("No History was found.");
                }

                ResultTransferResponseModel model = new ResultTransferResponseModel
                {
                    TransactionHistory = result
                };

                return Result<ResultTransferResponseModel>.Success(model, "History found.");
            }
            catch (Exception ex)
            {
                return Result<ResultTransferResponseModel>.SystemError(ex.Message);
            }
        }

        public Result<ResultTransferResponseModel> GetHistory(string phone)
        {
            try
            {
                var result = _db.TblTransactionHistories
                .AsNoTracking()
                .Where(x => x.DeleteFlag == false)
                .OrderByDescending(x => x.TranDate)
                .FirstOrDefault(x => x.FromMobileNo == phone);

                if (result is null)
                {
                    return Result<ResultTransferResponseModel>.ValidationError("Phone no doesn't exist.");
                }

                ResultTransferResponseModel model = new ResultTransferResponseModel
                {
                    TransactionHistory = result
                };

                return Result<ResultTransferResponseModel>.Success(model, "History found.");
            }
            catch (Exception ex)
            {
                return Result<ResultTransferResponseModel>.SystemError(ex.Message);
            }
        }

        public async Task<Result<ResultTransferResponseModel>> Transfer(string fromMobile, string toMobile, decimal amount, string pin, string notes)
        {
            Result<ResultTransferResponseModel> model = new Result<ResultTransferResponseModel>();

            try
            {
                var fromPhone = await _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.MobileNo == fromMobile);
                var toPhone = await _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.MobileNo == toMobile);

                TblTransactionHistory history = new TblTransactionHistory();

                #region Notes Check
                if (string.IsNullOrEmpty(notes))
                {
                    model = Result<ResultTransferResponseModel>.ValidationError("Notes cannot be blank.");
                    return model;
                }
                #endregion

                #region PhoneNo Check
                if (fromPhone.MobileNo == toPhone.MobileNo)
                {
                    model = Result<ResultTransferResponseModel>.ValidationError("Phone no can't not be the same.");
                    return model;
                }

                if (fromPhone is null)
                {
                    model = Result<ResultTransferResponseModel>.ValidationError("Sender phone no doesn't exist.");
                    return model;
                }
                #endregion

                #region Pin Check
                if (fromPhone.Pin != pin)
                {
                    model = Result<ResultTransferResponseModel>.ValidationError("Incorrect Pin");
                    return model;
                }
                #endregion

                #region Receiver Phone Check
                if (toPhone is null)
                {
                    model = Result<ResultTransferResponseModel>.ValidationError("Receiver phone no dees");
                    return model;
                }
                #endregion

                #region Amount Check
                if (amount > fromPhone.Balance || 10000 > fromPhone.Balance - amount)
                {
                    model = Result<ResultTransferResponseModel>.ValidationError("Insufficient Balance.");
                    return model;
                }
                #endregion

                fromPhone.Balance -= amount;
                toPhone.Balance += amount;

                _db.TblAccounts.Update(fromPhone);
                _db.TblAccounts.Update(toPhone);
                await _db.SaveChangesAsync();

                history.FromMobileNo = fromMobile;
                history.ToMobileNo = toMobile;
                history.Amount = amount;
                history.Notes = notes;
                history.TranDate = DateTime.Now;

                _db.TblTransactionHistories.Add(history);
                await _db.SaveChangesAsync();

                ResultTransferResponseModel result = new ResultTransferResponseModel
                {
                    TransactionHistory = history
                };

                return Result<ResultTransferResponseModel>.Success(result, "Success");
            }
            catch (Exception ex)
            {
                return Result<ResultTransferResponseModel>.SystemError(ex.Message);
            }
        }
    }
}
