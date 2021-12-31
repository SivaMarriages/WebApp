using Microsoft.AspNetCore.SpaServices.AngularCli;
using Siva.Marriages.WebApp.Helpers;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAppServices();

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new DefaultNamingStrategy()
    };
});

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
