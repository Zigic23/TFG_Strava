using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseAccessLayer.Objects
{
    public class SyncDbObject : DbObject
    {
        public long UserId { get; set; }
        public DateTimeOffset lastSyncDate { get; set; }

        public SyncDbObject()
        {
        }

        public SyncDbObject(DataRow row) : base(row)
        {
            this.UserId = (long)row["CD_USER"];
            this.lastSyncDate = (DateTimeOffset)row["FC_LAST_SYNC"];
        }
    }
}
