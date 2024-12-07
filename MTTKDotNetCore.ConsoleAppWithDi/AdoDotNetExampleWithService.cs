using MTTKDotNetCore.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.ConsoleApp
{
    public class AdoDotNetExampleWithService
    {
        private readonly string _connectionString = "Data Source=DESKTOP-6QTP69L\\MSSQLSERVER2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sa123;";
        private readonly AdoDotNetService _adoDotNetService;

        public AdoDotNetExampleWithService()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlag]
                              FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            var dt = _adoDotNetService.Query(query); 

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
                //Console.WriteLine(dr["DeleteFlag"]);
            }
        }

        public void Edit()
        {
            Console.Write("Blog Id : ");
            string id = Console.ReadLine();

            string query = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlag]
                              FROM [dbo].[Tbl_Blog] where BlogId= @BlogId";

            //SqlParameterModel[] sqlParameters = new SqlParameterModel[1];
            //sqlParameters[0] = new SqlParameterModel
            //{
            //    Name = "@BlogId",
            //    Value = id
            //};

            //var dt = _adoDotNetService.Query(query, sqlParameters);

            var dt = _adoDotNetService.Query(query, new SqlParameterModel("@BlogId", id));
            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);
        }

        public void Create()
        {
            Console.WriteLine("Blog Title :");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author :");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content :");
            string content = Console.ReadLine();

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

            var result = _adoDotNetService.Execute(queryInsert, 
                                                    new SqlParameterModel("@BlogTitle", title),
                                                    new SqlParameterModel("@BlogAuthor", author),
                                                    new SqlParameterModel("@BlogContent", content)
                                                    );
            Console.WriteLine(result == 1 ? "Saving successful.." : "Saving failed..");
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

            string queryInsert = $@"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                              ,[DeleteFlag] = 0
                         WHERE BlogId = @BlogId";

            int result = _adoDotNetService.ExecuteNonQuery(queryInsert,
                new SqlParameterModel("@BlogId", id),
                new SqlParameterModel("@BlogTitle", title),
                new SqlParameterModel("@BlogAuthor", author),
                new SqlParameterModel("@BlogContent", content));

            Console.WriteLine(result == 1 ? "Updating successfully.." : "Updating failed..");

        }

        public void Delete()
        {
            Console.Write("Blog Id : ");
            string id = Console.ReadLine();

            string queryDelete = @"UPDATE [dbo].[Tbl_Blog] SET [DeleteFlag] = 1
                                 WHERE BlogId = @BlogId";

            int result = _adoDotNetService.ExecuteNonQuery(queryDelete, new SqlParameterModel("@BlogId", id));

            Console.WriteLine(result > 0 ? "Successfully Deleted" : "Deleting failed");
        }
    }
}
