using BussinessLogicLayer.Base;
using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Objects
{
    public class TrainingTypeObject : MObject
    {
        public int TrainingTypeCode { get; set; }
        public string TrainingTypeName { get; set; }

        public TrainingTypeObject()
        {
        }

        public TrainingTypeObject(DbObject dbObject) : base(dbObject)
        {
            TrainingTypeDbObject dbItem = dbObject as TrainingTypeDbObject;
            this.TrainingTypeCode = dbItem.TrainingTypeCode;
            this.TrainingTypeName = dbItem.TrainingTypeName;
        }
    }
}
