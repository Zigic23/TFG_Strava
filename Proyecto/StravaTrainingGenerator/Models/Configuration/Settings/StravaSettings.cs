using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaTrainingGenerator.Models.Configuration.Settings
{
    public class StravaSettings
    {
        public string strava_url { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
        public string authorize_url { get; set; }
        public string redirect_uri { get; set; }
        public string scopes { get; set; }
    }
}
