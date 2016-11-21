using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

            string premission="";
            string username = FormsAuthentication.GetAuthCookie().Value();
                // get premission levels

            if ()
        }
        
    }
}