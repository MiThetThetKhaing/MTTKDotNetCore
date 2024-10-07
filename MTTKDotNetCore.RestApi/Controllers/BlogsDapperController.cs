using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.Database.Models;
using MTTKDotNetCore.RestApi.DataModels;
using MTTKDotNetCore.RestApi.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace MTTKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=DESKTOP-6QTP69L\\MSSQLSERVER2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sa123; TrustServerCertificate=true";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_Blog where DeleteFlag = 0;";
                var lst = db.Query<BlogDataModel>(query).ToList();

                //foreach (var item in lst)
                //{
                //    Console.WriteLine(item.BlogId);
                //    Console.WriteLine(item.BlogTitle);
                //    Console.WriteLine(item.BlogAuthor);
                //    Console.WriteLine(item.BlogContent);
                //}
                return Ok(lst);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_Blog where BlogId = @BlogId and DeleteFlag = 0;";
                var lst = db.Query<BlogDataModel>(query, new BlogDataModel
                {
                    BlogId = id
                }).FirstOrDefault();

                if (lst == null)
                {
                    return BadRequest("No data Found");
                }

                return Ok(lst);
            }
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogViewModel blog)
        {
            string queryInsert = $@"INSERT INTO [dbo].[Tbl_Blog]
                                   ([BlogTitle]
                                   ,[BlogAuthor]
                                   ,[BlogContent]
                                   ,[DeleteFlag])
                             VALUES
                                   (@BlogTitle
                                   ,@BlogAuthor
                                   ,@BlogContent
                                   ,0)";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(queryInsert, new
                {
                    BlogTitle = blog.Title,
                    BlogAuthor = blog.Author,
                    BlogContent = blog.Content,
                });
                return Ok(result == 1 ? "Saving Successful.." : "Saving Failed..");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogViewModel blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string queryUpdate = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                              ,[DeleteFlag] = 0
                         WHERE BlogId = @BlogId";

                int result = db.Execute(queryUpdate, new
                {
                    BlogId = id,
                    BlogTitle = blog.Title,
                    BlogAuthor = blog.Author,
                    BlogContent = blog.Content,
                });
                return Ok(result == 1 ? "Updating successfully.." : "Updating Failed..");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string conditions = "";

            if (!string.IsNullOrEmpty(blog.Title))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Parameter!");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string queryUpdate = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";

            int result = db.Execute(queryUpdate, new
            {
                BlogId = id,
                BlogTitle = blog.Title,
                BlogAuthor = blog.Author,
                BlogContent = blog.Content,
            });

            return Ok(result == 1 ? "Updating Successfully.." : "Updating Failed..");
        }
    }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string deleteQuery = @"UPDATE [dbo].[Tbl_Blog] SET [DeleteFlag] = 1 WHERE BlogId = @BlogId;";
                int result = db.Execute(deleteQuery, new
                {
                    BlogId = id
                });

                return Ok(result == 1 ? "Successfully deleted.." : "Deleting failed..");

            }
        }
    }
}
