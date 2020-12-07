using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StoneMVCCore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using StoneMVCCore.Models.Configuration.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace StoneMVCCore.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILogger<BaseController> logger, IOptions<ConnectionStrings> connectionStrings, IOptions<KeysSettings> keysSettings, IConfiguration configuration) : base(logger, connectionStrings, keysSettings, configuration)
        {
        }

        public IActionResult Index()
        {
            return View();
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
}
