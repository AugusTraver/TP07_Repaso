using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp07_Repaso.Models;

namespace Tp07_Repaso.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
