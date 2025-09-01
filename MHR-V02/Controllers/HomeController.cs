using MHR_V02.Filters;
using MHR_V02.Models;
using MHR_V02.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MHR_V02.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [LoggableAction]
        [HttpGet]
        public IActionResult Index()
        {
            SetCommonLocalizedValues();
            return View();
        }
        
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public IActionResult Privacy()
        {
            SetCommonLocalizedValues();
            return View(); 
        }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}