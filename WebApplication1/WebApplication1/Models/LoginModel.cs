using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter a user name")]
        public String Username { get; set; }


        [Required(ErrorMessage = "Enter a password")]
        public String Password { get; set; }
    }
}