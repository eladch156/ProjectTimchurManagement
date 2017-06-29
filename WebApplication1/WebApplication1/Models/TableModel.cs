using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Database;

namespace WebApplication1.Models
{
    /// <summary>
    /// Model representation of a datatable.
    /// </summary>
    public class TableModel
    {
        /// <summary>
        /// Title of the datatable.
        /// </summary>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// The table's headers.
        /// </summary>
        public List<string> header
        {
            set;
            get;
        }
        /// <summary>
        /// The table's (2D) content.
        /// </summary>
        public List<List<string>> values
        {
            set;
            get;
        }
    }
}