using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebApplication1.Database;
/// <summary>
/// Class used to execute the supplier-extraction algorithm.
/// </summary>
namespace WebApplication1.AlgoTimchur
{
    public class TichurAlgo
    {
        /// <summary>
        /// Performs the extraction and returns the result.
        /// </summary>
        /// <param name="info">Specifics of the information required for the extraction.</param>
        /// <param name="user_id">ID of the user requesting the extraction.</param>
        /// <returns></returns>
        public EndResultTichur Execute(TichurInfo info, string user_id)
        {
            Cache.gen_lock.WaitOne();
            TablePullResult TRE;
            // Attempting extraction
            try
            {
                PullSuppList pull = new ExtractSuppList();
                TRE = pull.TichurExtract(info);
            }
            // Extraction failed, catching cause of error
            catch (Exception e)
            {
                EndResultTichur res = new EndResultTichur();
                res.Status = "error";
                res.data = new List<string[]>();
                res.data.Add(new string[] { e.Message });
                Cache.gen_lock.ReleaseMutex();
                return res;
            }
            // Assuming extraction completed successfully,
            // Captures the data and updates the database entity accordingly before returning.
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
                    Trace.Write(e.Message);
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
                    res2.data.Add(new string[] { i.ToString(),suptic.PositionInList.Value.ToString(),tich.Units.Name,tich.Clusters.Auctions.AuctionNumber, tich.Clusters.Auctions.Name,tich.TichurNumber,tich.Clusters.DisplayNumber.Value.ToString(),tich.Clusters.Name,supa.Name,supa.CompanyNumber,supa.ContactName,supa.EmailAddress,supa.PhoneNumber,tich.DateTimeCreated.Value.ToString("yyyy:MM:dd:HH:mm:ss") });
                    ent2.SuppliersTichurim.Add(suptic);
                    i++;

                }
                ent2.SaveChanges();
                }
                catch(Exception e)
                {
                    Trace.Write(e.Message);
                }
            }
            Cache.gen_lock.ReleaseMutex();
            return res2;
        } 
    }
}