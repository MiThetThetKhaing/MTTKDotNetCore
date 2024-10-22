using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MTTKDotNetCore.TodoListRestAPI.ViewModels;

namespace MTTKDotNetCore.TodoListRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCategoryAdoDotNetController : Controller
    {
        private readonly string _connectionString = "Data Source=DESKTOP-6QTP69L\\MSSQLSERVER2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sa123; TrustServerCertificate=true";

        [HttpGet]
        public IActionResult GetCategories()
        {
            List<CategoryViewModel> lst = new List<CategoryViewModel>();

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            string query = @"SELECT [CategoryID]
                          ,[CategoryName]
                            FROM [dbo].[TaskCategory] where DeleteFlag = 0;";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["CategoryID"]);
                Console.WriteLine(reader["CategoryName"]);

                var item = new CategoryViewModel
                {
                    Id = Convert.ToInt32(reader["CategoryId"]),
                    Name = Convert.ToString(reader["CategoryName"])
                };
                lst.Add(item);
            }
            connection.Close();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            CategoryViewModel category = new CategoryViewModel();
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string query = @"SELECT [CategoryID]
                            ,[CategoryName]
                            FROM [dbo].[TaskCategory] where CategoryID = @CategoryId and DeleteFlag = 0;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CategoryId", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["CategoryID"]);
                Console.WriteLine(reader["CategoryName"]);

                category = new CategoryViewModel
                {
                    Id = Convert.ToInt32(reader["CategoryID"]),
                    Name = Convert.ToString(reader["CategoryName"])
                };
            }
            connection.Close();
            if (category.Id > 0)
            {
                return Ok(category);
            }
            else
            {
                return BadRequest("No data Found!");
            }
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryViewModel category)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[TaskCategory]
                           ([CategoryName],[DeleteFlag])
                     VALUES
                           (@CategoryName,0)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CategoryName", category.Name);

            int result = command.ExecuteNonQuery();
            connection.Close();

            return Ok(result > 0 ? "Saving Successfully.." : "Saving failed..");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryViewModel category)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = @"UPDATE [dbo].[TaskCategory]
                           SET [CategoryName] = @CategoryName
                         WHERE CategoryID = @CategoryId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CategoryName", category.Name);
            cmd.Parameters.AddWithValue("@CategoryId", id);

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            return Ok(result > 0 ? "Updating Successfully.." : "Updating failed..");
        }

        [HttpPatch("{id}")]
        public IActionResult PatchCategory(int id, CategoryViewModel category)
        {
            string conditions = "";
            if (!string.IsNullOrEmpty(category.Name))
            {
                conditions += " [CategoryName] = @CategoryName, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Parameters!");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);
            
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[TaskCategory] SET {conditions} WHERE CategoryID = @CategoryId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CategoryId", id);
            if (!string.IsNullOrEmpty(category.Name))
            {
                command.Parameters.AddWithValue("@CategoryName", category.Name);
            }

            int result = command.ExecuteNonQuery();
            connection.Close();

            return Ok(result > 0 ? "Updating successfully.." : "Updating failed..");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            //var deleteQuery = @"DELETE FROM [dbo].[TaskCategory]
            //                    WHERE CategoryID = @CategoryId";
            var deleteQuery = @"UPDATE [dbo].[TaskCategory]
                                   SET [DeleteFlag] = 1
                                 WHERE CategoryID = @CategoryId;";

            SqlCommand command = new SqlCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@CategoryId", id);

            int result = command.ExecuteNonQuery();
            connection.Close();

            return Ok(result > 0 ? "Successfully Deleted.." : "Deleting Failed..");
        }
    }
}
