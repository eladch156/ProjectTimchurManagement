using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.AuthAbstract;
using WebApplication1.Database;

namespace WebApplication1.Infrastructure.AuthAbstract
{
    /// <summary>
    /// Object representation of database authentication.
    /// </summary>
    public class DatabaseProvider : IAuthProvider
    {
        /// <summary>
        /// Authenticate user details via database login details.
        /// </summary>
        /// <param name="username">Given user's username.</param>
        /// <param name="Password">Given user's password.</param>
        /// <returns>True, if said user exists within context, false otherwise.</returns>
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