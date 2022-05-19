using BussinessLogicLayer.Base;
using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLogicLayer.Objects
{
    public class DayTrainingObject : MObject
    {
        public Guid DayTrainingCode { get; set; }
        public int TrainingCode { get; set; }
        public int WeekNumber { get; set; }
        public int TrainingTypeCode { get; set; }
        public string TrainingTypeName { get; set; }
        public bool Done { get; set; }
        public int? SeriesGroupCode { get; set; }
        public int? RunGroupCode { get; set; }
        public DateTime Date { get; set; }
        public bool ShortRun { get; set; }
        public int? WeekDay { get; set; }

        public List<ResultsDayObject> ResultsDay { get; set; }

        public DayTrainingObject()
        {
        }

        public DayTrainingObject(DbObject dbObject) : base(dbObject)
        {
            DayTrainingDbObject daytrainingDbObject = dbObject as DayTrainingDbObject;
            this.DayTrainingCode = daytrainingDbObject.DayTrainingCode;
            this.TrainingCode = daytrainingDbObject.TrainingCode;
            this.WeekNumber = daytrainingDbObject.WeekNumber;
            this.TrainingTypeCode = daytrainingDbObject.TrainingTypeCode;
            this.TrainingTypeName = daytrainingDbObject.TrainingTypeName;
            this.Done = daytrainingDbObject.Done;
            this.SeriesGroupCode = daytrainingDbObject.SeriesGroupCode;
            this.RunGroupCode = daytrainingDbObject.RunGroupCode;
            this.Date = daytrainingDbObject.Date;
            this.ShortRun = daytrainingDbObject.ShortRun;
            this.WeekDay = daytrainingDbObject.WeekDay;
            this.ResultsDay = daytrainingDbObject.ResultsDay?.Select(r => new ResultsDayObject(r)).ToList();
        }
    }
}
