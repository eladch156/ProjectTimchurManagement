using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ResetPassModel
    {
        public string Id
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string Key
        {
            get;
            set;
        }
        public string NewPass
        {
            get;
            set;
        }
    }
}