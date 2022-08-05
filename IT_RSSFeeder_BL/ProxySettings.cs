using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IT_RSSFeeder_BL
{
    public sealed class ProxySettings
    {
        public string IP { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool UseAuthentication { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
