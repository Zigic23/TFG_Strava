using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Exceptions;
using DatabaseAccessLayer.Objects;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DatabaseAccessLayer.Managers
{
    public class SyncDbManager : DbManager<SyncDbObject>
    {
        public SyncDbManager(string dbConnectionString) : base(dbConnectionString)
        {
        }

        public SyncDbObject GetUserSync(long userId)
        {
            try
            {
                SqlDatabase db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_pr_SYNC_Sel"))
                {
                    db.AddInParameter(dbCommand, "@CD_USER", DbType.Int64, userId);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        SyncDbObject result = null;

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            result = new SyncDbObject(ds.Tables[0].Rows[0]);
                        }

                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                throw new DatabaseException(e);
            }
        }

        public SyncDbObject UpdateUserSync(long userId)
        {
            try
            {
                SqlDatabase db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_pr_SYNC_Upd"))
                {
                    db.AddInParameter(dbCommand, "@CD_USER", DbType.Int64, userId);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        SyncDbObject result = null;

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            result = new SyncDbObject(ds.Tables[0].Rows[0]);
                        }

                        return result;
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

        public override SyncDbObject Insert(SyncDbObject dbObject)
        {
            throw new NotImplementedException();
        }

        public override List<SyncDbObject> SelAll()
        {
            throw new NotImplementedException();
        }

        public override List<SyncDbObject> SelAll(dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public override SyncDbObject SelById(object id)
        {
            throw new NotImplementedException();
        }

        public override SyncDbObject Update(SyncDbObject dbObject)
        {
            throw new NotImplementedException();
        }
    }
}
