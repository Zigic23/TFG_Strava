using System;
using System.Collections.Generic;
using System.Text;

namespace StravaConnector.Objects
{
    public class Athlete : BaseAthlete
    {
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string sex { get; set; }
        public bool premium { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int badge_type_id { get; set; }
        public string profile_medium { get; set; }
        public string profile { get; set; }
        public string friend { get; set; }
        public string follower { get; set; }
        public int follower_count { get; set; }
        public int friend_count { get; set; }
        public int mutual_friend_count { get; set; }
        public int athlete_type { get; set; }
        public string date_preference { get; set; }
        public string measurement_preference { get; set; }
        public List<string> clubs { get; set; }
        public string ftp { get; set; }
        public int weight { get; set; }
        public List<Bike> bikes { get; set; }
        public List<Shoe> shoes { get; set; }
    }

    public class Bike
    {
        public string id { get; set; }
        public bool primary { get; set; }
        public string name { get; set; }
        public int resource_state { get; set; }
        public long distance { get; set; }
    }

    public class Shoe
    {
        public string id { get; set; }
        public bool primary { get; set; }
        public string name { get; set; }
        public int resource_state { get; set; }
        public long distance { get; set; }
    }
}
