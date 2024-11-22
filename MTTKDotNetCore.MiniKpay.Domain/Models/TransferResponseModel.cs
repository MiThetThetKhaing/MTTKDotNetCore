using MTTKDotNetCore.MiniKpay.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.MiniKpay.Domain.Models;

public class TransferResponseModel
{
    public BaseResponseModel Response { get; set; }
}

public class ResultTransferResponseModel
{
    public TblTransactionHistory TransactionHistory { get; set; }
}
