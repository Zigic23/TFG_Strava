using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccessLayer.Objects
{
    public class LapResultDbObject
    {
        public DateTime Date { get; set; }
        public int NumSerie { get; set; }
        public int TimeDone { get; set; }
        public int Desnivel { get; set; }
        public int AverageFrecuency { get; set; }
        public int RithmDone { get; set; }
        public int DistanceDone { get; set; }
        public int? RateDone { get; set; }
    }
}
