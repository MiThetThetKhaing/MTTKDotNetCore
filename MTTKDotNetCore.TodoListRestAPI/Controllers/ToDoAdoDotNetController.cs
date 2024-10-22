using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MTTKDotNetCore.TodoListRestAPI.ViewModels;
using System.Threading.Tasks;

namespace MTTKDotNetCore.TodoListRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoAdoDotNetController : Controller
    {
        private readonly string _connectionString = "Data Source=DESKTOP-6QTP69L\\MSSQLSERVER2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sa123; TrustServerCertificate=true";

        [HttpGet]
        public IActionResult GetToDoLists()
        {
            List<ToDoViewModel> list = new List<ToDoViewModel>();

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            var query = @"SELECT [TaskID]
                          ,[TaskTitle]
                          ,[TaskDescription]
                          ,[CategoryID]
                          ,[PriorityLevel]
                          ,[Status]
                          ,[DueDate]
                          ,[CreatedDate]
                          ,[CompletedDate]
                      FROM [dbo].[ToDoList] where DeleteFlag = 0;";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var items = new ToDoViewModel
                {
                    Id = Convert.ToInt32(reader["TaskID"]),
                    Title = Convert.ToString(reader["TaskTitle"]),
                    Description = Convert.ToString(reader["TaskDescription"]),
                    CategoryID = Convert.ToInt32(reader["CategoryId"]),
                    PriorityLevel = Convert.ToInt32(reader["PriorityLevel"]),
                    Status = Convert.ToString(reader["Status"]),
                    DueDate = Convert.ToDateTime(reader["DueDate"]),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    CompletedDate = reader["CompletedDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["CompletedDate"])
                };
                list.Add(items);
            }
            connection.Close();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetToDo(int id)
        {
            ToDoViewModel todo = new ToDoViewModel();
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            var query = @"SELECT [TaskID]
                          ,[TaskTitle]
                          ,[TaskDescription]
                          ,[CategoryID]
                          ,[PriorityLevel]
                          ,[Status]
                          ,[DueDate]
                          ,[CreatedDate]
                          ,[CompletedDate]
                      FROM [dbo].[ToDoList] where TaskID = @TaskId and DeleteFlag = 0;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TaskId", id);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                todo = new ToDoViewModel
                {
                    Id = Convert.ToInt32(reader["TaskID"]),
                    Title = Convert.ToString(reader["TaskTitle"]),
                    Description = Convert.ToString(reader["TaskDescription"]),
                    CategoryID = Convert.ToInt32(reader["CategoryId"]),
                    PriorityLevel = Convert.ToInt32(reader["PriorityLevel"]),
                    Status = Convert.ToString(reader["Status"]),
                    DueDate = Convert.ToDateTime(reader["DueDate"]),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    CompletedDate = reader["CompletedDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["CompletedDate"]),
                };
            }
            connection.Close();
            if (todo.Id > 0)
            {
                return Ok(todo);
            }
            else
            {
                return BadRequest("No data found!");
            }
        }

        [HttpPost]
        public IActionResult CreateToDo(ToDoViewModel todo)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = @"INSERT INTO [dbo].[ToDoList]
                           ([TaskTitle]
                           ,[TaskDescription]
                           ,[CategoryID]
                           ,[PriorityLevel]
                           ,[Status]
                           ,[DueDate]
                           ,[CreatedDate]
                           ,[CompletedDate]
                           ,[DeleteFlag])
                     VALUES
                           (@TaskTitle
                           ,@TaskDescription
                           ,@CategoryID
                           ,@PriorityLevel
                           ,@Status
                           ,@DueDate
                           ,@CreatedDate
                           ,@CompletedDate
                           ,0)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@TaskTitle", todo.Title);
            cmd.Parameters.AddWithValue("@TaskDescription", todo.Description);
            cmd.Parameters.AddWithValue("@CategoryID", todo.CategoryID);
            cmd.Parameters.AddWithValue("@PriorityLevel", todo.PriorityLevel);
            cmd.Parameters.AddWithValue("@Status", todo.Status);
            cmd.Parameters.AddWithValue("@DueDate", todo.DueDate);
            cmd.Parameters.AddWithValue("@CreatedDate", todo.CreatedDate);
            if (todo.CompletedDate != null && todo.Status == "Completed")
            {
                cmd.Parameters.AddWithValue("@CompletedDate", todo.CreatedDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CompletedDate", DBNull.Value);
            }
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            return Ok(result > 0 ? "Saving Successfully.." : "Saving failed..");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateToDo(int id, ToDoViewModel todo)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = @"UPDATE [dbo].[ToDoList]
                           SET [TaskTitle] = @TaskTitle
                              ,[TaskDescription] = @TaskDescription
                              ,[CategoryID] = @CategoryID
                              ,[PriorityLevel] = @PriorityLevel
                              ,[Status] = @Status
                              ,[DueDate] = @DueDate
                              ,[CompletedDate] = @CompletedDate
                         WHERE TaskID = @TaskId and DeleteFlag = 0;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@TaskId", id);
            cmd.Parameters.AddWithValue("@TaskTitle", todo.Title);
            cmd.Parameters.AddWithValue("@TaskDescription", todo.Description);
            cmd.Parameters.AddWithValue("@CategoryID", todo.CategoryID);
            cmd.Parameters.AddWithValue("@PriorityLevel", todo.PriorityLevel);
            cmd.Parameters.AddWithValue("@Status", todo.Status);
            cmd.Parameters.AddWithValue("@DueDate", todo.DueDate);
            if (todo.Status == "Completed")
            {
                cmd.Parameters.AddWithValue("@CompletedDate", todo.CompletedDate = DateTime.Now);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CompletedDate", DBNull.Value);
            }
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            return Ok(result > 0 ? "Updating Successfully.." : "Updating failed..");
        }

        [HttpPatch("{id}")]
        public IActionResult PatchToDo(int id, ToDoViewModel todo)
        {
            string conditions = "";
            if (!string.IsNullOrEmpty(todo.Title))
            {
                conditions += " [TaskTitle] = @TaskTitle, ";
            }
            if (!string.IsNullOrEmpty(todo.Description))
            {
                conditions += " [TaskDescription] = @TaskDescription, ";
            }
            if (null != todo.CategoryID)
            {
                conditions += " [CategoryID] = @CategoryID, ";
            }
            if (null != todo.PriorityLevel)
            {
                conditions += " [PriorityLevel] = @PriorityLevel, ";
            }
            if (!string.IsNullOrEmpty(todo.Status))
            {
                conditions += " [Status] = @Status, ";
            }
            if (null != todo.DueDate)
            {
                conditions += " [DueDate] = @DueDate, ";
            }
            if (null != todo.CompletedDate)
            {
                conditions += " [CompletedDate] = @CompletedDate, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Parameters!");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[ToDoList] SET {conditions} WHERE TaskID = @TaskId AND DeleteFlag = 0;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TaskId", id);
            if (!string.IsNullOrEmpty(todo.Title))
            {
                command.Parameters.AddWithValue("@TaskTitle", todo.Title);
            }
            if (!string.IsNullOrEmpty(todo.Description))
            {
                command.Parameters.AddWithValue("@TaskDescription", todo.Description);
            }
            if (null != todo.CategoryID)
            {
                command.Parameters.AddWithValue("@CategoryID", todo.CategoryID);
            }
            if (null != todo.PriorityLevel)
            {
                command.Parameters.AddWithValue("@PriorityLevel", todo.PriorityLevel);
            }
            if (!string.IsNullOrEmpty(todo.Status))
            {
                command.Parameters.AddWithValue("@Status", todo.Status);
            }
            if (null != todo.DueDate)
            {
                command.Parameters.AddWithValue("@DueDate", todo.DueDate);
            }
            if (todo.Status == "Completed")
            {
                command.Parameters.AddWithValue("@CompletedDate", todo.CompletedDate = DateTime.Now);
            }
            if (todo.Status != "Completed")
            {
                command.Parameters.AddWithValue("@CompletedDate", DBNull.Value);
            }
            int result = command.ExecuteNonQuery();
            connection.Close();

            return Ok(result > 0 ? "Updating successfully.." : "Updating failed..");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            //var deleteQuery = @"DELETE FROM [dbo].[ToDoList]
            //                    WHERE TaskID = @TaskId";
            var deleteQuery = @"UPDATE [dbo].[ToDoList]
                                   SET [DeleteFlag] = 1
                                 WHERE TaskID = @TaskId;";

            SqlCommand command = new SqlCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@TaskId", id);

            int result = command.ExecuteNonQuery();
            connection.Close();
            return Ok(result > 0 ? "Successfully Deleted.." : "Deleting Failed..");
        }
    }
}
