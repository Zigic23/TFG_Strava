using System.Collections.Generic;

namespace StoneMVCCore.Models.Configuration.Settings
{
    public class StoneSettings
    {
        public static readonly string KEY = "StoneSettings";
        public List<StoneLogSettings> StoneLog { get; set; }
        public CustomErrors CustomErrors { get; set; }
    }

    public class CustomErrors
    {
        public string Mode { get; set; }
    }

    public class StoneLogSettings
    {
        public static readonly string KEY = "StoneLog";
        public string Provider { get; set; }
        public Dictionary<string, string> AdditionalProperties { get; set; }
    }
}
