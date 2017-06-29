using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    /// <summary>
    /// Model used for the Login portion of the system.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Username used for login.
        /// </summary>
        [Required]
        public String Username { get; set; }
        /// <summary>
        /// Password used for login.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        /// <summary>
        /// Redirection URL upon login.
        /// </summary>
        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}