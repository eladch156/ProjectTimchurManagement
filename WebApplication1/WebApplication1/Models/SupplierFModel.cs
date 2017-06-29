using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Database;

namespace WebApplication1.Models
{
    /// <summary>
    /// Model representation of a supplier database entity.
    /// </summary>
    public class SupplierFModel
    {
        /// <summary>
        /// Object representation of the supplier proper.
        /// </summary>
        public Suppliers supliers
        {
            get;
            set;
        }
        /// <summary>
        /// List of auctions this supplier is limited to participate in.
        /// </summary>
        [Required]
        [Display(Name = "SUPAuctionID", ResourceType = typeof(Resources.BasicAno))]
        public IEnumerable<int?> Limitations { get; set; }
        /// <summary>
        /// Area code of phone number.
        /// </summary>
        [Required]
        [Display(Name = "SUPPrefixID", ResourceType = typeof(Resources.BasicAno))]
        public string Prefix { get; set; }
        /// <summary>
        /// Supplier contact phone number.
        /// </summary>
        [Required]
        [Display(Name = "SUPANumberID", ResourceType = typeof(Resources.BasicAno))]
        [RegularExpression("[0-9][0-9][0-9][0-9][0-9][0-9][0-9]", ErrorMessageResourceName = "InvalidPN", ErrorMessageResourceType = typeof(Resources.BasicAno))]
        public string ActualNumber { get; set; }
        /// <summary>
        /// Email address of the supplier.
        /// </summary>
        [Display(Name = "SUPEmail", ResourceType = typeof(Resources.BasicAno))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Resources.BasicAno))]
        public string ActualEmail { get; set; }
    }
}