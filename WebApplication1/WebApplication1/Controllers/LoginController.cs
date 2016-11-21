using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Infrastructure;
using WebApplication1.AuthAbstract;
using WebApplication1.Models;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        IAuthProvider prov;
        // GET: Login
        public LoginController(IAuthProvider prove)
        {

            this.prov = prove;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel mod)
        {
            if(ModelState.IsValid)
            {
                if(prov.Authenticate(mod.Username,mod.Password))

                {
                    FormsAuthentication.SetAuthCookie(mod.Username, false);
                    
                    return Redirect(Url.Action("MainIndex", "Main"));
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}