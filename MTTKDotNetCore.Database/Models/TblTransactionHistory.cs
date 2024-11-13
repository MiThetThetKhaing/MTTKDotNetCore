using System;
using System.Collections.Generic;

namespace MTTKDotNetCore.Database.Models;

public partial class TblTransactionHistory
{
    public int TranId { get; set; }

    public string FromMobileNo { get; set; } = null!;

    public string ToMobileNo { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Notes { get; set; } = null!;

    public DateTime TranDate { get; set; }

    public bool DeleteFlag { get; set; }
}
