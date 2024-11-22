using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.MiniKpay.Database.Models;
using MTTKDotNetCore.MiniKpay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.MiniKpay.Domain.Features
{
    public class UserService
    {
        private readonly AppDbContext _db = new AppDbContext();
        private Result<UserResponseModel> model = new Result<UserResponseModel>();

        public Result<UserResponseModel> CreateUserAccount(TblAccount account)
        {
            try
            {
                var phone = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.MobileNo == account.MobileNo);

                if (account.MobileNo is null)
                {
                    return Result<UserResponseModel>.ValidationError("Phone Number can't be blank.");
                }

                if (account.Pin.Length < 6 || account.Pin.Length > 6)
                {
                    return Result<UserResponseModel>.ValidationError("Incorrect Pin length.");
                }

                if (phone != null)
                {
                    return Result<UserResponseModel>.ValidationError("Phone Number is already registered.");
                }

                _db.TblAccounts.Add(account);
                _db.SaveChanges();

                UserResponseModel result = new UserResponseModel
                {
                    TblAccount = account,
                };

                return Result<UserResponseModel>.Success(result, "User is successfully Created.");
            }
            catch (Exception ex)
            {
                return Result<UserResponseModel>.SystemError(ex.Message);
            }
        }

        public Result<UserResponseModel> UpdateUserAccount(int id, TblAccount account)
        {
            try
            {
                var item = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.Id == id);
                if (item is null)
                {
                    return Result<UserResponseModel>.ValidationError("Account doesn't exist.");
                }

                item.FullName = account.FullName;
                item.MobileNo = account.MobileNo;
                if (account.Balance > 0)
                {
                    item.Balance = account.Balance;
                }
                else
                {
                    return Result<UserResponseModel>.ValidationError("Incorrect Balance.");
                }

                if (account.Pin.Length < 6 || account.Pin.Length > 6)
                {
                    return Result<UserResponseModel>.ValidationError("Incorrect Pin length.");
                }
                item.Pin = account.Pin;

                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                UserResponseModel result = new UserResponseModel
                {
                    TblAccount = item
                };

                return Result<UserResponseModel>.Success(result, "User profile is successfully updated.");
            }
            catch (Exception ex)
            {
                return Result<UserResponseModel>.SystemError(ex.Message);
            }
            
        }

        public Result<UserResponseModel> ChangePin(string phoneNo, string newPin)
        {
            try
            {
                var account = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.MobileNo == phoneNo);
                if (account is null)
                {
                    return Result<UserResponseModel>.ValidationError("User doesn't exist.");
                }

                if (newPin.Length < 6 || newPin.Length > 0)
                {
                    return Result<UserResponseModel>.ValidationError("Incorrect Pin length.");
                }
                account.Pin = newPin;
                _db.SaveChanges();

                UserResponseModel result = new UserResponseModel
                {
                    TblAccount = account
                };

                return Result<UserResponseModel>.Success(result, "Pin is successfully changed.");
            }
            catch (Exception ex)
            {
                return Result<UserResponseModel>.SystemError(ex.Message);
            }
        }

        public Result<UserResponseModel> GetAccount(int id)
        {
            try
            {
                var result = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.Id == id);
                if (result is null)
                {
                    return Result<UserResponseModel>.ValidationError("User doesn't exist.");
                }

                UserResponseModel model = new UserResponseModel
                {
                    TblAccount = result
                };

                return Result<UserResponseModel>.Success(model, "User exist.");
            }
            catch (Exception ex)
            {
                return Result<UserResponseModel>.SystemError(ex.Message);
            }    
        }

        public Result<UserResponseModel> PatchUserAccount(int id, TblAccount account)
        {
            try
            {
                var result = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.Id == id);
                if (result is null)
                {
                    return Result<UserResponseModel>.ValidationError("User doesn't exist.");
                }

                if (!string.IsNullOrEmpty(account.FullName))  // when test show mobileNo and pin is required
                {
                    result.FullName = account.FullName;
                }
                if (!string.IsNullOrEmpty(account.MobileNo))
                {
                    result.MobileNo = account.MobileNo;
                }
                if (!string.IsNullOrEmpty(account.Pin))
                {
                    result.Pin = account.Pin;
                }
                _db.Entry(result).State = EntityState.Modified;
                _db.SaveChanges();

                UserResponseModel model = new UserResponseModel
                {
                    TblAccount = result
                };

                return Result<UserResponseModel>.Success(model, "User profile is successfully updated.");
            }
            catch (Exception ex)
            {
                return Result<UserResponseModel>.SystemError(ex.Message);
            }
        }

        public Result<UserResponseModel> DeleteAccount(int id)
        {
            try
            {
                var item = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.Id == id);

                if (item is null)
                {
                    return Result<UserResponseModel>.ValidationError("User doesn't exist.");
                }
                item.DeleteFlag = true;

                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                UserResponseModel result = new UserResponseModel
                {
                    TblAccount = item
                };

                return Result<UserResponseModel>.Success(result, "User is successfully deleted.");
            }
            catch (Exception ex)
            {
                return Result<UserResponseModel>.SystemError(ex.Message);
            }    
        }
    }
}
