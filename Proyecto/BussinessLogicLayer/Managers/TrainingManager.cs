using BussinessLogicLayer.Base;
using BussinessLogicLayer.Objects;
using BussinessLogicLayer.Objects.Requests;
using DatabaseAccessLayer.Managers;
using DatabaseAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLogicLayer.Managers
{
    public class TrainingManager : Manager
    {
        private TrainingDbManager trainingDbManager;
        private DayTrainingDbManager dayTrainingDbManager;
        private DataDbManager dataDbManager;
        private ResultsDayDbManager resultsDayDbManager;
        public TrainingManager(string dbConnectionString) : base(dbConnectionString)
        {
            this.trainingDbManager = new TrainingDbManager(dbConnectionString);
            this.dayTrainingDbManager = new DayTrainingDbManager(dbConnectionString);
            this.dataDbManager = new DataDbManager(dbConnectionString);
            this.resultsDayDbManager = new ResultsDayDbManager(dbConnectionString);
        }

        public TrainingObject postTraining(CreateTrainingRequestObject requestObject)
        {
            TrainingDbObject trainingDbObject = trainingDbManager.postTraining(requestObject.getDbObject());
            //Ahora vamos a rellenar las series desde aqui, recogeremos los catalogos de series y carreras y
            //los días de entrenamiento que sean series, carrera larga o carrera corta
            SeriesTimesAndRunInfoDbObject seriesTimesAndRuns = dataDbManager.SelTimesSeriesAndRunInfo(trainingDbObject.TrainingCode);
            List<DayTrainingDbObject> daysTraining = dayTrainingDbManager.GetAllSeriesAndRuns(trainingDbObject.TrainingCode);

            daysTraining = daysTraining.OrderBy(d => d.WeekDay).ToList();

            List<ResultsDayObject> results = new List<ResultsDayObject>();

            foreach(DayTrainingDbObject dayTraining in daysTraining)
            {
                //Series
                if (dayTraining.TrainingTypeCode == 1)
                {
                    List<SeriesInfoDbObject> series = seriesTimesAndRuns.Series.FindAll(s => s.GroupCode == dayTraining.SeriesGroupCode);

                    series = series.OrderBy(s => s.Order).ToList();
                    int currentSerie = 1;
                    //Primero vamos a añadir un elemento que será el calentamiento
                    results.Add(new ResultsDayObject()
                    {
                        DayTrainingCode = dayTraining.DayTrainingCode,
                        NumSerie = currentSerie,
                        DistObjective = 10,
                        DistType = "min",
                        RithmObjective = null,
                        SerieName = "Calentamiento"
                    });

                    //Para cada serie tendremos que añadir un objeto resultDayObject por cada elemento
                    foreach (SeriesInfoDbObject serie in series)
                    {
                        int repetitionNumber = currentSerie;
                        for (int i = 0; i < serie.RepetitionNumber; i++)
                        {
                            //Añadimos la serie
                            results.Add(new ResultsDayObject()
                            {
                                DayTrainingCode = dayTraining.DayTrainingCode, 
                                NumSerie = ((2 * i) + 1) + repetitionNumber, 
                                DistObjective = serie.Distance,
                                DistType = "m",
                                SerieName = "Serie " + (i + 1),
                                RithmObjective = seriesTimesAndRuns.Times.GetRithmObjective(serie.Distance, "serie")
                            });
                            results.Add(new ResultsDayObject()
                            {
                                DayTrainingCode = dayTraining.DayTrainingCode,
                                NumSerie = (2 * (i + 1)) + repetitionNumber,
                                DistObjective = (int)serie.Recovery,
                                DistType = serie.RecoveryType,
                                SerieName = "Descanso " + (i + 1),
                                RithmObjective = null
                            });
                            currentSerie += 2;
                        }
                    }

                    //Por ultimo vamos a añadir un elemento que será el enfriamiento
                    results.Add(new ResultsDayObject()
                    {
                        DayTrainingCode = dayTraining.DayTrainingCode,
                        NumSerie = currentSerie + 1,
                        DistObjective = 10,
                        DistType = "min",
                        SerieName = "Enfriamiento",
                        RithmObjective = null
                    });

                } 
                //Carreras
                else if(dayTraining.TrainingTypeCode == 2)
                {
                    List<RunInfoDbObject> runs = seriesTimesAndRuns.Runs.FindAll(s => s.GroupCode == dayTraining.RunGroupCode && s.IsShortRun);

                    runs = runs.OrderBy(s => s.Order).ToList();
                    int currentRun = 1;
                    //Primero vamos a añadir un elemento que será el calentamiento
                    results.Add(new ResultsDayObject()
                    {
                        DayTrainingCode = dayTraining.DayTrainingCode,
                        NumSerie = currentRun,
                        DistObjective = 1500,
                        DistType = "m",
                        SerieName = "Calentamiento",
                        RithmObjective = null
                    });
                    currentRun += 1;

                    //Para cada carrera tendremos que añadir un objeto resultDayObject por cada elemento
                    foreach (RunInfoDbObject run in runs)
                    {
                        //Añadimos la carrera
                        results.Add(new ResultsDayObject()
                        {
                            DayTrainingCode = dayTraining.DayTrainingCode,
                            NumSerie = currentRun,
                            DistObjective = run.Distance,
                            DistType = "m",
                            SerieName = "Serie " + (currentRun - 1),
                            RithmObjective = seriesTimesAndRuns.Times.GetRithmObjective(run.Distance, run.IsShortRun ? "short" : "long")
                        });
                        currentRun += 1;
                    }

                    //Por ultimo vamos a añadir un elemento que será el enfriamiento
                    results.Add(new ResultsDayObject()
                    {
                        DayTrainingCode = dayTraining.DayTrainingCode,
                        NumSerie = currentRun,
                        DistObjective = 1500,
                        DistType = "m",
                        SerieName = "Enfriamiento",
                        RithmObjective = null
                    });
                } 
                else if(dayTraining.TrainingTypeCode == 3)
                {
                    List<RunInfoDbObject> runs = seriesTimesAndRuns.Runs.FindAll(s => s.GroupCode == dayTraining.RunGroupCode && !s.IsShortRun);

                    runs = runs.OrderBy(s => s.Order).ToList();
                    int currentRun = 1;

                    //Para cada carrera tendremos que añadir un objeto resultDayObject por cada elemento
                    foreach (RunInfoDbObject run in runs)
                    {
                        //Añadimos la carrera
                        results.Add(new ResultsDayObject()
                        {
                            DayTrainingCode = dayTraining.DayTrainingCode,
                            NumSerie = currentRun,
                            DistObjective = run.Distance,
                            DistType = "m",
                            SerieName = "Serie " + (currentRun - 1),
                            RithmObjective = seriesTimesAndRuns.Times.GetRithmObjective(run.Distance, run.IsShortRun ? "short" : "long")
                        });
                        currentRun += 1;
                    }
                }
            }

            //Ahora con los resultados los guardamos en bbdd. Ahi pondremos los tiempos objetivos a partir de la tabla de times
            resultsDayDbManager.postResults(results.Select(r => r.GetDbObject()).ToList());


            return new TrainingObject(trainingDbObject);
        }

        public List<TrainingObject> GetTrainingsByUserId(long userId)
        {
            List<TrainingDbObject> trainings = trainingDbManager.GetTrainingsByUserId(userId);
            return trainings.Select(t => new TrainingObject(t)).ToList();
        }

        public TrainingObject GetTrainingById(int code, long userId)
        {
            TrainingDbObject training = trainingDbManager.GetTrainingById(code, userId);
            return new TrainingObject(training);
        }
    }
}
