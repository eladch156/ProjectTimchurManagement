using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.AuthAbstract
{
    /// <summary>
    /// Interface regarding any service which involves authentication.
    /// </summary>
    public interface IAuthProvider
    {
        /// <summary>
        /// Function representing user authentication.
        /// </summary>
        /// <param name="username">The username by which the authentication is done.</param>
        /// <param name="Password">The user's password.</param>
        /// <returns>Whether authentication completed successfully.</returns>
        bool Authenticate(String username, String Password);
    }
}
