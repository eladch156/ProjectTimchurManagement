using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.AlgoTimchur
{
    /// <summary>
    /// A class container for the result of the extraction algorithm.
    /// </summary>
    public class EndResultTichur
    {
        /// <summary>
        /// String representation of the return's result's status.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// The data extracted from the database.
        /// </summary>
        public List<string []> data { get; set; }
    }
}