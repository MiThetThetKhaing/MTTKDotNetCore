using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.ConsoleApp3
{
    internal class RefitExample
    {
        public async Task Run()
        {
            var blogApi = RestService.For<IBlogApi>("https://localhost:7015");

            // get list blog
            var list = await blogApi.GetBlogs();
            foreach (var blog in list)
            {
                Console.WriteLine(blog.BlogTitle);
            }

            // get blog
            try
            {
                var item = await blogApi.GetBlog(6);
                var item2 = await blogApi.GetBlog(100);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No Data found.");
                }
            }

            // create blog
            var item3 = await blogApi.CreateBlog(new BlogModel
            {
                BlogTitle = "test new",
                BlogAuthor = "test new 1",
                BlogContent = "test new 2"
            });

            // put blog
            var item4 = await blogApi.PutBlog(1035, new BlogModel
            {
                BlogTitle = "put new",
                BlogAuthor = "put new 2",
                BlogContent = "put new 3"
            });

            // patch blog
            try
            {
                var item5 = await blogApi.PatchBlog(1035, new BlogModel
                {
                    BlogTitle = "test title"
                });
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad Request.");
                }
            }

            // delete blog
            var item6 = await blogApi.DeleteBlog(1036);
        }
    }
}
