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
    public partial class SuppliersClusetrs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Nullable<int> ClusetrID { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<System.DateTime> LastTimeInList { get; set; }
        public Nullable<System.DateTime> FormarLastTimeInList { get; set; }
        public Nullable<int> StatusID { get; set; }
    
        public virtual Clusetrs Clusetrs { get; set; }
        public virtual Statuses Statuses { get; set; }
        public virtual Suppliers Suppliers { get; set; }
    }
}