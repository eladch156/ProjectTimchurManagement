using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.AuthAbstract
{
    public interface IAuthProvider
    {
        bool Authenticate(String username, String Password);
    }
}
