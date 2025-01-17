// See https://aka.ms/new-console-template for more information
using MTTKDotNetCore.Database.Models;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

//AppDbContext db = new AppDbContext();
//var lst = db.TblBlogs.ToList();

var blog = new BlogModel
{
    Id = 1,
    Title = "Test Title",
    Author = "Test Author",
    Content = "Test Content"
};

//string jsonStr = JsonConvert.SerializeObject(blog, Formatting.Indented); // C# to json
string jsonStr = blog.ToJson(); // C# to json

string jsonStr2 = """{"Id":1,"Title":"Test Title","Author":"Test Author","Content":"Test Content"}""";
var blog2 = JsonConvert.DeserializeObject<BlogModel>(jsonStr2)!;
Console.WriteLine(blog2.Title);

Console.WriteLine(jsonStr);

public class BlogModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}

public static class Extensions // DevCode
{
    public static string ToJson(this Object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return jsonStr;
    }
}


