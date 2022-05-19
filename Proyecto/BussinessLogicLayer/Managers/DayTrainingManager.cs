using BussinessLogicLayer.Base;
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
    public class DayTrainingManager : Manager
    {
        private DayTrainingDbManager trainingDbManager;
        public DayTrainingManager(string dbConnectionString) : base(dbConnectionString)
        {
            this.trainingDbManager = new DayTrainingDbManager(dbConnectionString);
        }

        public List<DayTrainingObject> GetByTrainingWeek(int trainingId, int week, long userId)
        {
            List<DayTrainingDbObject> dayTrainings = trainingDbManager.GetByTrainingWeek(trainingId, week, userId);

            return dayTrainings.Select(t => new DayTrainingObject(t)).ToList();
        }

        public DayTrainingObject GetDayTraining(Guid DayTrainingCode, long userId)
        {
            DayTrainingDbObject dayTrainingDb = trainingDbManager.GetDayTraining(DayTrainingCode, userId);

            dayTrainingDb.ResultsDay = dayTrainingDb.ResultsDay.OrderBy(r => r.NumSerie).ToList();
            return new DayTrainingObject(dayTrainingDb);
        }
    }
}
