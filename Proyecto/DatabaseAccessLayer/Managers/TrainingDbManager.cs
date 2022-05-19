using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Exceptions;
using DatabaseAccessLayer.Objects;
using DatabaseAccessLayer.Objects.Requests;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DatabaseAccessLayer.Managers
{
    public class TrainingDbManager : DbManager<TrainingDbObject>
    {
        public TrainingDbManager(string dbConnectionString) : base(dbConnectionString)
        {
        }

        public TrainingDbObject postTraining(CreateTrainingRequestDbObject createRequest)
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_TRAINING_Create"))
                {
                    db.AddInParameter(dbCommand, "@FC_START", DbType.DateTime, createRequest.StartPlanDate);
                    db.AddInParameter(dbCommand, "@NM_TOTAL_SECS", DbType.Int32, createRequest.TotalSecs);
                    db.AddInParameter(dbCommand, "@CD_PLAN_TYPE", DbType.Int32, createRequest.PlanType);
                    db.AddInParameter(dbCommand, "@CD_TYPE_LUNES", DbType.Int32, createRequest.Lunes);
                    db.AddInParameter(dbCommand, "@CD_TYPE_MARTES", DbType.Int32, createRequest.Martes);
                    db.AddInParameter(dbCommand, "@CD_TYPE_MIERCOLES", DbType.Int32, createRequest.Miercoles);
                    db.AddInParameter(dbCommand, "@CD_TYPE_JUEVES", DbType.Int32, createRequest.Jueves);
                    db.AddInParameter(dbCommand, "@CD_TYPE_VIERNES", DbType.Int32, createRequest.Viernes);
                    db.AddInParameter(dbCommand, "@CD_TYPE_SABADO", DbType.Int32, createRequest.Sabado);
                    db.AddInParameter(dbCommand, "@CD_TYPE_DOMINGO", DbType.Int32, createRequest.Domingo);
                    db.AddInParameter(dbCommand, "@CD_USER", DbType.Int64, createRequest.UserCode);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        TrainingDbObject result = null;

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            result = new TrainingDbObject(ds.Tables[0].Rows[0]);
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

        public List<TrainingDbObject> GetTrainingsByUserId(long userId)
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_TRAINING_SelByUserId"))
                {
                    db.AddInParameter(dbCommand, "@CD_USER", DbType.Int64, userId);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        List<TrainingDbObject> result = new List<TrainingDbObject>();

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach(DataRow row in ds.Tables[0].Rows)
                            {
                                result.Add(new TrainingDbObject(row));
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
        
        public TrainingDbObject GetTrainingById(int code, long userId)
        {
            try
            {
                Database db = GetDatabase();
                using (DbCommand dbCommand = db.GetStoredProcCommand("STRA_TRAINING_SelById"))
                {
                    db.AddInParameter(dbCommand, "@CD_TRAINING", DbType.Int32, code);
                    db.AddInParameter(dbCommand, "@CD_USER", DbType.Int64, userId);

                    using (DataSet ds = db.ExecuteDataSet(dbCommand))
                    {
                        TrainingDbObject result = null;

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            result = new TrainingDbObject(ds.Tables[0].Rows[0]);
                        }
                        else
                            throw new Exception("No se ha encontrado el entrenamiento.");

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

        public override TrainingDbObject Insert(TrainingDbObject dbObject)
        {
            throw new NotImplementedException();
        }

        public override List<TrainingDbObject> SelAll()
        {
            throw new NotImplementedException();
        }

        public override List<TrainingDbObject> SelAll(dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public override TrainingDbObject SelById(object id)
        {
            throw new NotImplementedException();
        }

        public override TrainingDbObject Update(TrainingDbObject dbObject)
        {
            throw new NotImplementedException();
        }
    }
}
