using Dapper;
using MTTKDotNetCore.ConsoleApp.Models;
using MTTKDotNetCore.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.ConsoleApp
{
    public class DapperExampleWithService
    {
        private readonly string _connectionString = "Data Source=DESKTOP-6QTP69L\\MSSQLSERVER2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sa123;";
        private readonly DapperService _dapperService;

        public DapperExampleWithService()
        {
            _dapperService = new DapperService(_connectionString);
        }

        public void Read()
        {
            string query = "select * from tbl_Blog where DeleteFlag = 0;";
            var lst = _dapperService.Query<BlogDapperDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Edit(int id)
        {
            string query = "select * from tbl_Blog where DeleteFlag = 0 and BlogId = @BlogId;";
            var item = _dapperService.QueryFirstOrDefault<BlogDapperDataModel>(query, new BlogDapperDataModel
            {
                BlogId = id
            });

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

            int result = _dapperService.Execute(queryInsert, new
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            });
            Console.WriteLine(result == 1 ? "Saving Successful.." : "Saving Failed..");
        }

        public void Update(int id, string title, string author, string content)
        {
            string queryUpdate = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                              ,[DeleteFlag] = 0
                         WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(queryUpdate, new
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });

            Console.WriteLine(result == 1 ? "Updating Successfully.." : "Updating Failed..");
        }

        public void Delete(int id)
        {
            string deleteQuery = @"UPDATE [dbo].[Tbl_Blog] SET [DeleteFlag] = 1 WHERE BlogId = @BlogId;";
            int result = _dapperService.Execute(deleteQuery, new
            {
                BlogId = id
            });

            Console.WriteLine(result == 1 ? "Delete Successfully.." : "Delete Failed..");
        }
    }
}
