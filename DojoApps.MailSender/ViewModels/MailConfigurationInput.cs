using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DojoApps.MailSender.ViewModels
{
    public class MailConfigurationInput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Recipients { get; set; }

        public string SmtpHost { get; set; }
    }
}