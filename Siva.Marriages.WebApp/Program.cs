using Microsoft.AspNetCore.SpaServices.AngularCli;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

if (Debugger.IsAttached)
{
    app.UseSpa(builder =>
    {
        builder.Options.SourcePath = "ClientApp";
        builder.UseAngularCliServer(npmScript: "start");
    });
}
else
{
    app.MapFallbackToFile("index.html");
}

app.Run();
