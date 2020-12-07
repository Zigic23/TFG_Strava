using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Exceptions;
using DatabaseAccessLayer.Objects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.Managers
{
    public class DemoDbManager : DbManager<DemoDbObject>
    {
        public DemoDbManager(string dbConnectionString) : base(dbConnectionString)
        {
            // constructor sobre el super-constructor se puede dejar en blanco
        }

        public override List<DemoDbObject> SelAll()
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("ABDC_pr_ENTITY_SelAll"))
                {
                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        List<DemoDbObject> items = new List<DemoDbObject>();

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                                items.Add(new DemoDbObject(row));
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

        public override List<DemoDbObject> SelAll(dynamic parameters)
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("ABDC_pr_ENTITY_SelAllByParameter"))
                {
                    db.AddInParameter(dbCommand, "@CD_FOREIGN_KEY", DbType.Int32, parameters.id);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        List<DemoDbObject> items = new List<DemoDbObject>();

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                                items.Add(new DemoDbObject(row));
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

        public override DemoDbObject SelById(object id)
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("ABDC_pr_ENTITY_SelById"))
                {
                    db.AddInParameter(dbCommand, "@CD_IDENTIFIER", DbType.Int32, id);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            return new DemoDbObject(ds.Tables[0].Rows[0]);
                        else
                            return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new DatabaseException(e);
            }
        }

        public override DemoDbObject Insert(DemoDbObject dbObject)
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("ABDC_pr_ENTITY_Insert"))
                {
                    db.AddInParameter(dbCommand, "@DS_PARAMETER_1", DbType.String, dbObject.dsParameter1);
                    db.AddInParameter(dbCommand, "@DS_PARAMETER_2", DbType.String, dbObject.dsParameter2);
                    db.AddInParameter(dbCommand, "@DS_PARAMETER_3", DbType.String, dbObject.dsParameter3);
                    db.AddInParameter(dbCommand, "@DS_PARAMETER_4", DbType.String, dbObject.dsParameter4);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            return new DemoDbObject(ds.Tables[0].Rows[0]);
                        else
                            return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new DatabaseException(e);
            }
        }

        public override DemoDbObject Update(DemoDbObject dbObject)
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("ABDC_pr_ENTITY_Update"))
                {
                    db.AddInParameter(dbCommand, "@CD_IDENTIFIER", DbType.Int32, dbObject.cdIdentifier);

                    db.AddInParameter(dbCommand, "@DS_PARAMETER_1", DbType.String, dbObject.dsParameter1);
                    db.AddInParameter(dbCommand, "@DS_PARAMETER_2", DbType.String, dbObject.dsParameter2);
                    db.AddInParameter(dbCommand, "@DS_PARAMETER_3", DbType.String, dbObject.dsParameter3);
                    db.AddInParameter(dbCommand, "@DS_PARAMETER_4", DbType.String, dbObject.dsParameter4);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            return new DemoDbObject(ds.Tables[0].Rows[0]);
                        else
                            return null;
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
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("ABDC_pr_ENTITY_Delete"))
                {
                    db.AddInParameter(dbCommand, "@CD_IDENTIFIER", DbType.Int32, id);

                    return db.ExecuteNonQuery(dbCommand) > 0;
                }
            }
            catch (Exception e)
            {
                throw new DatabaseException(e);
            }
        }
    }
}
