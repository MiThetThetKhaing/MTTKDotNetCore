using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.Domain.Features.Account
{
    public class DepositService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public TblAccount CreateDeposit(string mobileNo, decimal balance)
        {
            var mobile = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.MobileNo == mobileNo);
            if (mobile != null)
            {
                if (balance > 0)
                {
                    mobile.Balance += balance;

                    _db.TblAccounts.Update(mobile);
                    _db.SaveChanges();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            return mobile;
        }
    }
}
