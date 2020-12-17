using Microsoft.AspNetCore.Authentication.OAuth;
using StoneMVCCore.Models.Configuration.Settings;
using StravaTrainingGenerator.Models.Configuration.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaTrainingGenerator.Models.Configuration.OAuth
{
    public class StravaOAuthOptions : OAuthOptions
    {
        public StravaSettings StravaSettings { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }

        public StravaOAuthOptions() { }

        public StravaOAuthOptions(StravaSettings stravaSettings, ConnectionStrings connectionStrings)
        {
            this.StravaSettings = stravaSettings;
            this.ConnectionStrings = connectionStrings;
        }
    }
}
