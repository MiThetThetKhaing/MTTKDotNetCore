namespace MTTKDotNetCore.TodoListRestAPI.DataModels
{
    public class ToDoDataModel
    {
        public int TaskID { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public int CategoryID { get; set; }
        public string PriorityLevel { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}