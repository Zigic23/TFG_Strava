using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseAccessLayer.Objects
{
    public class PlanTypeDbObject : DbObject
    {
        public int PlanTypeCode { get; set; }
        public string PlanTypeName { get; set; }

        public PlanTypeDbObject()
        {
        }

        public PlanTypeDbObject(DataRow row) : base(row)
        {
            this.PlanTypeCode = (int)row["CD_PLAN_TYPE"];
            this.PlanTypeName = (string)row["DS_PLAN_TYPE"];
        }
    }
}
