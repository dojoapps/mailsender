using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DojoApps.MailSender.Models
{
    public class MailRequest
    {
        public int HostId { get; set; }

        public string Destination { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public Dictionary<string,string> Extras { get; set; }

        public bool Async { get; set; }

        public string RedirectUri { get; set; }
    }
}