using DojoManager.Data;
using DojoManager.Services;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
var builder = WebApplication.CreateBuilder(args);
Env.Load();
// Add services to the container.
builder.Services.AddControllersWithViews();
string? password = Environment.GetEnvironmentVariable("POSTGRESPSW");
string? connectionString = $"Host=localhost;Port=5433;Database=DojoManager;Username=postgres;Password={password}";
builder.Configuration["ConnectionStrings:PostgreSQL"] = connectionString;
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
