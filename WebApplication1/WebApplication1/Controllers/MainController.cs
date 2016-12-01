using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Database;
using WebApplication1.Infrastructure.AuthAbstract;
using WebApplication1.Models;
using Microsoft.Office.Interop.Excel;

namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
       [Authorize]
       [AuthRestriections(AccessLevel= "Accountant,Purchase Manager")]
        public ActionResult MainIndex()
        {
            foreach (UserGenData item in SingletonDatabase.Instance().list )
                {
                if(item.Name == HttpContext.User.Identity.Name)
                {
                    return View(item);
                }
            }
            return View();
        }
        [Authorize]
        [AuthRestriections(AccessLevel = "Accountant,Purchase Manager")]
        public ActionResult TichurSuppCreate()
        {
            foreach (UserGenData item in SingletonDatabase.Instance().list)
            {
                if (item.Name == HttpContext.User.Identity.Name)
                {
                    return View(item);
                }
            }
            return View();

        }
       
        [Authorize]
        [AuthRestriections(AccessLevel = "Accountant,Purchase Manager")]
        public ActionResult TichurExisting()
        {
            foreach (UserGenData item in SingletonDatabase.Instance().list)
            {
                if (item.Name == HttpContext.User.Identity.Name)
                {
                    return View(item);
                }
            }
            return View();

        }
        [Authorize]
        [AuthRestriections(AccessLevel = "Purchase Manager")]
        public ActionResult MangUsers()
        {
            foreach (UserGenData item in SingletonDatabase.Instance().list)
            {
                if (item.Name == HttpContext.User.Identity.Name)
                {
                    return View(item);
                }
            }
            return View();

        }
        [Authorize]
        [AuthRestriections(AccessLevel = "Purchase Manager")]
        public ActionResult MangSuppliers()
        {
            foreach (UserGenData item in SingletonDatabase.Instance().list)
            {
                if (item.Name == HttpContext.User.Identity.Name)
                {
                    return View(item);
                }
            }
            return View();

        }
        [Authorize]
        [AuthRestriections(AccessLevel = "Purchase Manager")]

        public ActionResult MangAuctions()
        {
            foreach (UserGenData item in SingletonDatabase.Instance().list)
            {
                if (item.Name == HttpContext.User.Identity.Name)
                {
                    return View(item);
                }
            }
            return View();

        }
        [Authorize]
        [AuthRestriections(AccessLevel = "Purchase Manager")]
        public ActionResult MangClusters()
        {
            foreach (UserGenData item in SingletonDatabase.Instance().list)
            {
                if (item.Name == HttpContext.User.Identity.Name)
                {
                    return View(item);
                }
            }
            return View();

        }
        [Authorize]
        [AuthRestriections(AccessLevel = "Purchase Manager")]
        public ActionResult MangUnits()
        {
            foreach (UserGenData item in SingletonDatabase.Instance().list)
            {
                if (item.Name == HttpContext.User.Identity.Name)
                {
                    return View(item);
                }
            }
            return View();

        }
        public ActionResult UnAuthError()
        {
            return View();
        }
    }
}