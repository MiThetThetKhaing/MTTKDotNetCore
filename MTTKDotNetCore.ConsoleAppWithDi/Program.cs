// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using MTTKDotNetCore.ConsoleApp;

//Console.WriteLine("Hello, World!");
//Console.ReadLine();
//Console.ReadKey();

// md => markdown 

// C# <=> Database

// ADO.NET
// Dapper
// EFCore / EntityFramework (ORM)

// C# => sql query

// nuget

// max connection = 100
// 100 - 99
// 101


//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();

//DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("So Cute", "KKO", "Something");
//dapperExample.Edit(1);
//dapperExample.Edit(2);
//dapperExample.Update(2, "haha", "usus", "update");
//dapperExample.Delete(3);

//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Read();
//eFCoreExample.Create("new", "kkoo", "create new blog");
//eFCoreExample.Edit(10);
//eFCoreExample.Update(10, "update", "Mttk", "updating");
//eFCoreExample.Delete(8);

//AdoDotNetExampleWithService adoDotNetExampleWithService = new AdoDotNetExampleWithService();
//adoDotNetExampleWithService.Read();

//DapperExampleWithService dapperExampleWithService = new DapperExampleWithService();
//dapperExampleWithService.Read();

var services = new ServiceCollection().AddSingleton<AdoDotNetExample>().BuildServiceProvider();
var services2 = new ServiceCollection().AddSingleton<AdoDotNetExampleWithService>().BuildServiceProvider(); 
var servicesDapper = new ServiceCollection().AddSingleton<DapperExample>().BuildServiceProvider();
var servicesDapper2 = new ServiceCollection().AddSingleton<DapperExampleWithService>().BuildServiceProvider();
var servicesEfcore = new ServiceCollection().AddSingleton<EFCoreExample>().BuildServiceProvider();

//var adoDotNetExample = services.GetRequiredService<AdoDotNetExample>();
//adoDotNetExample.Read();

//var adoDotNetExampleWithService = services2.GetRequiredService<AdoDotNetExampleWithService>();
//adoDotNetExampleWithService.Read();
//adoDotNetExampleWithService.Update();
//adoDotNetExampleWithService.Create();
//adoDotNetExampleWithService.Delete();

//var dapperExample = servicesDapper.GetRequiredService<DapperExample>();
//dapperExample.Read();
//dapperExample.Update(1037, "update dapper", "update dapper author", "update dapper content");

//var dapperExampleWithService = servicesDapper2.GetRequiredService<DapperExampleWithService>();
//dapperExampleWithService.Read();
//dapperExampleWithService.Update(6, "update title", "update author", "update content");

var eFCoreExample = servicesEfcore.GetRequiredService<EFCoreExample>();
eFCoreExample.Read();

Console.ReadKey();
