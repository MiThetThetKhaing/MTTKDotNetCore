using System;
using System.Collections.Generic;

namespace MTTKDotNetCore.Database.Models;

public partial class TblAccount
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public decimal Balance { get; set; }

    public string Pin { get; set; } = null!;

    public bool DeleteFlag { get; set; }
}
