using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoneMVCCore.Models.Configuration.Settings;

namespace StoneMVCCore.Controllers
{
    public class ControlesController : BaseController
    {
        public ControlesController(ILogger<BaseController> logger, IOptions<ConnectionStrings> connectionStrings, IOptions<KeysSettings> keysSettings, IConfiguration configuration) : base(logger, connectionStrings, keysSettings, configuration)
        {
        }

        // GET: Controles
        public ActionResult Index()
        {
            //_logger.LogTrace(StoneEventIdExtensions.Create(StoneEventId.Controller), "{TraceIdentifier} - Index", TraceIdentifier);
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/Controles.cshtml");
        }
        public ActionResult Botones()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/Botones.cshtml");
        }
        public ActionResult MigasDePan()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/MigasDePan.cshtml");
        }
        public ActionResult Checkbox()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/Checkbox.cshtml");
        }
        public ActionResult Tablas()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/Tablas.cshtml");
        }
        public ActionResult Enlaces()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/Enlaces.cshtml");
        }
        public ActionResult Popups()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/Popups.cshtml");
        }
        public ActionResult CajasTexto()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/CajasTexto.cshtml");
        }
        public ActionResult Radiobutton()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/Radiobutton.cshtml");
        }
        public ActionResult CajasSeleccion()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/CajasSeleccion.cshtml");
        }
        public ActionResult Pasos()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/Pasos.cshtml");
        }
        public ActionResult Etiquetas()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/Etiquetas.cshtml");
        }
        public ActionResult SeleccionFicheros()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/SeleccionFicheros.cshtml");

        }
        public ActionResult Switchs()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/Switchs.cshtml");
        }
        public ActionResult Tabs()
        {
            ViewBag.ControlesActive = "active";
            return View("~/Views/Controles/Tabs.cshtml");
        }
    }
}