using Dapper;
using MTTKDotNetCore.ConsoleApp.Models;
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
    public class DapperExample
    {
        private readonly string _connectionString = AppSettings.ConnectionString;
        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_Blog where DeleteFlag = 0;";
                var lst = db.Query<BlogDapperDataModel>(query).ToList();
                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }
            }
            // DTO => Data Transfer Object
        }

        public void Create(string title, string author, string content)
        {
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

            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(queryInsert, new
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                });
                Console.WriteLine(result == 1 ? "Saving Successful.." : "Saving Failed..");
            }
        }

        public void Edit(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_Blog where DeleteFlag = 0 and BlogId = @BlogId;";
                var item = db.Query<BlogDapperDataModel>(query, new BlogDapperDataModel
                {
                    BlogId = id
                }).FirstOrDefault();

                //if (item == null)
                if (item is null)
                {
                    Console.WriteLine("No Data Found..");
                    return;
                }
                
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
            }
        }

        public void Update(int id, string title, string author, string content)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string queryUpdate = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                              ,[DeleteFlag] = 0
                         WHERE BlogId = @BlogId";

                int result = db.Execute(queryUpdate, new
                {
                    BlogId = id,
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                });

                Console.WriteLine(result == 1 ? "Updating Successfully.." : "Updating Failed..");
            }
        }

        public void Delete(int id) 
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string deleteQuery = @"UPDATE [dbo].[Tbl_Blog] SET [DeleteFlag] = 1 WHERE BlogId = @BlogId;";
                int result = db.Execute(deleteQuery, new
                {
                    BlogId = id
                });

                Console.WriteLine(result == 1 ? "Delete Successfully.." : "Delete Failed..");
            }
        }
    }
}
