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
using StravaTrainingGenerator.Models.Configuration.Settings;
using StravaTrainingGenerator.Models.ViewModels;

namespace StravaTrainingGenerator.Controllers
{
    [Authorize]
    public class TrainingsController : BaseController
    {
        private TrainingManager trainingManager;
        private DayTrainingManager dayTrainingManager;
        private SyncManager syncManager;
        public TrainingsController(ILogger<BaseController> logger, IOptions<ConnectionStrings> connectionStrings, IOptions<KeysSettings> keysSettings, IOptions<StravaSettings> stravaSettings, IConfiguration configuration) : base(logger, connectionStrings, keysSettings, configuration)
        {
            this.trainingManager = new TrainingManager(connectionStrings.Value["CadenaConexion"]);
            this.dayTrainingManager = new DayTrainingManager(connectionStrings.Value["CadenaConexion"]);
            this.syncManager = new SyncManager(connectionStrings.Value["CadenaConexion"], stravaSettings.Value.strava_url);
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

        public ActionResult GetGridDayTrainingWeek(int trainingId, int week)
        {
            Athlete user = HttpContext.Session.Get<Athlete>(SessionKeys.UserKey);
            try
            {
                List<DayTrainingObject> trainings = dayTrainingManager.GetByTrainingWeek(trainingId, week, user.id);
                return Json(trainings.ToArray());
            }
            catch
            {
                return Json(new { errorMsg = "Ha ocurrido un error al recoger los entrenamientos" });
            }
        }

        public ActionResult SeeResults(Guid DayTrainingCode)
        {
            Athlete user = HttpContext.Session.Get<Athlete>(SessionKeys.UserKey);
            try
            {
                DayTrainingObject dayTraining = dayTrainingManager.GetDayTraining(DayTrainingCode, user.id);
                return View(dayTraining);
            }
            catch
            {
                return RedirectToAction("Index", "Errores");
            }
        }

        public ActionResult UpdateStravaValues()
        {
            Athlete user = HttpContext.Session.Get<Athlete>(SessionKeys.UserKey);
            try
            {
                bool result = syncManager.SyncUser(user.id, Request.Cookies["t"]);
                return Json(new { result });
            }
            catch
            {
                return Json(new { result = false, message = "Ha ocurrido un error al procesar la sincronización" });
            }
        }

        [HttpPut]
        public ActionResult SetCompleted(Guid DayTrainingCode)
        {
            Athlete user = HttpContext.Session.Get<Athlete>(SessionKeys.UserKey);
            try
            {
                DayTrainingObject result = dayTrainingManager.SetDayCompleted(DayTrainingCode, user.id);
                return Json(new { result = result != null });
            }
            catch
            {
                return Json(new { result = false, message = "Ha ocurrido un error al marcar como completado el día" });
            }
        }
    }
}
