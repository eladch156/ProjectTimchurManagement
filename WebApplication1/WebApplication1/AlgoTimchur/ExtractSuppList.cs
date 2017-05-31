using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using WebApplication1.Database;
using System.Linq.Expressions;
using LinqKit;
using System.Data.Entity;

/**
 * A) Find all the suppliers relevant to the given cluster
 * B) Sort them into groups based on latest extraction date regarding said cluster.
 * C) While date-groups can be extracted fulled into the list of "polled" suppliers, do so.
 * D) Once a date-group can only extracted partially, do so randomly.
 */
namespace WebApplication1.AlgoTimchur
{
    public class ExtractSuppList : PullSuppList
    {

        public TablePullResult TichurAlgorithem(TichurInfo input)
        {
            using (TimchurDatabaseEntities ent = new TimchurDatabaseEntities())
            {
                // Checking for duplicate tichurs
                Database.Tichurim check = ent.Tichurim.Where(x => x.TichurNumber == input.TichurNumber).FirstOrDefault();
                if (ent.Tichurim.Where(x => x.TichurNumber == input.TichurNumber).Count() != 0 && check.UnitID == input.UnitID && check.StatusID == 1)
                {
                    throw new Exception("תיחור כבר קיים בתוך המערכת");
                }
                if (ent.Clusters.Where(x => x.ID == input.ClusterID).First().SuppliersClusters.Count() < ent.Clusters.Where(x => x.ID == input.ClusterID).First().SuppliersInTichur)
                {
                    throw new Exception("אין מספיק ספקים על מנת לבצע תיחור נא שנה בסל את מס' הספקים בתיחור");
                }
                TablePullResult res = new TablePullResult();
                int suppliersRemaining = 0;
                if (ent.Clusters.Where(x => x.ID == input.ClusterID).FirstOrDefault() != null)
                    suppliersRemaining = ent.Clusters.Where(x => x.ID == input.ClusterID).FirstOrDefault().SuppliersInTichur.Value;
                

                var sortByUsagePre = (from sup in ent.Suppliers
                                   join supcluc in ent.SuppliersClusters
                                   on sup.ID equals supcluc.SupplierID
                                   where supcluc.ClusterID == input.ClusterID && sup.StatusID == 1
                                   orderby supcluc.LastTimeInList
                                   select new { Sup = sup, SupCl = supcluc });
                var sortByUsage = sortByUsagePre.AsExpandable().GroupBy(x => DbFunctions.AddMilliseconds(x.SupCl.LastTimeInList,-x.SupCl.LastTimeInList.Value.Millisecond)).OrderBy(x=>x.Key);
                //Date-grouped iterator

                var itr = sortByUsage.GetEnumerator();
                itr.MoveNext();
                var rnd = new Random();
                while (suppliersRemaining > 0)
                {

                    //oldest group of suppliers
                    var dateGroup = itr.Current;
                    //Need to pick a random subset of the suppliers
                    if (dateGroup.Count() > suppliersRemaining)
                    {
                        var shuffled = dateGroup.OrderBy(i => rnd.Next());
                        // Going over the shuffled list until enough are extracted
                        foreach (var supplier in shuffled)
                        {
                            res.table.Add(supplier.Sup.ID.ToString());
                            if (--suppliersRemaining == 0)
                            {

                                break;
                            }
                        }
                        itr.MoveNext();
                    }
                    else
                    {
                        //Need at least one entire dategroup - adding all suppliers in current set.
                        foreach (var supplier in dateGroup)
                        {
                            res.table.Add(supplier.Sup.ID.ToString());
                        }
                        suppliersRemaining -= dateGroup.Count();
                        itr.MoveNext();
                    }
                }
                return res;
            }
        }
    }
}