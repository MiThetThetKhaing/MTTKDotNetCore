using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MTTKDotNetCore.RestApi.ViewModels;
using MTTKDotNetCore.Shared;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace MTTKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetWithServiceController : Controller
    {
        private readonly string _connectionString;
        private readonly AdoDotNetService _adoDotNetService;

        public BlogsAdoDotNetWithServiceController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection")!;
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();

            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

            var dt = _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows)
            {
                //    Console.WriteLine(dr["BlogId"]);
                //    Console.WriteLine(dr["BlogTitle"]);
                //    Console.WriteLine(dr["BlogAuthor"]);
                //    Console.WriteLine(dr["BlogContent"]);
                //    Console.WriteLine(dr["DeleteFlag"]);

                var item = new BlogViewModel
                {
                    Id = Convert.ToInt32(dr["BlogId"]),
                    Title = Convert.ToString(dr["BlogTitle"]),
                    Author = Convert.ToString(dr["BlogAuthor"]),
                    Content = Convert.ToString(dr["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"])
                };
                lst.Add(item);
            }

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            BlogViewModel blog = new BlogViewModel();

            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where BlogId = @BlogId and DeleteFlag = 0";
            var dt = _adoDotNetService.Query(query, new SqlParameterModel("@BlogId", id));
            foreach (DataRow dr in dt.Rows)
            {
                blog = new BlogViewModel
                {
                    Id = Convert.ToInt32(dr["BlogId"]),
                    Title = Convert.ToString(dr["BlogTitle"]),
                    Author = Convert.ToString(dr["BlogAuthor"]),
                    Content = Convert.ToString(dr["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"])
                };
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogViewModel blog)
        {
            string queryInsert = @"INSERT INTO [dbo].[Tbl_Blog]
                                   ([BlogTitle]
                                   ,[BlogAuthor]
                                   ,[BlogContent]
                                   ,[DeleteFlag])
                             VALUES
                                   (@BlogTitle
                                   ,@BlogAuthor
                                   ,@BlogContent
                                   ,0)";

            var result = _adoDotNetService.ExecuteNonQuery(queryInsert, 
                new SqlParameterModel("@BlogTitle", blog.Title),
                new SqlParameterModel("@BlogAuthor", blog.Author),
                new SqlParameterModel("@BlogContent", blog.Content)
                );

            return Ok(result == 1 ? "Saving successful.." : "Saving failed..");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogViewModel blog)
        {
            string queryInsert = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                              ,[DeleteFlag] = 0
                         WHERE BlogId = @BlogId";

            var result = _adoDotNetService.ExecuteNonQuery(queryInsert, 
                new SqlParameterModel("@BlogId", id),
                new SqlParameterModel("@BlogTitle", blog.Title),
                new SqlParameterModel("@BlogAuthor", blog.Author),
                new SqlParameterModel("@BlogContent", blog.Content)
                );

            return Ok(result == 1 ? "Updating successfully.." : "Updating failed..");
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
                return BadRequest("Invalid Parameters!");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string queryInsert = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId AND DeleteFlag = 0";

            var parameterList = new List<SqlParameterModel>();

            parameterList.Add(new SqlParameterModel("@BlogId", id));

            if (!string.IsNullOrEmpty(blog.Title))
            {
                parameterList.Add(new SqlParameterModel("@BlogTitle", blog.Title));
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                parameterList.Add(new SqlParameterModel("@BlogAuthor", blog.Author));
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                parameterList.Add(new SqlParameterModel("@BlogContent", blog.Content));
            }
            var result = _adoDotNetService.ExecuteNonQuery(queryInsert, parameterList.ToArray());

            return Ok(result == 1 ? "Updating successfully.." : "Updating failed..");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            string queryDelete = @"UPDATE [dbo].[Tbl_Blog] SET [DeleteFlag] = 1
                                 WHERE BlogId = @BlogId";

            var result = _adoDotNetService.ExecuteNonQuery(queryDelete, new SqlParameterModel("@BlogId", id));

            return Ok(result == 1 ? "Successfully deleted.." : "Deleting failed..");
        }
    }
}
