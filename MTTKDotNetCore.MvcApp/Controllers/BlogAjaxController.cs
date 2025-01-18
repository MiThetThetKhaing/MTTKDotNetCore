using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.Database.Models;
using MTTKDotNetCore.Domain.Features.Blog;
using MTTKDotNetCore.MvcApp.Models;

namespace MTTKDotNetCore.MvcApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogAjaxController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // CRUD
        // Read

        [ActionName("Index")]
        public IActionResult BlogList()
        {
            var list = _blogService.GetBlogs();
            return View("BlogList", list);
        }

        [ActionName("List")]
        public IActionResult BlogListAjax()
        {
            var list = _blogService.GetBlogs();
            return Json(list);
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
            MessageModel model;
            try
            {
                _blogService.CreateBlog(new TblBlog
                {
                    BlogTitle = requestModel.Title,
                    BlogAuthor = requestModel.Author,
                    BlogContent = requestModel.Content
                });

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog Created Successfully";

                model = new MessageModel(true, "Blog Created Successfully");
            }
            catch (Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();

                model = new MessageModel(false, ex.ToString());
            }
            return Json(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult BlogDelete(BlogRequestModel requestModel)
        {
            MessageModel model;
            try
            {
                _blogService.DeleteBlog(requestModel.Id);

                model = new MessageModel(true, "Blog Deleted Successfully");
            }
            catch (Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();

                model = new MessageModel(false, ex.ToString());
            }
            return Json(model);
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
            MessageModel model;
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

                model = new MessageModel(true, "Blog Updated Successfully");
            }
            catch (Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();

                model = new MessageModel(false, ex.ToString());
            }

            return Json(model);
        }

        public class MessageModel
        {
            public MessageModel()
            {

            }

            public MessageModel(bool isSuccess, string message)
            {
                IsSuccess = isSuccess;
                Message = message;
            }

            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }
    }
}
