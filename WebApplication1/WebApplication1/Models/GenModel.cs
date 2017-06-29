using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    /// <summary>
    /// Represents a query feedback, 
    /// in terms of its status, and an accompanying message.
    /// </summary>
    public class GenModel
    {
        /// <summary>
        /// The status of the query.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// String representing the message accompanying the query's result.
        /// </summary>
        public List<string []> data { get; set; }
    }
}