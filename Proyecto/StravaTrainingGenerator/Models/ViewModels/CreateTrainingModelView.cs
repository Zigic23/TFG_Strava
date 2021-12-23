using BussinessLogicLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaTrainingGenerator.Models.ViewModels
{
    public class CreateTrainingModelView
    {
        public List<PlanTypeObject> planTypes { get; set; }
        public List<TrainingTypeObject> trainingTypes { get; set; }
    }
}
