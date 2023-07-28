using ExampleApp;
using ExampleApp.Models;

var config = new ConfigurationBuilder()
    .AddCommandLine(args)
    .AddEnvironmentVariables()
    .Build();

if ((config["INITDB"] ?? "false") == "true")
{
    Console.WriteLine("Preparing database ...");
    SeedData.EnsurePopulated(new ProductDbContext());
    Console.WriteLine("Database preparation complete");
}
else
{
    Console.WriteLine("Starting ASP.NET...");
    var host = new WebHostBuilder()
    .UseConfiguration(config)
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseStartup<Startup>()
    .Build();

    host.Run();
}
