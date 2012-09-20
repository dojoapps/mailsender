using System.Web.Mvc;

namespace DojoApps.MailSender.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DisplayIf(this HtmlHelper helper, bool condition)
        {
            return condition ? null : MvcHtmlString.Create("style=\"display: none\"");
        }
    }
}