using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication1.AuthAbstract;
using WebApplication1.Models;
using System.Net;
using System.Web.Security;
using WebApplication1.Database;

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
        [HttpGet]
        public ActionResult Index(string returnUrl)
        { 
            
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }
        public ActionResult LoginOut()
        {
            this.Logout();
            return RedirectToAction("Index");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                
                    return HttpContext.GetOwinContext().Authentication;
                
               
            }
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {

            if(!ModelState.IsValid)
            {
                return View();
            }
           if(this.prov.Authenticate(model.Username,model.Password))
            {
                //need to add taking from database
                
                SignInAsync(model, true);


                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }
            ModelState.AddModelError("FailedLogin", "Invalid username or password");
            return View(model);
        }
        private void Logout()
        {
            Request.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
        }
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
                //צריך להוסיף להוציא שם מהמסד

           
            
        }
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
                return Url.Action("MainIndex", "Main");

            }
            return returnUrl;
        }

      
      
    }
}