using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApplication1.Models
{
    /// <summary>
    /// Model representation of the database connection
    /// between supplier and cluster.
    /// Currently unused.
    /// </summary>
    public class SCTSModel
    {
        public String col_regex
        {
            get;
            set;
        }
        public String row_regex
        {
            get;
            set;
        }
        public String paging_factor
        {
            get;
            set;
        }
    }
}