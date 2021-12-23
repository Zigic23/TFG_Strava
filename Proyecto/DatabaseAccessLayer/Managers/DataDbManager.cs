using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Exceptions;
using DatabaseAccessLayer.Objects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DatabaseAccessLayer.Managers
{
    public class DataDbManager : DbManager<DemoDbObject>
    {
        public DataDbManager(string dbConnectionString) : base(dbConnectionString)
        {
            // constructor sobre el super-constructor se puede dejar en blanco
        }

        public List<PlanTypeDbObject> SelAllPlanTypes()
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_PLAN_TYPE_SelAll"))
                {
                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        List<PlanTypeDbObject> items = new List<PlanTypeDbObject>();

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                                items.Add(new PlanTypeDbObject(row));
                        }

                        return items;
                    }
                }
            }
            catch (Exception e)
            {
                throw new DatabaseException(e);
            }
        }

        public List<TrainingTypeDbObject> SelAllTrainingTypes()
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_TRAINING_TYPE_SelAll"))
                {
                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        List<TrainingTypeDbObject> items = new List<TrainingTypeDbObject>();

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                                items.Add(new TrainingTypeDbObject(row));
                        }

                        return items;
                    }
                }
            }
            catch (Exception e)
            {
                throw new DatabaseException(e);
            }
        }

        public override bool Delete(object id)
        {
            throw new NotImplementedException();
        }
        public override DemoDbObject Insert(DemoDbObject dbObject)
        {
            throw new NotImplementedException();
        }
        public override List<DemoDbObject> SelAll()
        {
            throw new NotImplementedException();
        }
        public override List<DemoDbObject> SelAll(dynamic parameters)
        {
            throw new NotImplementedException();
        }
        public override DemoDbObject SelById(object id)
        {
            throw new NotImplementedException();
        }
        public override DemoDbObject Update(DemoDbObject dbObject)
        {
            throw new NotImplementedException();
        }
    }
}
