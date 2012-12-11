using System.Net;
using System.Net.Http;
using System.Web.Http;
using DojoApps.MailSender.Helpers;
using DojoApps.MailSender.Helpers.Tasks;
using DojoApps.MailSender.Models;
using System;
using System.Linq;
using DojoApps.MailSender.Tasks;

namespace DojoApps.MailSender.Controllers
{
    public class MailApiController : RavenApiController
    {
        [HttpPost]
        public HttpResponseMessage Send([FromBody]MailRequest request)
        {
            if (request == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid request");
            }

            var configuration = DocumentSession.Load<MailConfiguration>(request.HostId);

            if (configuration == null || !configuration.Recipients.Any(x => x.Equals(request.Destination, StringComparison.InvariantCultureIgnoreCase)))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid request");
            }

            if ( request.Async )
            {
                TaskExecutor.ExecuteTask(new SendEmailTask(configuration, request));
                var response = Request.CreateResponse(HttpStatusCode.OK, new { Message = "Message sent" });
                if (!string.IsNullOrEmpty(request.RedirectUri))
                {
                    response.Headers.Location = new Uri(request.RedirectUri);
                }

                return response;
            } 
            else
            {
                var sendEmail = new SendEmailTask(configuration, request);
                if (sendEmail.Run(DocumentSession) == false )
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                       "Unable to send the email");
                }

                var response = Request.CreateResponse(HttpStatusCode.Redirect);
                response.Headers.Location = new Uri(request.RedirectUri);

                return response;

            }
        }
    }
}
