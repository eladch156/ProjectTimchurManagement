using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.AuthAbstract;
using WebApplication1.Database;

namespace WebApplication1.Infrastructure.AuthAbstract
{
    public class DatabaseProvider : IAuthProvider
    {
        public bool Authenticate(string username, string Password)
        {
            Users user;
            using (var context = new TimchurDatabaseEntities())
            {
                user = context.Users.Where(s => (s.IDCardNumber).ToString() == username).Where(s=> (s.Password)==Password).FirstOrDefault<Users>();
            }
            if (user == null)
                return false;
            return true;
        }
    }
}