using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseAccessLayer.Objects
{
    public class DayTrainingDbObject : DbObject
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
        public int? SensationCode { get; set; }
        public string SensationName { get; set; }

        public List<ResultsDayDbObject> ResultsDay { get; set; }

        public DayTrainingDbObject()
        {
        }

        public DayTrainingDbObject(DataRow row) : base(row)
        {
            this.DayTrainingCode = (Guid)row["CD_DAY_TRAINING"];
            this.TrainingCode = (int)row["CD_TRAINING"];
            this.WeekNumber = (int)row["NM_WEEK"];
            this.TrainingTypeCode = (int)row["CD_TRAINING_TYPE"];
            this.TrainingTypeName = row["DS_TRAINING_TYPE"] != DBNull.Value ? (string)row["DS_TRAINING_TYPE"] : null;
            this.Done = (bool)row["IT_DONE"];
            this.SeriesGroupCode = row["CD_SERIES_GROUP"] != DBNull.Value ? (int?)row["CD_SERIES_GROUP"] : null;
            this.RunGroupCode = row["CD_RUN_GROUP"] != DBNull.Value ? (int?)row["CD_RUN_GROUP"] : null;
            this.Date = (DateTime)row["FC_DATE"];
            this.ShortRun = (bool)row["IT_SHORT_RUN"];
            this.WeekDay = row.Table.Columns.Contains("NM_WEEKDAY") && row["NM_WEEKDAY"] != DBNull.Value ? (int?)row["NM_WEEKDAY"] : null;
            this.SensationCode = row.Table.Columns.Contains("CD_SENSATION") && row["CD_SENSATION"] != DBNull.Value ? (int?)row["CD_SENSATION"] : null;
            this.SensationName = row.Table.Columns.Contains("DS_SENSATION") && row["DS_SENSATION"] != DBNull.Value ? (string)row["DS_SENSATION"] : null;

            ResultsDay = new List<ResultsDayDbObject>();
        }
    }
}
