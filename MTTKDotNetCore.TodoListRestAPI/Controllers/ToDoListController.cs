using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.Database.Models;
using System.Reflection.Metadata;

namespace MTTKDotNetCore.TodoListRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : Controller
    {
        private readonly AppDbContext _db = new AppDbContext(); 

        [HttpGet]
        public IActionResult GetToDoLists()
        {
            var lst = _db.ToDoLists
                .AsNoTracking()
                .Where(x => x.DeleteFlag == false)
                .ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetToDoList(int id)
        {
            var items = _db.ToDoLists.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.TaskId == id);
            if (items is null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost]
        public IActionResult CreateToDoList(ToDoList todo)
        {
            if (todo.Status == "Completed")
            {
                todo.CompletedDate = DateTime.Now;
            }
            if (todo.Status != "Completed")
            {
                todo.CompletedDate = null;
            }
            var result = _db.ToDoLists.Add(todo);
            _db.SaveChanges();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateToDoList(int id, ToDoList todo)
        {
            var item = _db.ToDoLists.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                return NotFound();
            }

            item.TaskTitle = todo.TaskTitle;
            item.TaskDescription = todo.TaskDescription;
            item.CategoryId = todo.CategoryId;
            item.PriorityLevel = todo.PriorityLevel;
            item.Status = todo.Status;

            if (todo.Status is "Completed")
            {
                item.CompletedDate = DateTime.Now;
            }
            else
            {
                item.CompletedDate = todo.CompletedDate;
            }
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchToDoList(int id, ToDoList todo)
        {
            var item = _db.ToDoLists.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(todo.TaskTitle))
            {
                item.TaskTitle = todo.TaskTitle;
            }
            if (!string.IsNullOrEmpty(todo.TaskDescription))
            {
                item.TaskDescription = todo.TaskDescription;
            }
            if (!string.IsNullOrEmpty(todo.Status))
            {
                item.Status = todo.Status;
            }
            if (todo.Status == "Completed")
            {
                item.CompletedDate = DateTime.Now;
            }
            if (todo.Status != "Completed")
            {
                item.CompletedDate = null;
            }
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDoList(int id)
        {
            var item = _db.ToDoLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                return NotFound();
            }

            item.DeleteFlag = true;
            _db.Entry(item).State = EntityState.Modified;

            //_db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();

            return Ok(item);
        }
    }
}
