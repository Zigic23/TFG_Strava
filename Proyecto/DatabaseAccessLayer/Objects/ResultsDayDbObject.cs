using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseAccessLayer.Objects
{
    public class ResultsDayDbObject : DbObject
    {
        public Guid? ResultsDayCode { get; set; }
        public Guid DayTrainingCode { get; set; }
        public int NumSerie { get; set; }
        public int? SecondsDone { get; set; }
        public int DistObjective { get; set; }
        public string DistType { get; set; }
        public int? Desnivel { get; set; }
        public int? AverageFrecuency { get; set; }
        public int? RithmDone { get; set; }
        public int? RithmObjective { get; set; }
        public string SerieName { get; set; }
        public int? DistDone { get; set; }

        public ResultsDayDbObject()
        {
        }
         
        public ResultsDayDbObject(DataRow row) : base(row)
        {
            this.ResultsDayCode = (Guid)row["CD_RESULTS_DAY"];
            this.DayTrainingCode = (Guid)row["CD_DAY_TRAINING"];
            this.NumSerie = (int)row["NM_SERIE"];
            this.SecondsDone = row["NM_SEC_TIME_DONE"] != DBNull.Value ? (int?)row["NM_SEC_TIME_DONE"] : null;
            this.DistObjective = (int)row["NM_DIST_OBJ"];
            this.DistType = (string)row["DS_DIST_TYPE"];
            this.Desnivel = row["NM_DESNIVEL"] != DBNull.Value ? (int?)row["NM_DESNIVEL"] : null;
            this.AverageFrecuency = row["NM_FCMX"] != DBNull.Value ? (int?)row["NM_FCMX"] : null;
            this.RithmDone = row["NM_RITHM_DONE"] != DBNull.Value ? (int?)row["NM_RITHM_DONE"] : null;
            this.RithmObjective = row["NM_RITHM_OBJ"] != DBNull.Value ? (int?)row["NM_RITHM_OBJ"] : null;
            this.SerieName = (string)row["DS_SERIE_NAME"];
            this.DistDone = row["NM_DIST_DONE"] != DBNull.Value ? (int?)row["NM_DIST_DONE"] : null;
        }
    }
}
