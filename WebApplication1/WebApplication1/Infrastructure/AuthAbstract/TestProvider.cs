using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.AuthAbstract
{
    public class TestProvider : IAuthProvider
    {
        public bool Authenticate(string Username, string Password)
        {
           if(Username.Equals("Admin") && Password.Equals("pass1234"))
            {
                return true;
            }
            if (Username.Equals("RegUser") && Password.Equals("root1234"))
            {
                return true;
            }
            return false;
        }
    }
}