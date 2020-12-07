using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoneMVCCore.Models.Configuration.Settings;

namespace StoneMVCCore.Controllers
{
    public class ErroresController : BaseController
    {
        public ErroresController(ILogger<BaseController> logger, IOptions<ConnectionStrings> connectionStrings, IOptions<KeysSettings> keysSettings, IConfiguration configuration) : base(logger, connectionStrings, keysSettings, configuration)
        {
        }

        // GET: Errores
        public ActionResult Index()
        {
            ViewBag.ErrorsActive = "active";

            return View("~/Views/Errores/Errores.cshtml");
        }

        public ActionResult ErrorGenerico()
        {
            ViewBag.ErrorsActive = "active";

            return View("~/Views/Errores/ErrorGenerico.cshtml");
        }

        public ActionResult Error404()
        {
            ViewBag.ErrorsActive = "active";

            return View("~/Views/Errores/Error404.cshtml");
        }
    }
}