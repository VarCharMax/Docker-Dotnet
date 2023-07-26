using ExampleApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var host = configuration["DBHOST"] ?? "localhost";
var port = configuration["DBPORT"] ?? "3306";
var password = configuration["DBPASSWORD"] ?? "mysecret";

var connectionString = $"server={host};port={port};database=products;user=root;password={password};";

builder.Services.AddTransient<IRepository, DummyRepository>();
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseMySQL(connectionString)
);

builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddTransient<IRepository, ProductRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

SeedData.EnsurePopulated(app);

app.Run();
