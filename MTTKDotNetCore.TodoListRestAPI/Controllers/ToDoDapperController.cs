using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using MTTKDotNetCore.TodoListRestAPI.DataModels;
using MTTKDotNetCore.TodoListRestAPI.ViewModels;
using System.Data;

namespace MTTKDotNetCore.TodoListRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoDapperController : Controller
    {
        private readonly string _connectionString = "Data Source=DESKTOP-6QTP69L\\MSSQLSERVER2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sa123; TrustServerCertificate=true";

        [HttpGet]
        public IActionResult GetTodoList()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT [TaskID]
                                  ,[TaskTitle]
                                  ,[TaskDescription]
                                  ,[CategoryID]
                                  ,[PriorityLevel]
                                  ,[Status]
                                  ,[DueDate]
                                  ,[CreatedDate]
                                  ,[CompletedDate]
                                  ,[DeleteFlag]
                              FROM [dbo].[ToDoList] WHERE DeleteFlag = 0";
                var lst = db.Query<ToDoDataModel>(query).ToList();
                return Ok(lst);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTodo(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT [TaskID]
                                  ,[TaskTitle]
                                  ,[TaskDescription]
                                  ,[CategoryID]
                                  ,[PriorityLevel]
                                  ,[Status]
                                  ,[DueDate]
                                  ,[CreatedDate]
                                  ,[CompletedDate]
                                  ,[DeleteFlag]
                              FROM [dbo].[ToDoList] 
                              WHERE DeleteFlag = 0 AND TaskID = @TaskId";
                var item = db.Query<ToDoDataModel>(query, new ToDoDataModel
                {
                    TaskID = id
                }).FirstOrDefault();
                if (item is null)
                {
                    return BadRequest("No Data Found!");
                }
                else
                {
                    return Ok(item);
                }
            };
        }

        [HttpPost]
        public IActionResult CreateTodo(ToDoViewModel todo)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string queryInsert = @"INSERT INTO [dbo].[ToDoList]
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
                                           ,@CategoryId
                                           ,@PriorityLevel
                                           ,@Status
                                           ,@DueDate
                                           ,@CreatedDate
                                           ,@CompletedDate
                                           ,0);";
                var result = db.Execute(queryInsert, new
                {
                    TaskTitle = todo.Title,
                    TaskDescription = todo.Description,
                    CategoryId = todo.CategoryID,
                    PriorityLevel = todo.PriorityLevel,
                    Status = todo.Status,
                    DueDate = todo.DueDate,
                    CreatedDate = todo.CreatedDate,
                    CompletedDate = todo.CompletedDate
                });
                return Ok(result > 0 ? "Successfully Created.." : "Creating failed..");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, ToDoViewModel todo)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string queryUpdate = @"UPDATE [dbo].[ToDoList]
                                       SET [TaskTitle] = @TaskTitle
                                          ,[TaskDescription] = @TaskDescription
                                          ,[CategoryID] = @CategoryId
                                          ,[PriorityLevel] = @PriorityLevel
                                          ,[Status] = @Status
                                          ,[DueDate] = @DueDate
                                          ,[CompletedDate] = @CompletedDate
                                     WHERE TaskID = @TaskId";
                var result = db.Execute(queryUpdate, new
                {
                    TaskId = id,
                    TaskTitle = todo.Title,
                    TaskDescription = todo.Description,
                    CategoryId = todo.CategoryID,
                    PriorityLevel = todo.PriorityLevel,
                    Status = todo.Status,
                    DueDate = todo.DueDate,
                    CompletedDate = todo.Status == "Completed" ? todo.CompletedDate = DateTime.Now : todo.CompletedDate = null
                });
                return Ok(result > 0 ? "Successfully Updated.." : "Updating failed..");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTodo(int id, ToDoViewModel todo)
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
                conditions += " [CategoryID] = @CategoryId, ";
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
                return BadRequest("Invalid Parameter!");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string queryPatch = $@"UPDATE [dbo].[ToDoList] SET {conditions} WHERE TaskID = @TaskId";

                var result = db.Execute(queryPatch, new
                {
                    TaskId = id,
                    TaskTitle = todo.Title,
                    TaskDescription = todo.Description,
                    CategoryId = todo.CategoryID,
                    PriorityLevel = todo.PriorityLevel,
                    Status = todo.Status,
                    DueDate = todo.DueDate,
                    CompletedDate = todo.Status == "Completed" ? todo.CompletedDate = DateTime.Now : todo.CompletedDate = null
                });
                return Ok(result > 0 ? "Successfully Updated.." : "Updating failed..");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string queryDelete = @"UPDATE [dbo].[ToDoList]
                                   SET [DeleteFlag] = 1
                                 WHERE TaskID = @TaskId;";
                var result = db.Execute(queryDelete, new
                {
                    TaskId = id
                });
                return Ok(result > 0 ? "Successfully Deleted.." : "Deleting failed..");
            }
        }
    }
}
