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
using System.Text.RegularExpressions;
using WebApplication1.AlgoTimchur;

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
        [Authorize]
        [HttpPost]
        [AuthRestriections(Name = "/Main/TichurSuppCreate")]
        public ActionResult GetCluByAuc(int auctionId)
        {
            using (TimchurDatabaseEntities ent = new TimchurDatabaseEntities())
            {
                
                List<SelectListItem> li = new List<SelectListItem>();
                foreach(Clusetrs clu in ent.Clusetrs.Where(x => x.AuctionID == auctionId && x.StatusID==1).OrderBy(x=>x.DisplayNumber))
                {
                    li.Add(new SelectListItem() { Value = clu.ID.ToString(), Text = clu.DisplayNumber+"-"+clu.Name });
                }
                return Json(li);
            }
           
        }
        public class TNData
        {
            public string id { get; set; }
            public string unit_field { get; set; }
        }
        [Authorize]
        [HttpPost]
        [AuthRestriections(Name = "/Main/TichurExisting")]
        public ActionResult GetByTichurNumber(TNData data)
        {
           
            using (TimchurDatabaseEntities ent = new TimchurDatabaseEntities())
            {

                Users use = ent.Users.Where(x => x.IDCardNumber == User.Identity.Name).First();
                bool f = User.IsInRole("Admin");
                string uni_fin = User.IsInRole("Admin") ? data.unit_field : use.UnitID.ToString();
                int tem = 0;
                if (uni_fin!=null)
               tem= Int32.Parse(uni_fin);
                if (tem==0 || ent.Tichurim.Where(x => x.UnitID == tem && x.TichurNumber == data.id && (f || x.StatusID == 1)).Count()==0)
                {
                    GenModel failRes = new GenModel();
                    failRes.data = new List<string[]>();
                    failRes.data.Add(new string[] { "תיחור אינו קיים בתוך השרת(או תחת היחידה הנתונה של המשתמש או שהוכנסה)" });
                    failRes.Status = "error";
                    return Json(failRes); 
                }
              
                if (!User.IsInRole("User"))
                {
                    if (data.unit_field == null)
                    {
                        GenModel failRes = new GenModel();
                        failRes.data = new List<string[]>();
                        failRes.data.Add(new string[] { "אנא בחר ערך בשדה יחידה" });
                        failRes.Status = "error";
                        return Json(failRes);
                    }
                }
               
                Tichurim tic = ent.Tichurim.Where(x => x.UnitID == tem && x.TichurNumber == data.id && (f || x.StatusID == 1)).First();
                if (ent.UnitsAuctions.Where(x => x.UnitID==tem && x.AuctionID==tic.Clusetrs.AuctionID).Count()>0)
                {
                    if (User.IsInRole("User"))
                    {
                        Cache.gen_lock.WaitOne();
                        ent.Tichurim.Where(x => x.UnitID == tem && x.TichurNumber == data.id &&  x.StatusID == 1).First().DateTimeSelected = DateTime.Now;
                        ent.SaveChanges();
                        Cache.gen_lock.ReleaseMutex();
                    }
                    tic = ent.Tichurim.Where(x => x.UnitID == tem && x.TichurNumber == data.id && (f || x.StatusID == 1)).First();
                    GenModel Res = new GenModel();
                    Res.Status = "K";
                    Res.data = new List<string[]>();
                    Res.data.Add(new string[] { "מספר שורה", "מס' בתוצאת שליפה", "יחידה", "מס' מכרז", "שם מכרז", "תיחור", "מספר סל", "שם סל", "שם ספק", "ח.פ", "איש קשר", "אימייל", "טלפון", "תאריך ושעה" });
                    int i = 0;
                    foreach (SuppliersTichurim st in tic.SuppliersTichurim)
                    {
                        Suppliers sup = ent.Suppliers.Where(x => x.ID == st.SupplierID).First();
                        i++;
                        Res.data.Add(new string[] { i.ToString(), st.PositionInList.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, sup.Name, sup.CompanyNumber, sup.ContactName, sup.EmailAddress, sup.PhoneNumber, tic.DateTimeCreated.Value.ToString() });


                    }
                    return Json(Res);
                }
                else
                {
                    GenModel failRes = new GenModel();
                    failRes.data = new List<string[]>();
                    failRes.data.Add(new string[] { "משתמש אינו רשאי לצפות בתיחור" });
                    failRes.Status = "error";
                    return Json(failRes);
                }
              
            }
                
        }
        public class TEBDData
        {
            public string From { get; set; }
            public string To { get; set; }
            public string unit_field { get; set; }
            public string clu_field { get; set; }
            public string auc_field { get; set; }
        }
        [Authorize]
        [HttpPost]
        [AuthRestriections(Name = "/Main/TichurExisting")]
        public ActionResult GetByTichurDates(TEBDData data)
        {
        
            DateTime to;
            string[] to_str = data.To.Split('/');
            to = new DateTime(Int32.Parse(to_str[2]), Int32.Parse(to_str[0]), Int32.Parse(to_str[1]));
            DateTime from;
            string[] from_str = data.From.Split('/');
            from = new DateTime(Int32.Parse(from_str[2]), Int32.Parse(from_str[0]), Int32.Parse(from_str[1]));
            if (from>to)
            {
                GenModel failRes = new GenModel();
                failRes.data = new List<string[]>();
                failRes.data.Add(new string[] { "טווח תאריכים אינו תקין" });
                failRes.Status = "error";
                return Json(failRes);
            }

            using (TimchurDatabaseEntities ent = new TimchurDatabaseEntities())
            {
                GenModel Res = new GenModel();
                Users use = ent.Users.Where(x => x.IDCardNumber == User.Identity.Name).First();
                bool f = User.IsInRole("Admin");
                string uni_fin = User.IsInRole("Admin") ? data.unit_field : use.UnitID.ToString();
                int tem = 0;
                if (uni_fin!=null)
                tem = Int32.Parse(uni_fin);
                int i;
                if (User.IsInRole("User"))
                {
                    if (data.clu_field == null && data.auc_field == null)
                    {
                       Res = new GenModel();
                        Res.Status = "K";
                        Res.data = new List<string[]>();
                        Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                        i = 0;
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.UnitID==tem && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, tic.DateTimeCreated.Value.ToString(), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }
                    else if (data.clu_field == null)
                    {
                        Res = new GenModel();
                        Res.Status = "K";
                        Res.data = new List<string[]>();
                        Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                        i = 0;
                        int al = Int32.Parse(data.auc_field);
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.Clusetrs.AuctionID== al && x.UnitID == tem && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x =>x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, tic.DateTimeCreated.Value.ToString(), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }
                    else if (data.auc_field == null)
                    {
                        Res = new GenModel();
                        Res.Status = "K";
                        Res.data = new List<string[]>();
                        Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                        i = 0;
                        int cl = Int32.Parse(data.clu_field);
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.ClusterID == cl && x.UnitID == tem && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, tic.DateTimeCreated.Value.ToString(), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }
                    else
                    {
                        Res = new GenModel();
                        Res.Status = "K";
                        Res.data = new List<string[]>();
                        Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                        i = 0;
                        int al = Int32.Parse(data.auc_field);
                        int cl = Int32.Parse(data.clu_field);
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.Clusetrs.AuctionID == al && x.ClusterID == cl && x.UnitID == tem && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, tic.DateTimeCreated.Value.ToString(), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }
                }
                if (data.unit_field == null)
                {
                    if (data.clu_field == null && data.auc_field == null)
                    {
                        Res = new GenModel();
                        Res.Status = "K";
                        Res.data = new List<string[]>();
                        Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                        i = 0;
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, tic.DateTimeCreated.Value.ToString(), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }
                    else if (data.clu_field == null)
                    {
                        Res = new GenModel();
                        Res.Status = "K";
                        Res.data = new List<string[]>();
                        Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                        i = 0;
                        int al = Int32.Parse(data.auc_field);
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.Clusetrs.AuctionID == al && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, tic.DateTimeCreated.Value.ToString(), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }
                    else if (data.auc_field == null)
                    {
                        Res = new GenModel();
                        Res.Status = "K";
                        Res.data = new List<string[]>();
                        Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                        i = 0;
                        int cl = Int32.Parse(data.clu_field);
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.ClusterID == cl && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, tic.DateTimeCreated.Value.ToString(), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }
                    else
                    {
                        Res = new GenModel();
                        Res.Status = "K";
                        Res.data = new List<string[]>();
                        Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                        i = 0;
                        int al = Int32.Parse(data.auc_field);
                        int cl = Int32.Parse(data.clu_field);
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.Clusetrs.AuctionID == al && x.ClusterID == cl && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, tic.DateTimeCreated.Value.ToString(), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }
                }
                else
                {
                    if (data.clu_field == null && data.auc_field == null)
                    {
                        Res = new GenModel();
                        Res.Status = "K";
                        Res.data = new List<string[]>();
                        Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                        i = 0;
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.UnitID == tem && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, tic.DateTimeCreated.Value.ToString(), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }
                    else if (data.clu_field == null)
                    {
                        Res = new GenModel();
                        Res.Status = "K";
                        Res.data = new List<string[]>();
                        Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                        i = 0;
                        int al = Int32.Parse(data.auc_field);
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.Clusetrs.AuctionID == al && x.UnitID == tem && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, tic.DateTimeCreated.Value.ToString(), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }
                    else if (data.auc_field == null)
                    {
                        Res = new GenModel();
                        Res.Status = "K";
                        Res.data = new List<string[]>();
                        Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                        i = 0;
                        int cl = Int32.Parse(data.clu_field);
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.ClusterID == cl && x.UnitID == tem && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, tic.DateTimeCreated.Value.ToString(), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }
                    else
                    {
                        Res = new GenModel();
                        Res.Status = "K";
                        Res.data = new List<string[]>();
                        Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                        i = 0;
                        int al = Int32.Parse(data.auc_field);
                        int cl = Int32.Parse(data.clu_field);
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.Clusetrs.AuctionID == al && x.ClusterID == cl && x.UnitID == tem && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusetrs.Auctions.AuctionNumber, tic.Clusetrs.Auctions.Name, tic.TichurNumber, tic.Clusetrs.DisplayNumber.Value.ToString(), tic.Clusetrs.Name, tic.DateTimeCreated.Value.ToString(), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }

                }




              
                    
              

            }
            return Json("");
        }
        public class TCanData
        {
            public string id { get; set; }
            public string comment { get; set; }
        }
        [Authorize]
        [HttpPost]
        [AuthRestriections(Name = "/Main/TichurCancel")]
        public ActionResult CancelTichur(TCanData can)
        {
            try
            {
                using (TimchurDatabaseEntities ent = new TimchurDatabaseEntities())
                {
                    Cache.gen_lock.WaitOne();
                    int cid = Int32.Parse(can.id);
                    Tichurim tic = ent.Tichurim.Where(x => x.ID == cid).First();
                    foreach (SuppliersTichurim sti in tic.SuppliersTichurim)
                    {
                        if (sti.Suppliers.SuppliersClusetrs.Where(x2 => x2.ClusetrID == tic.ClusterID).First().LastTimeInList == tic.DateTimeCreated)
                        {
                            sti.Suppliers.SuppliersClusetrs.Where(x2 => x2.ClusetrID == tic.ClusterID).First().LastTimeInList = sti.Suppliers.SuppliersClusetrs.Where(x2 => x2.ClusetrID == tic.ClusterID).First().FormarLastTimeInList;

                        }
                    }
                    tic.UpdatedUserID = ent.Users.Where(x => x.IDCardNumber == User.Identity.Name).First().ID;
                    tic.DateTimeUpdated = DateTime.Now;
                    tic.StatusID = 2;
                    tic.UpdatedComment = can.comment;
                    ent.SaveChanges();
                    Cache.gen_lock.ReleaseMutex();
                    return Json("ביטול תיחור הצליח");
                }
            }
            catch(Exception e)
            {
                return Json("ביטול תיחור נכשל");
            } 
        }
        public class TichurNe
        {
            public string unit_id { get; set; }
            public string clu_id { get; set; }
            public string auc_id { get; set; }
            public string tichur_id { get; set; }
        }
        [Authorize]
        [HttpPost]
        [AuthRestriections(Name = "/Main/TichurSuppCreate")]
        public ActionResult GetTichurProcc(TichurNe NI)
        {
            if (NI.unit_id == null || NI.auc_id==null || NI.clu_id==null)
            {
                EndResultTichur res = new EndResultTichur();
                res.Status = "error";
                res.data = new List<string[]>();
                res.data.Add(new string[] { "אנא מלא את כל הפרטים המתאימים" });
                return Json(res);
            }
            else
            {
                TichurInfo inf=new TichurInfo();
                inf.AuctionID = Int32.Parse(NI.auc_id);
                inf.CluestrID = Int32.Parse(NI.clu_id);
                inf.UnitID = Int32.Parse(NI.unit_id);
                inf.TichurNumber = NI.tichur_id;
                TichurAlgo ta = new TichurAlgo();
                EndResultTichur er = ta.Execute(inf, User.Identity.Name);
                return Json(er);
            }
        }
    }
}