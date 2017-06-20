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
using System.Collections.Generic;
using System.Net.Mail;

/// <summary>
/// Controller for the login mechanism of the application.
/// </summary>
namespace WebApplication1.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public static Dictionary<String, String> email_key = new Dictionary<String, String>();
        IAuthProvider prov;
        // GET: Login
        public ActionResult SendResetEmail(string email)
        {
            Random rand = new Random();
            string key = "";
            int i = 0;
            for (i = 0; i < 16; i++)
            {
                int[] randoms = new int [] { rand.Next(48, 58), rand.Next(65, 91), rand.Next(97, 123) };
                int rand_indx = rand.Next(0, 3);
                int curr_byte = randoms[rand_indx];
                char c = (char)curr_byte;
                key += c;
            }
            var urlBuilder =
    new System.UriBuilder(Request.Url.AbsoluteUri)
    {
        Path = Url.Action("ResetPassword", "Login"),
        Query = null,
    };

            Uri uri = urlBuilder.Uri;
            string url = urlBuilder.ToString();
            String m = "<a href='"+ url +"?key=" + key + "'>לחץ כאן להמשך איפוס סיסמא</a>";
            GMailer.GmailUsername = "tichurmanagsys@gmail.com";
            GMailer.GmailPassword = "Manymany55";
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            GMailer mailer = new GMailer();
            mailer.ToEmail = email;
            mailer.Subject = "מערכת ניהול מכרזים - איפוס סיסמא";
            mailer.Body = string.Format(body, "tichurmanagsys@gmail.com", "מערכת לניהול מכרזים, לא להגיב!", m);
            mailer.IsHtml = true;
            
            mailer.Send();
            bool exist = false;
            foreach (String s in LoginController.email_key.Keys)
            {
                if (s.CompareTo(email) == 0)
                    exist = true;
            }
            if(exist)
            LoginController.email_key.Remove(email);

            LoginController.email_key.Add(email, key);
          

 
                return Json("Sent",JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPassModel mod)
        {
           
            using (TimchurDatabaseEntities ent = new TimchurDatabaseEntities())
            {

                bool valid = ent.Users.Where(x => x.IDCardNumber == mod.Id && x.Email == mod.Email).Count()>0;
                bool exist = false;
                foreach(String s in LoginController.email_key.Keys)
                {
                    if (s.CompareTo(mod.Email) == 0)
                        exist = true;
                }
                bool key_email_validation = exist && LoginController.email_key[mod.Email].CompareTo(mod.Key)==0;
                if(valid && key_email_validation)
                {
                    Cache.gen_lock.WaitOne();
                    var my_use = ent.Users.Where(x => x.IDCardNumber == mod.Id && x.Email == mod.Email);
                    foreach (Users use in my_use)
                    {
                        use.Password = mod.NewPass.ToString();
                    }
                    ent.SaveChanges();
                    Cache.gen_lock.ReleaseMutex();
                    ModelState.AddModelError("הצליח", "שינוי פרטים הצליח");
                    return View(mod);
                }
                else
                {
                    ModelState.AddModelError("שגיאה","פרטים לא נכונים, אנא וודא את השדות ושלח שוב");
                    return View(mod);
                }
            }
                
        }
        public ActionResult ResetPassword(string key)
        {
            ResetPassModel mod = new ResetPassModel();
            mod.Id = "";
            mod.Key = key;
            mod.Email = "";
            mod.NewPass = "";
            return View(mod);
        }
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