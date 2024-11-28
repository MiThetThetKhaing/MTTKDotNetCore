using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.PointOfSale.Domain.Features
{
    public class ProductService
    {
        public readonly AppDbContext _db;
        private readonly Result<ProductResponseModel> _model = new Result<ProductResponseModel>();

        public ProductService()
        {
            _db = new AppDbContext();
        }

        public async Task<Result<ProductResponseModel>> GetProducts()
        {
            try
            {
                var items = await _db.TblProducts.AsNoTracking().Where(x => x.DeleteFlag == false).ToListAsync();
                if (items is null)
                {
                    return Result<ProductResponseModel>.ValidationError("No Product found!");
                }

                var result = new ProductResponseModel
                {
                    TblProducts = items
                };

                return Result<ProductResponseModel>.Success(result, "Product are found.");
            }
            catch (Exception ex)
            {
                return Result<ProductResponseModel>.SystemError(ex.Message);
            }
        }

        public async Task<Result<ProductResponseModel>> GetProduct(int id)
        {
            try
            {
                var item = await _db.TblProducts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.ProductId == id);

                if (item is null)
                    return Result<ProductResponseModel>.ValidationError("No Product found.");

                var result = new ProductResponseModel
                {
                    TblProduct = item
                };
                return Result<ProductResponseModel>.Success(result, "Product found.");
            }
            catch (Exception ex)
            {
                return Result<ProductResponseModel>.SystemError(ex.Message);
            }
        }

        public async Task<Result<ProductResponseModel>> CreateProduct(TblProduct product)
        {
            try
            {
                var SameProductCode = await _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == product.ProductCode);
                var CategoryCode = await _db.TblProductCategories.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCategoryCode == product.ProductCategoryCode);

                if (SameProductCode != null)
                {
                    return Result<ProductResponseModel>.ValidationError("Product code already exist.");
                }
                if (CategoryCode is null) 
                {
                    return Result<ProductResponseModel>.ValidationError("Product Category Code doesn't exists. Please create first!");
                }
                if (product.ProductCode.Length > 12 || product.ProductCode.Length < 12)
                {
                    return Result<ProductResponseModel>.ValidationError("Product code length must be 12 digit.");
                }
                if (string.IsNullOrEmpty(product.ProductCategoryCode))
                {
                    return Result<ProductResponseModel>.ValidationError("You need to put Product Category Code.");
                }
                if (string.IsNullOrEmpty(product.ProductName))
                {
                    return Result<ProductResponseModel>.ValidationError("You need to put Product Name");
                }
                if (product.Price is 0)
                {
                    return Result<ProductResponseModel>.ValidationError("You need to put your Product price.");
                }

                await _db.TblProducts.AddAsync(product);
                await _db.SaveChangesAsync();

                var result = new ProductResponseModel
                {
                    TblProduct = product
                };

                return Result<ProductResponseModel>.Success(result, "Product is successfully created.");
            }
            catch (Exception ex)
            {
                return Result<ProductResponseModel>.SystemError(ex.Message);
            }
        }

        public async Task<Result<ProductResponseModel>> UpdateProduct(int id, TblProduct product)
        {
            try
            {
                var item = await _db.TblProducts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.ProductId == id);
                var SameProductCode = await _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == product.ProductCode);
                var CategoryCode = await _db.TblProductCategories.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCategoryCode == product.ProductCategoryCode);

                if (item is null)
                    return Result<ProductResponseModel>.ValidationError("No Product Found.");


                if (!string.IsNullOrEmpty(product.ProductName))
                {
                    item.ProductName = product.ProductName;
                }
                if (!string.IsNullOrEmpty(product.ProductCode))
                {
                    item.ProductCode = product.ProductCode;
                }
                if (!string.IsNullOrEmpty(product.ProductCategoryCode))
                {
                    item.ProductCategoryCode = product.ProductCategoryCode;
                }
                if (product.Price != 0)
                {
                    item.Price = product.Price;
                }

                if (SameProductCode != null)
                    return Result<ProductResponseModel>.ValidationError("Product code already exist.");

                if (CategoryCode is null)
                    return Result<ProductResponseModel>.ValidationError("Product Category Code doesn't exists. Please create first!");

                if (product.ProductCode!.Length > 12 || product.ProductCode.Length < 12)
                    return Result<ProductResponseModel>.ValidationError("Product code length must be 12 digit.");

                _db.Entry(item).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                var result = new ProductResponseModel
                {
                    TblProduct = item
                };
                return Result<ProductResponseModel>.Success(result, "Product is successfully updated.");
            }
            catch (Exception ex)
            {
                return Result<ProductResponseModel>.SystemError(ex.Message);
            }
        }

        public async Task<Result<ProductResponseModel>> DeleteProduct(int id)
        {
            try
            {
                var item = await _db.TblProducts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefaultAsync(x => x.ProductId == id);

                if (item is null)
                    return Result<ProductResponseModel>.ValidationError("No product found.");

                item.DeleteFlag = true;

                _db.Entry(item).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                var result = new ProductResponseModel
                {
                    TblProduct = item
                };
                return Result<ProductResponseModel>.Success(result, "Product is successfully deleted.");
            }
            catch (Exception ex)
            {
                return Result<ProductResponseModel>.SystemError(ex.Message);
            }
        }
    }
}
