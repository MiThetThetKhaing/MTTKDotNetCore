using MTTKDotNetCore.Database.Models;

namespace MTTKDotNetCore.TodoListRestAPI.DataModels
{
    public class CategoryDataModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        //public virtual ICollection<ToDoList> ToDoLists { get; set; }
    }
}
