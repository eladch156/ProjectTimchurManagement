using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication2.Startup))]
namespace WebApplication2
{
    /// <summary>
    /// Initialize the application through its configuration.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configurates the authentication.
        /// </summary>
        /// <param name="app">The app builder used for configuration.</param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
