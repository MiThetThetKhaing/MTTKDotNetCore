using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/snakes", () =>
{
    var folderPath = "Data/Snakes.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<SnakeModel[]>(jsonStr)!;
    //var result = JsonConvert.DeserializeObject<SnakeResponseModel>(jsonStr);

    return Results.Ok(result);
})
.WithName("GetSnakes")
.WithOpenApi();

app.MapGet("/snake", (int id) =>
{
    var folderPath = "Data/Snakes.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<SnakeModel[]>(jsonStr)!;

    var item = result.FirstOrDefault(x => x.Id == id);
    if (item is null) return Results.BadRequest("No Data Found!");

    return Results.Ok(item);
})
.WithName("GetSnake")
.WithOpenApi();

//app.MapPost("/snakes", (SnakeModel snake) =>
//{
//    var folderPath = "Data/Snakes.json";

//    var obj = new SnakeResponseModel()
//    {
//        Property1 = new SnakeModel[]
//        {
//            new SnakeModel
//            {
//                Id = snake.Id,
//                MMName = snake.MMName,
//                EngName = snake.EngName,
//                Detail = snake.Detail,
//                IsPoison = snake.IsPoison,
//                IsDanger = snake.IsDanger
//            }
//        }
//    };
//    var jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
//    File.WriteAllText(folderPath, jsonStr);

//    //var jsonStr = File.ReadAllText(folderPath);

//    return Results.Ok("Json is successfully uploaded.");
//})
//.WithName("CreateSnake")
//.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


public class SnakeResponseModel
{
    public SnakeModel[] Property1 { get; set; }
}

public class SnakeModel
{
    public int Id { get; set; }
    public string MMName { get; set; }
    public string EngName { get; set; }
    public string Detail { get; set; }
    public string IsPoison { get; set; }
    public string IsDanger { get; set; }
}
