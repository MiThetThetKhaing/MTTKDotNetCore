using MTTKDotNetCore.ConsoleApp4;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = AppSettings.ConnectionString;

        public void Read()
        {
            Console.WriteLine("Connection String : " + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine("Connection Opening....");
            connection.Open();
            Console.WriteLine("Connection Opened....");

            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                //Console.WriteLine(dr["DeleteFlag"]);
            }


            Console.WriteLine("Connection Closing....");
            connection.Close();
            Console.WriteLine("Connection Closed....");

            //DataSet
            // DataTable
            // DataRow
            // DataColumn

            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //    //Console.WriteLine(dr["DeleteFlag"]);
            //}
        }
    
        public void Create()
        {
            Console.WriteLine("Blog Title :");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author :");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content :");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string queryInsert = $@"INSERT INTO [dbo].[Tbl_Blog]
                                   ([BlogTitle]
                                   ,[BlogAuthor]
                                   ,[BlogContent]
                                   ,[DeleteFlag])
                             VALUES
                                   (@BlogTitle
                                   ,@BlogAuthor
                                   ,@BlogContent
                                   ,0)";

            SqlCommand cmd2 = new SqlCommand(queryInsert, connection);
            cmd2.Parameters.AddWithValue("@BlogTitle", title);
            cmd2.Parameters.AddWithValue("@BlogAuthor", author);
            cmd2.Parameters.AddWithValue("@BlogContent", content);

            //SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
            //DataTable dt2 = new DataTable();
            //adapter.Fill(dt2);

            int result = cmd2.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Saving successful.." : "Saving failed..");

            //if (result == 1)
            //{
            //    Console.WriteLine("Saving successful..");
            //}
            //else
            //{
            //    Console.WriteLine("Saving failed..");
            //}
        }

        public void Edit()
        {
            Console.Write("Blog Id : ");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where BlogId= @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data found..");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);
            //    //Console.WriteLine(dr["DeleteFlag"]);
        }

        public void Update()
        {
            Console.WriteLine("Blog Id :");
            string id = Console.ReadLine();

            Console.WriteLine("Blog Title :");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author :");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content :");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string queryInsert = $@"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                              ,[DeleteFlag] = 0
                         WHERE BlogId = @BlogId";

            SqlCommand cmd2 = new SqlCommand(queryInsert, connection);
            cmd2.Parameters.AddWithValue("@BlogId", id);
            cmd2.Parameters.AddWithValue("@BlogTitle", title);
            cmd2.Parameters.AddWithValue("@BlogAuthor", author);
            cmd2.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd2.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Updating successfully.." : "Updating failed..");

        }

        public void Delete()
        {
            Console.Write("Blog Id : ");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string queryDelete = @"UPDATE [dbo].[Tbl_Blog] SET [DeleteFlag] = 1
                                 WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(queryDelete, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            if (result == 1)
            {
                Console.WriteLine("Delete Complete..");
            }
            else
            {
                Console.WriteLine("Delete failed..");
            }

        }
    }
}
