using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DojoApps.MailSender
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SendMail",
                routeTemplate: "api/mail/send",
                defaults: new { controller = "MailApi", action = "Send"}
                );
        }
    }
}
