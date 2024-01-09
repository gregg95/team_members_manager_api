using Application;
using Infrastructure;
using Microsoft.OpenApi.Models;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var corsPolicyName = "MyAllowSpecificOrigins";

services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});

services.AddInfractucture(builder.Configuration);
services.AddApplication();
services.AddControllers();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TeamMembersManagerAPI_v1",
        Version = "v1"
    });
});

var app = builder.Build();

var dev = app.Environment.IsDevelopment();

app.ApplyMigrations();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseErrorHandlingMiddleware();

app.UseCors(corsPolicyName);

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeamMembersManagerAPI_v1");
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();