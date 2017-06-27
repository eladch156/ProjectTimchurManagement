using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1
{
    public class NotificationsHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        public void AddUserOperation()
        {
            
            int? id = -1;
            Cache.gen_lock.WaitOne();
            try
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {
                    entity.Users.Add(((Users)SingletonCache.Instance().Storage[Context.User.Identity.Name]));
                    entity.SaveChanges();
                   
                }
                using (TimchurDatabaseEntities entity2 = new TimchurDatabaseEntities())
                {
                    string strm = ((Users)(SingletonCache.Instance().Storage[Context.User.Identity.Name])).IDCardNumber;
                    id = entity2.Users.Where(x => x.IDCardNumber == strm).First().ID;
                }
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, משתמש נוסף למערכת";
            }
            catch (Exception e)
            {
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, הוספת משתמש נכשלה";
                System.Diagnostics.Trace.Write(e.ToString());
            }
            SingletonCache.Instance().Storage[Context.User.Identity.Name] = null;
            Cache.gen_lock.ReleaseMutex();
            string str = Context.User.Identity.Name;
            string msg = "";
            if(SingletonCache.Instance().last_msg.Keys.Contains(str))
            {
                msg = SingletonCache.Instance().last_msg[str];
            }
            string to_s=string.Format("סטאטוס:" + msg);
            Clients.Caller.sendMessage(id.Value.ToString());
        }
        public void EditUserOperation()
        {

            int? id = -1;
            Cache.gen_lock.WaitOne();
            try
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {
                   
                    var original = entity.Users.Find(((Users)SingletonCache.Instance().Storage[Context.User.Identity.Name]).ID);

                    if (original != null)
                    {
                        entity.Entry(original).CurrentValues.SetValues(((Users)SingletonCache.Instance().Storage[Context.User.Identity.Name]));
                        entity.SaveChanges();
                    }
                   
                    entity.SaveChanges();
                   
                }
                using (TimchurDatabaseEntities entity2 = new TimchurDatabaseEntities())
                {
                    string strm = ((Users)(SingletonCache.Instance().Storage[Context.User.Identity.Name])).IDCardNumber;
                    id = entity2.Users.Where(x => x.IDCardNumber == strm).First().ID;
                }
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, משתמש עודכן במערכת";
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.ToString());
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, עדכון משתמש נכשל";
               
            }
            SingletonCache.Instance().Storage[Context.User.Identity.Name] = null;
            Cache.gen_lock.ReleaseMutex();
            string str = Context.User.Identity.Name;
            string msg = "";
            if (SingletonCache.Instance().last_msg.Keys.Contains(str))
            {
                msg = SingletonCache.Instance().last_msg[str];
            }
            string to_s = string.Format("סטאטוס:" + msg);
            Clients.Caller.sendMessage(id.Value.ToString());
        }
        public void AddAuctionOperation()
        {

            int? id = -1;
            Cache.gen_lock.WaitOne();
            try
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {
                    Auctions curr = ((Auctions)SingletonCache.Instance().Storage[Context.User.Identity.Name]);
                    entity.Auctions.Add(curr);
                    entity.SaveChanges();

                }
                using (TimchurDatabaseEntities entity2 = new TimchurDatabaseEntities())
                {
                    string strm = ((Auctions)(SingletonCache.Instance().Storage[Context.User.Identity.Name])).AuctionNumber;
                    id = entity2.Auctions.Where(x => x.AuctionNumber == strm).First().ID;
                }
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, מכרז נוסף למערכת";
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.ToString());
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, הוספת מכרז נכשלה";
                
            }
            SingletonCache.Instance().Storage[Context.User.Identity.Name] = null;
            Cache.gen_lock.ReleaseMutex();
            string str = Context.User.Identity.Name;
            string msg = "";
            if (SingletonCache.Instance().last_msg.Keys.Contains(str))
            {
                msg = SingletonCache.Instance().last_msg[str];
            }
            string to_s = string.Format("סטאטוס:" + msg);
            Clients.Caller.sendMessage(id.Value.ToString());
        }
        public void EditAuctionOperation()
        {

            int? id = -1;
            Cache.gen_lock.WaitOne();
            try
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {

                    var original = entity.Auctions.Find(((Auctions)SingletonCache.Instance().Storage[Context.User.Identity.Name]).ID);

                    if (original != null)
                    {
                        Auctions curr = ((Auctions)SingletonCache.Instance().Storage[Context.User.Identity.Name]);
                      
                        entity.Entry(original).CurrentValues.SetValues(((Auctions)SingletonCache.Instance().Storage[Context.User.Identity.Name]));
                        entity.SaveChanges();
                    }
                   
                    entity.SaveChanges();

                }
                using (TimchurDatabaseEntities entity2 = new TimchurDatabaseEntities())
                {
                    string strm = ((Auctions)(SingletonCache.Instance().Storage[Context.User.Identity.Name])).AuctionNumber;
                    id = entity2.Auctions.Where(x => x.AuctionNumber == strm).First().ID;
                }
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, מכרז עודכן במערכת";
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.ToString());
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, עדכון מכרז נכשל";
                
            }
            SingletonCache.Instance().Storage[Context.User.Identity.Name] = null;
            Cache.gen_lock.ReleaseMutex();
            string str = Context.User.Identity.Name;
            string msg = "";
            if (SingletonCache.Instance().last_msg.Keys.Contains(str))
            {
                msg = SingletonCache.Instance().last_msg[str];
            }
            string to_s = string.Format("סטאטוס:" + msg);
            Clients.Caller.sendMessage(id.Value.ToString());
        }
        public void AddClusterOperation()
        {

            int? id = -1;
            Cache.gen_lock.WaitOne();
            try
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {
                    entity.Clusters.Add(((Clusters)SingletonCache.Instance().Storage[Context.User.Identity.Name]));
                    entity.SaveChanges();

                }
                using (TimchurDatabaseEntities entity2 = new TimchurDatabaseEntities())
                {
                    byte strm = ((Clusters)(SingletonCache.Instance().Storage[Context.User.Identity.Name])).DisplayNumber.Value;
                    id = entity2.Clusters.Where(x => x.DisplayNumber == strm).First().ID;
                }
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, סל נוסף למערכת";
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.ToString());
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, הוספת סל נכשלה";
              
            }
            SingletonCache.Instance().Storage[Context.User.Identity.Name] = null;
            Cache.gen_lock.ReleaseMutex();
            string str = Context.User.Identity.Name;
            string msg = "";
            if (SingletonCache.Instance().last_msg.Keys.Contains(str))
            {
                msg = SingletonCache.Instance().last_msg[str];
            }
            string to_s = string.Format("סטאטוס:" + msg);
            Clients.Caller.sendMessage(id.Value.ToString());
        }
        public void EditClusterOperation()
        {

            int? id = -1;
            Cache.gen_lock.WaitOne();
            try
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {

                    var original = entity.Clusters.Find(((Clusters)SingletonCache.Instance().Storage[Context.User.Identity.Name]).ID);

                    if (original != null)
                    {
                        entity.Entry(original).CurrentValues.SetValues(((Clusters)SingletonCache.Instance().Storage[Context.User.Identity.Name]));
                        entity.SaveChanges();
                    }

                    entity.SaveChanges();

                }
                using (TimchurDatabaseEntities entity2 = new TimchurDatabaseEntities())
                {
                    byte strm = ((Clusters)(SingletonCache.Instance().Storage[Context.User.Identity.Name])).DisplayNumber.Value;
                    id = entity2.Clusters.Where(x => x.DisplayNumber == strm).First().ID;
                }
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, סל עודכן במערכת";
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.ToString());
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, עדכון סל נכשל";
                
            }
            SingletonCache.Instance().Storage[Context.User.Identity.Name] = null;
            Cache.gen_lock.ReleaseMutex();
            string str = Context.User.Identity.Name;
            string msg = "";
            if (SingletonCache.Instance().last_msg.Keys.Contains(str))
            {
                msg = SingletonCache.Instance().last_msg[str];
            }
            string to_s = string.Format("סטאטוס:" + msg);
            Clients.Caller.sendMessage(id.Value.ToString());
        }
        public void AddUnitOperation()
        {

            int? id = -1;
            Cache.gen_lock.WaitOne();
            UnitFModel mf = ((UnitFModel)SingletonCache.Instance().Storage[Context.User.Identity.Name]);
            try
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {
                    
                    entity.Units.Add(mf.unit);
                    entity.SaveChanges();

                }
                using (TimchurDatabaseEntities entity2 = new TimchurDatabaseEntities())
                {
                    string strm = mf.unit.Name;
                    id = entity2.Units.Where(x => x.Name == strm).First().ID;
                    if (mf.Limitions != null)
                    {
                        foreach (int i in mf.Limitions)
                        {
                            UnitsAuctions ua = new UnitsAuctions();
                            ua.AuctionID = i;
                            ua.UnitID = id;
                            entity2.UnitsAuctions.Add(ua);
                        }
                    }
                    entity2.SaveChanges();
                }
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, יחידה נוספה למערכת";
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.ToString());
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, הוספת יחידה נכשלה";
                
            }
            SingletonCache.Instance().Storage[Context.User.Identity.Name] = null;
            Cache.gen_lock.ReleaseMutex();
            string str = Context.User.Identity.Name;
            string msg = "";
            if (SingletonCache.Instance().last_msg.Keys.Contains(str))
            {
                msg = SingletonCache.Instance().last_msg[str];
            }
            string to_s = string.Format("סטאטוס:" + msg);
            Clients.Caller.sendMessage(id.Value.ToString());
        }
        public void editUnitOperation()
        {

            int? id = -1;
            Cache.gen_lock.WaitOne();
            UnitFModel mf = null;
            if (SingletonCache.Instance().Storage.ContainsKey(Context.User.Identity.Name))
            mf=((UnitFModel)SingletonCache.Instance().Storage[Context.User.Identity.Name]);
            try
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {

                    var original = entity.Units.Find(mf.unit.ID);
             

                    if (original != null)
                    {
                         entity.UnitsAuctions.RemoveRange(entity.UnitsAuctions.Where(x => x.UnitID == mf.unit.ID));
                        if (mf.Limitions != null)
                        {
                            foreach (int i in mf.Limitions)
                            {
                                UnitsAuctions ua = new UnitsAuctions();
                                ua.AuctionID = i;
                                ua.UnitID = mf.unit.ID;
                                entity.UnitsAuctions.Add(ua);
                            }
                        }
                        entity.Entry(original).CurrentValues.SetValues(((UnitFModel)SingletonCache.Instance().Storage[Context.User.Identity.Name]).unit);
                        entity.SaveChanges();
                    }

                    

                }
                using (TimchurDatabaseEntities entity2 = new TimchurDatabaseEntities())
                {
                    int strm = mf.unit.ID;
                    id = entity2.Units.Where(x => x.ID == strm).First().ID;
                }
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, יחידה עודכה במערכת";
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.ToString());
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, עדכון יחידה נכשל במערכת";
               
            }
            SingletonCache.Instance().Storage[Context.User.Identity.Name] = null;
            Cache.gen_lock.ReleaseMutex();
            string str = Context.User.Identity.Name;
            string msg = "";
            if (SingletonCache.Instance().last_msg.Keys.Contains(str))
            {
                msg = SingletonCache.Instance().last_msg[str];
            }
            string to_s = string.Format("סטאטוס:" + msg);
            Clients.Caller.sendMessage(id.Value.ToString());
        }
        public void AddSupplierOperation()
        {
            
            int? id = -1;
            Cache.gen_lock.WaitOne();
            SupplierFModel mf = ((SupplierFModel)SingletonCache.Instance().Storage[Context.User.Identity.Name]);
            try
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {
                    if(mf.ActualEmail==null)
                    {
                        mf.supliers.EmailAddress = "";
                    }
                    else
                    {
                        mf.supliers.EmailAddress = mf.ActualEmail;
                    }
                    
                  mf.supliers.PhoneNumber = mf.Prefix + mf.ActualNumber;
                    entity.Suppliers.Add(mf.supliers);
                    entity.SaveChanges();

                }
                using (TimchurDatabaseEntities entity2 = new TimchurDatabaseEntities())
                {
                    string strm = mf.supliers.Name;
                    id = entity2.Suppliers.Where(x => x.Name == strm).First().ID;
                    if (mf.Limitions != null)
                    {
                        foreach (int i in mf.Limitions)
                        {
                            SuppliersClusters ua = new SuppliersClusters();
                            ua.ClusterID = i;
                            ua.SupplierID = mf.supliers.ID;
                            ua.FormarLastTimeInList = new DateTime(2000,1,1);
                            ua.LastTimeInList = new DateTime(2000, 1, 1);
                            ua.StatusID = 1;
                            entity2.SuppliersClusters.Add(ua);
                        }
                    }
                    entity2.SaveChanges();
                }
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, ספק נוסף למערכת";
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.ToString());
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, הוספת ספק נכשלה";

            }
            SingletonCache.Instance().Storage[Context.User.Identity.Name] = null;
            Cache.gen_lock.ReleaseMutex();
            string str = Context.User.Identity.Name;
            string msg = "";
            if (SingletonCache.Instance().last_msg.Keys.Contains(str))
            {
                msg = SingletonCache.Instance().last_msg[str];
            }
            string to_s = string.Format("סטאטוס:" + msg);
            Clients.Caller.sendMessage(id.Value.ToString());
        }
        public void editSupplierOperation()
        {

            int? id = -1;
            Cache.gen_lock.WaitOne();
            SupplierFModel mf = ((SupplierFModel)SingletonCache.Instance().Storage[Context.User.Identity.Name]);
            try
            {
                using (TimchurDatabaseEntities entity = new TimchurDatabaseEntities())
                {

                    var original = entity.Suppliers.Find(mf.supliers.ID);


                    if (original != null)
                    {
                        
                        if (mf.Limitions != null)
                        {
                            foreach (int i in mf.Limitions)
                            {
                                SuppliersClusters ua = new SuppliersClusters();
                                if (entity.SuppliersClusters.Where(x => x.ClusterID == i && x.SupplierID == mf.supliers.ID).Count() > 0)
                                {
                                    entity.SuppliersClusters.Where(x => x.ClusterID == i && x.SupplierID == mf.supliers.ID).First().StatusID = 1;
                                }
                                else
                                {
                                    ua.ClusterID = i;
                                    ua.SupplierID = mf.supliers.ID;
                                    ua.FormarLastTimeInList = new DateTime(2000, 1, 1);
                                    ua.LastTimeInList = new DateTime(2000, 1, 1);
                                    ua.StatusID = 1;
                                    entity.SuppliersClusters.Add(ua);
                                }
                            }
                       
                           
                        }
                        foreach (SuppliersClusters sc in entity.SuppliersClusters.Where(x => !mf.Limitions.Contains(x.ClusterID) && x.SupplierID == mf.supliers.ID))
                        {
                            sc.StatusID = 2;
                        }
                        if (mf.ActualEmail == null)
                        {
                            mf.supliers.EmailAddress = "";
                        }
                        else
                        {
                            mf.supliers.EmailAddress = mf.ActualEmail;
                        }

                        mf.supliers.PhoneNumber = mf.Prefix + mf.ActualNumber;
                        entity.Entry(original).CurrentValues.SetValues(((SupplierFModel)SingletonCache.Instance().Storage[Context.User.Identity.Name]).supliers);
                        entity.SaveChanges();
                    }



                }
                using (TimchurDatabaseEntities entity2 = new TimchurDatabaseEntities())
                {
                    int strm = mf.supliers.ID;
                    id = entity2.Suppliers.Where(x => x.ID == strm).First().ID;
                }
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, ספק עודכן במערכת";
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.Write(e.ToString());
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = "בפעולה האחרונה, עדכון ספק נכשלה במערכת";

            }
            SingletonCache.Instance().Storage[Context.User.Identity.Name] = null;
            Cache.gen_lock.ReleaseMutex();
            string str = Context.User.Identity.Name;
            string msg = "";
            if (SingletonCache.Instance().last_msg.Keys.Contains(str))
            {
                msg = SingletonCache.Instance().last_msg[str];
            }
            string to_s = string.Format("סטאטוס:" + msg);
            Clients.Caller.sendMessage(id.Value.ToString());
        }
        public void SendNotification()
        {
            if (SingletonCache.Instance().last_msg[Context.User.Identity.Name] != null)
            {
                string message = (string)(SingletonCache.Instance().last_msg[Context.User.Identity.Name].Clone());
                SingletonCache.Instance().last_msg[Context.User.Identity.Name] = null;
                Clients.Caller.broadcastNotification(message);
            }
        }
    }
}