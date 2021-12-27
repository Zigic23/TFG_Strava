using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BussinessLogicLayer.Managers;
using BussinessLogicLayer.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoneMVCCore.Models.Configuration.Settings;
using StravaConnector.Objects;
using StravaTrainingGenerator.Models.Configuration.Session;
using StravaTrainingGenerator.Models.ViewModels;

namespace StravaTrainingGenerator.Controllers
{
    [Authorize]
    public class TrainingsController : BaseController
    {
        private TrainingManager trainingManager;
        public TrainingsController(ILogger<BaseController> logger, IOptions<ConnectionStrings> connectionStrings, IOptions<KeysSettings> keysSettings, IConfiguration configuration) : base(logger, connectionStrings, keysSettings, configuration)
        {
            this.trainingManager = new TrainingManager(connectionStrings.Value["CadenaConexion"]);
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetGridTrainings()
        {
            Athlete user = HttpContext.Session.Get<Athlete>(SessionKeys.UserKey);
            try
            {
                List<TrainingObject> trainings = trainingManager.GetTrainingsByUserId(user.id);
                return Json(trainings.ToArray());
            }
            catch
            {
                return Json(new { errorMsg = "Ha ocurrido un error al recoger los entrenamientos" });
            }
        }

        [Route("/Trainings/{code}")]
        public ActionResult Detail(int code)
        {
            Athlete user = HttpContext.Session.Get<Athlete>(SessionKeys.UserKey);
            try
            {
                TrainingObject training = trainingManager.GetTrainingById(code, user.id);
                DetailTrainingModelView model = new DetailTrainingModelView()
                {
                    training = training
                };
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index", "Errores");
            }
        }

        public ActionResult SeeResults(int code, int trainingCode)
        {
            return View(code);
        }
    }
}
