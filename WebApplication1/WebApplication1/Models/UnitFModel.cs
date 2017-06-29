using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Database;

namespace WebApplication1.Models
{
    /// <summary>
    /// Model representing the Unit database entity.
    /// </summary>
    public class UnitFModel
    {
        /// <summary>
        /// The unit proper.
        /// </summary>
        public Units unit
        {
            get;
            set;
        }
        /// <summary>
        /// ID of the auctions to which this unit is limited.
        /// </summary>
        public IEnumerable<int?> Limitations { get; set; }
    }
}