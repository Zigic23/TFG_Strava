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
    public class DayTrainingDbManager : DbManager<DayTrainingDbObject>
    {
        public DayTrainingDbManager(string dbConnectionString) : base(dbConnectionString)
        {
        }

        public List<DayTrainingDbObject> GetByTrainingWeek(int trainingId, int week, long userId)
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_DAY_TRAINING_SelByTrainingAndWeek"))
                {
                    db.AddInParameter(dbCommand, "@CD_TRAINING", DbType.Int32, trainingId);
                    db.AddInParameter(dbCommand, "@NM_WEEK", DbType.Int32, week);
                    db.AddInParameter(dbCommand, "@CD_USER", DbType.Int64, userId);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        List<DayTrainingDbObject> result = new List<DayTrainingDbObject>();

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                result.Add(new DayTrainingDbObject(row));
                            }
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

        public List<DayTrainingDbObject> GetAllSeriesAndRuns(int trainingId)
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_DAY_TRAINING_SelSeriesAndRuns"))
                {
                    db.AddInParameter(dbCommand, "@CD_TRAINING", DbType.Int32, trainingId);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        List<DayTrainingDbObject> result = new List<DayTrainingDbObject>();

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                result.Add(new DayTrainingDbObject(row));
                            }
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

        public DayTrainingDbObject GetDayTraining(Guid DayTrainingCode, long userId)
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_DAY_TRAINING_SelWithResults"))
                {
                    db.AddInParameter(dbCommand, "@CD_DAY_TRAINING", DbType.Guid, DayTrainingCode);
                    db.AddInParameter(dbCommand, "@CD_USER", DbType.Int64, userId);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        DayTrainingDbObject result = new DayTrainingDbObject();

                        if (ds != null && ds.Tables.Count > 1 && ds.Tables[0].Rows.Count > 0)
                        {
                            result = new DayTrainingDbObject(ds.Tables[0].Rows[0]);

                            foreach (DataRow row in ds.Tables[1].Rows)
                            {
                                result.ResultsDay.Add(new ResultsDayDbObject(row));
                            }
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

        public DayTrainingDbObject SetDayCompleted(Guid DayTrainingCode, long userId)
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_DAY_TRAINING_SetDayCompleted"))
                {
                    db.AddInParameter(dbCommand, "@CD_DAY_TRAINING", DbType.Guid, DayTrainingCode);
                    db.AddInParameter(dbCommand, "@CD_USER", DbType.Int64, userId);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        DayTrainingDbObject result = new DayTrainingDbObject();

                        if (ds != null && ds.Tables.Count > 1 && ds.Tables[0].Rows.Count > 0)
                        {
                            result = new DayTrainingDbObject(ds.Tables[0].Rows[0]);
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

        public override DayTrainingDbObject Insert(DayTrainingDbObject dbObject)
        {
            throw new NotImplementedException();
        }

        public override List<DayTrainingDbObject> SelAll()
        {
            throw new NotImplementedException();
        }

        public override List<DayTrainingDbObject> SelAll(dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public override DayTrainingDbObject SelById(object id)
        {
            throw new NotImplementedException();
        }

        public override DayTrainingDbObject Update(DayTrainingDbObject dbObject)
        {
            throw new NotImplementedException();
        }
    }
}
