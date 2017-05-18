using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class StatsModel
    {
        public string cl_id
        {

            get;
            set;
        }
        public string ul_id
        {

            get;
            set;
        }
        public SortedDictionary<int, String> TichurName
        {
            get;
            set;
        }
        public SortedDictionary<int, String> SupplierName
        {
            get;
            set;
        }
        public SortedDictionary<int, List<int>> SupplierInTichur
        {
            get;
            set;
        }
    }
}