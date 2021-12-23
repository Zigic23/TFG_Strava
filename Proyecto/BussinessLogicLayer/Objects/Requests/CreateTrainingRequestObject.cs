using BusinessLogicLayer.Base;
using DatabaseAccessLayer.Base;
using DatabaseAccessLayer.Objects.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogicLayer.Objects.Requests
{
    public class CreateTrainingRequestObject : MObject
    {
        public DateTime StartPlanDate { get; set; }
        public int TotalSecs { get; set; }
        public int PlanType { get; set; }
        public int Lunes { get; set; }
        public int Martes { get; set; }
        public int Miercoles { get; set; }
        public int Jueves { get; set; }
        public int Viernes { get; set; }
        public int Sabado { get; set; }
        public int Domingo { get; set; }
        public long UserCode { get; set; }

        public CreateTrainingRequestObject()
        {
        }

        public CreateTrainingRequestObject(DbObject dbObject) : base(dbObject)
        {
        }

        public CreateTrainingRequestDbObject getDbObject()
        {
            return new CreateTrainingRequestDbObject()
            {
                StartPlanDate = StartPlanDate,
                TotalSecs = TotalSecs,
                PlanType = PlanType,
                Lunes = Lunes,
                Martes = Martes,
                Miercoles = Miercoles,
                Jueves = Jueves,
                Viernes = Viernes,
                Sabado = Sabado,
                Domingo = Domingo,
                UserCode = UserCode
            };
        }
    }
}
