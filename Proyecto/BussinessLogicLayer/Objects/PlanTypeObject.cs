using BusinessLogicLayer.Base;
using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Objects
{
    public class PlanTypeObject : MObject
    {
        public int PlanTypeCode { get; set; }
        public string PlanTypeName { get; set; }

        public PlanTypeObject()
        {
        }

        public PlanTypeObject(DbObject dbObject) : base(dbObject)
        {
            PlanTypeDbObject dbItem = dbObject as PlanTypeDbObject;
            this.PlanTypeCode = dbItem.PlanTypeCode;
            this.PlanTypeName = dbItem.PlanTypeName;
        }
    }
}
