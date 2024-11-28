using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.PointOfSale.Domain.Features
{
    public class ProductCategoryService
    {
        public readonly AppDbContext _db;
        private readonly Result<ProductCategoryResponseModel> _model = new Result<ProductCategoryResponseModel>();


        public ProductCategoryService()
        {
            _db = new AppDbContext();
        }

        public async Task<Result<ProductCategoryResponseModel>> GetProductCategories()
        {
            try
            {
                var item = await _db.TblProductCategories.AsNoTracking().Where(x => x.DeleteFlag == false).ToListAsync();

                if (item is null)
                    return Result<ProductCategoryResponseModel>.ValidationError("No data found.");

                var result = new ProductCategoryResponseModel
                {
                    TblProductCategories = item
                };
                return Result<ProductCategoryResponseModel>.Success(result, "Product categories found.");
            }
            catch (Exception ex)
            {
                return Result<ProductCategoryResponseModel>.SystemError(ex.Message);
            }
        }

        public async Task<Result<ProductCategoryResponseModel>> GetProductCategory(int id)
        {
            try
            {
                var item = await _db.TblProductCategories.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.ProductCategoryId == id);

                if (item is null)
                    return Result<ProductCategoryResponseModel>.ValidationError("No data found.");

                var result = new ProductCategoryResponseModel
                {
                    TblProductCategory = item
                };

                return Result<ProductCategoryResponseModel>.Success(result, "Product category found.");
            }
            catch (Exception ex)
            {
                return Result<ProductCategoryResponseModel>.SystemError(ex.Message);
            }
        }

        public async Task<Result<ProductCategoryResponseModel>> CreateProductCategories(TblProductCategory productCategory)
        {
            try
            {
                var sameProductCode = await _db.TblProductCategories.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCategoryCode == productCategory.ProductCategoryCode);

                if (sameProductCode != null)
                {
                    return Result<ProductCategoryResponseModel>.ValidationError("Product category code is already exists.");
                }
                if (productCategory.ProductCategoryCode.Length > 6 || productCategory.ProductCategoryCode.Length < 6)
                {
                    return Result<ProductCategoryResponseModel>.ValidationError("Code length can't be grater than 6 digit.");
                }
                if (string.IsNullOrEmpty(productCategory.ProductCategoryCode))
                {
                    return Result<ProductCategoryResponseModel>.ValidationError("Product category code can't be blank.");
                }
                if (string.IsNullOrEmpty(productCategory.ProductCategoryName))
                {
                    return Result<ProductCategoryResponseModel>.ValidationError("Product category name can't be blank.");
                }

                await _db.TblProductCategories.AddAsync(productCategory);
                await _db.SaveChangesAsync();

                var result = new ProductCategoryResponseModel
                {
                    TblProductCategory = productCategory
                };
                return Result<ProductCategoryResponseModel>.Success(result, "Product category successfully created.");
            }
            catch (Exception ex)
            {
                return Result<ProductCategoryResponseModel>.SystemError(ex.Message);
            }
        }

        public async Task<Result<ProductCategoryResponseModel>> UpdateProductCategory(int id, TblProductCategory productCategory)
        {
            try
            {
                var item = await _db.TblProductCategories.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.ProductCategoryId == id);

                if (item is null)
                {
                    return Result<ProductCategoryResponseModel>.ValidationError("No data found.");
                }
                if (productCategory.ProductCategoryCode.Length > 6)
                {
                    return Result<ProductCategoryResponseModel>.ValidationError("Code length can't be grater than 6 digit.");
                }

                if (!string.IsNullOrEmpty(productCategory.ProductCategoryCode))
                {
                    item.ProductCategoryCode = productCategory.ProductCategoryCode;
                }
                if (!string.IsNullOrEmpty(productCategory.ProductCategoryName))
                {
                    item.ProductCategoryName = productCategory.ProductCategoryName;
                }

                _db.Entry(item).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                var result = new ProductCategoryResponseModel
                {
                    TblProductCategory = item
                };

                return Result<ProductCategoryResponseModel>.Success(result, "Product Category Updating successfully.");
            }
            catch (Exception ex)
            {
                return Result<ProductCategoryResponseModel>.SystemError(ex.Message);
            }
        }

        public async Task<Result<ProductCategoryResponseModel>> DeleteProductCategory(int id)
        {
            try
            {
                var item = await _db.TblProductCategories.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.ProductCategoryId == id);

                if (item is null)
                {
                    return Result<ProductCategoryResponseModel>.ValidationError("No data found.");
                }

                item.DeleteFlag = true;

                _db.Entry(item).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                var result = new ProductCategoryResponseModel
                {
                    TblProductCategory = item
                };

                return Result<ProductCategoryResponseModel>.Success(result, "Product category is successfully deleted.");
            }
            catch (Exception ex)
            {
                return Result<ProductCategoryResponseModel>.SystemError(ex.Message);
            }
        }
    }
}
