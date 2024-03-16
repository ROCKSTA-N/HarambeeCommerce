using HarambeeCommerceWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace HarambeeCommerceWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IApiIntegrationService _apiIntegrationService;

    public HomeController(ILogger<HomeController> logger, IApiIntegrationService apiIntegrationService)
    {
        _logger = logger;
        _apiIntegrationService = apiIntegrationService;
    }

    public IActionResult Index()
    {
        return View();
    }

}