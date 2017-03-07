using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Database;

namespace WebApplication1.AlgoTimchur
{
    public class TichurAlgo
    {

        public EndResultTichur Execute(TichurInfo info, string user_id)
        {
            Cache.gen_lock.WaitOne();
            TablePullResult TRE;
            try
            {
                PullSuppList pull = new ExtractSuppList();
                TRE = pull.TichurAlgorithem(info);
               
            }
            catch (Exception e)
            {
                EndResultTichur res = new EndResultTichur();
                res.Status = "error";
                res.data = new List<string[]>();
                res.data.Add(new string[] { "תיחור כבר קיים בתוך המערכת" });
                Cache.gen_lock.ReleaseMutex();
                return res;
            }
            using (Database.TimchurDatabaseEntities ent = new Database.TimchurDatabaseEntities())
            {
                DateTime tic_date = DateTime.Now;
                try
                {
                    foreach (string sup in TRE.table)
                {
                   
                    int sid = Int32.Parse(sup);
                    Suppliers supa = ent.Suppliers.Where(x => x.ID == sid).First();
                    SuppliersClusters scl = supa.SuppliersClusters.Where(x => x.ClusterID == info.ClusterID).First();
                    scl.FormarLastTimeInList = scl.LastTimeInList;
                    scl.LastTimeInList = tic_date;


                }
                
                    Users user = ent.Users.Where(x => x.IDCardNumber == user_id).First();
                    Tichurim tichur = new Tichurim();
                    tichur.UnitID = info.UnitID;
                    tichur.ClusterID = info.ClusterID;
                    tichur.TichurNumber = info.TichurNumber;
                    tichur.StatusID = 1;
                    tichur.DateTimeCreated = DateTime.Now;
                    tichur.DateTimeSelected = DateTime.Now;
                    tichur.DateTimeUpdated = DateTime.Now;
                    tichur.CreatedUserID = user.ID;
                    tichur.UpdatedUserID = user.ID;
                    tichur.UpdatedComment = "Created";
                    ent.Tichurim.Add(tichur);
                    ent.SaveChanges();
                }
                catch(Exception e)
                {
                    
                }
            }
            EndResultTichur res2 = new EndResultTichur();
            res2.Status = "K";
            res2.data = new List<string[]>();
            using (Database.TimchurDatabaseEntities ent2 = new Database.TimchurDatabaseEntities())
            {

                try
                {
                    int i = 0;
                Tichurim tich = ent2.Tichurim.Where(x => x.TichurNumber == info.TichurNumber && x.StatusID == 1).First();
                foreach (string supp in TRE.table)
                {
                    int sup_id = Int32.Parse(supp);
                    SuppliersTichurim suptic = new SuppliersTichurim();
                    suptic.TichurID = tich.ID;
                    suptic.SupplierID = sup_id;
                    suptic.PositionInList = byte.Parse(i.ToString());
                    Suppliers supa = ent2.Suppliers.Where(x => x.ID == sup_id).First();
                    res2.data.Add(new string[] { i.ToString(),suptic.PositionInList.Value.ToString(),tich.Units.Name,tich.Clusters.Auctions.AuctionNumber, tich.Clusters.Auctions.Name,tich.TichurNumber,tich.Clusters.DisplayNumber.Value.ToString(),tich.Clusters.Name,supa.Name,supa.CompanyNumber,supa.ContactName,supa.EmailAddress,supa.PhoneNumber,tich.DateTimeCreated.ToString() });
                    ent2.SuppliersTichurim.Add(suptic);
                    i++;

                }
                ent2.SaveChanges();
                }
                catch(Exception e)
                {

                }


            }
            Cache.gen_lock.ReleaseMutex();
            return res2;
        }


    } }