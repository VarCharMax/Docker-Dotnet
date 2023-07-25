using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExampleApp.Models;
using Microsoft.Extensions.Configuration;

namespace ExampleApp.Controllers;

public class HomeController : Controller
{
    // private readonly ILogger<HomeController> _logger;
    private readonly IRepository repository;
    private readonly string message;

    public HomeController(IRepository repo, IConfiguration config)
    {
        repository = repo;
        message = config["MESSAGE"] ?? "Essential Docker";
    }

    public IActionResult Index()
    {
        ViewBag.Message = message;

        return View(repository.Products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
