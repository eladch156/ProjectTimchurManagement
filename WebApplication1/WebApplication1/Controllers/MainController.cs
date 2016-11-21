using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Infrastructure.AuthAbstract;

namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
       [Authorize]
       [AuthRestriections(AccessLevel="Accoutant,Purchase Manager")]
        public ActionResult MainIndex()
        {
            return View();
        }
    }
}