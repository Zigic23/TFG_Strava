using System;
using System.Collections.Generic;
using System.Text;

namespace StravaConnector.Objects
{
    public class StravaAccessToken
    {
        public string token_type { get; set; }
        public long expires_at { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string access_token { get; set; }
    }
}
