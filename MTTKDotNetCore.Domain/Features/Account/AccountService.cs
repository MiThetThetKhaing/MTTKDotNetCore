using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.Domain.Features.Account
{
    // Business Logic + Data Access
    public class AccountService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public List<TblAccount> GetAccounts()
        {
            var result = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return result;
        }

        public TblAccount GetAccount(int id)
        {
            var result = _db.TblAccounts.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (result is null)
            {
                return null;
            }
            return result;
        }

        public object CreateAccount(TblAccount account)
        {
            var phone = _db.TblAccounts.FirstOrDefault(x => x.MobileNo == account.MobileNo);
            Console.WriteLine(phone);
            if (phone == null)
            {
                _db.TblAccounts.Add(account);
                _db.SaveChanges();
            }
            else
            {
                //Console.WriteLine("Mobile phone no is already taken.");
                var error = new ErrorResponse
                {
                    errorMessage = "Mobile phone no is already taken."
                };
                return error;
            }
            return account;
        }

        public TblAccount UpdateAccount(int id, TblAccount account)
        {
            var item = _db.TblAccounts.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return null;
            }

            item.FullName = account.FullName;
            item.MobileNo = account.MobileNo;
            if (account.Balance > 0)
            {
                item.Balance = account.Balance;
            }
            else
            {
                return null;
            }
            item.Pin = account.Pin;
 
            
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }

        public TblAccount PatchAccount(int id, TblAccount account)
        {
            var result = _db.TblAccounts.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (result is null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty (account.FullName))  // when test show mobileNo and pin is required
            {
                result.FullName = account.FullName;
            }
            if (!string.IsNullOrEmpty(account.MobileNo))
            {
                result.MobileNo = account.MobileNo;
            }
            if (account.Balance != null && account.Balance > 0)
            {
                result.Balance = account.Balance;
            }
            if (!string.IsNullOrEmpty(account.Pin))
            {
                result.Pin = account.Pin;
            }

            _db.Entry(result).State = EntityState.Modified;
            _db.SaveChanges();

            return result;
        }

        public bool? DeleteAccount(int id)
        {
            var item = _db.TblAccounts.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (item is null ) return null;

            item.DeleteFlag = true;

            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();

            return result > 0;
        }
    }
}
