using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using WebApplication1.Database;

[assembly: OwinStartup(typeof(WebApplication1.App_Start.Startup))]

namespace WebApplication1.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            SingletonCache.Instance().list.Add(new Models.RoleModel() { Name="Admin", Role="Admin",GivenName = "אדם בלוע " });
            SingletonCache.Instance().list.Add(new Models.RoleModel() { Name = "RegUser", Role = "User",GivenName="צ'רנחוסבקי שמוליק " });
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Login/Index"),
                  ExpireTimeSpan = TimeSpan.FromMinutes(3)
            });
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
