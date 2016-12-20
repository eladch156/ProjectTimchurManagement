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
        public ActionResult ApproveMsg()
        {

            return View();
        }
        // GET: Main
       
        public ActionResult MainIndex()
        {
          
            
            return View();
        }
       
        public ActionResult TichurSuppCreate()
        {
          
            return View();

        }
       
       
        public ActionResult TichurExisting()
        {
          
            return View();

        }
       
        public ActionResult MangUsers()
        {
      
            return View();

        }

        public ActionResult MangSuppliers()
        {
          
            return View();

        }
       
      
        public ActionResult MangAuctions()
        {
        
            return View();

        }
       
        public ActionResult MangClusters()
        {
     
            return View();

        }
      
        public ActionResult MangUnits()
        {
           
            return View();

        }
        public ActionResult UnAuthError()
        {
            return View();
        }
    }
}