using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Exceptions;
using DatabaseAccessLayer.Objects;
using DatabaseAccessLayer.Objects.Requests;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DatabaseAccessLayer.Managers
{
    public class ResultsDayDbManager : DbManager<ResultsDayDbObject>
    {
        public ResultsDayDbManager(string dbConnectionString) : base(dbConnectionString)
        {
        }

        public List<ResultsDayDbObject> postResults(List<ResultsDayDbObject> resultsDay)
        {
            try
            {
                SqlDatabase db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_RESULTS_DAY_InsBulk"))
                {
                    DataTable resultsTable = CreateResultsTable(resultsDay);

                    db.AddInParameter(dbCommand, "@RESULTS", SqlDbType.Structured, resultsTable);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        List<ResultsDayDbObject> result = new List<ResultsDayDbObject>();

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                result.Add(new ResultsDayDbObject(row));
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

        public List<ResultsDayDbObject> postStravaValues(long userCode, DateTimeOffset lastSyncDate, List<LapResultDbObject> resultsDay)
        {
            try
            {
                SqlDatabase db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_pr_RESULTS_DAY_InsLaps"))
                {
                    DataTable resultsTable = CreateLapResultsTable(resultsDay);

                    db.AddInParameter(dbCommand, "@RESULTS", SqlDbType.Structured, resultsTable);
                    db.AddInParameter(dbCommand, "@CD_USER", SqlDbType.BigInt, userCode);
                    db.AddInParameter(dbCommand, "@FC_LAST_SYNC", SqlDbType.DateTimeOffset, lastSyncDate);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        List<ResultsDayDbObject> result = new List<ResultsDayDbObject>();

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                result.Add(new ResultsDayDbObject(row));
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

        private DataTable CreateResultsTable(List<ResultsDayDbObject> resultsDay)
        {
            DataTable table = new DataTable();

            table.Columns.Add("CD_DAY_TRAINING", typeof(Guid));
            table.Columns.Add("NM_SERIE", typeof(int));
            table.Columns.Add("NM_DIST_OBJ", typeof(int));
            table.Columns.Add("DS_SERIE_NAME", typeof(string));
            table.Columns.Add("DS_DIST_TYPE", typeof(string));
            table.Columns.Add("NM_RITHM_OBJ", typeof(int));

            foreach(ResultsDayDbObject resultDay in resultsDay)
            {
                table.Rows.Add
                (
                    resultDay.DayTrainingCode,
                    resultDay.NumSerie,
                    resultDay.DistObjective,
                    resultDay.SerieName,
                    resultDay.DistType,
                    resultDay.RithmObjective
                );
            }

            return table;
        }

        private DataTable CreateLapResultsTable(List<LapResultDbObject> resultsDay)
        {
            DataTable table = new DataTable();

            table.Columns.Add("FC_DATE", typeof(DateTime));
            table.Columns.Add("NM_SERIE", typeof(int));
            table.Columns.Add("NM_SEC_TIME_DONE", typeof(int));
            table.Columns.Add("NM_DESNIVEL", typeof(int));
            table.Columns.Add("NM_FCMX", typeof(string));
            table.Columns.Add("NM_FCMAX", typeof(int));
            table.Columns.Add("NM_RITHM_DONE", typeof(int));
            table.Columns.Add("NM_DIST_DONE", typeof(int));
            table.Columns.Add("NM_RATE", typeof(int));

            foreach(LapResultDbObject resultDay in resultsDay)
            {
                table.Rows.Add
                (
                    resultDay.Date,
                    resultDay.NumSerie,
                    resultDay.TimeDone,
                    resultDay.Desnivel,
                    resultDay.AverageFrecuency,
                    resultDay.MaxFrecuency,
                    resultDay.RithmDone,
                    resultDay.DistanceDone,
                    resultDay.RateDone
                );
            }

            return table;
        }

        public override bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public override ResultsDayDbObject Insert(ResultsDayDbObject dbObject)
        {
            throw new NotImplementedException();
        }

        public override List<ResultsDayDbObject> SelAll()
        {
            throw new NotImplementedException();
        }

        public override List<ResultsDayDbObject> SelAll(dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public override ResultsDayDbObject SelById(object id)
        {
            throw new NotImplementedException();
        }

        public override ResultsDayDbObject Update(ResultsDayDbObject dbObject)
        {
            throw new NotImplementedException();
        }
    }
}
