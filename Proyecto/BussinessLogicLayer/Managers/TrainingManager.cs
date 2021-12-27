using BusinessLogicLayer.Base;
using BussinessLogicLayer.Objects;
using BussinessLogicLayer.Objects.Requests;
using DatabaseAccessLayer.Managers;
using DatabaseAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<TrainingObject> GetTrainingsByUserId(long userId)
        {
            List<TrainingDbObject> trainings = trainingDbManager.GetTrainingsByUserId(userId);
            return trainings.Select(t => new TrainingObject(t)).ToList();
        }

        public TrainingObject GetTrainingById(int code, long userId)
        {
            TrainingDbObject training = trainingDbManager.GetTrainingById(code, userId);
            return new TrainingObject(training);
        }
    }
}
