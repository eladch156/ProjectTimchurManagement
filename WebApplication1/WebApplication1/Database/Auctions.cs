//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class Auctions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Auctions()
        {
            this.Clusetrs = new HashSet<Clusetrs>();
            this.UnitsAuctions = new HashSet<UnitsAuctions>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [RegularExpression("[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]", ErrorMessageResourceName = "patrNotMa", ErrorMessageResourceType = typeof(Resources.BasicAno))]
        [Display(Name = "AUAuctionNumber", ResourceType = typeof(Resources.BasicAno))]
        public string AuctionNumber { get; set; }
        [Required]
        [Display(Name = "AUName", ResourceType = typeof(Resources.BasicAno))]
        public string Name { get; set; }
        [Required]
        [Display(Name = "AUStatusID", ResourceType = typeof(Resources.BasicAno))]
        public Nullable<int> StatusID { get; set; }
    
        public virtual Statuses Statuses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clusetrs> Clusetrs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnitsAuctions> UnitsAuctions { get; set; }
    }
}