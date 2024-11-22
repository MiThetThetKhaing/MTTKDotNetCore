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
    public class WithdrawService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public Result<UserResponseModel> CreateWithdraw(string mobileNo, decimal balance)
        {
            try
            {
                var mobile = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.MobileNo == mobileNo);

                #region Validation

                if (mobile is null)
                {
                    return Result<UserResponseModel>.ValidationError("Mobile No doesn't exist.");
                }
                if (balance <= 0)
                {
                    return Result<UserResponseModel>.ValidationError("Incorrect amount.");
                }
                if (10000 >= mobile.Balance - balance)
                {
                    return Result<UserResponseModel>.ValidationError("Insufficient balance!");
                }

                #endregion

                mobile.Balance -= balance;

                var result = _db.TblAccounts.Update(mobile);
                _db.SaveChanges();

                UserResponseModel model = new UserResponseModel
                {
                    TblAccount = mobile
                };

                return Result<UserResponseModel>.Success(model, "Withdraw process is successfully completed.");
            }
            catch (Exception ex)
            {
                return Result<UserResponseModel>.SystemError(ex.Message);
            }
        }
    }
}

