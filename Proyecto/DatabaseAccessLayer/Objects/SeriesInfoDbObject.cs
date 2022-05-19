using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseAccessLayer.Objects
{
    public class SeriesInfoDbObject : DbObject
    {
        public int GroupCode { get; set; }
        public int RepetitionNumber { get; set; }
        public int Distance { get; set; }
        public decimal Recovery { get; set; }
        public int Order { get; set; }
        public string RecoveryType { get; set; }

        public SeriesInfoDbObject()
        {
        }

        public SeriesInfoDbObject(DataRow row) : base(row)
        {
            this.GroupCode = (int)row["CD_GROUP"];
            this.RepetitionNumber = (int)row["NM_REP"];
            this.Distance = (int)row["NM_DIST"];
            this.Recovery = (decimal)row["NM_REC"];
            this.Order = (int)row["NM_ORDER"];
            this.RecoveryType = (string)row["DS_TYPE_REC"];
        }
    }
}
