using MHR_V02.Data;
using MHR_V02.Filters;
using MHR_V02.Services;
using MHR_V02.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using MHR_V02.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();


// Add DbContext with SQL Server provider
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Language Resources

builder.Services.AddLocalization(options => options.ResourcesPath = Path.Combine("MHR_V02", "Resources"));

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en", "fa", "ur" };// زبان‌های پشتیبانی شده
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
    options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
    options.SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
});





// Register services
builder.Services.AddScoped<MigrationService>();
builder.Services.AddTransient<SeedDataService>();
builder.Services.AddScoped<MHR_V02.Filters.LogActionFilter>(); // ثبت فیلتر اکشن
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<AccessControlFilter>(); // ثبت فیلتر AccessControlFilter


// Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// End session support

// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login";
        options.LogoutPath = "/Users/Logout";
    });
// End authentication

var app = builder.Build();


// Add Resources
app.UseRequestLocalization();
// End Resources




// Apply migrations and seed data automatically
using (var scope = app.Services.CreateScope())
{
    var migrationService = scope.ServiceProvider.GetRequiredService<MigrationService>();
    migrationService.ApplyMigrations();

    var seedDataService = scope.ServiceProvider.GetRequiredService<SeedDataService>();
    seedDataService.Initialize(scope.ServiceProvider); // ارسال serviceProvider به متد Initialize
    // استخراج و ذخیره‌ی نام اکشن‌ها
    ActionNameExtractor.ExtractActionNames(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// Enable session management
app.UseSession();
// End session management

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();
// End authentication and authorization

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Users}/{action=Login}/{id?}");

app.Run();
