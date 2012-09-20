using System.Threading.Tasks;
using System.Web.Mvc;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DojoApps.MailSender.Helpers
{
    public class RavenApiController : ApiController
    {
        public IDocumentSession DocumentSession { get; set; }

        public override Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Web.Http.Controllers.HttpControllerContext controllerContext, System.Threading.CancellationToken cancellationToken)
        {
            using (DocumentSession = WebApiApplication.Store.OpenSession())
            {
                return base.ExecuteAsync(controllerContext, cancellationToken);
            }
        }
    }

    public class RavenMvcController : Controller
    {
        public IDocumentSession DocumentSession { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DocumentSession = WebApiApplication.Store.OpenSession();

            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if ( DocumentSession != null )
            {
                DocumentSession.Dispose();
                DocumentSession = null;
            }
        }
    }
}