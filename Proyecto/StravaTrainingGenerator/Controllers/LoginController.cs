﻿using System.Collections.Generic;
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

        public ActionResult Login()
        {
            return RedirectToAction("Index", "Main");
        }

        // GET: Login
        public ActionResult Index()
        {
            return View("~/Views/Login/Login.cshtml");
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Index");
        }
    }
}