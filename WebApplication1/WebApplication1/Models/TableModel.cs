using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Database;

namespace WebApplication1.Models
{
    public class TableModel
    {
        public string Title
        {
            get;
            set;
        }
        public List<string> header
        {
            set;
            get;
        }
        public List<List<string>> values
        {
            set;
            get;
        }
      

        
      
    }
}