using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DojoApps.MailSender.Helpers;
using DojoApps.MailSender.Models;

namespace DojoApps.MailSender.Controllers
{
    [Authorize]
    public class AdminController : RavenMvcController
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string pwd)
        {
            if ( pwd.Equals(ConfigurationManager.AppSettings["AdminPassword"], StringComparison.InvariantCultureIgnoreCase))
            {
                FormsAuthentication.SetAuthCookie("admin", false);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Index(int page=0)
        {
            var configurations = DocumentSession.Query<MailConfiguration>().Customize(x => x.WaitForNonStaleResultsAsOfNow()).ToList();

            return View(configurations);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var conf = new MailConfiguration();
            if ( id.HasValue )
            {
                conf = DocumentSession.Load<MailConfiguration>(id);

                if (conf == null)
                {
                    return HttpNotFound();
                }
            }

            conf.Recipients = new List<string>() { string.Join(",", conf.Recipients) };

            return View(conf);
        }

        public ActionResult Save(int? id, MailConfiguration input)
        {
            MailConfiguration conf;
            if ( !id.HasValue )
            {
                conf = new MailConfiguration();
                DocumentSession.Store(conf);
            } else
            {
                conf = DocumentSession.Load<MailConfiguration>(id);
            }

            conf.Name = input.Name;
            conf.From = input.From;
            conf.Recipients =
                (input.Recipients != null)
                    ? input.Recipients[0].Split(',').ToList()
                    : new List<string>() { String.Empty };
            conf.SmtpEnableSsl = input.SmtpEnableSsl;
            conf.SmtpHost = input.SmtpHost;
            conf.SmtpPassword = input.SmtpPassword;
            conf.SmtpUser = input.SmtpUser;
            conf.SmtpPort = input.SmtpPort;

            conf.Recipients.ForEach(x => x = x.Trim());

            DocumentSession.SaveChanges();

            TempData["Message"] = "Configuração salva!";

            return RedirectToAction("Index");
        }
    }
}
