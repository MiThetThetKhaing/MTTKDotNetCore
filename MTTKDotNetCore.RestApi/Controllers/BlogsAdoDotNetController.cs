using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MTTKDotNetCore.Database.Models;
using MTTKDotNetCore.RestApi.ViewModels;
using System.Collections.Generic;

namespace MTTKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=DESKTOP-6QTP69L\\MSSQLSERVER2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sa123; TrustServerCertificate=true";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                //lst.Add(new BlogViewModel
                //{
                //    Id = Convert.ToInt32(reader["BlogId"]),
                //    Title = Convert.ToString(reader["BlogTitle"]),
                //    Author = Convert.ToString(reader["BlogAuthor"]),
                //    Content = Convert.ToString(reader["BlogContent"]),
                //    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                //});
                var item = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                };
                lst.Add(item);
            }
            connection.Close();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where BlogId=@BlogId and DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);

                var item = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                };
                lst.Add(item);
            }
            connection.Close();
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogViewModel blog)
        {
            //BlogViewModel model = new BlogViewModel();

            //Console.WriteLine("Blog Title :");
            //string title = Console.ReadLine();

            //Console.WriteLine("Blog Author :");
            //string author = Console.ReadLine();

            //Console.WriteLine("Blog Content :");
            //string content = Console.ReadLine();
            //string title = model.Title;
            //string author = model.Author;
            //string content = model.Content;

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

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

            SqlCommand cmd2 = new SqlCommand(queryInsert, connection);
            cmd2.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd2.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd2.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = cmd2.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Saving successful.." : "Saving failed..");

            return Ok(blog);
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateBlogs(int id, TblBlog blog)
        //{

        //}

        //[HttpPatch("{id}")]
        //public IActionResult PatchBlog(int id, TblBlog blog)
        //{

        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteBlogs(int id)
        //{

        //}
    }
}
