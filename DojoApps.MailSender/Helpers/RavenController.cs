using System.Threading.Tasks;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DojoApps.MailSender.Helpers
{
    public class RavenController : ApiController
    {
        public IDocumentSession Session { get; set; }

        public override Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Web.Http.Controllers.HttpControllerContext controllerContext, System.Threading.CancellationToken cancellationToken)
        {
            using (Session = WebApiApplication.Store.OpenSession())
            {
                return base.ExecuteAsync(controllerContext, cancellationToken);
            }
        }
    }
}