using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BussinessLogicLayer.Managers;
using BussinessLogicLayer.Objects;
using BussinessLogicLayer.Objects.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoneMVCCore.Models.Configuration.Settings;
using StravaConnector.Objects;
using StravaTrainingGenerator.Models.Configuration.Session;
using StravaTrainingGenerator.Models.Requests;
using StravaTrainingGenerator.Models.ViewModels;

namespace StravaTrainingGenerator.Controllers
{
    [Authorize]
    public class CreateTrainingController : BaseController
    {
        private DataManager dataManager;
        private TrainingManager trainingManager;

        public CreateTrainingController(ILogger<BaseController> logger, IOptions<ConnectionStrings> connectionStrings, IOptions<KeysSettings> keysSettings, IConfiguration configuration) : base(logger, connectionStrings, keysSettings, configuration)
        {
            this.dataManager = new DataManager(connectionStrings.Value["CadenaConexion"]);
            this.trainingManager = new TrainingManager(connectionStrings.Value["CadenaConexion"]);
        }

        public IActionResult Index()
        {
            try
            {
                List<PlanTypeObject> planTypes = dataManager.GetPlanTypes();
                List<TrainingTypeObject> trainingTypes = dataManager.GetTrainingTypes();
                CreateTrainingModelView model = new CreateTrainingModelView()
                {
                    planTypes = planTypes,
                    trainingTypes = trainingTypes
                };

                return View(model);
            } catch(Exception e)
            {
                return RedirectToAction("Index", "Errores");
            }
        }

        public ActionResult PostTraining([FromForm] PostTrainingRequest postTraining)
        {
            Athlete user = HttpContext.Session.Get<Athlete>(SessionKeys.UserKey);
            CreateTrainingRequestObject requestObject = postTraining.getBLLObject(user.id, out string error);
            if (error == null)
            {
                TrainingObject trainingObject = trainingManager.postTraining(requestObject);
                return RedirectToAction("Detail", "Trainings", new { code = trainingObject.TrainingCode });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
