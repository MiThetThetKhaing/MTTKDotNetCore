using System;
using System.Collections.Generic;

namespace MTTKDotNetCore.PointOfSale.Database.Models;

public partial class TblProductCategory
{
    public int ProductCategoryId { get; set; }

    public string? ProductCategoryCode { get; set; } = null!;

    public string? ProductCategoryName { get; set; } = null!;

    public bool DeleteFlag { get; set; }
}
