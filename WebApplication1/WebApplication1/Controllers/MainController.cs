﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
       [Authorize]
        public ActionResult MainIndex()
        {
            return View();
        }
    }
}