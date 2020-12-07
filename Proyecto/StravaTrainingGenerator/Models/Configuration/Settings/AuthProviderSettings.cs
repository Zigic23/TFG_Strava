using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoneMVCCore.Models.Configuration.Settings
{
    public class AuthProviderSettings
    {
        public string Domain { get; set; }
        public string Provider { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string SuperAdminRoleName { get; set; }
    }
}
