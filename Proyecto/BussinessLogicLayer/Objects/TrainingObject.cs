using BussinessLogicLayer.Base;
using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Objects
{
    public class TrainingObject : MObject
    {
        public int TrainingCode { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int PlanTypeCode { get; set; }
        public string PlanTypeName { get; set; }
        public int? PlanTypeWeeks { get; set; }
        public int TempStart5Number { get; set; }
        public string Start5kmMark { get; set; }
        public int TimeCode { get; set; }
        public long UserCode { get; set; }

        public TrainingObject()
        {
        }

        public TrainingObject(DbObject dbObject) : base(dbObject)
        {
            TrainingDbObject trainingDbObject = dbObject as TrainingDbObject;
            this.TrainingCode = trainingDbObject.TrainingCode;
            this.StartDate = trainingDbObject.StartDate;
            this.EndDate = trainingDbObject.EndDate;
            this.PlanTypeCode = trainingDbObject.PlanTypeCode;
            this.PlanTypeName = trainingDbObject.PlanTypeName;
            this.PlanTypeWeeks = trainingDbObject.PlanTypeWeeks;
            this.TempStart5Number = trainingDbObject.TempStart5Number;
            this.Start5kmMark = $"{trainingDbObject.TempStart5Number / 60} min. {trainingDbObject.TempStart5Number % 60} seg.";
            this.TimeCode = trainingDbObject.TimeCode;
            this.UserCode = trainingDbObject.UserCode;
        }
    }
}
