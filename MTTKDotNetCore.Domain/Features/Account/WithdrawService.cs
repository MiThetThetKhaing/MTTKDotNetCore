using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.Domain.Features.Account
{
    public class WithdrawService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public object CreateWithdraw(string mobileNo, decimal balance)
        {
            var mobile = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.MobileNo == mobileNo);
            if (mobile != null)
            {
                if (balance > 0)
                {
                    if (10000 >= mobile.Balance - balance)
                    {
                        var error = new ErrorResponse
                        {
                            errorMessage = "Insufficient Balance."
                        };
                        return error;
                    }
                    else
                    {
                        mobile.Balance -= balance;
                    }
                }
                else
                {
                    var error = new ErrorResponse
                    {
                        errorMessage = "Invalid Balance."
                    };
                    return error;
                }
                var result = _db.TblAccounts.Update(mobile);
                _db.SaveChanges();
            }
            else
            {
                var error = new ErrorResponse
                {
                    errorMessage = "Mobile phone number doesn't exist."
                };
                return error;
            }
            return mobile;
        }
    }
}
