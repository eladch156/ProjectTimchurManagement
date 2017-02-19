using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Database;
using WebApplication1.Infrastructure.AuthAbstract;
using WebApplication1.Models;

using Microsoft.Owin;
using System.Security.Claims;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Collections;
using System.Data;
using System.Threading;

namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
        private TimchurDatabaseEntities db = new TimchurDatabaseEntities();
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
        [HttpPost]
        [Authorize]
        [AuthRestriections(Name = "/Main/MangUsers")]
        public ActionResult MangUsers(Users user)
        {

            db = new TimchurDatabaseEntities();
            List<Users> li = db.Users.OrderBy(s => s.IDCardNumber).ToList();

            /** load from database user list **/
            /** transform loaded data into model fit for edit + present **/

            return View(li);

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/MangUsers")]
        public ActionResult MangUsers()
        {

            db = new TimchurDatabaseEntities();
            List<Users> li = db.Users.OrderBy(s => s.IDCardNumber).ToList();

            /** load from database user list **/
            /** transform loaded data into model fit for edit + present **/

            return View(li);

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/MangSuppliers")]
        public ActionResult MangSuppliers()
        {

            db = new TimchurDatabaseEntities();
            List<Suppliers> li = db.Suppliers.OrderBy(s => s.Name).OrderBy(s=> s.SuppliersClusetrs.FirstOrDefault().Clusetrs.Auctions.AuctionNumber).OrderBy(s => s.SuppliersClusetrs.FirstOrDefault().Clusetrs.DisplayNumber).ToList();

            /** load from database user list **/
            /** transform loaded data into model fit for edit + present **/

            return View(li);
        }

        [Authorize]
        [AuthRestriections(Name = "/Main/MangAuctions")]
        public ActionResult MangAuctions()
        {

            db = new TimchurDatabaseEntities();
            List<Auctions> li = db.Auctions.OrderBy(s => s.AuctionNumber).ToList();

            /** load from database user list **/
            /** transform loaded data into model fit for edit + present **/

            return View(li);

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/MangClusetrs")]
        public ActionResult MangClusetrs()
        {

            db = new TimchurDatabaseEntities();
            List<Clusetrs> li = db.Clusetrs.OrderBy(s => s.DisplayNumber).ToList();

            /** load from database user list **/
            /** transform loaded data into model fit for edit + present **/

            return View(li);

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/MangUnits")]
        public ActionResult MangUnits()
        {

            db = new TimchurDatabaseEntities();
            List<Units> li = db.Units.OrderBy(s => s.ID).ToList();

            /** load from database user list **/
            /** transform loaded data into model fit for edit + present **/

            return View(li);

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/UnAuthError")]
        public ActionResult UnAuthError()
        {
            return View();
        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddUser")]

        public ActionResult EditUser(int? id)
        {
          
                if (id == null)
                {
                    return View("EditUser");
                }
            using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
            {
                Users use = entity.Users.Where(x => x.ID == id).First();
                if (use == null)
                {
                    return View("EditUser");
                }
                else
                {
                    return View("EditUser",use);
                }
                


            }
        }
            
            
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddUser")]
        [HttpPost]
        public ActionResult EditUser(Users use)
        {

            if (ModelState.IsValid)
            {
                       SingletonCache.Instance().Storage[User.Identity.Name] = use;
                        return RedirectToAction("EUserLoadingScreen", "Main");
                   
            }
            else
            {
                return View(use);
            }



        }

        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddUser")]
        public ActionResult AddUser()
        {

         
                    return View();
        
        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddUser")]
        [HttpPost]
        public ActionResult AddUser(Users use)
        {

            if (ModelState.IsValid)
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {
                    int cou = entity.Users.Count(x =>x.IDCardNumber == use.IDCardNumber);
                    if(cou!=0)
                    {
                        ModelState.AddModelError("Exist", "המשתמש בעל תעודת הזהות הזאת כבר קיים.");
                        return View(use);
                    }
                    else
                    {
                        SingletonCache.Instance().Storage[User.Identity.Name] = use;
                        return RedirectToAction("UserLoadingScreen", "Main");
                    }
                }
                   
            }
            else
            {
                return View(use);
            }


        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddUser")]
        public ActionResult UserLoadingScreen(Users target)
        {
            return View(target);
        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddUser")]
        public ActionResult EUserLoadingScreen(Users target)
        {
            return View(target);
        }
    
    [Authorize]
    [AuthRestriections(Name = "/Main/EditOrAddAuction")]
    public ActionResult EditAuction(int? id)
    {

        if (id == null)
        {
            return View("EditAuction");
        }
        using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
        {
            Auctions auc = entity.Auctions.Where(x => x.ID == id).First();
            if (auc == null)
            {
                return View("EditAuction");
            }
            else
            {
                return View("EditAuction", auc);
            }



        }
    }


    [Authorize]
    [AuthRestriections(Name = "/Main/EditOrAddAuction")]
    [HttpPost]
    public ActionResult EditAuction(Auctions auc)
    {

        if (ModelState.IsValid)
        {
            SingletonCache.Instance().Storage[User.Identity.Name] = auc;
            return RedirectToAction("EAuctionLoadingScreen", "Main");

        }
        else
        {
            return View(auc);
        }



    }

    [Authorize]
    [AuthRestriections(Name = "/Main/EditOrAddAuction")]
    public ActionResult AddAuction()
    {


        return View();

    }
    [Authorize]
    [AuthRestriections(Name = "/Main/EditOrAddAuction")]
    [HttpPost]
    public ActionResult AddAuction(Auctions auc)
    {

        if (ModelState.IsValid)
        {
            using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
            {
                int cou = entity.Auctions.Count(x => x.AuctionNumber == auc.AuctionNumber);
                if (cou != 0)
                {
                    ModelState.AddModelError("Exist", "מכרז בעל מספר זה כבר קיים.");
                    return View(auc);
                }
                else
                {
                    SingletonCache.Instance().Storage[User.Identity.Name] = auc;
                    return RedirectToAction("AuctionLoadingScreen", "Main");
                }
            }

        }
        else
        {
            return View(auc);
        }


    }
    [Authorize]
    [AuthRestriections(Name = "/Main/EditOrAddAuction")]
    public ActionResult AuctionLoadingScreen(Auctions target)
    {
        return View(target);
    }
    [Authorize]
    [AuthRestriections(Name = "/Main/EditOrAddAuction")]
    public ActionResult EAuctionLoadingScreen(Auctions target)
    {
        return View(target);
    }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddClusetr")]
        public ActionResult EditClusetr(int? id)
        {

            if (id == null)
            {
                return View("EditClusetr");
            }
            using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
            {
                Clusetrs clu = entity.Clusetrs.Where(x => x.ID == id).First();
                if (clu == null)
                {
                    return View("EditClusetr");
                }
                else
                {
                    return View("EditClusetr", clu);
                }



            }
        }


        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddClusetr")]
        [HttpPost]
        public ActionResult EditClusetr(Clusetrs clu)
        {

            if (ModelState.IsValid)
            {
                SingletonCache.Instance().Storage[User.Identity.Name] = clu;
                return RedirectToAction("EClusetrLoadingScreen", "Main");

            }
            else
            {
                return View(clu);
            }



        }

        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddClusetr")]
        public ActionResult AddClusetr()
        {


            return View();

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddClusetr")]
        [HttpPost]
        public ActionResult AddClusetr(Clusetrs clu)
        {

            if (ModelState.IsValid)
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {
                    int cou = entity.Clusetrs.Count(x => x.DisplayNumber == clu.DisplayNumber);
                    if (cou != 0)
                    {
                        ModelState.AddModelError("Exist", "ספק בעל מספר זה כבר קיים.");
                        return View(clu);
                    }
                    else
                    {
                        SingletonCache.Instance().Storage[User.Identity.Name] = clu;
                        return RedirectToAction("ClusetrLoadingScreen", "Main");
                    }
                }

            }
            else
            {
                return View(clu);
            }


        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddClusetr")]
        public ActionResult ClusetrLoadingScreen(Clusetrs target)
        {
            return View(target);
        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddClusetr")]
        public ActionResult EClusetrLoadingScreen(Clusetrs target)
        {
            return View(target);
        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddUnit")]
        public ActionResult EditUnit(int? id)
        {

            if (id == null)
            {
                return View("EditUnit");
            }
            using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
            {
                Units uni = entity.Units.Where(x => x.ID == id).First();
                if (uni == null)
                {
                    return View("EditUnit");
                }
                else
                {
                    List<int?> fl=new List<int?>();
                    foreach (UnitsAuctions ua in uni.UnitsAuctions)
                    {
                        fl.Add(ua.AuctionID);
                    }
                    UnitFModel fm = new UnitFModel();
                    fm.unit = uni;
                    fm.Limitions = fl;
                            
                    return View("EditUnit", fm);
                }



            }
        }


        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddUnit")]
        [HttpPost]
        public ActionResult EditUnit(UnitFModel uni)
        {

            if (ModelState.IsValid)
            {
                SingletonCache.Instance().Storage[User.Identity.Name] = uni;
                return RedirectToAction("EUnitLoadingScreen", "Main");

            }
            else
            {
                return View(uni);
            }



        }

        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddUnit")]
        public ActionResult AddUnit()
        {


            return View();

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddUnit")]
        [HttpPost]
        public ActionResult AddUnit(UnitFModel uni)
        {

            if (ModelState.IsValid)
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {
                    int cou = entity.Units.Count(x => x.Name == uni.unit.Name);
                    if (cou != 0)
                    {
                        ModelState.AddModelError("Exist", "יחידה בעלת מספר זה כבר קיים.");
                        return View(uni);
                    }
                    else
                    {
                        SingletonCache.Instance().Storage[User.Identity.Name] = uni;
                        return RedirectToAction("UnitLoadingScreen", "Main");
                    }
                }

            }
            else
            {
                return View(uni);
            }


        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddUnit")]
        public ActionResult UnitLoadingScreen(UnitFModel target)
        {
            return View(target);
        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddUnit")]
        public ActionResult EUnitLoadingScreen(UnitFModel target)
        {
            return View(target);
        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddSupplier")]
        public ActionResult EditSupplier(int? id)
        {

            if (id == null)
            {
                return View("EditSupplier");
            }
            using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
            {
                Suppliers sup = entity.Suppliers.Where(x => x.ID == id).First();
                if (sup == null)
                {
                    return View("EditSupplier");
                }
                else
                {

                    SupplierFModel fm = new SupplierFModel();
                    fm.supliers = sup;
                    List<int?> fl = new List<int?>();
                    foreach (SuppliersClusetrs ua in sup.SuppliersClusetrs)
                    {
                        if(ua.Statuses.ID==1)
                        fl.Add(ua.ClusetrID);
                    }
                   
                   
                    fm.Limitions = fl;
                    return View("EditSupplier", fm);
                }



            }
        }


        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddSupplier")]
        [HttpPost]
        public ActionResult EditSupplier(SupplierFModel sup)
        {

            if (ModelState.IsValid)
            {
                SingletonCache.Instance().Storage[User.Identity.Name] = sup;
                return RedirectToAction("ESupplierLoadingScreen", "Main");

            }
            else
            {
                return View(sup);
            }



        }

        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddSupplier")]
        public ActionResult AddSupplier()
        {


            return View();

        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddSupplier")]
        [HttpPost]
        public ActionResult AddSupplier(SupplierFModel sup)
        {

            if (ModelState.IsValid)
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {
                    int cou = entity.Suppliers.Count(x => x.Name == sup.supliers.Name);
                    if (cou != 0)
                    {
                        ModelState.AddModelError("Exist", "יחידה בעלת מספר זה כבר קיים.");
                        return View(sup);
                    }
                    else
                    {
                        SingletonCache.Instance().Storage[User.Identity.Name] = sup;
                        return RedirectToAction("SupplierLoadingScreen", "Main");
                    }
                }

            }
            else
            {
                return View(sup);
            }


        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddSupplier")]
        public ActionResult SupplierLoadingScreen(SupplierFModel target)
        {
            return View(target);
        }
        [Authorize]
        [AuthRestriections(Name = "/Main/EditOrAddSupplier")]
        public ActionResult ESupplierLoadingScreen(SupplierFModel target)
        {
            return View(target);
        }
    }
}