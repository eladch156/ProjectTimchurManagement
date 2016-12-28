using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Database;
using WebApplication1.Infrastructure.AuthAbstract;
using WebApplication1.Models;
using Microsoft.Office.Interop.Excel;
using Microsoft.Owin;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
      
        [Authorize]
        [AuthRestriections(Name = "/Main/ApproveMsg")]
        public ActionResult ApproveMsg()
        {
           
            return View();
        }
        // GET: Main
        [Authorize]
        [AuthRestriections(Name = "/Main/MainIndex")]
        public ActionResult MainIndex()
        {
          
            
            return View();
        }
        [Authorize]
        [AuthRestriections(Name = "/Main/TichurSuppCreate")]
        public ActionResult TichurSuppCreate()
        {
          
            return View();

        }

        [Authorize]
        [AuthRestriections(Name = "/Main/TichurExisting")]
        public ActionResult TichurExisting()
        {
          
            return View();

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/MangUsers")]
        public ActionResult MangUsers()
        {
      
            return View();

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/MangSuppliers")]
        public ActionResult MangSuppliers()
        {
          
            return View();

        }

        [Authorize]
        [AuthRestriections(Name = "/Main/MangAuctions")]
        public ActionResult MangAuctions()
        {
        
            return View();

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/MangClusters")]
        public ActionResult MangClusters()
        {
     
            return View();

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/MangUnits")]
        public ActionResult MangUnits()
        {
           
            return View();

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/UnAuthError")]
        public ActionResult UnAuthError()
        {
            return View();
        }
     
    }
}