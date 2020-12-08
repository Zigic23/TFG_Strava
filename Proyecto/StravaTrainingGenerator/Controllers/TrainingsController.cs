using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoneMVCCore.Models.Configuration.Settings;

namespace StravaTrainingGenerator.Controllers
{
    public class TrainingsController : BaseController
    {
        public TrainingsController(ILogger<BaseController> logger, IOptions<ConnectionStrings> connectionStrings, IOptions<KeysSettings> keysSettings, IConfiguration configuration) : base(logger, connectionStrings, keysSettings, configuration)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Trainings/{code}")]
        public ActionResult Detail(int code)
        {
            return View(code);
        }

        public ActionResult SeeResults(int code, int trainingCode)
        {
            return View(code);
        }
    }
}
