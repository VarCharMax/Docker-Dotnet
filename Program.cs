using ExampleApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddCommandLine(args)
    .AddEnvironmentVariables()
    .Build();

if ((config["INITDB"] ?? "false") == "true")
{
    Console.WriteLine("Preparing datatbase ...");
    SeedData.EnsurePopulated(new ProductDbContext());
    Console.WriteLine("Datatbase preparation complete");
}
else
{
    System.Console.WriteLine("Starting ASP.NET...");
    var host1 = new WebHostBuilder()
    .UseConfiguration(config)
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseIISIntegration()
    .Build();
}

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

// SeedData.EnsurePopulated(app);

app.Run();
