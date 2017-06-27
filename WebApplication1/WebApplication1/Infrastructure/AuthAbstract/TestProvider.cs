/*
namespace WebApplication1.AuthAbstract
{
    /// <summary>
    /// Backdoor login authetication with default user data.
    /// </summary>
    public class TestProvider : IAuthProvider
    {
        /// <summary>
        /// Attempts to login with the default data.
        /// </summary>
        /// <param name="Username">Given username.</param>
        /// <param name="Password">Given password.</param>
        /// <returns></returns>
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
}*/