using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using WebApplication1.Database;

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
               Database.Tichurim check = ent.Tichurim.Where(x=>x.TichurNumber==input.TichurNumber).FirstOrDefault();
               if (check != null && check.UnitID == input.UnitID && check.StatusID == 1)
                {
                    throw new DuplicateKeyException(check);
                }
                TablePullResult res = new TablePullResult();
                int suppliersRemaining = 0;
                if (ent.Clusetrs.Where(x => x.ID == input.CluestrID).FirstOrDefault()!=null)
               suppliersRemaining = ent.Clusetrs.Where(x=>x.ID==input.CluestrID).FirstOrDefault().SuppliersInTichur.Value;
              
                
                var sortByUsage = (from sup in ent.Suppliers
                         join supcluc in ent.SuppliersClusetrs
                         on sup.ID equals supcluc.SupplierID
                         where supcluc.ClusetrID == input.CluestrID && sup.StatusID == 1
                         orderby supcluc.LastTimeInList
                         group sup by new { supcluc.LastTimeInList, supcluc.SupplierID} into dateGroups
                         orderby dateGroups.Key.LastTimeInList
                         select dateGroups);
                //Date-grouped iterator
                var itr = sortByUsage.ToList().GetEnumerator();
                itr.MoveNext();
                var rnd = new Random();
                while (suppliersRemaining > 0)
                {
                    
                    //oldest group of suppliers
                    var dateGroup = itr.Current;
                    //Need to pick a random subset of the suppliers
                    if (dateGroup.Count() < suppliersRemaining)
                    {
                        var shuffled = dateGroup.OrderBy(i => rnd.Next());
                        // Going over the shuffled list until enough are extracted
                        foreach (var supplier in shuffled)
                        {
                            res.table.Add(supplier.ID.ToString());
                            if (--suppliersRemaining == 0)
                            {
                                
                                break;
                            } 
                        }
                        itr.MoveNext();
                    } else
                    {
                        //Need at least one entire dategroup - adding all suppliers in current set.
                        foreach (var supplier in dateGroup)
                        {
                            res.table.Add(supplier.ID.ToString());
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