using System.Web.Mvc;

namespace WebApplication1.App_Start
{
    /// <summary>
    /// Filter configuration for the web application.
    /// </summary>
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}