using BussinessLogicLayer.Base;
using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BussinessLogicLayer.Objects
{
    public class ResultsDayObject : MObject
    {
        public Guid ResultsDayCode { get; set; }
        public Guid DayTrainingCode { get; set; }
        public int NumSerie { get; set; }
        public int? SecondsDone { get; set; }
        public int DistObjective { get; set; }
        public string DistType { get; set; }
        public int? Desnivel { get; set; }
        public int? AverageFrecuency { get; set; }
        public int? MaxFrecuency { get; set; }
        public int? RithmDone { get; set; }
        public string RithmDoneStr { get; set; }
        public int? RithmObjective { get; set; }
        public string RithmObjectiveStr { get; set; }
        public string SerieName { get; set; }
        public int? DistDone { get; set; }
        public int? RateDone { get; set; }

        public ResultsDayObject()
        {
        }

        public ResultsDayObject(DbObject dbObject) : base(dbObject)
        {
            ResultsDayDbObject dbItem = dbObject as ResultsDayDbObject;
            ResultsDayCode = dbItem.ResultsDayCode.Value;
            DayTrainingCode = dbItem.DayTrainingCode;
            NumSerie = dbItem.NumSerie;
            SecondsDone = dbItem.SecondsDone;
            DistObjective = dbItem.DistObjective;
            DistType = dbItem.DistType;
            Desnivel = dbItem.Desnivel;
            AverageFrecuency = dbItem.AverageFrecuency;
            MaxFrecuency = dbItem.MaxFrecuency;
            RithmDone = dbItem.RithmDone;
            if(dbItem.RithmDone != null)
                RithmDoneStr = $"{dbItem.RithmDone / 60}:{(dbItem.RithmDone % 60).ToString().PadLeft(2, '0')}";
            RithmObjective = dbItem.RithmObjective; 
            if(RithmObjective != null)
                RithmObjectiveStr = $"{dbItem.RithmObjective / 60}:{(dbItem.RithmObjective % 60).ToString().PadLeft(2, '0')}";
            SerieName = dbItem.SerieName;
            DistDone = dbItem.DistDone;
            RateDone = dbItem.RateDone;
        }

        public ResultsDayDbObject GetDbObject()
        {
            return new ResultsDayDbObject()
            {
                ResultsDayCode = this.ResultsDayCode,
                DayTrainingCode = this.DayTrainingCode,
                NumSerie = this.NumSerie,
                SecondsDone = this.SecondsDone,
                DistObjective = this.DistObjective,
                DistType = this.DistType,
                Desnivel = this.Desnivel,
                AverageFrecuency = this.AverageFrecuency,
                MaxFrecuency = this.MaxFrecuency,
                RithmDone = this.RithmDone,
                RithmObjective = this.RithmObjective,
                SerieName = this.SerieName,
                DistDone = this.DistDone,
                RateDone = this.RateDone
            };
        }
    }
}
