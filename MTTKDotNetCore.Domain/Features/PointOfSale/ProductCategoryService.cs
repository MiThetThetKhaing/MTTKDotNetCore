using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.Domain.Features.PointOfSale
{
    public class ProductCategoryService
    {
        AppDbContext _db = new AppDbContext();

        public List<TblProductCategory> GetProductCategories()
        {
            var result = _db.TblProductCategories.AsNoTracking().ToList();
            return result;
        }

        public TblProductCategory GetProductCategory(int id)
        {
            var result = _db.TblProductCategories.AsNoTracking().FirstOrDefault(x => x.ProductCategoryId == id);
            if (result is null)
            {
                return null;
            }
            return result;
        }

        public TblProductCategory CreateProductCategory(TblProductCategory productCategory)
        {
            _db.TblProductCategories.Add(productCategory);
            _db.SaveChanges();

            return productCategory;
        }

        public TblProductCategory UpdateProductCategory(int id, TblProductCategory productCategory)
        {
            var result = _db.TblProductCategories.AsNoTracking().FirstOrDefault(x => x.ProductCategoryId == id);
            if (result is null)
            {
                return null;
            }
            result.ProductCategoryName = productCategory.ProductCategoryName;
            result.ProductCategoryCode = productCategory.ProductCategoryCode;

            _db.Entry(result).State = EntityState.Modified;
            _db.SaveChanges();

            return result;
        }

        public TblProductCategory PatchProductCategory(int id, TblProductCategory productCategory)
        {
            var result = _db.TblProductCategories.AsNoTracking().FirstOrDefault(x => x.ProductCategoryId == id);
            if (result is null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(productCategory.ProductCategoryName))
            {
                result.ProductCategoryName = productCategory.ProductCategoryName;
            }
            if (!string.IsNullOrEmpty(productCategory.ProductCategoryCode))
            {
                result.ProductCategoryCode = productCategory.ProductCategoryCode;
            }

            _db.Entry(result).State = EntityState.Modified;
            _db.SaveChanges();

            return result;
        }

        public bool? DeleteProductCategory(int id)
        {
            var result = _db.TblProductCategories.AsNoTracking().FirstOrDefault(x => x.ProductCategoryId == id);
            if (result is null )
            {
                return null;
            }

            _db.Entry(result).State = EntityState.Deleted;
            _db.SaveChanges();

            return true;
        }
    }
}
