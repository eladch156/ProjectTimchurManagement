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
           
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Login/Index"),
                  ExpireTimeSpan = TimeSpan.FromMinutes(3)
            });
            SingletonCache.Instance().updater = new DatabaseUpdater();
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
