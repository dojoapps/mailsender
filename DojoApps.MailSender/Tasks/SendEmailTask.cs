using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using DojoApps.MailSender.Helpers.Tasks;
using DojoApps.MailSender.Models;

namespace DojoApps.MailSender.Tasks
{
    public class SendEmailTask : BackgroundTask
    {
        public MailConfiguration MailConfiguration { get; set; }

        public MailRequest MailRequest { get; set; }

        public SendEmailTask(MailConfiguration conf, MailRequest request)
        {
            this.MailRequest = request;
            this.MailConfiguration = conf;
        }

        public override void Execute()
        {
            using(var smtpClient = new SmtpClient(MailConfiguration.SmtpHost, MailConfiguration.SmtpPort))
            {
                smtpClient.EnableSsl = MailConfiguration.SmtpEnableSsl;
                smtpClient.Credentials = new NetworkCredential(MailConfiguration.SmtpUser, MailConfiguration.SmtpPassword);

                var message = new MailMessage(MailConfiguration.From, MailRequest.Destination);
                message.Subject = MailRequest.Subject;
                message.SubjectEncoding = Encoding.UTF8;

                var messageBody = new System.Text.StringBuilder(MailRequest.Body);

                foreach (var extra in MailRequest.Extras)
                {
                    messageBody.AppendFormat("{0} = {1}\n", extra.Key, extra.Value);
                }

                message.Body = messageBody.ToString();
                message.BodyEncoding = Encoding.UTF8;

                smtpClient.Send(message);
            }
        }
    }
}