using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoneMVCCore.Models.Configuration.Settings;

namespace StoneMVCCore.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(ILogger<BaseController> logger, IOptions<ConnectionStrings> connectionStrings, IOptions<KeysSettings> keysSettings, IConfiguration configuration) : base(logger, connectionStrings, keysSettings, configuration)
        {
        }

        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Authorized();
            else 
                return View("Login");
        }

        public ActionResult DoLogin(string returnUrl = "/")
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        public ActionResult Authorized()
        {
            return RedirectToAction("Index", "Trainings");
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Index");
        }
    }
}