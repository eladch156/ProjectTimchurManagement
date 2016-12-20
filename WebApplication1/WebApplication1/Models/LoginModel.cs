using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class LoginModel
    {
        [Required]
        public String Username { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}