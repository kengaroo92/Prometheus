using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Prometheus.Api.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// We're using the GetConfiguration method (defined below) to read the configuration
// during both build time and runtime. This is necessary to ensure that EF Core tools
// and the runtime pipeline both read the configuration correctly.
string connectionString = Environment.GetEnvironmentVariable("DefaultConnection") ?? builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

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
