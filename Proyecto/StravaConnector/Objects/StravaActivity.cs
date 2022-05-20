using System;
using System.Collections.Generic;
using System.Text;

namespace StravaConnector.Objects
{
    public class StravaActivity
    {
        public int resource_state { get; set; }
        public BaseAthlete athlete { get; set; }
        public string name { get; set; }
        public float distance { get; set; }
        public int moving_time { get; set; }
        public int elapsed_time { get; set; }
        public int total_elevation_gain { get; set; }
        public string type { get; set; }
        public object workout_type { get; set; }
        public long id { get; set; }
        public string external_id { get; set; }
        public long upload_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime start_date_local { get; set; }
        public string timezone { get; set; }
        public int utc_offset { get; set; }
        public object start_latlng { get; set; }
        public object end_latlng { get; set; }
        public object location_city { get; set; }
        public object location_state { get; set; }
        public string location_country { get; set; }
        public int achievement_count { get; set; }
        public int kudos_count { get; set; }
        public int comment_count { get; set; }
        public int athlete_count { get; set; }
        public int photo_count { get; set; }
        public Map map { get; set; }
        public bool trainer { get; set; }
        public bool commute { get; set; }
        public bool manual { get; set; }
        public bool _private { get; set; }
        public bool flagged { get; set; }
        public string gear_id { get; set; }
        public bool from_accepted_tag { get; set; }
        public float average_speed { get; set; }
        public int max_speed { get; set; }
        public float average_cadence { get; set; }
        public float average_watts { get; set; }
        public int weighted_average_watts { get; set; }
        public float kilojoules { get; set; }
        public bool device_watts { get; set; }
        public bool has_heartrate { get; set; }
        public float average_heartrate { get; set; }
        public int max_heartrate { get; set; }
        public int max_watts { get; set; }
        public int pr_count { get; set; }
        public int total_photo_count { get; set; }
        public bool has_kudoed { get; set; }
        public int suffer_score { get; set; }
    }

    public class Map
    {
        public string id { get; set; }
        public string summary_polyline { get; set; }
        public int resource_state { get; set; }
    }

}
