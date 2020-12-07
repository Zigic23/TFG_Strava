using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoneMVCCore.Models.Configuration.Settings;

namespace StoneMVCCore.Controllers
{
    public class FormulariosController : BaseController
    {
        public FormulariosController(ILogger<BaseController> logger, IOptions<ConnectionStrings> connectionStrings, IOptions<KeysSettings> keysSettings, IConfiguration configuration) : base(logger, connectionStrings, keysSettings, configuration)
        {
        }

        // GET: Errores
        public ActionResult Index()
        {
            ViewBag.FormsActive = "active";

            return View("~/Views/Formularios/Index.cshtml");
        }
    }
}