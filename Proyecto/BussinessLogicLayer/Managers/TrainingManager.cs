using BusinessLogicLayer.Base;
using BussinessLogicLayer.Objects;
using BussinessLogicLayer.Objects.Requests;
using DatabaseAccessLayer.Managers;
using DatabaseAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Managers
{
    public class TrainingManager : Manager
    {
        private TrainingDbManager trainingDbManager;
        public TrainingManager(string dbConnectionString) : base(dbConnectionString)
        {
            this.trainingDbManager = new TrainingDbManager(dbConnectionString);
        }

        public TrainingObject postTraining(CreateTrainingRequestObject requestObject)
        {
            TrainingDbObject trainingDbObject = trainingDbManager.postTraining(requestObject.getDbObject());
            return new TrainingObject(trainingDbObject);
        }
    }
}
