using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.RestApi.DataModels;
using MTTKDotNetCore.RestApi.ViewModels;
using MTTKDotNetCore.Shared;
using System.Data;

namespace MTTKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsDapperWithServiceController : Controller
    {
        private readonly string _connectionString = "Data Source=DESKTOP-6QTP69L\\MSSQLSERVER2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sa123;";
        private readonly DapperService _dapperService;

        public BlogsDapperWithServiceController()
        {
            _dapperService = new DapperService(_connectionString);
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_Blog where DeleteFlag = 0;";
            var lst = _dapperService.Query<BlogDataModel>(query).ToList();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from tbl_Blog where BlogId = @BlogId and DeleteFlag = 0;";
            var blog = _dapperService.QueryFirstOrDefault<BlogDataModel>(query, new BlogDataModel
            {
                BlogId = id
            });

            return Ok(blog);
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

            var result = _dapperService.Execute(queryInsert, new BlogDataModel
            {
                BlogTitle = blog.Title,
                BlogAuthor = blog.Author,
                BlogContent = blog.Content
            });
            return Ok(result > 0 ? "Successfully created.." : "Creating failed..");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogViewModel blog)
        {
            string queryUpdate = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                              ,[DeleteFlag] = 0
                         WHERE BlogId = @BlogId";

            var result = _dapperService.Execute(queryUpdate, new BlogDataModel
            {
                BlogId = id,
                BlogTitle = blog.Title,
                BlogAuthor = blog.Author,
                BlogContent = blog.Content
            });
            return Ok(result > 0 ? "Updating successfully.." : "Updating Failed..");
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

            string queryUpdate = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";

            var result = _dapperService.Execute(queryUpdate, new BlogDataModel
            {
                BlogId = id,
                BlogTitle = blog.Title,
                BlogAuthor = blog.Author,
                BlogContent = blog.Content
            });
            return Ok(result > 0 ? "Updating Successfully.." : "Updating Failed..");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            string deleteQuery = @"UPDATE [dbo].[Tbl_Blog] SET [DeleteFlag] = 1 WHERE BlogId = @BlogId;";

            var result = _dapperService.Execute(deleteQuery, new BlogDataModel
            {
                BlogId = id
            });
            return Ok(result > 0 ? "Successfully deleted.." : "Deleting failed..");
        }
    }
}
