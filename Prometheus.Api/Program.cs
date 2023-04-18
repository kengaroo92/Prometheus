using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Prometheus.Api.Data;
using System;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// We're using the GetConfiguration method (defined below) to read the configuration
// during both build time and runtime. This is necessary to ensure that EF Core tools
// and the runtime pipeline both read the configuration correctly.
string connectionString = Environment.GetEnvironmentVariable("DefaultConnection") ?? builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddCors(); // Add CORS support to the API

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

app.UseCors(policy =>
{
    policy
    .WithOrigins("*") // Eventually replace with the allowed origins. For now it allows any origin to connect.
    .AllowAnyMethod() // Allow any method. Can specify which methods to allow. Ex. ("GET", "POST").
    .AllowAnyHeader() // Allow any header. Can specify which headers to allow. Ex. ("Content-Type", "Authorization").
    .WithExposedHeaders("Content-Disposition"); // Custom headers you want to expose in the response.
});

// Make sure to keep this after the UseCors() method. It's essential for the middleware pipeline to work correctly.
app.UseAuthorization();

app.MapControllers();

app.Run();

// This static method is used to build the IConfiguration object.
// It reads the configuration from the appsettings.json file and other sources,
// such as environment variables and command-line arguments.
// By having this separate method, we can ensure that both the runtime pipeline
// and the EF Core tools read the configuration correctly.
// The code was adjusted this way to allow CLI database updates since we are using EFCore.
static IConfiguration GetConfiguration(string[] args)
{
    var configBuilder = new ConfigurationBuilder()
        .SetBasePath(Environment.CurrentDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .AddEnvironmentVariables()
        .AddCommandLine(args);

    return configBuilder.Build();
}
