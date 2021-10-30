using System;
using AsukaApi.Application;
using AsukaApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Migrations;
using Serilog;

Console.WriteLine(DateTime.UtcNow.ToString("R"));
Console.WriteLine(Environment.ProcessId);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Initialize Serilog logger from appsettings.json configurations.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

string connectionString = builder.Configuration.GetConnectionString("Docker");
DatabaseMigrations.ExecuteScripts(connectionString);

builder.Host.UseSerilog();
builder.Services.AddOptions();
builder.Services.AddInfrastructure(connectionString);

// I added this AddRouting line. Should go above AddControllers.
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy());

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebApi",
        Version = "v1"
    });
    c.CustomSchemaIds(type => type.FullName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AsukaNet-Api WebApi v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
