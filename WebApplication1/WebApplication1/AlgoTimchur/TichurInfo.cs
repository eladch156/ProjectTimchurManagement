using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.AlgoTimchur
{
    /// <summary>
    /// This is the data required as input for a tichur query,
    /// in object form.
    /// </summary>
    public class TichurInfo
    {
        public int UnitID { get; set; }
        public int ClusterID { get; set; }
        public int AuctionID { get; set; }
        public string TichurNumber { get; set; }
    }
}