using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseAccessLayer.Objects
{
    public class TrainingDbObject : DbObject
    {
        public int TrainingCode { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int PlanTypeCode { get; set; }
        public string PlanTypeName { get; set; }
        public int? PlanTypeWeeks { get; set; }
        public int TempStart5Number { get; set; }
        public int TimeCode { get; set; }
        public long UserCode { get; set; }
        public DateTimeOffset BornDate { get; set; }

        public TrainingDbObject()
        {
        }

        public TrainingDbObject(DataRow row) : base(row)
        {
            this.TrainingCode = (int)row["CD_TRAINING"];
            this.StartDate = (DateTimeOffset)row["FC_START"];
            this.EndDate = (DateTimeOffset)row["FC_END"];
            this.PlanTypeCode = (int)row["CD_PLAN_TYPE"];
            this.PlanTypeName = row.Table.Columns.Contains("DS_PLAN_TYPE") && row["DS_PLAN_TYPE"] != DBNull.Value ? (string)row["DS_PLAN_TYPE"] : null;
            this.PlanTypeWeeks = row.Table.Columns.Contains("NM_WEEKS") && row["NM_WEEKS"] != DBNull.Value ? (int?)row["NM_WEEKS"] : null;
            this.TempStart5Number = (int)row["NM_TEMP_START_5"];
            this.TimeCode = (int)row["CD_TIME"];
            this.UserCode = (long)row["CD_USER"];
            this.BornDate = (DateTimeOffset)row["FC_BORN"];
        }
    }
}
