using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoneMVCCore.Models.Configuration.Settings;

namespace StoneMVCCore.Controllers
{
    public class ContactoController : BaseController
    {
        public ContactoController(ILogger<BaseController> logger, IOptions<ConnectionStrings> connectionStrings, IOptions<KeysSettings> keysSettings, IOptions<MailSettings> mailSettings, IConfiguration configuration) : base(logger, connectionStrings, keysSettings, configuration)
        {
        }

        // GET: Contacto
        public ActionResult Contacto()
        {
            ViewBag.ContactoActive = "active";
            return View();
        }
    }
}