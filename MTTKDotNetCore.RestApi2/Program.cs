using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(x => new HttpClient()
{
    BaseAddress = new Uri(builder.Configuration.GetSection("ApiDomainUrl").Value!)
});
builder.Services.AddSingleton(x => new RestClient(builder.Configuration.GetSection("ApiDomainUrl").Value!));
builder.Services
    .AddRefitClient<IPickAPileApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiDomainUrl").Value!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

app.MapGet("/birds", async ([FromServices] HttpClient httpClient) =>
{
    var response = await httpClient.GetAsync("birds");
    return await response.Content.ReadAsStringAsync();
});

app.MapGet("/bagan-map", async ([FromServices] RestClient restClient) =>
{
    RestRequest request = new RestRequest("bagan-map", Method.Get);
    var response = await restClient.GetAsync(request);
    return response.Content;
});

app.MapGet("/pick-a-pile", async ([FromServices] IPickAPileApi pickAPile) =>
{
    var response = await pickAPile.GetPickAPile();
    return response;
});
app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}

public interface IPickAPileApi
{
    [Get("/pick-a-pile")]
    Task<List<PickAPileModel>> GetPickAPile();
}


public class PickAPileModel
{
    public int QuestionId { get; set; }
    public string QuestionName { get; set; }
    public string QuestionDesp { get; set; }
}
