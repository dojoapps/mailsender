using System.Net;
using System.Net.Http;
using System.Web.Http;
using DojoApps.MailSender.Helpers;
using DojoApps.MailSender.Helpers.Tasks;
using DojoApps.MailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DojoApps.MailSender.Tasks;

namespace DojoApps.MailSender.Controllers
{
    public class MailController : RavenController
    {
        [HttpPost]
        public HttpResponseMessage Send([FromBody]MailRequest request)
        {
            if (request == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid request");
            }

            var configuration = Session.Load<MailConfiguration>(request.HostId);

            if (configuration == null || !configuration.Recipients.Any(x => x.Equals(request.Destination, StringComparison.InvariantCultureIgnoreCase)))
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid request");
            }

            if ( request.Async == true )
            {
                TaskExecutor.ExecuteTask(new SendEmailTask(configuration, request));
            } 
            else
            {
                var sendEmail = new SendEmailTask(configuration, request);
                if (sendEmail.Run(Session) == false )
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                       "Não foi possível enviar este email");
                }

            }

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(request.RedirectUri);

            return response;
        }
    }
}
