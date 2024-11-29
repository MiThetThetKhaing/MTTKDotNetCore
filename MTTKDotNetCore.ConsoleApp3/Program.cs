// See https://aka.ms/new-console-template for more information
using MTTKDotNetCore.ConsoleApp3;

Console.WriteLine("Hello, World!");

// get, post, put, patch, delete

// resource , endpoint

HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.Read();
//await httpClientExample.Edit(1);
//await httpClientExample.Edit(101);
//await httpClientExample.Create(1, "something", "something");

await httpClientExample.Update(1 ,1, "something", "something");


