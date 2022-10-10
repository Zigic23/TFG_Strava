using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseAccessLayer.Objects.Requests
{
    public class CreateTrainingRequestDbObject : DbObject
    {
        public DateTime StartPlanDate { get; set; }
        public DateTime BornDate { get; set; }
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

        public CreateTrainingRequestDbObject()
        {
        }

        public CreateTrainingRequestDbObject(DataRow row) : base(row)
        {
        }
    }
}
