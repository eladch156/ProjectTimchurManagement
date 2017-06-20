using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using WebApplication1.AuthAbstract;
using WebApplication1.Models;
using WebApplication1.Database;
using System.Diagnostics;

/// <summary>
/// Controller for the login mechanism of the application.
/// </summary>
namespace WebApplication1.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        IAuthProvider prov;
        // GET: Login
        public LoginController(IAuthProvider prove)
        {
            this.prov = prove;
        }
        /// <summary>
        /// Forms a new LoginModel via redirection URL
        /// and enacts the View upon it.
        /// </summary>
        /// <param name="returnUrl">The redirection URL upon login.</param>
        /// <returns>Rendition of the created model by the View.</returns>
        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        /// <summary>
        /// Logs out of the application and redirects to the login page.
        /// </summary>
        /// <returns>Action redirection to "Index".</returns>
        public ActionResult LoginOut()
        {
            this.Logout();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Authentication Manager property.
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        /// <summary>
        /// Given an existing LoginModel, renders the View upon it.
        /// </summary>
        /// <param name="model">The Login model.</param>
        /// <returns>The ViewResult rendition of the Model, if applicable.</returns>
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
           if(this.prov.Authenticate(model.Username,model.Password))
            {
                SignInAsync(model, true);
                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }
            ModelState.AddModelError("FailedLogin", "Invalid username or password");
            return View(model);
        }
        /// <summary>
        /// Signs out of the user.
        /// </summary>
        private void Logout()
        {
            Request.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
        }
        /// <summary>
        /// Asynchronically logs in a given user.
        /// </summary>
        /// <param name="user">The loginmodel used to represent the given user.</param>
        /// <param name="isPersistent">Whether or not the authentication session is persistent.</param>
        private void SignInAsync(LoginModel user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            using (var conn = new TimchurDatabaseEntities())
            {
                Users User = conn.Users.Where(s => (s.IDCardNumber).ToString() == user.Username).FirstOrDefault<Users>();
                string role = "User";
                if (User.Roles.ID == 1)
                {
                    role = "User";
                }
                else
                {
                    role = "Admin";
                }
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, User.IDCardNumber), new Claim(ClaimTypes.GivenName, User.FullName), new Claim(ClaimTypes.Role, role) }, "ApplicationCookie");
                SingletonCache.Instance().last_msg[User.IDCardNumber] = null;
                AuthenticationManager.SignIn(
                   new AuthenticationProperties()
                   {
                       IsPersistent = isPersistent
                   }, identity);
            }  
        }
        /// <summary>
        /// Defines the redirection URL.
        /// </summary>
        /// <param name="returnUrl">The current redirection URL, if such exists.</param>
        /// <returns>The given redirection url if such exists, otherwise the default "main".</returns>
        private string GetRedirectUrl(string returnUrl)
        {
            try {
                if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                {
                    return Url.Action("MainIndex", "Main");
                }
            }
            catch (Exception e)
            {
                Trace.Write(e.Message);
                return Url.Action("MainIndex", "Main");
            }
            return returnUrl;
        }
    }
}