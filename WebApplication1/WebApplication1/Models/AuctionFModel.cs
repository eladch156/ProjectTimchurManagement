using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Database;

namespace WebApplication1.Models
{
    public class AuctionFModel
    {
        public Auctions auction
        {
            get;
            set;
        }
        public IEnumerable<int?> cluestrs { get; set; }
    }
}