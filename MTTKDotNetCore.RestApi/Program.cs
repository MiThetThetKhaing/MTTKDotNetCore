using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.Database.Models;
using MTTKDotNetCore.Domain.Features.Blog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Ui 
// Business layer
// Data access

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});
builder.Services.AddScoped<IBlogService, BlogService>();
//builder.Services.AddScoped<BlogV2Service>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
