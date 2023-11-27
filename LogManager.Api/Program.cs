using LogManager.Core.Repositories;
using LogManager.Core.Services;
using LogManager.Data;
using LogManager.Data.Repositories;
using LogManager.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure Entity Framework Core with postgresql   for the database

builder.Services.AddDbContextFactory<LogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LogManagerDB")), ServiceLifetime.Scoped);




// Register repositories for dependency injection

builder.Services.AddScoped<ILogMessageRepository, LogMessageRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();


// Register services for dependency injection
builder.Services.AddScoped<ILogMessageServices, LogMessageService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
