using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MTTKDotNetCore.TodoListRestAPI.DataModels;
using MTTKDotNetCore.TodoListRestAPI.ViewModels;
using System.Data;

namespace MTTKDotNetCore.TodoListRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCategoryDapperController : Controller
    {
        private readonly string _connectionString = "Data Source=DESKTOP-6QTP69L\\MSSQLSERVER2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sa123; TrustServerCertificate=true";

        [HttpGet]
        public IActionResult GetCategories()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT [CategoryID]
                                  ,[CategoryName]
                                  ,[DeleteFlag]
                              FROM [dbo].[TaskCategory] where DeleteFlag = 0;";
                var lst = db.Query<CategoryDataModel>(query).ToList();

                return Ok(lst);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = @"SELECT [CategoryID]
                                  ,[CategoryName]
                                  ,[DeleteFlag]
                              FROM [dbo].[TaskCategory] 
                              where CategoryID = @CategoryId 
                              and DeleteFlag = 0;";
                var item = db.Query<CategoryDataModel>(query, new CategoryDataModel
                {
                    CategoryId = id
                }).FirstOrDefault();
                if (item is null)
                {
                    return BadRequest("No Data Found");
                }
                return Ok(item);
            };
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryViewModel category)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var queryInsert = @"INSERT INTO [dbo].[TaskCategory]
                                           ([CategoryName]
                                           ,[DeleteFlag])
                                     VALUES
                                           (@CategoryName
                                            ,0);";

                var item = db.Execute(queryInsert, new
                {
                    CategoryName = category.Name
                });
                return Ok(item > 0 ? "Successfully Created.." : "Creating failed..");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryViewModel category)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var queryUpdate = @"UPDATE [dbo].[TaskCategory]
                                       SET [CategoryName] = @CategoryName
                                          ,[DeleteFlag] = 0
                                     WHERE CategoryID = @CategoryId;";

                var item = db.Execute(queryUpdate, new
                {
                    CategoryId = id,
                    CategoryName = category.Name
                });
                return Ok(item > 0 ? "Successfully Updated.." : "Updating Failed..");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchCategory(int id, CategoryViewModel category)
        {
            string conditions = "";

            if (!string.IsNullOrEmpty(category.Name))
            {
                conditions += " [CategoryName] = @CategoryName, ";
            }
            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Parameter!");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var queryPatch = $@"UPDATE [dbo].[TaskCategory] SET {conditions} WHERE CategoryID = @CategoryId;";

                var item = db.Execute(queryPatch, new
                {
                    CategoryId = id,
                    CategoryName = category.Name
                });
                return Ok(item > 0 ? "Successfully Updated.." : "Updating Failed..");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var deleteQuery = @"UPDATE [dbo].[TaskCategory]
                                       SET [DeleteFlag] = 1
                                     WHERE CategoryID = @CategoryId;";
                var item = db.Execute(deleteQuery, new
                {
                    CategoryId = id
                });
                return Ok(item > 0 ? "Successfully deleted.." : "Deleting failed..");
            }
        }
    }
}
