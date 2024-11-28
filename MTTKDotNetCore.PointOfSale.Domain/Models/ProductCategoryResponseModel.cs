using MTTKDotNetCore.PointOfSale.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.PointOfSale.Domain.Models
{
    public class ProductCategoryResponseModel
    {
        public TblProductCategory TblProductCategory { get; set; }

        public List<TblProductCategory> TblProductCategories { get; set; }
    }
}
