using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebApplication1.Database;

namespace WebApplication1.Infrastructure.AuthAbstract
{
    public class AuthRestriections : AuthorizeAttribute
    {
        public string AccessLevel { get; set; }
        protected override bool AuthorizeCore(HttpContextBase context)
        {
            var isAuthorized=base.AuthorizeCore(context);
            if (!isAuthorized)
                return false;

            
            string[] prem_list = AccessLevel.Split(',');

            string prem_user="";
            string username = HttpContext.Current.User.Identity.Name;
            if (username.Equals("Admin"))
                prem_user = "Purchase Manager"; // get premission levels for user - need to edit
            else if (username.Equals("RegUser"))
                prem_user = "Accountant";
            SingletonDatabase.Instance().list.Add(new Models.UserGenData() { Name = username, Job = prem_user });
            //redirection to error page in this case
           if(!prem_list.Contains(prem_user))
            {
               
                return false; 
            }
            return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Main", action = "UnAuthError" }));
            }
        }


    }
}