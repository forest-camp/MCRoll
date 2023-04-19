using MCRoll.Data;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables();

// configure database
var dbHost = builder.Configuration.GetValue<String>("DB_HOST");
var dbPort = builder.Configuration.GetValue<String>("DB_PORT");
var dbUser = builder.Configuration.GetValue<String>("DB_USER");
var dbPassword = builder.Configuration.GetValue<String>("DB_PASSWORD");
var dbName = builder.Configuration.GetValue<String>("DB_NAME");
String connectingString = $"Server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPassword};";
builder.Services.AddDbContextPool<MCRollDbContext>(options =>
    options.UseMySql(connectingString, ServerVersion.AutoDetect(connectingString)));

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}");
});

app.Run();