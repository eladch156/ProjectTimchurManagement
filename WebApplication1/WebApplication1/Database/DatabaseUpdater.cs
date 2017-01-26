using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1.Database
{

    public class DatabaseUpdater
    {
        public enum Table
        {
            Users,
           UnitAuctions,
            Units,
            Timchurim,
            Tichurim,
            Suplliers,
            Statuses,
            Roles,
            Cluestrs,
            Auctions
          
        }
        public Object LockForTichur=new object();
        public delegate void AddEnt(object param);
        public struct Request
        {
            public AddEnt func;
            public object param;
           
        };
       
        private Queue<Request> CurrDataSets;
        public DatabaseUpdater()
        {
            CurrDataSets = new Queue<Request>();
            ThreadStart thrs = new ThreadStart(Loop);
            Thread thr = new Thread(thrs);
            thr.Start();
            
         
        }
        public void Add(object param,Table type)
        {
            Request req;
            switch(type)
            {
                case Table.Users:
                    req.func = AddUser;
                    break;
                default:
                    break;
            }
            req.param = param;
        }
        public void Loop()
        {
            while (true)
            {
                lock (LockForTichur)
                {
                    if(CurrDataSets.Count>0)
                    {
                        Request entity = CurrDataSets.Dequeue();
                          entity.func(entity.param);
                      
                    }
                }



             }
        }
       
        public void AddUser(object param)
        {
            Users user = (Users)param;
            using (var ent = new TimchurDatabaseEntities())
            {
                ent.Users.Add(user);
                ent.SaveChanges();

            }
        }
    }
}