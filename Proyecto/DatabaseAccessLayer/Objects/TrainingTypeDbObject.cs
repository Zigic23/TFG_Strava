using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseAccessLayer.Objects
{
    public class TrainingTypeDbObject : DbObject
    {
        public int TrainingTypeCode { get; set; }
        public string TrainingTypeName { get; set; }

        public TrainingTypeDbObject()
        {
        }

        public TrainingTypeDbObject(DataRow row) : base(row)
        {
            this.TrainingTypeCode = (int)row["CD_TRAINING_TYPE"];
            this.TrainingTypeName = (string)row["DS_TRAINING_TYPE"];
        }
    }
}
