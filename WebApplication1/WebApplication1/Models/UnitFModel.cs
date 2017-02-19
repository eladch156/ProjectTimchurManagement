using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Database;

namespace WebApplication1.Models
{
    public class UnitFModel
    {
        public Units unit
        {
            get;
                set;
        }
        public IEnumerable<int?> Limitions { get; set; }
    }
}