using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Database
{
    /// <summary>
    /// Class representation of the cache storage; used for data context and authorization. 
    /// </summary>
    public class Cache
    {
        public Dictionary<string, string> last_msg = new Dictionary<string, string>();
        public static Mutex gen_lock=new Mutex();
        public Dictionary<string, Object> Storage = new Dictionary<string, object>();
        public Dictionary<string, string> role_map = new Dictionary<string, string>()
        {
            { "/Main/MainIndex","Admin,User" },
             { "/Main/MangAuctions","Admin" },
                          { "/Main/ApproveMsg","Admin,User" },
                            { "/Main/TichurSuppCreate","Admin,User" },
                             { "/Main/TichurExisting","Admin,User" },
                              { "/Main/MangUsers","Admin" },
                               { "/Main/MangSuppliers","Admin" },
            {"/Main/MangClusters","Admin"  },
                                 { "/Main/MangUnits","Admin" },
                                  { "/Main/UnAuthError","Admin,User" },
            {"/Main/EditOrAddUser","Admin" },
            {"/Main/EditOrAddAuction" ,"Admin"    },
             {"/Main/EditOrAddCluster" ,"Admin"    },
              {"/Main/EditOrAddUnit" ,"Admin"    },
               {"/Main/EditOrAddSupplier" ,"Admin"    },
            { "/Main/TichurCancel" ,"Admin"  },
              { "/Main/Stats" ,"Admin"  },
                  { "/Main/ConSupToAuctions" ,"Admin"  },
    };
     
    }
}



