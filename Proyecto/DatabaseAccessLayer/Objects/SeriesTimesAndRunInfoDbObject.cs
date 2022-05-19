using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccessLayer.Objects
{
    public class SeriesTimesAndRunInfoDbObject
    {
        public List<RunInfoDbObject> Runs { get; set; }
        public List<SeriesInfoDbObject> Series { get; set; }
        public TimesInfoDbObject Times { get; set; }

        public SeriesTimesAndRunInfoDbObject(List<RunInfoDbObject> Runs, List<SeriesInfoDbObject> Series, TimesInfoDbObject Times)
        {
            this.Runs = Runs;
            this.Series = Series;
            this.Times = Times;
        }
    }
}
