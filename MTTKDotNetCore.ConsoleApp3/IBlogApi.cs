using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.ConsoleApp3
{
    public interface IBlogApi
    {
        [Get("/api/blogs")]
        Task<List<BlogModel>> GetBlogs();

        [Get("/api/blogs/{id}")]
        Task<BlogModel> GetBlog(int id);

        [Post("/api/blogs/")]
        Task<BlogModel> CreateBlog(BlogModel blogModel);

        [Put("/api/blogs/{id}")]
        Task<BlogModel> PutBlog(int id, BlogModel blogModel);

        [Patch("/api/blogs/{id}")]
        Task<BlogModel> PatchBlog(int id, BlogModel blogModel);

        [Delete("/api/blogs/{id}")]
        Task<BlogModel> DeleteBlog(int id);
    }

    public class BlogModel
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string BlogAuthor { get; set; } = null!;

        public string BlogContent { get; set; } = null!;

        public bool DeleteFlag { get; set; }
    }
}
