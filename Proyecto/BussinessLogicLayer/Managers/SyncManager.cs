using BussinessLogicLayer.Base;
using DatabaseAccessLayer.Managers;
using DatabaseAccessLayer.Objects;
using StravaConnector.Objects;
using StravaConnector.RestManagers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Managers
{
    public class SyncManager : Manager
    {
        SyncDbManager syncDbManager;
        ActivitiesManager activitiesManager;
        ResultsDayDbManager resultsDbManager;
        public SyncManager(string dbConnectionString, string stravaUrl) : base(dbConnectionString)
        { 
            syncDbManager = new SyncDbManager(dbConnectionString);
            resultsDbManager = new ResultsDayDbManager(dbConnectionString);
            activitiesManager = new ActivitiesManager(stravaUrl);
        }

        public bool SyncUser(long userCode, string token)
        {
            SyncDbObject syncDbObject = syncDbManager.GetUserSync(userCode);

            //Preparamos las fechas para la busqueda de actividades
            long before = DateTimeOffset.Now.ToUnixTimeSeconds();
            long? after = null;
            if (syncDbObject != null)
            {
                //Que al menos haya medio día antes de la anterior sincronización para no explotar el servidor, ya que tiene que hacer muchas peticiones seguidas
                if (syncDbObject.lastSyncDate > DateTimeOffset.Now.AddHours(-12))
                    return false;

                after = syncDbObject.lastSyncDate.ToUnixTimeSeconds();
            }

            //Vamos a leer solamente 20 actividades cada vez que se sincronice
            List<StravaActivity> activities = activitiesManager.GetUserActivities(token, before, after, 1, 20);

            if(activities.Count > 0)
            {
                List<LapResultDbObject> lapsToSave = new List<LapResultDbObject>();
                DateTimeOffset? lastDate = null;
                foreach (StravaActivity activity in activities)
                {
                    //Para cada actividad sacaremos las laps
                    List<ActivityLaps> laps = activitiesManager.GetActivityLaps(token, activity.id);

                    //Ahora vamos a guardar las laps en la lista que irá a base de datos para actualizar valores
                    foreach (ActivityLaps lap in laps)
                    {
                        lapsToSave.Add(new LapResultDbObject()
                        {
                            NumSerie = lap.split,
                            Date = lap.start_date,
                            TimeDone = lap.moving_time,
                            Desnivel = (int)lap.total_elevation_gain,
                            AverageFrecuency = (int)lap.average_heartrate,
                            RithmDone = (int)(lap.moving_time * 1000 / lap.distance),
                            DistanceDone = (int)lap.distance
                        });
                    }

                    lastDate = activity.start_date;
                }

                resultsDbManager.postStravaValues(userCode, lastDate.HasValue ? lastDate.Value : DateTimeOffset.Now, lapsToSave);
            } 
            else
            {
                syncDbManager.UpdateUserSync(userCode);
            }

            return true;
        }
    }
}
