using ExampleApp;
using ExampleApp.Models;

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
    Console.WriteLine("Starting ASP.NET...");
    var host = new WebHostBuilder()
    .UseConfiguration(config)
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseStartup<Startup>()
    .Build();
    
    host.Run();
}
