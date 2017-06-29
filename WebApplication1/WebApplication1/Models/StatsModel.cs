using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    /// <summary>
    /// Model representation of a statistics graph.
    /// </summary>
    public class StatsModel
    {
        /// <summary>
        /// Cluster ID
        /// </summary>
        public string cl_id
        {
            get;
            set;
        }
        /// <summary>
        /// Tichur ID
        /// </summary>
        public string ul_id
        {
            get;
            set;
        }
        /// <summary>
        /// Dictionary mapping Tichur ID to name.
        /// </summary>
        public SortedDictionary<int, String> TichurName
        {
            get;
            set;
        }
        /// <summary>
        /// Dictionary mapping Supplier ID to name
        /// </summary>
        public SortedDictionary<int, String> SupplierName
        {
            get;
            set;
        }
        /// <summary>
        /// Dictionary mapping supplier to list of tichurim it participates in.
        /// </summary>
        public SortedDictionary<int, List<int>> SupplierInTichur
        {
            get;
            set;
        }
    }
}