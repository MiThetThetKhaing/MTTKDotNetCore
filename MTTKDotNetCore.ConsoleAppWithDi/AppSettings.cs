using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.ConsoleAppWithDi
{
    public class AppSettings
    {
        public static string ConnectionString { get; } = "Data Source=DESKTOP-6QTP69L\\MSSQLSERVER2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sa123;";
    }
}
