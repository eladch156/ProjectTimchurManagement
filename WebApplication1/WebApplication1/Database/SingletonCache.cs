using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Database
{
    public class SingletonCache
    {
        private static Cache _instance;



        // Constructor is 'protected'

        protected SingletonCache() 

        {

        }



        public static Cache Instance()

        {

            // Uses lazy initialization.

            // Note: this is not thread safe.

            if (_instance == null)

            {

                _instance = new Cache();

            }



            return _instance;

        }

    }
}
