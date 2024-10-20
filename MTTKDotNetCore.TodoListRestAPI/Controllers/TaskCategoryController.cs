using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.Database.Models;

namespace MTTKDotNetCore.TodoListRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCategoryController : Controller
    {
        private readonly AppDbContext _db = new AppDbContext();

        [HttpGet]
        public IActionResult GetCategories()
        {
            var lst = _db.TaskCategories.AsNoTracking().ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var item = _db.TaskCategories.AsNoTracking().FirstOrDefault(x => x.CategoryId == id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateCategory(TaskCategory category)
        {
            var result = _db.TaskCategories.Add(category);
            _db.SaveChanges();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, TaskCategory category)
        {
            var item = _db.TaskCategories.AsNoTracking().FirstOrDefault(x => x.CategoryId == id);
            if (item is null)
            {
                return NotFound();
            }

            item.CategoryName = category.CategoryName;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchCategory(int id, TaskCategory category)
        {
            var item = _db.TaskCategories.AsNoTracking().FirstOrDefault(x => x.CategoryId == id);
            if (item is null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(category.CategoryName))
            {
                item.CategoryName = category.CategoryName;
            }
 
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var item = _db.TaskCategories.AsNoTracking().FirstOrDefault(x => x.CategoryId == id);
            if (item is null)
            {
                return NotFound();
            }

            //item.DeleteFlag = true;
            //_db.Entry(item).State = EntityState.Modified;

            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();

            return Ok(item);
        }
    }
}
