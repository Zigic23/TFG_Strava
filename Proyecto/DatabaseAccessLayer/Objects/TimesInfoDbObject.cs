using DatabaseAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseAccessLayer.Objects
{
    public class TimesInfoDbObject : DbObject
    {
        public int TimeCode { get; set; }
        public int Mark5 { get; set; }
        public int Series400 { get; set; }
        public int Series600 { get; set; }
        public int Series800 { get; set; }
        public int Series1000 { get; set; }
        public int Series1200 { get; set; }
        public int Series1600 { get; set; }
        public int Series2000 { get; set; }
        public int TempoShort { get; set; }
        public int TempoMedium { get; set; }
        public int TempoLong { get; set; }
        public int TempoEasy { get; set; }
        public int LongMar { get; set; }
        public int LongMedMar { get; set; }

        public TimesInfoDbObject()
        {
        }

        public TimesInfoDbObject(DataRow row) : base(row)
        {
            this.TimeCode = (int)row["CD_TIME"];
            this.Mark5 = (int)row["NM_MARK_5"];
            this.Series400 = (int)row["NM_SERIES_400"];
            this.Series600 = (int)row["NM_SERIES_600"];
            this.Series800 = (int)row["NM_SERIES_800"];
            this.Series1000 = (int)row["NM_SERIES_1000"];
            this.Series1200 = (int)row["NM_SERIES_1200"];
            this.Series1600 = (int)row["NM_SERIES_1600"];
            this.Series2000 = (int)row["NM_SERIES_2000"];
            this.TempoShort = (int)row["NM_TEMPO_SHORT"];
            this.TempoMedium = (int)row["NM_TEMPO_MEDIUM"];
            this.TempoLong = (int)row["NM_TEMPO_LONG"];
            this.TempoEasy = (int)row["NM_TEMPO_EASY"];
            this.LongMar = (int)row["NM_LONG_MAR"];
            this.LongMedMar = (int)row["NM_LONG_MED_MAR"];
        }

        public int GetRithmObjective(int distance, string type)
        {
            switch(type)
            {
                case "serie":
                    switch(distance)
                    {
                        case 400:
                        default:
                            return Series400;

                        case 600:
                            return Series600;

                        case 800:
                            return Series800;

                        case 1000:
                            return Series1000;

                        case 1200:
                            return Series1200;

                        case 1600:
                            return Series1600;

                        case 2000:
                            return Series2000;
                    }
                case "short":
                    switch(distance)
                    {
                        case 1500:
                            return TempoEasy;

                        case 3000:
                        case 5000:
                            return TempoShort;

                        case 6500:
                            return TempoMedium;

                        case 8000:
                        case 10000:
                        default:
                            return TempoLong;
                    }
                case "long":
                    if (distance < 15000)
                        return LongMar;
                    else 
                        return LongMedMar;
                default:
                    return Mark5;
            }
        }
    }
}
