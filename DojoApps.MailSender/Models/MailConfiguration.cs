using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DojoApps.MailSender.Models
{
    public class MailConfiguration
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string From { get; set; }

        public string[] Recipients { get; set; }

        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }

        public bool SmtpEnableSsl { get; set; }

        public string SmtpUser { get; set; }

        public string SmtpPassword { get; set; }

    }
}