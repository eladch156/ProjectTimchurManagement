using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    /// <summary>
    /// Model representation of the user and its type.
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// String representation of username
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// String representation of full username
        /// </summary>
        public String GivenName { get; set; }
        /// <summary>
        /// String representation of the usertype/role.
        /// </summary>
        public String Role { get; set; }
    }
}