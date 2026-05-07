using DojoManager.Data;
using DojoManager.Services.DependencyInjectionHandler;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
Env.Load();
// Add services to the container.
builder.Services.AddControllersWithViews();
string? password = Environment.GetEnvironmentVariable("POSTGRESPSW");
string? connectionString = $"Host=localhost;Port=5433;Database=DojoManager;Username=postgres;Password={password}";
builder.Configuration["ConnectionStrings:PostgreSQL"] = connectionString;

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
