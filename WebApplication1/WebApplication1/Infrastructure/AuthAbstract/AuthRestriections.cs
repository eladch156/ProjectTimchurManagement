using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Infrastructure.AuthAbstract
{
    public class AuthRestriections : AuthorizeAttribute
    {
        public string Name { get; set; }
        protected override bool AuthorizeCore(HttpContextBase context)
        {
            var isAuthorized=base.AuthorizeCore(context);
            if (!isAuthorized)
                return false;
            var claimsIdentity = context.User.Identity as System.Security.Claims.ClaimsIdentity;
            var name = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.Name);

            string[] prem_list = SingletonCache.Instance().role_map[Name].Split(',');
            string prem_user = SingletonCache.Instance().list.Find((RoleModel mod) => { return mod.Name == (string)name.Value; }).Role;
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