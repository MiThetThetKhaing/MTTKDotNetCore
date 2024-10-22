using MTTKDotNetCore.Database.Models;

namespace MTTKDotNetCore.TodoListRestAPI.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public bool DeleteFlag { get; set; }

        //public virtual ICollection<ToDoList> ToDoLists { get; set; }
    }
}
