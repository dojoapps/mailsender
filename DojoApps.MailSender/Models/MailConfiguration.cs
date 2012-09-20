using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DojoApps.MailSender.Models
{
    public class MailConfiguration
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string From { get; set; }

        public List<string> Recipients { get; set; }

        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }

        [DisplayName("SSL?")]
        public bool SmtpEnableSsl { get; set; }

        public string SmtpUser { get; set; }

        public string SmtpPassword { get; set; }

        public MailConfiguration()
        {
            Recipients = new List<string>();
        }

    }
}