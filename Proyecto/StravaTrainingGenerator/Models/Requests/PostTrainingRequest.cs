using BussinessLogicLayer.Objects.Requests;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StravaTrainingGenerator.Models.Requests
{
    public class PostTrainingRequest
    {
        public string start_plan { get; set; }
        public int mins { get; set; }
        public int secs { get; set; }
        public int planType { get; set; }
        public string born_date { get; set; }
        public int lunes { get; set; }
        public int martes { get; set; }
        public int miercoles { get; set; }
        public int jueves { get; set; }
        public int viernes { get; set; }
        public int sabado { get; set; }
        public int domingo { get; set; }

        public CreateTrainingRequestObject getBLLObject(long UserCode, out string error)
        {
            DateTime startPlan;
            DateTime bornDate;
            if(DateTime.TryParseExact(start_plan, "yyyy-MM-dd", null, DateTimeStyles.None, out startPlan) && DateTime.TryParseExact(born_date, "yyyy-MM-dd", null, DateTimeStyles.None, out bornDate))
            {
                error = null;
                return new CreateTrainingRequestObject()
                {
                    StartPlanDate = startPlan,
                    BornDate = bornDate,
                    TotalSecs = mins * 60 + secs,
                    PlanType = planType,
                    Lunes = lunes,
                    Martes = martes,
                    Miercoles = miercoles,
                    Jueves = jueves,
                    Viernes = viernes,
                    Sabado = sabado,
                    Domingo = domingo,
                    UserCode = UserCode
                };
            } 
            else
            {
                error = "La fecha introducida es incorrecta";
                return null;
            }
            
        }
    }
}
