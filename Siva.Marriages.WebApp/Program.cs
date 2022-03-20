global using Microsoft.AspNetCore.Mvc;
global using Siva.Marriages.Business;
global using Siva.Marriages.Business.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Siva.Marriages.WebApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
//builder.Services.Configure<ApiBehaviorOptions>(options =>
//                {
//                    Request.Body.Position = 0;
//                    string mappingsFile = await new StreamReader(Request.Body).ReadToEndAsync();
//                    // Prevents auto model binding validation. Uncomment if you have custom validation logic
//                    options.SuppressModelStateInvalidFilter = true;
//                });

builder.Services.AddControllersWithViews(options => { 
    //options.va
});

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

// Uncomment for model bind debug
//app.Use(async (context, next) => {
//    context.Request.EnableBuffering();
//    await next();
//});

app.UseHttpsRedirection();
app.UseCookiePolicy(new CookiePolicyOptions() 
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    Secure = CookieSecurePolicy.Always
});
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
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
