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

            
            string[] prem_list = AccessLevel.Split(',');

            string username = HttpContext.Current.User.Identity.Name;
            Console.WriteLine(username);
            string prem_user = "Accoutant"; // get premission levels for user - need to edit
            //redirection to error page in this case
           if(!prem_list.Contains(prem_user))
            {
                return false; 
            }
            return true;
        }
        
    }
}