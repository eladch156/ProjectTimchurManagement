using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    /// <summary>
    /// Model used for the Password Reset feature.
    /// </summary>
    public class ResetPassModel
    {
        /// <summary>
        /// ID/username of the user whose password we reset.
        /// </summary>
        public string Id
        {
            get;
            set;
        }
        /// <summary>
        /// Email address of the user.
        /// </summary>
        public string Email
        {
            get;
            set;
        }
        /// <summary>
        /// Password reset authentication key.
        /// </summary>
        public string Key
        {
            get;
            set;
        }
        /// <summary>
        /// The new password for the user.
        /// </summary>
        public string NewPass
        {
            get;
            set;
        }
    }
}