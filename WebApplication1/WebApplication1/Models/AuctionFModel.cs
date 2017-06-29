using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Database;

namespace WebApplication1.Models
{
    /// <summary>
    /// Model container for Auction data entity.
    /// </summary>
    public class AuctionFModel
    {
        /// <summary>
        /// Object representation of the auction proper.
        /// </summary>
        public Auctions auction
        {
            get;
            set;
        }
        /// <summary>
        /// List of the clusters associated with the auction model.
        /// </summary>
        public IEnumerable<int?> clusters { get; set; }
    }
}