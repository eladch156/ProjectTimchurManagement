using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Database;

namespace WebApplication1.Models
{
    public class SupplierFModel
    {
        public Suppliers supliers
        {
            get;
            set;
        }
        [Required]
        [Display(Name = "SUPAuctionID", ResourceType = typeof(Resources.BasicAno))]
        public IEnumerable<int?> Limitions { get; set; }
        [Required]
        [Display(Name = "SUPPrefixID", ResourceType = typeof(Resources.BasicAno))]
        public string Prefix { get; set; }
        [Required]
        [Display(Name = "SUPANumberID", ResourceType = typeof(Resources.BasicAno))]
        [RegularExpression("[0-9][0-9][0-9][0-9][0-9][0-9][0-9]", ErrorMessageResourceName = "InvalidPN", ErrorMessageResourceType = typeof(Resources.BasicAno))]
        public string ActualNumber { get; set; }
       
        [Display(Name = "SUPEmail", ResourceType = typeof(Resources.BasicAno))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Resources.BasicAno))]
        public string ActualEmail { get; set; }
    }
}