using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TaskService.Api.Data;
using TaskService.Api.Repositories;
using TaskService.Repositories;
using TaskServiceClass = TaskService.TaskService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(); 
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("Default") 
                    ?? Environment.GetEnvironmentVariable("ConnectionString_Default");


builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ITaskRepository, EfTaskRepository>();
builder.Services.AddScoped<TaskServiceClass>();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.MapHealthChecks("/health");
app.Urls.Add("http://0.0.0.0:80");
app.Run();
