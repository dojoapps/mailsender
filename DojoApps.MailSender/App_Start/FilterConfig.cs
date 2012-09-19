using System.Web;
using System.Web.Mvc;

namespace DojoApps.MailSender
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}