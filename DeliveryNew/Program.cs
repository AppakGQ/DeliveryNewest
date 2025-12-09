using Microsoft.EntityFrameworkCore;
using DeliveryNew.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using DeliveryNew.Middleware;
using DeliveryNew.Filters;

// Fix for PostgreSQL DateTime issue
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Registering the GlobalActionFilter to apply to ALL controllers and actions in the application.
    // This filter logs the start and end of every action execution.
    options.Filters.Add<GlobalActionFilter>();
})
.AddViewLocalization()
.AddDataAnnotationsLocalization();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Custom Middleware
// Custom Middleware
// Deprecated: Moved 'app.UseRequestLogging()' below 'app.UseStaticFiles()' for better location in pipeline
// app.UseRequestLogging();

app.UseStaticFiles();

// Using the custom RequestLoggingMiddleware.
// This middleware logs every incoming HTTP request (Method + Path) and the response Status Code.
// It is placed early in the pipeline (but after static files) to capture requests reaching the application logic.
//app.UseRequestLogging(); 

app.UseSession(); // Enable Session Middleware
app.UseRouting();

// Localization Options
var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("ru-RU") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ru-RU"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Auth
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
