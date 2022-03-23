global using Microsoft.AspNetCore.Mvc;
global using Siva.Marriages.Business;
global using Siva.Marriages.Business.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Siva.Marriages.WebApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAppServices(builder.Configuration);

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddResponseCompression();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            await context.Response.WriteAsync($"Server error. Detailed message: {exception} ");
        });
    });
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseMiddleware<JwtMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseResponseCompression();
}

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
