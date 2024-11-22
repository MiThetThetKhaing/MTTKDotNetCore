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
    public class DepositService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public Result<UserResponseModel> CreateDeposit(string mobileNo, decimal balance)
        {
            Result<UserResponseModel> model = new Result<UserResponseModel>();

            try
            {
                var mobile = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.MobileNo == mobileNo);
                if (mobile is null)
                {
                    return Result<UserResponseModel>.ValidationError("Mobile Number doesn't exist.");
                }

                if (balance > 0)
                {
                    mobile.Balance += balance;

                    _db.TblAccounts.Update(mobile);
                    _db.SaveChanges();

                    return Result<UserResponseModel>.ValidationError("Insufficient Balance.");
                }

                UserResponseModel result = new UserResponseModel
                {
                    TblAccount = mobile
                };

                return Result<UserResponseModel>.Success(result, "Your Deposit process is successfully completed.");
            }
            catch (Exception ex)
            {
                return Result<UserResponseModel>.SystemError(ex.Message);
            }
        }
    }
}
