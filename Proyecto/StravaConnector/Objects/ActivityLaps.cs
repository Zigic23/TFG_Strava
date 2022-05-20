using System;
using System.Collections.Generic;
using System.Text;

namespace StravaConnector.Objects
{
    public class ActivityLaps
    {
        public long id { get; set; }
        public int resource_state { get; set; }
        public string name { get; set; }
        public BaseAthlete athlete { get; set; }
        public int elapsed_time { get; set; }
        public int moving_time { get; set; }
        public DateTime start_date { get; set; }
        public DateTime start_date_local { get; set; }
        public float distance { get; set; }
        public int start_index { get; set; }
        public int end_index { get; set; }
        public float total_elevation_gain { get; set; }
        public float average_speed { get; set; }
        public float max_speed { get; set; }
        public bool device_watts { get; set; }
        public float average_heartrate { get; set; }
        public float max_heartrate { get; set; }
        public int lap_index { get; set; }
        public int split { get; set; }
        public int pace_zone { get; set; }
    }

}
