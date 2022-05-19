using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseAccessLayer.Objects
{
    public class RunInfoDbObject : DbObject
    {
        public int GroupCode { get; set; }
        public int Distance { get; set; }
        public bool IsShortRun { get; set; }
        public int Order { get; set; }

        public RunInfoDbObject()
        {
        }
        public RunInfoDbObject(DataRow row) : base(row)
        {
            this.GroupCode = (int)row["CD_GROUP"];
            this.Distance = (int)row["NM_DIST"];
            this.IsShortRun = (bool)row["IT_SHORT"];
            this.Order = (int)row["NM_ORDER"];
        }
    }
}
