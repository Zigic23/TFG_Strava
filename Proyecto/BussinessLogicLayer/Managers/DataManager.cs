using BussinessLogicLayer.Base;
using BussinessLogicLayer.Objects;
using DatabaseAccessLayer.Managers;
using DatabaseAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLogicLayer.Managers
{
    public class DataManager : Manager
    {
        private DataDbManager dataManager;
        public DataManager(string dbConnectionString) : base(dbConnectionString)
        {
            dataManager = new DataDbManager(dbConnectionString);
        }

        public List<PlanTypeObject> GetPlanTypes()
        {
            List<PlanTypeDbObject> planTypes = dataManager.SelAllPlanTypes();
            return planTypes.Select(p => new PlanTypeObject(p)).ToList();
        }

        public List<TrainingTypeObject> GetTrainingTypes()
        {
            List<TrainingTypeDbObject> trainingTypes = dataManager.SelAllTrainingTypes();
            return trainingTypes.Select(t => new TrainingTypeObject(t)).ToList();
        }
    }
}
