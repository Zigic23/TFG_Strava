using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoneMVCCore.Models.Configuration.Settings
{
    public class MailSettings
    {
        public string From { get; set; }
        public string Protocol { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
