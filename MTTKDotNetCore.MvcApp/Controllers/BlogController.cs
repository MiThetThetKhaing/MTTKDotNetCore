using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.Database.Models;
using MTTKDotNetCore.Domain.Features.Blog;
using MTTKDotNetCore.MvcApp.Models;

namespace MTTKDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var list = _blogService.GetBlogs();
            return View(list);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult BlogSave(BlogRequestModel requestModel)
        {
            try
            {
                _blogService.CreateBlog(new TblBlog
                {
                    BlogTitle = requestModel.Title,
                    BlogAuthor = requestModel.Author,
                    BlogContent = requestModel.Content
                });

                //ViewBag.IsSuccess = true;
                //ViewBag.Message = "Blog Created Successfully.";

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog Created Successfully";
            }
            catch (Exception ex)
            {
                //ViewBag.IsSuccess = false;
                //ViewBag.Message = ex.ToString();

                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();
            }

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public IActionResult BlogDelete(int id)
        {
            _blogService.DeleteBlog(id);
            return RedirectToAction("Index");
        }

        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            var blog = _blogService.GetBlog(id);
            BlogRequestModel model = new BlogRequestModel()
            {
                Id = blog.BlogId,
                Title = blog.BlogTitle,
                Author = blog.BlogAuthor,
                Content = blog.BlogContent
            };
            return View("BlogEdit", model);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id, BlogRequestModel requestModel)
        {
            try
            {
                _blogService.UpdateBlog(id, new TblBlog
                {
                    BlogTitle = requestModel.Title,
                    BlogAuthor = requestModel.Author,
                    BlogContent = requestModel.Content
                });

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog Updated Successfully";
            }
            catch (Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();
            }

            return RedirectToAction("Index");
        }
    }
}
