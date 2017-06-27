using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Database;
using WebApplication1.Infrastructure.AuthAbstract;
using WebApplication1.Models;
using System.Web.UI.WebControls;
using System.Data;
using WebApplication1.AlgoTimchur;

/// <summary>
/// Controller for the application's primary function.
/// </summary>
namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
        private TimchurDatabaseEntities db = new TimchurDatabaseEntities();
        [Authorize]
        [AuthRestrictions(Name = "/Main/ApproveMsg")]
        public ActionResult ApproveMsg()
        {
            return View();
        }
        // GET: Main
        [Authorize]
        [AuthRestrictions(Name = "/Main/MainIndex")]
        public ActionResult MainIndex()
        {
            return View();
        }
        /// <summary>
        /// GET: Create new supplier page
        /// </summary>
        /// <returns>ViewResult of the given page</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/TichurSuppCreate")]
        public ActionResult TichurSuppCreate()
        {

            return View();

        }
        /// <summary>
        /// GET: Existing Tichurim
        /// </summary>
        /// <returns>ViewResult of the given page</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/TichurExisting")]
        public ActionResult TichurExisting()
        {
            return View();
        }
        /// <summary>
        /// POST: User management page
        /// </summary>
        /// <param name="user">The user to be managed</param>
        /// <returns>ViewResult of the given page</returns>
        [HttpPost]
        [Authorize]
        [AuthRestrictions(Name = "/Main/MangUsers")]
        public ActionResult MangUsers(Users user)
        {
            db = new TimchurDatabaseEntities();
            List<Users> li = db.Users.OrderBy(s => s.IDCardNumber).ToList();

            /** load from database user list **/
            /** transform loaded data into model fit for edit + present **/

            return View(li);

        }
        /// <summary>
        /// User management page, full list
        /// </summary>
        /// <returns>ViewResult of the given page</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/MangUsers")]
        public ActionResult MangUsers()
        {
            db = new TimchurDatabaseEntities();
            List<Users> li = db.Users.OrderBy(s => s.IDCardNumber).ToList();

            /** load from database user list **/
            /** transform loaded data into model fit for edit + present **/

            return View(li);

        }
        /// <summary>
        /// Suppliers management page
        /// </summary>
        /// <returns>ViewResult of the given page</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/MangSuppliers")]
        public ActionResult MangSuppliers()
        {
            db = new TimchurDatabaseEntities();
            List<Suppliers> li = db.Suppliers.OrderBy(s => s.Name).OrderBy(s=> s.SuppliersClusters.FirstOrDefault().Clusters.Auctions.AuctionNumber).OrderBy(s => s.SuppliersClusters.FirstOrDefault().Clusters.DisplayNumber).ToList();

            /** load from database user list **/
            /** transform loaded data into model fit for edit + present **/

            return View(li);
        }
        /// <summary>
        /// Auctions management page
        /// </summary>
        /// <returns>ViewResult of the given page</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/MangAuctions")]
        public ActionResult MangAuctions()
        {
            db = new TimchurDatabaseEntities();
            List<Auctions> li = db.Auctions.OrderBy(s => s.AuctionNumber).ToList();

            /** load from database user list **/
            /** transform loaded data into model fit for edit + present **/

            return View(li);

        }
        /// <summary>
        /// Cluster management page
        /// </summary>
        /// <returns>ViewResult of the given page</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/MangClusters")]
        public ActionResult MangClusters()
        {

            db = new TimchurDatabaseEntities();
            List<Clusters> li = db.Clusters.OrderBy(s => s.DisplayNumber).ToList();

            /** load from database user list **/
            /** transform loaded data into model fit for edit + present **/

            return View(li);

        }
        /// <summary>
        /// Units management page
        /// </summary>
        /// <returns>ViewResult of the given page</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/MangUnits")]
        public ActionResult MangUnits()
        {

            db = new TimchurDatabaseEntities();
            List<Units> li = db.Units.OrderBy(s => s.ID).ToList();

            /** load from database user list **/
            /** transform loaded data into model fit for edit + present **/

            return View(li);

        }
        /// <summary>
        /// User unauthorized to perform action ViewResult
        /// </summary>
        /// <returns>ViewResult of the given page</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/UnAuthError")]
        public ActionResult UnAuthError()
        {
            return View();
        }
        /// <summary>
        /// User edit by ID
        /// </summary>
        /// <param name="id">ID of the user at hand.</param>
        /// <returns>ViewResult of the given page</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddUser")]
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
        /// <summary>
        /// POST: Edit existing user
        /// </summary>
        /// <param name="use">The user edited</param>
        /// <returns>ViewResult of the given page</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddUser")]
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
        /// <summary>
        /// Adds a mew user to the database.
        /// </summary>
        /// <returns>ViewResult of the given page.</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddUser")]
        public ActionResult AddUser()
        {
                    return View();
        }
        /// <summary>
        /// Adds a mew user to the database.
        /// </summary>
        /// <param name="use">The user we wish to add.</param>
        /// <returns>ViewResult depending on the result of the query.</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddUser")]
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
        /// <summary>
        /// User creation loading screen
        /// </summary>
        /// <param name="target">The user modification which sent the query.</param>
        /// <returns>The view relevant to the given user.</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddUser")]
        public ActionResult UserLoadingScreen(Users target)
        {
            return View(target);
        }
        /// <summary>
        /// User edit loading screen
        /// </summary>
        /// <param name="target">The user modification which sent the query.</param>
        /// <returns>The view relevant to the given user.</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddUser")]
        public ActionResult EUserLoadingScreen(Users target)
        {
            return View(target);
        }
        /// <summary>
        /// Edit auction view.
        /// </summary>
        /// <param name="id">ID of the auction to be edited.</param>
        /// <returns>ViewResult of the given page.</returns>
        [Authorize]
    [AuthRestrictions(Name = "/Main/EditOrAddAuction")]
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
        /// <summary>
        /// Edit auction view.
        /// </summary>
        /// <param name="auc">Object representation of said auction.</param>
        /// <returns>ViewResult of the given page.</returns>
        [Authorize]
    [AuthRestrictions(Name = "/Main/EditOrAddAuction")]
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
        /// <summary>
        /// Add auction page.
        /// </summary>
        /// <returns>ViewResult of the given page.</returns>
        [Authorize]
    [AuthRestrictions(Name = "/Main/EditOrAddAuction")]
    public ActionResult AddAuction()
    {
        return View();
    }
        /// <summary>
        /// POST: Add new auction.
        /// </summary>
        /// <param name="auc">The auction we wish to add.</param>
        /// <returns>ViewResult depending of the result of the query..</returns>
        [Authorize]
    [AuthRestrictions(Name = "/Main/EditOrAddAuction")]
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
        /// <summary>
        /// Auction loading screen view
        /// </summary>
        /// <param name="target">The auction sent which directed to the page.</param>
        /// <returns>ViewResult of the given page.</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddAuction")]
        public ActionResult AuctionLoadingScreen(Auctions target)
        {
            return View(target);
        }
        /// <summary>
        /// Auction edit loading screen view.
        /// </summary>
        /// <param name="target">The auction sent which directed to this page.</param>
        /// <returns>ViewResult of the given page.</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddAuction")]
        public ActionResult EAuctionLoadingScreen(Auctions target)
        {
            return View(target);
        }
        /// <summary>
        /// Edit cluster view.
        /// </summary>
        /// <param name="id">ID of the cluster wish to edit.</param>
        /// <returns>ViewResult of the given page.</returns>
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddCluster")]
        public ActionResult EditCluster(int? id)
        {
            if (id == null)
            {
                return View("EditCluster");
            }
            using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
            {
                Clusters clu = entity.Clusters.Where(x => x.ID == id).First();
                if (clu == null)
                {
                    return View("EditCluster");
                }
                else
                {
                    return View("EditCluster", clu);
                }
            }
        }

        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddCluster")]
        [HttpPost]
        public ActionResult EditCluster(Clusters clu)
        {
            if (ModelState.IsValid)
            {
                SingletonCache.Instance().Storage[User.Identity.Name] = clu;
                return RedirectToAction("EClusterLoadingScreen", "Main");
            }
            else
            {
                return View(clu);
            }
        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddCluster")]
        public ActionResult AddCluster()
        {
            return View();
        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddCluster")]
        [HttpPost]
        public ActionResult AddCluster(Clusters clu)
        {

            if (ModelState.IsValid)
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {
                    int cou = entity.Clusters.Count(x => x.DisplayNumber == clu.DisplayNumber);
                    if (cou != 0)
                    {
                        ModelState.AddModelError("Exist", "ספק בעל מספר זה כבר קיים.");
                        return View(clu);
                    }
                    else
                    {
                        SingletonCache.Instance().Storage[User.Identity.Name] = clu;
                        return RedirectToAction("ClusterLoadingScreen", "Main");
                    }
                }

            }
            else
            {
                return View(clu);
            }


        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddCluster")]
        public ActionResult ClusterLoadingScreen(Clusters target)
        {
            return View(target);
        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddCluster")]
        public ActionResult EClusterLoadingScreen(Clusters target)
        {
            return View(target);
        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddUnit")]
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
        [AuthRestrictions(Name = "/Main/EditOrAddUnit")]
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
        [AuthRestrictions(Name = "/Main/EditOrAddUnit")]
        public ActionResult AddUnit()
        {


            return View();

        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddUnit")]
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
        [AuthRestrictions(Name = "/Main/EditOrAddUnit")]
        public ActionResult UnitLoadingScreen(UnitFModel target)
        {
            return View(target);
        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddUnit")]
        public ActionResult EUnitLoadingScreen(UnitFModel target)
        {
            return View(target);
        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddSupplier")]
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
                    foreach (SuppliersClusters ua in sup.SuppliersClusters)
                    {
                        if(ua.Statuses.ID==1)
                        fl.Add(ua.ClusterID);
                    }
                    if(sup.EmailAddress!=null)
                    {
                        fm.ActualEmail = sup.EmailAddress;
                    }
                   
                    fm.Limitions = fl;
                    return View("EditSupplier", fm);
                }



            }
        }


        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddSupplier")]
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
        [AuthRestrictions(Name = "/Main/EditOrAddSupplier")]
        public ActionResult AddSupplier()
        {


            return View();

        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddSupplier")]
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
        [AuthRestrictions(Name = "/Main/EditOrAddSupplier")]
        public ActionResult SupplierLoadingScreen(SupplierFModel target)
        {
            return View(target);
        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/EditOrAddSupplier")]
        public ActionResult ESupplierLoadingScreen(SupplierFModel target)
        {
            return View(target);
        }
        [Authorize]
        [HttpPost]
        [AuthRestrictions(Name = "/Main/TichurSuppCreate")]
        public ActionResult GetCluByAuc(int auctionId)
        {
            using (TimchurDatabaseEntities ent = new TimchurDatabaseEntities())
            {
                
                List<SelectListItem> li = new List<SelectListItem>();
                foreach(Clusters clu in ent.Clusters.Where(x => x.AuctionID == auctionId && x.StatusID==1).OrderBy(x=>x.DisplayNumber))
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
        [AuthRestrictions(Name = "/Main/TichurExisting")]
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
                    failRes.data.Add(new string[] { "תיחור אינו קיים ביחידה של המשתמש" });
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
                if (ent.UnitsAuctions.Where(x => x.UnitID==tem && x.AuctionID==tic.Clusters.AuctionID).Count()>0 || f)
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
                        Res.data.Add(new string[] { i.ToString(), st.PositionInList.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, sup.Name, sup.CompanyNumber, sup.ContactName, sup.EmailAddress, sup.PhoneNumber, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss") });


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
        [AuthRestrictions(Name = "/Main/TichurExisting")]
        public ActionResult GetByTichurAll()
        {
            GenModel Res;
            using (TimchurDatabaseEntities ent = new TimchurDatabaseEntities())
            {
                Res = new GenModel();
                Res.Status = "K";
                Res.data = new List<string[]>();
                Res.data.Add(new string[] { "", "", "", "", "", "", "", "", "" });
                int i = 0;
                foreach (Tichurim tic in ent.Tichurim)
                {

                    Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name  });
                    i++;

                }
            }
            return Json(Res);
        }
        [Authorize]
        [HttpPost]
        [AuthRestrictions(Name = "/Main/TichurExisting")]
        public ActionResult GetByTichurDates(TEBDData data)
        {
            DateTime to;
            string[] to_str = data.To.Split('/');
            to = new DateTime(Int32.Parse(to_str[2]), Int32.Parse(to_str[0]), Int32.Parse(to_str[1]));
            to=to.AddMinutes(59.0);
            to=to.AddHours(23.0);
            to=to.AddSeconds(59.0);
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
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.UnitID==tem && x.DateTimeCreated <= to && x.DateTimeCreated >= from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name });
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
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.Clusters.AuctionID== al && x.UnitID == tem && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x =>x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name });
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

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name });
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
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.Clusters.AuctionID == al && x.ClusterID == cl && x.UnitID == tem && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name });
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

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name });
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
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.Clusters.AuctionID == al && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name });
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

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name });
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
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.Clusters.AuctionID == al && x.ClusterID == cl && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name });
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

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name });
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
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.Clusters.AuctionID == al && x.UnitID == tem && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name });
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

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name });
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
                        foreach (Tichurim tic in ent.Tichurim.Where(x => (f || x.StatusID == 1) && x.Clusters.AuctionID == al && x.ClusterID == cl && x.UnitID == tem && x.DateTimeCreated < to && x.DateTimeCreated > from).OrderBy(x => x.DateTimeCreated))
                        {

                            Res.data.Add(new string[] { i.ToString(), tic.Units.Name, tic.Clusters.Auctions.AuctionNumber, tic.Clusters.Auctions.Name, tic.TichurNumber, tic.Clusters.DisplayNumber.Value.ToString(), tic.Clusters.Name, tic.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss"), tic.Statuses.Name });
                            i++;

                        }
                        return Json(Res);
                    }

                }
            }
            //return Json("");
        }
        public class TCanData
        {
            public string id { get; set; }
            public string comment { get; set; }
        }
        [Authorize]
        [HttpPost]
        [AuthRestrictions(Name = "/Main/TichurCancel")]
        public ActionResult CancelTichur(TCanData can)
        {
            Cache.gen_lock.WaitOne();
            try
            {
                using (TimchurDatabaseEntities ent = new TimchurDatabaseEntities())
                {
                   
                
                    Tichurim tic = ent.Tichurim.Where(x => x.TichurNumber == can.id).FirstOrDefault();
                    foreach (SuppliersTichurim sti in tic.SuppliersTichurim)
                    {
                        if (sti.Suppliers.SuppliersClusters.Where(x2 => x2.ClusterID == tic.ClusterID).First().LastTimeInList == tic.DateTimeCreated)
                        {
                            sti.Suppliers.SuppliersClusters.Where(x2 => x2.ClusterID == tic.ClusterID).First().LastTimeInList = sti.Suppliers.SuppliersClusters.Where(x2 => x2.ClusterID == tic.ClusterID).First().FormarLastTimeInList;

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
                Cache.gen_lock.ReleaseMutex();
                System.Diagnostics.Trace.Write(e.ToString());
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
        [AuthRestrictions(Name = "/Main/TichurSuppCreate")]
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
                inf.ClusterID = Int32.Parse(NI.clu_id);
                inf.UnitID = Int32.Parse(NI.unit_id);
                inf.TichurNumber = NI.tichur_id;
                TichurAlgo ta = new TichurAlgo();
                EndResultTichur er = ta.Execute(inf, User.Identity.Name);
                return Json(er);
            }
        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/UnAuthError")]
        public ActionResult Faq()
        {
            return View("Faq");
        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/Stats")]
        public ActionResult Stats()
        {
            StatsModel mode = new StatsModel();
            return View(mode);
        }
        [HttpPost]
        [Authorize]
        [AuthRestrictions(Name = "/Main/Stats")]
        public ActionResult Stats(StatsModel mod)
        {
            if(mod.cl_id==null || mod.ul_id==null)
            {
                ModelState.AddModelError("Null", "אנא הכנס ערכים");
                return View();
            }
            int u_id = Int32.Parse(mod.ul_id);
            int c_id = Int32.Parse(mod.cl_id);
            StatsModel stat = new StatsModel();
            stat.SupplierInTichur = new SortedDictionary<int, List<int>>();
            stat.SupplierName = new SortedDictionary<int, string>();
            stat.TichurName = new SortedDictionary<int, string>();
            int tn = 0;
            int sn = 0;
            using (TimchurDatabaseEntities ent = new TimchurDatabaseEntities())
            {
                foreach(Tichurim tic in ent.Tichurim.Where(x => x.ClusterID == c_id && x.UnitID== u_id).OrderBy(m=>m.DateTimeCreated))
                {
                    tn++;
                    stat.TichurName.Add(tn, tic.TichurNumber);
                    stat.SupplierInTichur[tn] = new List<int>();
                    foreach(SuppliersTichurim st in tic.SuppliersTichurim)
                    {
                        int ckey = -1;
                        foreach(int tk in stat.SupplierName.Keys)
                        {
                            if(stat.SupplierName[tk]==(st.Suppliers.CompanyNumber + "|" + st.Suppliers.Name))
                            {
                                ckey=tk;
                            }
                        }
                        if(ckey==-1)
                        {
                            stat.SupplierName.Add(sn, (st.Suppliers.CompanyNumber + "|" + st.Suppliers.Name));
                            ckey = sn;
                            sn++;
                        }
                        stat.SupplierInTichur[tn].Add(ckey);
                    }
                }
                if(tn==0)
                {
                    ModelState.AddModelError("Empty", "לא קיימים תחורים תחת היחידה והסל");
                    return View();
                }
            }
            return View(stat);
        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/ConSupToAuctions")]
        public ActionResult GetTableSupToAuctions()
        {
            String[][] table=null;
            using (TimchurDatabaseEntities ent = new TimchurDatabaseEntities())
            {
                table = new String[ent.Auctions.Count()+1][];

                List<Clusters> li_cl = ent.Clusters.ToList();
                List<Auctions> li_al = ent.Auctions.ToList();
                
                for (int i=0;i<ent.Auctions.Count()+1;i++)
                {
                    table[i] = new String[ent.Clusters.Count() + 1];
                    for (int j=0;j < ent.Clusters.Count() + 1; j++)
                    {
                        
                        if(i==0 && j==0)
                        {
                            table[0][0] = "שם מכרז/שם סל";
                        }
                        else if(i==0)
                        {
                            table[i][j] = (li_cl.ElementAt(j-1).ID + "<=>" + li_cl.ElementAt(j-1).Name);
                        }
                        else if(j==0)
                        {
                            table[i][j] = (li_al.ElementAt(i-1).ID + "<=>" + li_al.ElementAt(i-1).Name);
                        }
                        else
                        {
                            if(li_al.ElementAt(i-1).ID== li_cl.ElementAt(j-1).AuctionID)
                            {
                                table[i][j] = "1";
                            }
                            else
                            {
                                table[i][j] = "0";
                            }
                        }
                        
                    }
                  
                }
               
            }
            if (table == null) return Json("");
                return Json(table,JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        [AuthRestrictions(Name = "/Main/ConSupToAuctions")]
        public ActionResult ConSupToAuctions()
        {
           
            

            
            return View();
        }
        [HttpPost]
        [Authorize]
        [AuthRestrictions(Name = "/Main/ConSupToAuctions")]
        public ActionResult ConnOpSupToAuctions(int auc_id, int clu_id)
        {
            Cache.gen_lock.WaitOne();
            using (TimchurDatabaseEntities ent=new TimchurDatabaseEntities())
            {
                var my_clu = ent.Clusters.Where(x => x.ID == clu_id);
               foreach(Clusters clu in my_clu)
                {
                    clu.AuctionID = auc_id;
                }
                ent.SaveChanges();
            }

                Cache.gen_lock.ReleaseMutex();
            return Json("Finished");
        }
    }
}

