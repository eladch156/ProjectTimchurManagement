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
    /// <summary>
    /// Class representation of user authorization management through the singleton cache.
    /// </summary>
    public class AuthRestrictions : AuthorizeAttribute
    {
        public string Name { get; set; }
        /// <summary>
        /// Checks for authorization to the given request.
        /// </summary>
        /// <param name="context">Context checked for authorization.</param>
        /// <returns>Whether user is authorized or not given the context.</returns>
        protected override bool AuthorizeCore(HttpContextBase context)
        {
            var isAuthorized=base.AuthorizeCore(context);
            if (!isAuthorized)
                return false;
            var claimsIdentity = context.User.Identity as System.Security.Claims.ClaimsIdentity;
            var name = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.Name);
            string[] prem_list = SingletonCache.Instance().role_map[Name].Split(',');
            string role= claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.Role).Value;
            //redirection to error page in this case
            if (!prem_list.Contains(role))
            {
                return false; 
            }
            return true;
        }
        /// <summary>
        /// Handles a request by an unauthorized source.
        /// </summary>
        /// <param name="filterContext">The context checked for authorization.</param>
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