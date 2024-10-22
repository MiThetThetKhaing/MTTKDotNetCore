using MTTKDotNetCore.Database.Models;
using System.Diagnostics.Eventing.Reader;

namespace MTTKDotNetCore.TodoListRestAPI.DataModels
{
    public class CategoryDataModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool DeleteFlag { get; set; }

        //public virtual ICollection<ToDoList> ToDoLists { get; set; }
    }
}
