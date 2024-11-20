using Azure;
using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.Database.Models;
using MTTKDotNetCore.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.Domain.Features.Account
{
    public class TransferService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public async Task<Result<ResultTransferResponseModel>> CreateTransfer(string fromMobile, string toMobile, decimal amount, string pin, string notes)
        {
            //TransferResponseModel model = new TransferResponseModel();
            Result<ResultTransferResponseModel> model = new Result<ResultTransferResponseModel>();

            var fromPhone = await _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.MobileNo == fromMobile);
            var toPhone = await _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.MobileNo == toMobile);

            TblTransactionHistory history = new TblTransactionHistory();

            #region Notes Check
            if (string.IsNullOrEmpty(notes))
            {
                //model.Response = BaseResponseModel.ValidationError("999", "Notes cannot be blank.");
                model = Result<ResultTransferResponseModel>.ValidationError("Notes cannot be blank.");
                return model;
            }
            #endregion

            #region PhoneNo Check
            if (fromPhone.MobileNo == toPhone.MobileNo)
            {
                //model.Response = BaseResponseModel.ValidationError("999", "Phone no can't not be the same.");
                model = Result<ResultTransferResponseModel>.ValidationError("Phone no can't not be the same.");
                return model;
            }

            if (fromPhone is null)
            {
                //model.Response = BaseResponseModel.ValidationError("999", "Sender phone no doesn't exist.");
                model = Result<ResultTransferResponseModel>.ValidationError("Sender phone no doesn't exist.");
                return model;
                //goto Result; 
            }
            #endregion

            #region Pin Check
            if (fromPhone.Pin != pin)
            {
                //model.Response = BaseResponseModel.ValidationError("999", "Pin is not correct.");
                model = Result<ResultTransferResponseModel>.ValidationError("Incorrect Pin");
                return model;
            }
            #endregion

            #region Receiver Phone Check
            if (toPhone is null)
            {
                //model.Response = BaseResponseModel.ValidationError("999", "Receiver phone no doesn't exist.");
                model = Result<ResultTransferResponseModel>.ValidationError("Receiver phone no dees");
                return model;
            }
            #endregion

            #region Amount Check
            if (amount > fromPhone.Balance || 10000 > fromPhone.Balance - amount)
            {
                //model.Response = BaseResponseModel.ValidationError("999", "Insufficient Balance.");
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

            //model.Response = BaseResponseModel.Success("000", "Success.");
            model = Result<ResultTransferResponseModel>.Success(result, "Success");

        //Result:
            return model;
        }      
    }
}

