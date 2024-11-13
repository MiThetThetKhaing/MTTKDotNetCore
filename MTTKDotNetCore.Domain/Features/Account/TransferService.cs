using Azure;
using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.Domain.Features.Account
{
    public class TransferService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public object CreateTransfer(string fromMobile, string toMobile, decimal amount, string pin, string notes)
        {
            var fromPhone = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.MobileNo == fromMobile);
            var toPhone = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.MobileNo == toMobile);

            if (fromPhone != null && toPhone != null)
            {
                if (fromPhone.MobileNo != toPhone.MobileNo)
                {
                    if (fromPhone.Pin == pin)
                    {
                        if (amount > fromPhone.Balance || 10000 > fromPhone.Balance - amount)
                        {
                            var obj = new ErrorResponse
                            {
                                errorMessage = "Invalid amount!!!"
                            };
                            return obj;
                        }
                        else
                        {
                             fromPhone.Balance -= amount;
                             toPhone.Balance += amount;

                            _db.TblAccounts.Update(fromPhone);
                            _db.TblAccounts.Update(toPhone);
                            _db.SaveChanges();

                            TblTransactionHistory history = new TblTransactionHistory();
                            history.FromMobileNo = fromMobile;
                            history.ToMobileNo = toMobile;
                            history.Amount = amount;
                            history.Notes = notes;
                            history.TranDate = DateTime.Now;

                            _db.TblTransactionHistories.Add(history);
                            _db.SaveChanges();
                        }
                    }
                    else
                    {
                        var error = new ErrorResponse
                        {
                            errorMessage = "Uncorrect Pin!!!"
                        };
                        return error;
                    }
                }
                else
                {
                    var error = new ErrorResponse
                    {
                        errorMessage = "Mobile Phone Numbers cannot be the same!"
                    };
                    return error;
                }
            }
            else
            {
                var error = new ErrorResponse
                {
                    errorMessage = "Mobile phone number doesn't exist."
                };
                return error;
            }
            return "Transaction successfully completed...^_^";
        }
    }
}
