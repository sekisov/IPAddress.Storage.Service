using Microsoft.OpenApi.Models;
using IPAddress.Storage.Service.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .AddJsonFile("/secrets/appsettings.secrects.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "API IP address storage service",
            Version = "v1",
            Description = "API Description"
        });
});
// Add services to the container.

var configurator = new ConfigurationService(builder.Services, builder.Configuration);

configurator.Configure();

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
